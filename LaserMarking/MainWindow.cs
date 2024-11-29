using AxMBPLib2;
using MBPLib2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPDM.Interop.epdm;
using System.Collections;
using System.Diagnostics;
using static System.Net.WebRequestMethods;
using System.CodeDom;
using AxMBPActXLib;
using System.Threading;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection;

namespace LaserMarking
{
    public partial class MainWindow : Form
    {
        //UMIConnectionString now has no references after I fixed something while chaging conn strings - leaving in here in case it is needed - Andrew J
        public static string UMIConnectionString = $"Data Source=UMI-ERP-01 ;Initial Catalog = UMi_Tooling; Integrated Security = True; Connect Timeout = 120; ";
        public static string HHI_PUMIConnectionString = $"Data Source=UMI-ERP-01 ;Initial Catalog = HydraulicHoseInfo_prod; Integrated Security = True; Connect Timeout = 120; ";
        string SAPConnectionString = "Persist Security Info=True;Initial Catalog=UMIS;Integrated Security = True;Data Source=UMI-ERP-01;";
        private int SQLTest;

        //PDM Variables
        private IEdmVault5 vault1 = null;
        IEdmFile7 aFile;
        IEdmFolder5 ppoRetParentFolder;
        private IEdmBom bom;
        private IEdmBomView3 bomView;


        bool GenericProgram = true;
        bool isConnected = false;
        bool partNumsFliped = false;
        int blockNo;

        private Process externalProcess;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_NOACTIVATE = 0x0010;

        public MainWindow()
        {
            InitializeComponent();

            InitializeMarker();

            loadMaterialComboBox();

            AttemptToConnectToLaser();

            LoadFileNames();

            SetFormTitle();


            if (System.Diagnostics.Debugger.IsAttached)
            {
                SQLTest = 1;
            }
            else
            {
                SQLTest = 0;
            }
            SetCustomSourcesForTubeParts();


            //Set DB Conn strings to test/prod depending on environment
            SetDBConnections();

        }

        private void SetFormTitle()
        {
            // Get the assembly version
            Version version = Assembly.GetExecutingAssembly().GetName().Version;

            // Set the form title
            this.Text = $"Laser Marking - Version {version}";
        }

        // Fills Material & Size Combo box on init :: Complete
        private void loadMaterialComboBox()
        {
            ProgramMaterialCombo.Items.Add("SS");
            ProgramMaterialCombo.Items.Add("CS");

            ProgramSizeCombo.Items.Add("04");
            ProgramSizeCombo.Items.Add("06");
            ProgramSizeCombo.Items.Add("08");
            ProgramSizeCombo.Items.Add("10");
            ProgramSizeCombo.Items.Add("12");
            ProgramSizeCombo.Items.Add("16");
            ProgramSizeCombo.Items.Add("20");
            ProgramSizeCombo.Items.Add("24");
        }

        // ???
        private void InitializeMarker()
        {
            try
            {
                axMBActX2.InitMBActX(MarkingUnitTypes.MARKINGUNIT_MDX2500);
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show("Error initializing laser. \n\n " + error.Message);
            }

        }
        
        //Function to load items on Refresh Orders Click :: Complete
        private void updateOpenOrderItems() 
        {
            using (SqlConnection cn = new SqlConnection(SAPConnectionString))
            {
                try
                {
                    cn.Open();  // Open connection using the SQL connection string above
                    SqlCommand cmd2 = new SqlCommand("", cn);    //Declare text command for server connection
                    cmd2.CommandTimeout = 120; //set a long timeout in case of really complex queries 2019-04-30

                    cmd2.CommandText = @"select OWOR.ItemCode as Part_number, 
                                        OWOR.PlannedQty as Total_QTY, 
                                        WOR1.EndDate as  Due_date, 
                                        OWOR.OriginNum as ProductionNumber
                                        from WOR1 --rows
                                        left join OWOR on OWOR.DocEntry = WOR1.DocEntry  -- headers
                                        where WOR1.ItemCode like '%tub%'  
                                        and OWOR.Status != 'L' --closed
                                        and OWOR.Status!= 'C' -- Cancelled";

                    DataTable dt = new DataTable();
                    dt.Load(cmd2.ExecuteReader());

                    OrdersGridView.DataSource = dt;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting open orders" + ex);
                }
            }
        }

        // Adds Material Column to datagrid :: Complete
        private void GetOrderTubePNBTN_Click(object sender, EventArgs e)
        {
            OrdersGridView.Columns.Add("Material", "Material");
            OrdersGridView.Columns["Material"].DisplayIndex = 1;

            foreach (DataGridViewRow row in OrdersGridView.Rows)
            {
                //MessageBox.Show(""+  (row.Cells["Part_Number"].Value.ToString()).Substring(0,5) );
                try {
                    row.Cells["Material"].Value = SimplyGetTubeMaterialFromPN((row.Cells["Part_Number"].Value.ToString()).Substring(0, 5));
                }
                catch {}
                
            }
        }

        //Gets Material types for GetOrderTubePNBTN_Click :: Complete
        private object SimplyGetTubeMaterialFromPN(string PN)
        {
            try
            {
                IEdmVault7 vault2 = null;
                if (vault1 == null)
                {
                    vault1 = new EdmVault5();
                }
                vault2 = (IEdmVault9)vault1;
                if (!vault1.IsLoggedIn)
                {
                    vault1.LoginAuto("UMIS", this.Handle.ToInt32());
                }

                aFile = (IEdmFile7)vault1.GetFileFromPath($@"C:\UMIS\UMi Parts\80000\{PN}.slddrw", out ppoRetParentFolder);
                if (aFile != null)
                {

                    EdmBomInfo[] derivedBOMs;

                    aFile.GetDerivedBOMs(out derivedBOMs);

                    bom = (IEdmBom)vault2.GetObject(EdmObjectType.EdmObject_BOM, derivedBOMs[0].mlBomID); // USE ID OF DERIVED BOM 1
                    bomView = (IEdmBomView3)(IEdmBomView2)bom.GetView(0);


                    EdmBomColumn[] BomColumns = null;
                    bomView.GetColumns(out BomColumns);

                    object[] BomRows = null;
                    bomView.GetRows(out BomRows);

                    object cellVar = null;
                    object ComputedValue = null;
                    string config = null;
                    bool ReadOnlyOut = false;
                    List<string> TubesFound = new List<string>();

                    //search all cells inluding item column (different column type)
                    foreach (IEdmBomCell CELL in BomRows)
                    {
                        int ColumnCount = 1;

                       
                            if (ColumnCount <= (BomColumns.Count() - 1)) // in case BOM has more columns than expeted
                            {
                                CELL.GetVar(BomColumns[ColumnCount].mlVariableID, BomColumns[ColumnCount].meType, out cellVar, out ComputedValue, out config, out ReadOnlyOut);
                                if ((cellVar.ToString()).Contains("304LT") || (cellVar.ToString()).Contains("316LT"))
                                {
                                    TubesFound.Add(cellVar.ToString());
                                    
                                }
                                else if ((cellVar.ToString()).Contains("4130") || (cellVar.ToString()).Contains("A513-5") || (cellVar.ToString()).Contains("A513-1") || (cellVar.ToString()).Contains("J524") || (cellVar.ToString()).Contains("J525"))
                                {
                                    TubesFound.Add(cellVar.ToString());
                                }
                                else
                                {

                                }
                            }
                    }
                    if (TubesFound.Count == 0)
                    {
                        return "";
                    }
                    else if (TubesFound.Count > 1)
                    {
                        return "";
                    }
                    else
                    {
                        return TubesFound[0];
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //Sets connection strings based on whether we are debugging or not :: Complete
        private void SetDBConnections()
        {
            if (SQLTest == 1)
            {
                //UMIConnectionString = $"Data Source=UMI-ERP-01 ;Initial Catalog = UMi_Tooling; Integrated Security = True; Connect Timeout = 120; ";
                HHI_PUMIConnectionString = $"Data Source=UMI-ERP-01 ;Initial Catalog = HydraulicHoseInfo_test; Integrated Security = True; Connect Timeout = 120; ";
                SAPConnectionString = "Persist Security Info=True;Initial Catalog=UMIS_UAT;Integrated Security = True;Data Source=UMI-ERP-01;";
            }
            else
            {
                //UMIConnectionString = $"Data Source=UMI-ERP-01 ;Initial Catalog = UMi_Tooling; Integrated Security = True; Connect Timeout = 120; ";
                HHI_PUMIConnectionString = $"Data Source=UMI-ERP-01 ;Initial Catalog = HydraulicHoseInfo_prod; Integrated Security = True; Connect Timeout = 120; ";
                SAPConnectionString = "Persist Security Info=True;Initial Catalog=UMIS;Integrated Security = True;Data Source=UMI-ERP-01;";
            }

        }

        // Connect to laser :: Complete
        private void AttemptToConnectToLaser()
        {
            bool is_success = false;
            try
            {
                //If the application is exited without disconnected, the
                //instance may exclusively use the online connection.In this
                //case, release the online connection.
                if (axMBActX2.Comm.IsOnline)
                {
                    axMBActX2.Comm.Offline();
                }
                
                axMBActX2.Comm.ConnectionType = MBPLib2.ConnectionTypes.CONNECTION_USB;
                is_success = axMBActX2.Comm.Online();
                
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                //MessageBox.Show(error.Message);
            }

            if (is_success)
            {
                //MessageBox.Show("Connection Success");
            }
            else
            {
                MessageBox.Show("Connection Failed");
            }

        }

        // Call connect to laser :: Complete
        private void markerConnectButton_Click(object sender, EventArgs e)
        {
            AttemptToConnectToLaser();
        }

        // Disconnect laser :: Complete
        private void MarkerDisconnectButton_Click(object sender, EventArgs e)
        {

            try
            {
                if (!axMBActX2.Comm.IsOnline)
                {
                    MessageBox.Show("Offline Now");
                }
                else
                {
                    bool is_success = axMBActX2.Comm.Offline();
                    if (is_success)
                    {
                        MessageBox.Show("Offline Success");
                    }
                    else
                    {
                        MessageBox.Show("Offline Failed");
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        // Calls function to load items on Refresh Orders Click :: Complete
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            updateOpenOrderItems();
        }

        // Marks part :: Probably works
        private void Mark_Part_Click(object sender, EventArgs e)
        {
            Mark_Part.BackColor = Color.Red;
            try
            {
                if (axMBActX2.Operation.IsCameraFinderView == true)
                {
                    axMBActX2.Operation.IsCameraFinderView = false;

                }
                else
                {
                    axMBActX2.Operation.IsCameraFinderView = true;


                }
                isConnected = true;
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
                isConnected = false;
            }
            try
            {

                axMBActX2.SaveControllerJob(0);

                axMBActX2.IsBlockingCommunication = true;

                axMBActX2.Operation.SetCurrentJobNo(0);

                axMBActX2.Operation.StartMarking();

                bool result2;
                double time2;

                axMBActX2.Operation.GetMarkingResult(out result2,out time2);

                isConnected = true;

            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
                isConnected = false;
            }

            sendMarkedToDB();

        }

        // sends info about marked part to DB :: Complete
        private void sendMarkedToDB()
        {
            using (SqlConnection cn = new SqlConnection(HHI_PUMIConnectionString))
            {
                try
                {
                    cn.Open(); 

                    // Prepare the SQL command with parameters
                    string sql = @"
            INSERT INTO LaserMarkings (DateTimeMarked, PrimaryPN, SecondaryPN, ProductionNumber, HeatNumber, IsConnected)
            VALUES (@DateTimeMarked, @PrimaryPN, @SecondaryPN, @ProductionNumber, @HeatNumber, @IsConnected)";

                    using (SqlCommand cmd2 = new SqlCommand(sql, cn))
                    {
                        cmd2.CommandTimeout = 120; // Set a long timeout in case of really complex queries

                        DataGridViewRow row = this.OrdersGridView.SelectedRows[0];
                        string ProductionNumber = "";
                        try { ProductionNumber = row.Cells["ProductionNumber"].Value.ToString(); }
                        catch { }

                        // Add parameters to the command
                        cmd2.Parameters.AddWithValue("@DateTimeMarked", DateTime.Now); // Set to current date and time
                        
                        cmd2.Parameters.AddWithValue("@PrimaryPN", PartNumAndRevBox.Text);
                        
                        cmd2.Parameters.AddWithValue("@SecondaryPN", CustPartNumAndRevBox.Text);
                        
                        cmd2.Parameters.AddWithValue("@ProductionNumber", ProductionNumber); 
                        cmd2.Parameters.AddWithValue("@HeatNumber", HeatBox.Text);
                        cmd2.Parameters.AddWithValue("@IsConnected", isConnected); // Replace with actual test flag value

                        // Execute the command
                        cmd2.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error)
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        // Flips IsCameraFinderView after marking end event :: Probably works
        private void axMBActX1_EvMarkingEnd(object sender, _DMBActXEvents_EvMarkingEndEvent e)
        {
            try
            {
                if (axMBActX2.Operation.IsCameraFinderView == true)
                {
                    axMBActX2.Operation.IsCameraFinderView = false;

                }
                else
                {
                    axMBActX2.Operation.IsCameraFinderView = true;


                }
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }
            //MessageBox.Show("Marking end");
            Mark_Part.BackColor = SystemColors.ControlLight;
        }

        // Turns ligh on :: Complete
        private void LightOffButton_Click(object sender, EventArgs e)
        {
            try
            {
                axMBActX2.Operation.SetLighting(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        // Turns ligh off :: Complete
        private void LightOnButton_Click(object sender, EventArgs e)
        {
            try
            {
                axMBActX2.Operation.SetLighting(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }

            
        }

        // Loads label when click on part :: Complete
        private void OrdersGridView_Click(object sender, EventArgs e)
        {
            RemoveComboBoxHandlers();
            comboBox1.SelectedIndex = -1;
            ProgramMaterialCombo.SelectedIndex = -1;
            ProgramSizeCombo.SelectedIndex = -1;
            LogoComboBox.SelectedIndex = -1;
            save.Enabled = true;
            btnOpenMarkerBuilder.Enabled = true;
            Mark_Part.BackColor = SystemColors.ControlLight ;
            string SelectedPN = "";
            double diam;
            double wall;
            string partnum;
            string mtl;
            bool fileFound;
            string PNSub = " ";
            string orderRev = "0";
            LabelBox.CheckedChanged -= LabelBox_CheckedChanged;
            ImageBox.CheckedChanged -= ImageBox_CheckedChanged;
            DateCheckBox.CheckedChanged -= DateCheckBox_CheckedChanged;
            PN1Box.CheckedChanged -= PN1Box_CheckedChanged;
            PN2Box.CheckedChanged -= PN2Box_CheckedChanged;
            Desc1Box.CheckedChanged -= Desc1Box_CheckedChanged;
            Desc2Box.CheckedChanged -= Desc2Box_CheckedChanged;
            HeatCheckBox.CheckedChanged -= HeatCheckBox_CheckedChanged;

            try
            {
                LabelBox.Checked = true;
                ImageBox.Checked = true;
                DateCheckBox.Checked = true;
                PN1Box.Checked = true;
                PN2Box.Checked = true;
                Desc1Box.Checked = true;
                Desc2Box.Checked = true;
                HeatCheckBox.Checked = true;
            }
            catch
            {
                // Handle exceptions if necessary
            }
            finally
            {
                // Re-subscribe to the event
                LabelBox.CheckedChanged += LabelBox_CheckedChanged;
                ImageBox.CheckedChanged += ImageBox_CheckedChanged;
                DateCheckBox.CheckedChanged += DateCheckBox_CheckedChanged;
                PN1Box.CheckedChanged += PN1Box_CheckedChanged;
                PN2Box.CheckedChanged += PN2Box_CheckedChanged;
                Desc1Box.CheckedChanged += Desc1Box_CheckedChanged;
                Desc2Box.CheckedChanged += Desc2Box_CheckedChanged;
                HeatCheckBox.CheckedChanged += HeatCheckBox_CheckedChanged;
            }


            DateTime thisDay = DateTime.Today;
            string customdate = thisDay.ToString("yyyy-MM-dd");

            DateBox.Text = customdate;

            if (OrdersGridView.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.OrdersGridView.SelectedRows[0];
                SelectedPN = row.Cells["Part_Number"].Value.ToString();
                try { POTxtBox.Text = row.Cells["ProductionNumber"].Value.ToString(); }
                catch { POTxtBox.Text = ""; }
                

            }
           
            if (SelectedPN.Contains("_"))
            {
                PNSub = SelectedPN.Split('_')[0];
                orderRev = SelectedPN.Split('_')[1];
            }

            // SelectedPN: PN as shown in table (may have rev)
            // PNSub: PN
            // orderRev: rev

            // Not sure what happens (No TK rn)
            if (SelectedPN[0] == 'T')
            {
                DataTable dt = new DataTable();
          
                dt.Columns.Add("Part_Number");
                dt.Columns.Add("Description");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("UOM");
                GetTubeKitFromPDMBom(PNSub,dt);
                OrdersGridView.DataSource = dt;
            }

            // P case
            else if (SelectedPN[0] == 'P')
            {
                CheckForCustomProgram(SelectedPN);
                if (SelectedPN.Contains("_"))
                {
                    SelectedPN = PNSub;
                }
                if (GenericProgram)
                {
                    GetTubePartNumberFromPDMBom(SelectedPN, out diam, out wall,out  partnum, out mtl, out fileFound);
                    if (fileFound)
                    {
                        OpenGenericProgram();
                        FillTubeDetails(SelectedPN, " ");
                    }
                }
            }

            // From 8xxxx (no rev)
            else if (SelectedPN[0] == '8' && SelectedPN.Length == 5)
            {

                IEdmVault7 vault2 = null;
                if (vault1 == null)
                {
                    vault1 = new EdmVault5();
                }

                vault2 = (IEdmVault7)vault1;

                if (!vault1.IsLoggedIn)
                {
                    vault1.LoginAuto("UMIS", this.Handle.ToInt32());
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("Part_Number");
                dt.Columns.Add("Customer");
                dt.Columns.Add("CustomerPN");
                dt.Columns.Add("Rev");
                dt.Columns.Add("Description");
                DataRow dr;
                aFile = (IEdmFile7)vault2.GetFileFromPath($@"C:\UMIS\UMi Parts\80000\{SelectedPN}.slddrw", out ppoRetParentFolder);

                try
                {
                    IEdmEnumeratorVariable10 enumVariable = (IEdmEnumeratorVariable10)aFile.GetEnumeratorVariable();
                    bool getVarSuccess = enumVariable.GetVarAsText("PartNo", "@", ppoRetParentFolder.ID, out object poPartNo);
                    getVarSuccess = enumVariable.GetVarAsText("Customer Name", "@", ppoRetParentFolder.ID, out object poCustomer);
                    getVarSuccess = enumVariable.GetVarAsText("CustomerPN", "@", ppoRetParentFolder.ID, out object poCustomerPN);
                    getVarSuccess = enumVariable.GetVarAsText("Revision", "@", ppoRetParentFolder.ID, out object poRevision);
                    getVarSuccess = enumVariable.GetVarAsText("Description", "@", ppoRetParentFolder.ID, out object poDescription);
                    dr = dt.NewRow();



                    dr["Part_Number"] = GetValueOrDefault(poPartNo);
                    dr["Customer"] = GetValueOrDefault(poCustomer);
                    dr["CustomerPN"] = GetValueOrDefault(poCustomerPN);
                    dr["Rev"] = GetValueOrDefault(poRevision);
                    dr["Description"] = GetValueOrDefault(poDescription);

                    dt.Rows.Add(dr);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                OrdersGridView.DataSource = dt;
                DataGridViewRow row = this.OrdersGridView.SelectedRows[0];
                orderRev = row.Cells["Rev"].Value.ToString();

                CheckForCustomProgram(SelectedPN + "_" + orderRev);

                if (GenericProgram)
                {
                    GetTubePartNumberFromPDMBom(SelectedPN, out diam, out wall, out partnum, out mtl, out fileFound);
                    if (fileFound)
                    {
                        OpenGenericProgram();
                        FillTubeDetails(SelectedPN, orderRev);
                    }
                }
            }
            else
            {
                CheckForCustomProgram(SelectedPN);
                if (GenericProgram)
                {
                    GetTubePartNumberFromPDMBom(PNSub, out diam, out wall, out partnum, out mtl, out fileFound);
                    if (fileFound)
                    {
                        OpenGenericProgram();
                        FillTubeDetails(PNSub, orderRev);
                    }
                }
            }
            for (int i = 1; i < 10; i++)
            {
                try
                {
                    axMBActX2.Block(i).X = 0;
                    axMBActX2.Block(i).Y = 0;
                }
                catch
                {
                }
            }
            RestoreComboBoxHandlers();
        }

        // Returns "N/A" when null or empty :: Complete
        private string GetValueOrDefault(object value)
        {
            return string.IsNullOrEmpty(value as string) ? "N/A" : value.ToString();
        }

        // Called from OrdersGridView_Click when have "TK" :: Untested (No TK currently)
        private void GetTubeKitFromPDMBom(string partNumber, DataTable dt)
        {
            
            try
            {

                IEdmVault7 vault2 = null;                                                                                   
                if (vault1 == null)
                {
                    vault1 = new EdmVault5();
                }

                vault2 = (IEdmVault7)vault1;
                if (!vault1.IsLoggedIn)
                {
                    vault1.LoginAuto("UMIS", this.Handle.ToInt32());
                }

                string path = $@"C:\UMIS\Projects\";
                var folderPath = System.IO.Path.GetDirectoryName(path);
                var folder = vault2.GetFolderFromPath(folderPath);
                IEdmPos5 FolderPos = folder.GetFirstSubFolderPosition();
                bool found = false;
                while (!FolderPos.IsNull && !found)
                {
                    IEdmFolder5 SubFolder = folder.GetNextSubFolder(FolderPos);

                    IEdmPos5 FilePos = SubFolder.GetFirstFilePosition();
                    while (!FilePos.IsNull)
                    {
                        IEdmFile5 file = SubFolder.GetNextFile(FilePos);
                        if (file != null && file.Name.Contains(partNumber) && file.Name.EndsWith(".slddrw", StringComparison.OrdinalIgnoreCase)) 
                        {
                            aFile = (IEdmFile7)file;
                            found = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not find PN: " + partNumber + " in projects folder.\n" + ex.Message);
            }

            try
            {
                IEdmVault7 vault2 = null;
                if (vault1 == null)
                {
                    vault1 = new EdmVault5();
                }
                vault2 = (IEdmVault9)vault1;
                if (!vault1.IsLoggedIn)
                {
                    vault1.LoginAuto("UMIS", this.Handle.ToInt32());
                }

                if (aFile!= null)
                {
                    Dictionary<string, string> tubes = new Dictionary<string, string>();
                    List<string> BOMnames = new List<string>();
                    EdmBomInfo[] derivedBOMs;

                    aFile.GetDerivedBOMs(out derivedBOMs);


                    int BOMid = derivedBOMs[derivedBOMs.Length - 1].mlBomID; //use the latest version if multiple BOMs are not found
                    bom = (IEdmBom)vault2.GetObject(EdmObjectType.EdmObject_BOM, BOMid); //use the latest version of the BOM
                    bomView = (IEdmBomView3)(IEdmBomView2)bom.GetView(0);


                    EdmBomColumn[] bomColumns = null;
                    bomView.GetColumns(out bomColumns);
                    
                    object[] bomRows;
                    bomView.GetRows(out bomRows);

                    object cellVar = null;
                    object computedValue = null;
                    string config = null;
                    bool readOnly = false;
                    bool rightBOM = true;
                    
                    foreach (IEdmBomCell cell in bomRows)
                    {
                        for (int j = 1; j < bomColumns.Count(); j++) //columns
                        {
                            if (bomColumns[j].mbsCaption.ToLower().Contains("uom"))
                            {
                                cell.GetVar(bomColumns[j].mlVariableID, bomColumns[j].meType, out cellVar, out computedValue, out config, out readOnly);
                                if (cellVar.ToString() == "FT")
                                {
                                    rightBOM = false;
                                    break;
                                }
                            }
                        }
                    }
                    if (!rightBOM)
                    {
                        BOMid = derivedBOMs[derivedBOMs.Length - 2].mlBomID;
                        bom = (IEdmBom)vault2.GetObject(EdmObjectType.EdmObject_BOM, BOMid); //use the latest version of the BOM
                        bomView = (IEdmBomView3)(IEdmBomView2)bom.GetView(0);

                        bomView.GetColumns(out bomColumns);

                        bomView.GetRows(out bomRows);
                    }
                    
                    DataRow dr = null;
                    foreach (IEdmBomCell cell in bomRows)
                    {
                        
                        dr = dt.NewRow();
 
                        for (int j = 1; j < bomColumns.Count(); j++) //columns
                        {

                            cell.GetVar(bomColumns[j].mlVariableID, bomColumns[j].meType, out cellVar, out computedValue, out config, out readOnly);
                            
                            string column = bomColumns[j].mbsCaption.ToLower();

                            if (column.Contains("item"))
                            {
                                dr["Item"] = cellVar.ToString();                        
                            }
                            //if column is item do nothing
                            else if (column.Contains("partno") || column.Contains("part number") || column.Contains("part no."))
                            {
                                dr["Part_Number"] = cellVar.ToString();
                            }
                            else if (column.Contains("description"))
                            {
                                dr["Description"] = cellVar.ToString();
                            }
                            else if (column.Contains("qty"))
                            {
                                dr["Quantity"] = cellVar.ToString();
                            }
                            else if (column.Contains("uom"))
                            {
                                dr["UOM"] = cellVar.ToString();
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                MessageBox.Show("Error looking up BOM.\n\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error looking up BOM.\n\n" + ex.Message);
            }
        }

        // Checks if we have a saved program for the part :: Complete
        private void CheckForCustomProgram(string PN)
        {
            string FilePath = $@"\\UMISSERVER2\UMI\Engineering\LaserMarkingProfiles\{PN}.MA2";
            if (System.IO.File.Exists(FilePath))
            {
                try
                {
                    if (axMBActX2.OpenJob(FilePath))
                    {
                        string partNum = "";
                        string customerNum = "";
                        string description = "";
                        string description2 = "";
                        string heatin = "";
                        axMBActX2.Context = ContextTypes.CONTEXT_EDITING;
                        axMBActX2.OpenJob(FilePath);
                        try
                        {
                            JobTitleLabel.Text = axMBActX2.Job.Title;
                            try
                            {
                                partNum = axMBActX2.Block(3).Text;
                            }
                            catch {}

                            try
                            {
                                customerNum = axMBActX2.Block(4).Text;
                            }
                            catch{ }

                            try
                            {
                                description = axMBActX2.Block(5).Text;
                            }
                            catch{}

                            try
                            {
                                description2 = axMBActX2.Block(6).Text;
                            }
                            catch{}
                            try
                            {
                                heatin = axMBActX2.Block(7).Text;
                            }
                            catch { }
                            try
                            {
                                string disabled = axMBActX2.Block(16).Text; //New disabled "DO NOT MODIFY" block
                                if (disabled == "DO NOT MODIFY")
                                {
                                    save.Enabled = false; // Disable the button
                                    btnOpenMarkerBuilder.Enabled = false;
                                }
                            }
                            catch (System.Runtime.InteropServices.COMException error)
                            {
                            }
                            PartNumAndRevBox.Text = partNum;
                            CustPartNumAndRevBox.Text = customerNum;
                            DescLine1Box.Text = description;
                            DescLine2Box.Text = description2;
                            HeatBox.Text = heatin;  

                            try
                            {
                                if (axMBActX2.Block(8).IsMarkingEnable)
                                {
                                    QRCheckBox.Checked = true;
                                }
                                else
                                {
                                    QRCheckBox.Checked = false;
                                }
                            }
                            catch { }
                                UpdateCurrentProgramBlocks(0);
                            
                        }
                        catch (System.Runtime.InteropServices.COMException error)
                        {
                            MessageBox.Show(error.Message + error);
                        }  
                    }               
                }
                catch (System.Runtime.InteropServices.COMException error)
                {
                    MessageBox.Show(error.Message + error);
                }
                MessageBox.Show("Custom program found for PN "+PN);
                GenericProgram = false;
            }
            else
            {
                if (PN.Contains("_"))
                {
                    var parts = PN.Split('_');
                    string selectedPN = parts[0];
                    int orderRev;
                    if (parts.Length > 1 && int.TryParse(parts[1], out orderRev))
                    {
                        orderRev--;
                        if (orderRev >= 0)
                        {
                            UpdateCurrentProgramRecursive(selectedPN + "_" + orderRev, orderRev);
                        } else
                        {
                            GenericProgram = true;
                        }
                    }
                    else
                    {
                        GenericProgram = true;
                    }
                }
            }
        }

        // Gets information for label from PN :: Complete
        private void GetTubePartNumberFromPDMBom(string pNSub, out double diameter, out double wallThick, out string partNumber, out string mtl, out bool fileFound)
        {
            string myfolder = "";
            diameter = 9999;
            wallThick = 9999;
            partNumber = "";
            mtl = "";
            string folderName = " ";
            fileFound = false;
            //GetFileFromSearch*******************
            try
            {

                IEdmVault7 vault2 = null;
                if (vault1 == null)
                {
                    vault1 = new EdmVault5();
                }

                vault2 = (IEdmVault7)vault1;
                if (!vault1.IsLoggedIn)
                {
                    vault1.LoginAuto("UMIS", this.Handle.ToInt32());
                }

                if (pNSub[0] != 'P')
                {
                    folderName = pNSub[0]+"0000";
                    aFile = (IEdmFile7)vault2.GetFileFromPath($@"C:\UMIS\UMi Parts\{pNSub[0]}0000\{pNSub}.slddrw", out ppoRetParentFolder);
                    fileFound = true;
                    if (aFile == null)
                    {
                        fileFound = false;
                        throw new Exception("Drawing not found for " + pNSub + " from " + folderName + " folder. Please create before continuing.");
                    }    
                }
                else
                {
                    folderName = "Project";
                    string path = $@"C:\UMIS\Projects\";
                    var folderPath = System.IO.Path.GetDirectoryName(path);
                    var folder = vault2.GetFolderFromPath(folderPath);
                    myfolder = folderPath.ToString();
                    IEdmPos5 FolderPos = folder.GetFirstSubFolderPosition();
                    while (!FolderPos.IsNull && !fileFound)
                    {
                        IEdmFolder5 SubFolder = folder.GetNextSubFolder(FolderPos);

                        IEdmPos5 FilePos = SubFolder.GetFirstFilePosition();
                        while (!FilePos.IsNull)
                        {
                            IEdmFile5 file = SubFolder.GetNextFile(FilePos);
                            if (file != null && file.Name.Contains(pNSub) && file.Name.EndsWith(".slddrw", StringComparison.OrdinalIgnoreCase))
                            {
                                aFile = (IEdmFile7)file;
                                fileFound = true;
                            }
                        }
                    }
                    if (fileFound == false)
                    {
                        throw new Exception("Drawing not found for " + pNSub + " from " + folderName + " folder. Please create before continuing.");
                        return;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Drawing not found for "+pNSub+" from "+folderName+" folder. Please create before continuing." );
                return;
            }

            if (fileFound == true)
            {
                //GetBOMFromFILE*******************
                try
                {
                    IEdmVault7 vault2 = null;
                    if (vault1 == null)
                    {
                        vault1 = new EdmVault5();
                    }
                    vault2 = (IEdmVault9)vault1;
                    if (!vault1.IsLoggedIn)
                    {
                        vault1.LoginAuto("UMIS", this.Handle.ToInt32());
                    }

                    if (aFile != null)
                    {

                        EdmBomInfo[] derivedBOMs;

                        aFile.GetDerivedBOMs(out derivedBOMs);

                        bom = (IEdmBom)vault2.GetObject(EdmObjectType.EdmObject_BOM, derivedBOMs[(derivedBOMs.Length - 1)].mlBomID); // USE ID OF DERIVED BOM at end of the list. I assume it is the latest date, most recent.
                        bomView = (IEdmBomView3)(IEdmBomView2)bom.GetView(0);

                        EdmBomColumn[] BomColumns = null;
                        bomView.GetColumns(out BomColumns);

                        object[] BomRows = null;
                        bomView.GetRows(out BomRows);

                        object cellVar = null;
                        object ComputedValue = null;
                        string config = null;
                        bool ReadOnlyOut = false;
                        List<string> TubesFound = new List<string>();

                        //search all cells inluding item column (different column type)
                        foreach (IEdmBomCell CELL in BomRows)
                        {
                            int ColumnCount = 0;

                            foreach (EdmBomColumn COLUMN in BomColumns)
                            {
                                //MessageBox.Show("" + COLUMN.mbsCaption);
                                if (ColumnCount <= (BomColumns.Count() - 1)) // in case BOM has more columns than expeted
                                {

                                    CELL.GetVar(BomColumns[ColumnCount].mlVariableID, BomColumns[ColumnCount].meType, out cellVar, out ComputedValue, out config, out ReadOnlyOut);
                                    if ((cellVar.ToString()).Contains("304LT") || (cellVar.ToString()).Contains("316LT"))
                                    {
                                        TubesFound.Add(cellVar.ToString());
                                        mtl = "SS";
                                    }
                                    else if (((cellVar.ToString()).Contains("4130") || (cellVar.ToString()).Contains("A513-5") || (cellVar.ToString()).Contains("A513-1") || (cellVar.ToString()).Contains("J524") || (cellVar.ToString()).Contains("J525")) && (!(cellVar.ToString()).Contains("J524 ")) && (!(cellVar.ToString()).Contains("J524,")))
                                    {
                                        TubesFound.Add(cellVar.ToString());
                                        mtl = "CS";
                                    }
                                    else
                                    {

                                    }
                                    //MessageBox.Show("Col Position: " + ColumnCount + "\n Col ml variable: " + BomColumns[ColumnCount].mlVariableID + "\n cell item :" + cellItem + "\n Col Type for [abc] :" + BomColumns[ColumnCount].meType);
                                    ColumnCount += 1;



                                }
                            }
                        }
                        if (TubesFound.Count == 0)
                        {
                            MessageBox.Show("No Tube Part Numbers Found For " + pNSub + " from " + myfolder);
                            return;
                        }
                        else if (TubesFound.Count > 1)
                        {
                            string total = "";
                            foreach (string find in TubesFound)
                            {
                                total += find + "\n";
                            }
                            MessageBox.Show("More than one tube was found:\n" + total);
                        }
                        else
                        {
                            partNumber = TubesFound[0];
                            SelectedMaterialPN.Text = partNumber;
                            diameter = Convert.ToDouble(TubesFound[0].Substring(TubesFound[0].Length - 7, 4)) / 1000;
                            wallThick = Convert.ToDouble(TubesFound[0].Substring(TubesFound[0].Length - 3, 3)) / 1000;
                        }
                    }
                }
                catch (System.Runtime.InteropServices.COMException ex)
                {
                    MessageBox.Show("An error occured retrieving data from the BOM for " + pNSub + " from " + myfolder + "\n\n Error message: " + ex.Message);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured retrieving data from the BOM for " + pNSub + " from " + myfolder + "\n\n Error message: " + ex.Message);
                    return;
                }

                //APPLY PROGRAM BASED ON SIZE
                try
                {

                }
                catch
                {

                }
                AttemptToSelectProgram(diameter, mtl);
            }
        }

        // Selects prgram based on diameter & material :: Complete
        private void AttemptToSelectProgram(double diameter, string mtl)
        {
            ProgramSizeCombo.SelectedIndex = -1;
            ProgramMaterialCombo.SelectedIndex = -1; ;
            if(mtl == "SS")
            {
                ProgramMaterialCombo.SelectedItem = "SS";
                if (diameter == 1.500)
                {
                    ProgramSizeCombo.SelectedItem = "24";
                }else if (diameter == 1.250)
                {
                    ProgramSizeCombo.SelectedItem = "20";
                }else if (diameter == 1.000)
                {
                    ProgramSizeCombo.SelectedItem = "16";
                }else if (diameter == 0.750)
                {
                    ProgramSizeCombo.SelectedItem = "12";
                }
                else if (diameter == 0.625)
                {
                    ProgramSizeCombo.SelectedItem = "10";
                }
                else if (diameter == 0.500)
                {
                    ProgramSizeCombo.SelectedItem = "08";
                }
                else if (diameter == 0.375)
                {
                    ProgramSizeCombo.SelectedItem = "06";    
                }
                else if (diameter == 0.250)
                {
                    ProgramSizeCombo.SelectedItem = "04";
                }
                else
                {
                    MessageBox.Show("No stainless program exists for "+diameter+" inch.");
                }

            }
            else
            {
                ProgramMaterialCombo.SelectedItem = "CS";
                if (diameter == 1.500)
                {
                    ProgramSizeCombo.SelectedItem = "24";
                }
                else if (diameter == 1.250)
                {
                    ProgramSizeCombo.SelectedItem = "20";
                }
                else if (diameter == 1.000)
                {
                    ProgramSizeCombo.SelectedItem = "16";
                }
                else if (diameter == 0.750)
                {
                    ProgramSizeCombo.SelectedItem = "12";
                }
                else if (diameter == 0.625)
                {
                    ProgramSizeCombo.SelectedItem = "10";
                }
                else if (diameter == 0.500)
                {
                    ProgramSizeCombo.SelectedItem = "08";
                }
                else if (diameter == 0.375)
                {
                    ProgramSizeCombo.SelectedItem = "06";
                }
                else if (diameter == 0.250)
                {
                    ProgramSizeCombo.SelectedItem = "04";
                }
                else
                {
                    MessageBox.Show("No CARBON program exists for " + diameter + " inch.");
                }
            }
        }

        // Fills details (text blocks) in for the label :: Complete
        private void FillTubeDetails(string TubePartNumber, string OrderRev)
        {
            try
            {

                IEdmVault7 vault2 = null;
                if (vault1 == null)
                {
                    vault1 = new EdmVault5();
                }

                vault2 = (IEdmVault7)vault1;
                if (!vault1.IsLoggedIn)
                {
                    vault1.LoginAuto("UMIS", this.Handle.ToInt32());
                }
                if (TubePartNumber[0] != 'P')
                {
                    aFile = (IEdmFile7)vault1.GetFileFromPath($@"C:\UMIS\UMi Parts\{TubePartNumber[0]}0000\{TubePartNumber}.slddrw", out ppoRetParentFolder);

                }
                else
                {
                    string path = $@"C:\UMIS\Projects\";
                    var folderPath = System.IO.Path.GetDirectoryName(path);
                    var folder = vault2.GetFolderFromPath(folderPath);
                    IEdmPos5 FolderPos = folder.GetFirstSubFolderPosition();
                    bool found = false;
                    while (!FolderPos.IsNull && !found)
                    {
                        IEdmFolder5 SubFolder = folder.GetNextSubFolder(FolderPos);

                        IEdmPos5 FilePos = SubFolder.GetFirstFilePosition();
                        while (!FilePos.IsNull)
                        {
                            IEdmFile5 file = SubFolder.GetNextFile(FilePos);
                            if (file != null && file.Name.Contains(TubePartNumber) && file.Name.EndsWith(".slddrw", StringComparison.OrdinalIgnoreCase))
                            {
                                aFile = (IEdmFile7)file;
                                found = true;
                            }
                        }
                    }
                }

                IEdmEnumeratorVariable6 enumVariable = (IEdmEnumeratorVariable6)aFile.GetEnumeratorVariable();
                bool getVarSuccess = enumVariable.GetVarFromDb("PartNo", "@", out object poPartNo);
                getVarSuccess = enumVariable.GetVarFromDb("CustomerPN", "@", out object poCustomerPN);
                getVarSuccess = enumVariable.GetVarFromDb("Revision", "@", out object poRevision);
                getVarSuccess = enumVariable.GetVarFromDb("Description", "@", out object poDescription);
               
                if (poRevision == null)
                {
                    poRevision = 0;
                }
                if (poCustomerPN == null)
                {
                    poCustomerPN = "";
                }
                if (poDescription == null)
                {
                    poDescription = "";
                }

                PartNumAndRevBox.Text = poPartNo.ToString() +"_"+ poRevision.ToString();
                CustPartNumAndRevBox.Text = poCustomerPN.ToString();
                string desc = poDescription.ToString();
                int DescLengthAllow = 32;
                if (desc.Length <= DescLengthAllow)
                {
                    DescLine1Box.Text = desc;
                }
                else if (desc.Length > DescLengthAllow && desc.Length <= (DescLengthAllow * 2))
                {
                    DescLine1Box.Text = desc.Substring(0, DescLengthAllow);
                    DescLine2Box.Text = desc.Substring(DescLengthAllow, desc.Length - DescLengthAllow);
                }
                else
                {
                    DescLine1Box.Text = desc.Substring(0, DescLengthAllow);
                    DescLine2Box.Text = desc.Substring(DescLengthAllow, DescLengthAllow);

                }
                using (SqlConnection cn = new SqlConnection(HHI_PUMIConnectionString))
                {
                    try
                    {
                        cn.Open();  // Open connection using the SQL connection string above
                        SqlCommand cmd2 = new SqlCommand("", cn);    //Declare text command for server connection
                        cmd2.CommandTimeout = 120; //set a long timeout in case of really complex queries 2019-04-30
                        cmd2.Parameters.AddWithValue("@Search", TubePartNumber);
                        cmd2.CommandText = "" + " select Customer_id FROM TubeAssemblies where PartNo = @Search";
                        SqlDataReader reader2 = cmd2.ExecuteReader();
                        while (reader2.Read())
                        {
                            UpdateCurrentProgramBlocks(Convert.ToInt32(reader2[0]));
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error getting open orders" + ex);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // Sets label to the text in boxes :: Complete
        private void UpdateCurrentProgramBlocks(int customerId)
        {
            partNumsFliped = false;
            try { if (axMBActX2.Block(2).IsEditable) { axMBActX2.Block(2).Text = DateBox.Text.ToUpper(); } }
            catch { axMBActX2.Block(2).Text = " "; }
            try { if (axMBActX2.Block(3).IsEditable) { axMBActX2.Block(3).Text = PartNumAndRevBox.Text.ToUpper(); } }
            catch { axMBActX2.Block(3).Text = " "; }
            try { if (axMBActX2.Block(4).IsEditable) { axMBActX2.Block(4).Text = CustPartNumAndRevBox.Text.ToUpper(); } }
            catch { axMBActX2.Block(4).Text = " "; }
            try { if (axMBActX2.Block(5).IsEditable) { axMBActX2.Block(5).Text = DescLine1Box.Text.ToUpper(); } }
            catch { axMBActX2.Block(5).Text = " "; }
            try { if (axMBActX2.Block(6).IsEditable) { axMBActX2.Block(6).Text = DescLine2Box.Text.ToUpper(); } }
            catch { axMBActX2.Block(6).Text = " "; }
            try { if (axMBActX2.Block(7).IsEditable) { axMBActX2.Block(7).Text = HeatBox.Text.ToUpper(); } }
            catch { axMBActX2.Block(7).Text = " "; }
            if (customerId != 0)
            {
                try 
                { 
                    
                    if (axMBActX2.Block(1).IsEditable) 
                    { 
                        
                        axMBActX2.Block(1).IsLogoAspectRatioKeep = true;
                        //axMBActX2.Block(1).LogoWidth = 10.250; //in mm
                        DirectoryInfo directory = new DirectoryInfo(@"U:\Engineering\CUSTOMERLOGO\");
                        axMBActX2.Block(1).Text = ""+ directory.ToString()+ customerId.ToString()+ ".MHL";

                    } 
                
                }
                catch {
                    DirectoryInfo directory = new DirectoryInfo(@"U:\Engineering\CUSTOMERLOGO\");
                    axMBActX2.Block(1).Text = "" + directory.ToString() + "Multiflow.MHL";
                }
            }
            

        }

        // Calls UpdateCurrentProgramBlocks(0) when any text for a block changes :: Complete
        private void BlockText_TextChanged(object sender, EventArgs e)
        {
            UpdateCurrentProgramBlocks(0);
        }

        // Flips part numbers :: Complete
        private void FlipPartNumbersButton_Click(object sender, EventArgs e)
        {
            string tempPN = PartNumAndRevBox.Text;
            string tempCustPN = CustPartNumAndRevBox.Text;
            if (partNumsFliped) partNumsFliped = false;
            else partNumsFliped = true;

            PartNumAndRevBox.Text = tempCustPN;
            CustPartNumAndRevBox.Text = tempPN;
        }

        // Sets cameras position (works I think) Should add ability to move from user inputs??
        private void SetCameraPosition_Click(object sender, EventArgs e)
        {
            double PositionX = 1.000;
            double PositionY = 2.000;
            double PositionZ = 3.00;
            bool IsAffectWorkpiecePositionAdjustment = false;
            try
            {
                axMBActX2.Operation.StartCameraScanningMode2(ShutterStateTypes.SHUTTERSTATE_OPEN);
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }
        
            try
            {
                axMBActX2.Operation.SetCameraPosition(PositionX, PositionY, PositionZ, IsAffectWorkpiecePositionAdjustment);
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }

            try
            {
                axMBActX2.Operation.FinishCameraScanningMode();
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }


        }

        // Opens a generic program for material and size :: Complete
        private void OpenGenericProgram()
        {
            if (ProgramMaterialCombo.SelectedIndex != -1 && ProgramSizeCombo.SelectedIndex != -1)
            {
                try
                {
                    string FilePath = @"\\UMISSERVER2\UMI\Engineering\LaserMarkingProfiles\" + ProgramSizeCombo.SelectedItem.ToString() + "-" + ProgramMaterialCombo.SelectedItem.ToString() + "-A.MA2";
                    if (axMBActX2.OpenJob(FilePath))
                    {
                        axMBActX2.Context = ContextTypes.CONTEXT_EDITING;
                        axMBActX2.OpenJob(FilePath);
                        JobTitleLabel.Text = axMBActX2.Job.Title;
                        var block = axMBActX2.Block(8);
                        try
                        {
                            // Check if IsMarkingEnable property is accessible
                            if (block.IsMarkingEnable)
                            {
                                block.IsMarkingEnable = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            // Handle the exception if accessing the property fails
                            Console.WriteLine($"Error accessing IsMarkingEnable: {ex.Message}");
                        }
                        axMBActX2.Block(3).X = 10;

                    }
                    
                }
                catch (System.Runtime.InteropServices.COMException error)
                {
                    MessageBox.Show(error.Message + error);
                }
            } else
            {
                ProgramMaterialCombo.SelectedIndex = 0;
                ProgramSizeCombo.SelectedIndex = 4;
                OpenGenericProgram();
                MessageBox.Show("The size and/or material could not be found. A generic program has opened with default size and material, adjust these to fit your tube.");
                
            }
        }

        // (8xxxx) Populate with all part numbers :: Complete
        private void AllPartNumBtn_Click(object sender, EventArgs e)
        {
            try
            {
                IEdmVault7 vault2 = null;
                if (vault1 == null)
                {
                    vault1 = new EdmVault5();
                }

                vault2 = (IEdmVault7)vault1;
                if (!vault1.IsLoggedIn)
                {
                    vault1.LoginAuto("UMIS", this.Handle.ToInt32());
                }
                //Show the Select File dialog

                DataTable dt = new DataTable();
                dt.Columns.Add("Part_Number");
                DataRow dr = null;

                string path = $@"C:\UMIS\UMi Parts\80000\";

                var folderPath = System.IO.Path.GetDirectoryName(path);
                var folder = vault2.GetFolderFromPath(folderPath);
                IEdmPos5 FilePos = folder.GetFirstFilePosition();
                while (!FilePos.IsNull)
                {
                    IEdmFile5 file = folder.GetNextFile(FilePos);

                    if (file != null && file.Name.EndsWith(".slddrw", StringComparison.OrdinalIgnoreCase))
                    {
                        
                        dr = dt.NewRow();
                        dr["Part_Number"] = Path.GetFileNameWithoutExtension(file.Name);
                        dt.Rows.Add(dr);
                        

                    }
                }
                dt.DefaultView.Sort = "Part_Number" + " " + "ASC";
                dt = dt.DefaultView.ToTable();

                OrdersGridView.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // Change image to MultiFlow :: Complete
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                    axMBActX2.Block(1).IsLogoAspectRatioKeep = true;
                    //axMBActX2.Block(1).LogoWidth = 10.250; //in mm
                    DirectoryInfo directory = new DirectoryInfo(@"U:\Engineering\CUSTOMERLOGO\");
                    axMBActX2.Block(1).Text = "" + directory.ToString() + "Multiflow.MHL";
            }
            catch { MessageBox.Show("No Image Block (1) exists or is not editable"); }
        }

        // Changes if desc2 is marked :: Complete
        private void Desc2Box_CheckedChanged(object sender, EventArgs e)
        {
            try 
            {
                if (Desc2Box.Checked)
                {
                    axMBActX2.Block(6).IsMarkingEnable = true;
                }
                else
                {
                    axMBActX2.Block(6).IsMarkingEnable = false;
                }
            } catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show("No Desc2 Block (6) exists or is not editable");
            }

        }

        // Changes if heat number is marked :: Complete
        private void HeatCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (HeatCheckBox.Checked)
                {
                    axMBActX2.Block(7).IsMarkingEnable = true;
                }
                else
                {
                    axMBActX2.Block(7).IsMarkingEnable = false;
                }
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show("No Heat Number Block (7) exists or is not editable");
            }
        }

        // Changes QR marking & editability :: Complete
        private void QRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (QRCheckBox.Checked)
                {
                    QRCodeDataBox.ReadOnly = false;

                    axMBActX2.Block(8).IsMarkingEnable = true;

                    if (axMBActX2.Block(8).IsEditable)
                    {
                        axMBActX2.Block(8).Text = "umisolutions.ca";
                    }
                }
                else
                {
                    QRCodeDataBox.ReadOnly = true;
                    axMBActX2.Block(8).IsMarkingEnable = false;
                }
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show("No QR Block (8) exists or is not editable");
            }
        }

        // Change QR based on txt :: Complete
        private void QRCodeDataBox_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (axMBActX2.Block(8).IsEditable)
                {
                    axMBActX2.Block(8).Text = QRCodeDataBox.Text;
                }
            }
            catch
            {
                axMBActX2.Block(8).Text = "umisolutions.ca";
            }
        }

        // Saves the file :: Complete
        private void save_Click(object sender, EventArgs e)
        {

            string partNumAndRev = PartNumAndRevBox.Text.Trim();
            string FilePath = $@"\\UMISSERVER2\UMI\Engineering\LaserMarkingProfiles\{partNumAndRev}.MA2";

            try
            {

                if (System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);
                }
                axMBActX2.SaveJob($@"U:\Engineering\LaserMarkingProfiles\{partNumAndRev}.MA2");


            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }

        }

        // Opens Marker Builder plus into panel :: Complete
        private void btnOpenMarkerBuilder_MouseClick(object sender, MouseEventArgs e)
        {

            // Prompt the user with a confirmation message box
            var result = MessageBox.Show("Try and format your Label with the block editor. Are you sure you want to open the Marker Builder?",
                                           "Open Marker Builder",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Question);

            // Check the user's choice
            if (result == DialogResult.No)
            {
                return; // Exit the function if the user selects "Cancel"
            }

            // save the file (use normal save)... will guarentee file exists
            save_Click(sender, e);
            panel1.BringToFront();

            // open file in marker builder
            string filePath = $@"U:\Engineering\LaserMarkingProfiles\{PartNumAndRevBox.Text.Trim()}.MA2";
            string markingBuilderPath = @"C:\Program Files (x86)\KEYENCE\MarkingBuilderPlus_Ver2\MarkingBuilderPlus.exe";

            try
            {
                if (externalProcess != null && !externalProcess.HasExited)
                {
                    externalProcess.Kill(); // Forcefully close the application
                    externalProcess.Dispose(); // Release the process resources
                    externalProcess = null; // Clear the reference
                }
                externalProcess = Process.Start(markingBuilderPath, filePath);
                externalProcess.WaitForInputIdle();

                while (externalProcess.MainWindowHandle == IntPtr.Zero)
                {
                    Thread.Sleep(100);
                    externalProcess.Refresh();
                }

                // Set the parent of the external app window to panel1
                SetParent(externalProcess.MainWindowHandle, this.panel1.Handle);

                externalProcess.EnableRaisingEvents = true;

                externalProcess.Exited += ExternalProcess_Exited;

                panel1.Size = new System.Drawing.Size(1070, 800);
                panel1.Location = new System.Drawing.Point(456, 0);

                // Resize the external app window to fit the panel
                ResizeChildWindow(externalProcess.MainWindowHandle, panel1.Width, panel1.Height);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening the application: " + ex.Message);
            }
        }

        // Event handler for when the external process exits :: Complete
        private void ExternalProcess_Exited(object sender, EventArgs e)
        {
            // This will be called when the external app closes
            if (panel1.InvokeRequired)
            {
                // Use Invoke to update the UI from the non-UI thread
                panel1.Invoke(new Action(() => ResizePanel()));
            }
            else
            {
                ResizePanel();
            }
        }

        // Method to resize panel1 to 0,0 :: Complete
        private void ResizePanel()
        {
            panel1.Size = new System.Drawing.Size(0, 0);
            panel1.Location = new System.Drawing.Point(0, 0); // Optionally move it off-screen
        }

        // Resizes panel for extrenal app :: Complete
        private void ResizeChildWindow(IntPtr hWnd, int width, int height)
        {
            // Set the position and size of the child window
            SetWindowPos(hWnd, IntPtr.Zero, 0, 0, width, height, SWP_NOZORDER | SWP_NOACTIVATE);
        }

        // Calls OrdersGridView_Click to load in new (saved) file :: Complete
        private void btnRefreshTag_Click(object sender, EventArgs e)
        {
            OrdersGridView_Click(sender, e);
        }

        private void Get_Z_Click(object sender, EventArgs e)
        {
            axMBActX2.SaveControllerJob(0);
            axMBActX2.Operation.StartZTracking(0);

            double diffZ;
            double tiltX;
            double tiltY;
            int stab;
            int time;
            bool isWithinTolerance;
            bool result;
            axMBActX2.Operation.GetZTrackingResult(0, out diffZ, out tiltX, out tiltY, out stab, out time, out isWithinTolerance, out result);
            MessageBox.Show(" diffZ: " + diffZ + " tiltX: " + tiltX + " tiltY: " + tiltY + " stab: " + stab + " time: " + time + " isWithinTolerance: " + isWithinTolerance + " result: " + result);

        }

        private void EditingContextButton_Click(object sender, EventArgs e)
        {
            axMBActX2.Context = ContextTypes.CONTEXT_EDITING;
        }

        private void ControllerContextButton_Click(object sender, EventArgs e)
        {
            try
            {
                axMBActX2.Context = ContextTypes.CONTEXT_CONTROLLER;
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex);
            }

        }

        private void CameraFinderViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (axMBActX2.Operation.IsCameraFinderView == true)
                {
                    axMBActX2.Operation.IsCameraFinderView = false;

                }
                else
                {
                    axMBActX2.Operation.IsCameraFinderView = true;


                }
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }

        }

        private void ShowFileListButton_Click(object sender, EventArgs e)
        {
            int Count;
            string List = "";
            try
            {
                Count = axMBActX2.UpdateControllerFileCount(MBPLib2.ControllerFileTypes.CONTROLLERFILE_JOB);
                for (int index = 0; index < Count; index++)
                {
                    List = List + axMBActX2.ControllerFile(index).JobNo + ":" + axMBActX2.ControllerFile(index).FileName + "\n";
                }
                MessageBox.Show("controller Job file list is below...\n" + List);
            }
            catch (System.Runtime.InteropServices.COMException error)
            {



            }
        }

        private void ClearErrors_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                axMBActX2.Operation.ClearError();
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Errors_Btn_Click(object sender, EventArgs e)
        {
            int Count;
            string List = "";
            try
            {
                Count = axMBActX2.UpdateErrorCount();
                if (Count > 0)
                {
                    for (int index = 0; index < Count; index++)
                    {
                        List = List + axMBActX2.Error(index).ErrorCode + ":" + axMBActX2.Error(index).ErrorCaption + "\n";
                    }
                    MessageBox.Show("Occurring error list is below...\n" + List);
                }
                else
                {
                    MessageBox.Show("No Errors.");
                }

            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void OpenControllerJob_Click(object sender, EventArgs e)
        {
            try
            {
                if (axMBActX2.SendControllerJob
                (3, ""))
                {
                    MessageBox.Show("Sent ");
                }
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }

            try
            {
                axMBActX2.OpenControllerJob(3);
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void GetLengthsBtn_Click(object sender, EventArgs e) //used to checking all SW boms!!!!!!!!!!!!!!!!!!
        {
            using (SqlConnection cn = new SqlConnection(HHI_PUMIConnectionString))
            {
                try
                {
                    cn.Open();  // Open connection using the SQL connection string above
                    SqlCommand cmd2 = new SqlCommand("", cn);    //Declare text command for server connection
                    cmd2.CommandTimeout = 120; //set a long timeout in case of really complex queries 2019-04-30

                    cmd2.Parameters.AddWithValue("@Search", "%%");

                    cmd2.CommandText = "" +

                    //" DECLARE @temp TABLE (SO_Number VARCHAR(255),Order_Date VARCHAR(255),Promise_Date VARCHAR(255),Customer VARCHAR(255),Part_Number VARCHAR(255),Open_Qty VARCHAR(255),Price VARCHAR(255),Open_Amount VARCHAR(255))    " +
                    //" INSERT @temp EXEC CheckOpenOrders                                                                                                                                                                                  " +
                    //" SELECT Part_Number,Open_Qty,  Customer, Order_Date,Promise_Date, SO_Number  FROM @temp                                                                                                                                                                                                 " +
                    //" where Part_Number like '80%' and LEN(Part_Number) <9  and (SO_Number like '%' + @Search + '%' or Part_Number like '%' + @Search + '%')                                                                                                                                                                                   ";

                    "  SELECT TOP (10000) [PartNo] as Part_Number,c.Name as Customer,[CustomerPN] ,[CustomerRev],[Description],[TubeQty]             " +
                    " FROM TubeAssemblies ta                                                                             " +
                    "left join Customers c on c.id = Customer_id                                                          " +
                    " where PartNo like '8%'                                             " +
                    " order by PartNo desc                                                                                ";
                    DataTable dt = new DataTable();
                    dt.Load(cmd2.ExecuteReader());

                    OrdersGridView.DataSource = dt;


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting open orders" + ex);
                }
            }
            runlengthCheck();
        }

        // Called from GetLengthsBtn_Click
        private void runlengthCheck() //used to checking all SW boms!!!!!!!!!!!!!!!!!!
        {

            string SelectedPN = "";
            string SelectedRev = "";
            string SelectedCustomerPN = "";
            double len;
            string partnum;
            string mtl;

            string printout = "";
            foreach (DataGridViewRow rowwwww in OrdersGridView.Rows)
            {
                SelectedPN = rowwwww.Cells["Part_Number"].Value.ToString();
                SelectedCustomerPN = rowwwww.Cells["Customer"].Value.ToString();

                string PNSub = SelectedPN.Substring(0, 5);
                string OrderRev = " ";
                if (SelectedPN.Length > 5)
                {
                    OrderRev = SelectedPN.Substring(6, SelectedPN.Length - 6);
                }


                GetTubeLengthFromSWDrawing(PNSub, out len, out partnum, out mtl);
                printout += PNSub + "\t" + len + "\n";
                Debug.WriteLine(PNSub + "\t" + len + "\n");
            }

            MessageBox.Show("" + printout);



        }

        private void GetTubeLengthFromSWDrawing(string pNSub, out double length, out string partNumber, out string mtl)//used to checking all SW boms!!!!!!!!!!!!!!!!!!
        {
            length = 99999.99;
            partNumber = "";
            mtl = "";
            //GetFileFromSearch*******************
            try
            {

                IEdmVault7 vault2 = null;
                if (vault1 == null)
                {
                    vault1 = new EdmVault5();
                }

                vault2 = (IEdmVault7)vault1;
                if (!vault1.IsLoggedIn)
                {
                    vault1.LoginAuto("UMIS", this.Handle.ToInt32());
                }


                aFile = (IEdmFile7)vault1.GetFileFromPath($@"C:\UMIS\UMi Parts\80000\{pNSub}.slddrw", out ppoRetParentFolder);



            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                MessageBox.Show("Error selecting drawing given PN: " + pNSub + " From 80000 folder\n." + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting drawing given PN: " + pNSub + " From 80000 folder.\n" + ex.Message);
            }

            //GetBOMFromFILE*******************
            try
            {
                IEdmVault7 vault2 = null;
                if (vault1 == null)
                {
                    vault1 = new EdmVault5();
                }
                vault2 = (IEdmVault9)vault1;
                if (!vault1.IsLoggedIn)
                {
                    vault1.LoginAuto("UMIS", this.Handle.ToInt32());
                }

                if (aFile != null)
                {

                    try
                    {
                        EdmBomInfo[] derivedBOMs;

                        aFile.GetDerivedBOMs(out derivedBOMs);

                        if (derivedBOMs != null)
                        {
                            bom = (IEdmBom)vault2.GetObject(EdmObjectType.EdmObject_BOM, derivedBOMs[(derivedBOMs.Length - 1)].mlBomID); // USE ID OF DERIVED BOM at end of the list. I assume it is the latest date, most recent.
                            bomView = (IEdmBomView3)(IEdmBomView2)bom.GetView(0);


                            EdmBomColumn[] BomColumns = null;
                            bomView.GetColumns(out BomColumns);

                            object[] BomRows = null;
                            bomView.GetRows(out BomRows);

                            object cellVar = null;
                            object cellVar2 = null;
                            object ComputedValue = null;
                            string config = null;
                            bool ReadOnlyOut = false;
                            List<string> TubesFound = new List<string>();

                            //search all cells inluding item column (different column type)
                            int qtyCol = 9;
                            int uomCol;

                            foreach (IEdmBomCell CELL in BomRows)
                            {
                                //MessageBox.Show(CELL.GetPathName());
                                int ColumnCount = 0;

                                if (CELL.GetPathName().Contains("UMi Parts") == true)
                                {
                                    //MessageBox.Show("Yaya"+ CELL.GetPathName());
                                    foreach (EdmBomColumn COLUMN in BomColumns)
                                    {
                                        if (ColumnCount <= (BomColumns.Count() - 1)) // in case BOM has more columns than expeted
                                        {

                                            if (COLUMN.mbsCaption == "QTY")
                                            {
                                                qtyCol = ColumnCount;
                                            }
                                            if (COLUMN.mbsCaption == "UOM")
                                            {
                                                uomCol = ColumnCount;
                                            }
                                            //MessageBox.Show("" + COLUMN.mbsCaption);
                                            if (COLUMN.mbsCaption == "UOM")
                                            {
                                                CELL.GetVar(BomColumns[ColumnCount].mlVariableID, BomColumns[ColumnCount].meType, out cellVar, out ComputedValue, out config, out ReadOnlyOut);
                                                if (cellVar.ToString().Contains("FT") == true)
                                                {
                                                    CELL.GetVar(BomColumns[qtyCol].mlVariableID, BomColumns[qtyCol].meType, out cellVar2, out ComputedValue, out config, out ReadOnlyOut);

                                                    //MessageBox.Show(""+ cellVar2.ToString());
                                                    length = Convert.ToDouble(cellVar2);

                                                }
                                            }
                                            ColumnCount += 1;



                                        }
                                    }
                                }

                            }
                        }

                    }
                    catch
                    {

                    }





                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                //MessageBox.Show("Error looking up BOM/ determining the diameter and wall thickness.\n\n" + ex.Message);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error looking up BOM/ determining the diameter and wall thickness.\n\n" + ex.Message);
            }



        }

        // Changes the block for changes to be applied to
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1){
            string selectedItem = comboBox1.SelectedItem.ToString();
            Control[] controlsToDisable = { wp1, wp10, wm1, wm10, hp1, hp10, hm1, hm10, MinusTen, MinusOne, PlusTen, PlusOne, widthBox, heightBox };

            if (selectedItem == null || selectedItem.Length == 0)
            {
                foreach (var control in controlsToDisable)
                {
                    control.Enabled = false;
                }
            } else
            {
                foreach (var control in controlsToDisable)
                {
                    control.Enabled = true;
                }
            }

            var match = System.Text.RegularExpressions.Regex.Match(selectedItem, @"\d+");

            blockNo = int.Parse(match.Value); // Convert the extracted number to int

                if (comboBox1.SelectedIndex != -1)
                {
                    try
                    {
                        widthBox.Text = axMBActX2.Block(blockNo).CharWidth.ToString();
                        heightBox.Text = axMBActX2.Block(blockNo).CharHeight.ToString();
                    }
                    catch
                    {
                        try
                        {
                            widthBox.Text = axMBActX2.Block(blockNo).LogoWidth.ToString();
                            heightBox.Text = axMBActX2.Block(blockNo).LogoHeight.ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }

                }
            } else
            {
                Control[] controlsToDisable = { wp1, wp10, wm1, wm10, hp1, hp10, hm1, hm10, MinusTen, MinusOne, PlusTen, PlusOne, widthBox, heightBox };
                foreach (var control in controlsToDisable)
                {
                    control.Enabled = false;
                }
            }
        }

        private void widthBox_TextChanged(object sender, EventArgs e)
        {
            double charWidth = 0;
            if (comboBox1.SelectedItem.ToString() != "Block 1" && comboBox1.SelectedItem.ToString() != "Block 0")
            {
                try
                {
                    if (double.TryParse(widthBox.Text, out charWidth))
                    {
                        axMBActX2.Block(blockNo).CharWidth = charWidth;
                    }
                    else
                    {
                        MessageBox.Show("Invalid input. Please enter a valid number for width.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            } else
            {
                try
                {
                    if (double.TryParse(widthBox.Text, out charWidth))
                    {
                        axMBActX2.Block(blockNo).LogoWidth = charWidth;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void heightBox_TextChanged(object sender, EventArgs e)
        {
            double charHeight = 0;
            if (comboBox1.SelectedItem.ToString() != "Block 1")
            {
                try
                {
                    if (double.TryParse(heightBox.Text, out charHeight))
                    {
                        axMBActX2.Block(blockNo).CharHeight = charHeight;
                    }
                    else
                    {
                        MessageBox.Show("Invalid input. Please enter a valid number for height.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                try
                {
                    if (double.TryParse(heightBox.Text, out charHeight))
                    {
                        axMBActX2.Block(blockNo).LogoHeight = charHeight;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void wp10_Click(object sender, EventArgs e)
        {
            double temp;
            double.TryParse(widthBox.Text, out temp);
            temp *= 1.1;
            widthBox.Text = temp.ToString();
        }

        private void wp1_Click(object sender, EventArgs e)
        {
            double temp;
            double.TryParse(widthBox.Text, out temp);
            temp *= 1.01;
            widthBox.Text = temp.ToString();
        }

        private void wm10_Click(object sender, EventArgs e)
        {
            double temp;
            double.TryParse(widthBox.Text, out temp);
            temp *= 0.9;
            widthBox.Text = temp.ToString();
        }

        private void wm1_Click(object sender, EventArgs e)
        {
            double temp;
            double.TryParse(widthBox.Text, out temp);
            temp *= 0.99;
            widthBox.Text = temp.ToString();
        }

        private void hp10_Click(object sender, EventArgs e)
        {
            double temp;
            double.TryParse(heightBox.Text, out temp);
            temp *= 1.1;
            heightBox.Text = temp.ToString();
        }

        private void hp1_Click(object sender, EventArgs e)
        {
            double temp;
            double.TryParse(heightBox.Text, out temp);
            temp *= 1.01;
            heightBox.Text = temp.ToString();
        }

        private void hm10_Click(object sender, EventArgs e)
        {
            double temp;
            double.TryParse(heightBox.Text, out temp);
            temp *= 0.9;
            heightBox.Text = temp.ToString();
        }

        private void hm1_Click(object sender, EventArgs e)
        {
            double temp;
            double.TryParse(heightBox.Text, out temp);
            temp *= 0.99;
            heightBox.Text = temp.ToString();
        }

        private void PlusTen_Click(object sender, EventArgs e)
        {
            try
            {
                axMBActX2.Block(blockNo).X += 1;
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void PlusOne_Click(object sender, EventArgs e)
        {
            try
            {
                axMBActX2.Block(blockNo).X += 0.1;
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void MinusTen_Click(object sender, EventArgs e)
        {
            try
            {
                axMBActX2.Block(blockNo).X -= 1;
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void MinusOne_Click(object sender, EventArgs e)
        {
            try
            {
                axMBActX2.Block(blockNo).X -= 0.1;
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Desc1Box_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Desc1Box.Checked)
                {
                    axMBActX2.Block(5).IsMarkingEnable = true;
                }
                else
                {
                    axMBActX2.Block(5).IsMarkingEnable = false;
                }
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show("No Desc 1 Block (5) exists or is not editable");
            }
        }

        private void PN2Box_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (PN2Box.Checked)
                {
                    axMBActX2.Block(4).IsMarkingEnable = true;
                }
                else
                {
                    axMBActX2.Block(4).IsMarkingEnable = false;
                }
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show("No Secondary PN Block (4) exists or is not editable");
            }
        }

        private void PN1Box_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (PN1Box.Checked)
                {
                    axMBActX2.Block(3).IsMarkingEnable = true;
                }
                else
                {
                    axMBActX2.Block(3).IsMarkingEnable = false;
                }
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show("No Primary PN Block (3) exists or is not editable");
            }
        }

        private void DateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (DateCheckBox.Checked)
                {
                    axMBActX2.Block(2).IsMarkingEnable = true;
                }
                else
                {
                    axMBActX2.Block(2).IsMarkingEnable = false;
                }
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show("No Date Block (2) exists or is not editable");
            }
        }

        private void LabelBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (LabelBox.Checked)
                {
                    axMBActX2.Block(0).IsMarkingEnable = true;
                }
                else
                {
                    axMBActX2.Block(0).IsMarkingEnable = false;
                }
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show("No Background Block (0) exists or is not editable");
            }
        }

        private void ImageBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ImageBox.Checked)
                {
                    axMBActX2.Block(1).IsMarkingEnable = true;
                }
                else
                {
                    axMBActX2.Block(1).IsMarkingEnable = false;
                }
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show("No Image Block (1) exists or is not editable");
            }
        }

        private void LoadFileNames()
        {
            string directoryPath = @"U:\Engineering\CUSTOMERLOGO";

            if (Directory.Exists(directoryPath))
            {
                var files = Directory.GetFiles(directoryPath, "*.MHL")
                                     .Select(Path.GetFileNameWithoutExtension) // Get file names without extension
                                     .Select(fileName =>
                                     {
                                         // Check if the name is all numbers
                                         if (fileName.All(char.IsDigit))
                                         {
                                             string customerName = GetCustomerName(fileName);
                                             return $"{fileName} - {customerName}";
                                         }

                                         // Return the original file name if not all numbers
                                         return fileName;
                                     });

                LogoComboBox.Items.Clear();
                LogoComboBox.Items.AddRange(files.ToArray());
            }
            else
            {
                MessageBox.Show("Error getting logos");
            }
        }

        private string GetCustomerName(string custID)
        {
            string customerName = string.Empty;

            using (SqlConnection cn = new SqlConnection(HHI_PUMIConnectionString))
            {
                try
                {
                    cn.Open();
                    string sql = @"SELECT TOP (1) Name FROM Customers WHERE id LIKE @CustID";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@CustID", custID);

                        // Execute the command and get the customer name
                        customerName = cmd.ExecuteScalar() as string; // Get single result
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            return customerName ?? "Unknown Customer"; // Default value if no name found
        }




        private void LogoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LogoComboBox.SelectedIndex != -1){
                try
                {
                    string selectedFileName = GetOriginalFileName(LogoComboBox.SelectedItem.ToString());
                    axMBActX2.Block(1).IsLogoAspectRatioKeep = true;
                    DirectoryInfo directory = new DirectoryInfo(@"U:\Engineering\CUSTOMERLOGO\");
                    axMBActX2.Block(1).Text = "" + directory.ToString() + selectedFileName;
                }
                catch { MessageBox.Show("No Image Block (1) exists or is not editable"); }
            }
        }

        private string GetOriginalFileName(string modifiedFileName)
        {
            // Check if the modified file name contains " - "
            if (modifiedFileName.Contains(" - "))
            {
                // Split the string to get the part before " - "
                string originalName = modifiedFileName.Split(new[] { " - " }, StringSplitOptions.None)[0];

                // Add back the .MHL extension
                return originalName + ".MHL";
            }

            // If no appended name, return the modified name with .MHL
            return modifiedFileName + ".MHL";
        }

        private void ProgramMaterialCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check that ProgramMaterialCombo is selected
            if (ProgramSizeCombo.SelectedIndex != -1)
            {
                OpenGenericProgram();
                UpdateCurrentProgramBlocks(0);
            }
        }

        private void ProgramSizeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check that ProgramSizeCombo is selected
            if (ProgramMaterialCombo.SelectedIndex != -1)
            {
                OpenGenericProgram();
                UpdateCurrentProgramBlocks(0);
            }
        }

        private void RemoveComboBoxHandlers()
        {
            ProgramMaterialCombo.SelectedIndexChanged -= ProgramMaterialCombo_SelectedIndexChanged;
            ProgramSizeCombo.SelectedIndexChanged -= ProgramSizeCombo_SelectedIndexChanged;
        }

        private void RestoreComboBoxHandlers()
        {
            ProgramMaterialCombo.SelectedIndexChanged += ProgramMaterialCombo_SelectedIndexChanged;
            ProgramSizeCombo.SelectedIndexChanged += ProgramSizeCombo_SelectedIndexChanged;
        }

        private void UpdateCurrentProgramRecursive(string PN, int rev)
        {
            string FilePath = $@"\\UMISSERVER2\UMI\Engineering\LaserMarkingProfiles\{PN}.MA2";
            if (System.IO.File.Exists(FilePath))
            {
                try
                {
                    if (axMBActX2.OpenJob(FilePath))
                    {
                        
                        axMBActX2.Context = ContextTypes.CONTEXT_EDITING;
                        axMBActX2.OpenJob(FilePath);
                        try
                        {
                            
                            try
                            {
                                string disabled = axMBActX2.Block(16).Text; //New disabled "DO NOT MODIFY" block
                                if (disabled == "DO NOT MODIFY")
                                {
                                    save.Enabled = false; // Disable the button
                                    btnOpenMarkerBuilder.Enabled = false;
                                }
                            }
                            catch (System.Runtime.InteropServices.COMException error)
                            {
                            }
                            try
                            {
                                if (axMBActX2.Block(8).IsMarkingEnable)
                                {
                                    QRCheckBox.Checked = true;
                                }
                                else
                                {
                                    QRCheckBox.Checked = false;
                                }
                            }
                            catch { }
                            UpdateCurrentProgramBlocks(0);

                        }
                        catch (System.Runtime.InteropServices.COMException error)
                        {
                            MessageBox.Show(error.Message + error);
                        }
                    }
                }
                catch (System.Runtime.InteropServices.COMException error)
                {
                    MessageBox.Show(error.Message + error);
                }
                var parts = PN.Split('_');
                string selectedPN = parts[0];
                FillTubeDetails(selectedPN, rev.ToString());
                MessageBox.Show("Custom program found for old revision " + PN);
                GenericProgram = false;
            }
            else
            {
                if (PN.Contains("_"))
                {
                    var parts = PN.Split('_');
                    string selectedPN = parts[0];
                    int orderRev;
                    if (parts.Length > 1 && int.TryParse(parts[1], out orderRev))
                    {
                        orderRev--;
                        if (orderRev >= 0)
                        {
                            UpdateCurrentProgramRecursive(selectedPN + "_" + orderRev, rev);
                        }
                        else
                        {
                            GenericProgram = true;
                        }
                    }
                    else
                    {
                        GenericProgram = true;
                    }
                }
            }
        }

        private void btnLoadResults_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(HHI_PUMIConnectionString))
            {
                try
                {
                    cn.Open();  // Open connection using the SQL connection string above
                    SqlCommand cmd = new SqlCommand("", cn);    //Declare text command for server connection

                    cmd.CommandText = "SELECT id, TubeID, FittingPN, TubeEndStyleID, " +
                    "ConePN, PowerLevel, Duration, Height, MeasuredJointClearance, " +
                    "BrazeRings, Comments, DateEntered " +
                    "FROM BrazeParameters";


                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());

                    dgvBrazeParameters.DataSource = dt;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting open orders" + ex);
                }
            }
        }

        // Adds autocomplete data for the tube part number
        private void SetCustomSourcesForTubeParts()
        {
            txtTubePN.AutoCompleteCustomSource.Clear();
            

            AutoCompleteStringCollection acTn = new AutoCompleteStringCollection();

            using (SqlConnection cnex = new SqlConnection(HHI_PUMIConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT(PartNo) FROM Tubes", cnex);
                try
                {
                    cnex.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                
                        acTn.Add(reader["PartNo"].ToString());
                        Console.WriteLine(reader["PartNo"].ToString());

                    }
                }

                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            txtTubePN.AutoCompleteCustomSource = acTn;

        }

        // Adds a braze entry to the database
        private void btnAddBrazeEntry_Click(object sender, EventArgs e)
        {
            if (!BrazeEntryValidationChecks())
            {
                return;
            }
            else
            {
                Console.WriteLine("We will do somthing here");
            }
            
        }

        // Performs validation checks on the data input for braze entry before trying to enter it
        private bool BrazeEntryValidationChecks()
        {
            string tubePN = txtTubePN.Text.ToString();
            if (!CheckTubePNExists(tubePN))
            {
                MessageBox.Show($"A tube with PN {tubePN} does not exist in the Tubes database. It will need to be entered first. \n" +
                    $"A tool to enter a tube will be available soon. For now contact softwaredev@umis.ca", "Error: No Tube Exists",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // TODO: A check here to see if the end style is in our DB - should also do the same auto fill thing as the tube PN

            int test = -1;
            if (!int.TryParse(txtPowerLevel.Text.ToString(), out test))
            {
                MessageBox.Show($"Power Level is not a valid integer. Please enter a valid integer.", "Power Level Invalid",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } 
            

            test = -1;
            if(!int.TryParse(txtDuration.Text.ToString(), out test))
            {
                MessageBox.Show($"Duration is not a valid integer. Please enter a valid integer.", "Duration Invalid",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            

            double testDouble = -1;
            if (!double.TryParse(txtHeight.Text.ToString(), out testDouble))
            {
                MessageBox.Show($"Height is not a valid number. Please enter a valid number.", "Height Invalid",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
           

            testDouble = -1;
            if (!double.TryParse(txtJointClearance.Text.ToString(), out testDouble))
            {
                MessageBox.Show($"Joint Clearance is not a valid number. Please enter a valid number.", "Joint Clearance Invalid",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            

            testDouble = -1;
            if (!double.TryParse(txtBrazeRings.Text.ToString(), out testDouble))
            {
                MessageBox.Show($"Braze Rings is not a valid number. Please enter a valid number.", "Braze Rings Invalid",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool CheckTubePNExists(string tubePN)
        {
            using (SqlConnection cnex = new SqlConnection(HHI_PUMIConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT id FROM Tubes WHERE PartNo = @PartNo", cnex);
                cmd.Parameters.AddWithValue("@PartNo", tubePN); // Set to current date and time

                try
                {
                    cnex.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {

                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }

                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return false;
            }

        }

        /* IMPLEMENT THIS LATER ON
        private bool CheckEndStyleExists(string endStyle)
        {
            using (SqlConnection cnex = new SqlConnection(HHI_PUMIConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT id FROM Tubes WHERE PartNo = @PartNo", cnex);
                cmd.Parameters.AddWithValue("@PartNo", tubePN); // Set to current date and time

                try
                {
                    cnex.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {

                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }

                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return false;
            }

        }
        */
    }
}


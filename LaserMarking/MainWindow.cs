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

            if (System.Diagnostics.Debugger.IsAttached)
            {
                SQLTest = 1;
            }
            else
            {
                SQLTest = 0;
            }

            //Set DB Conn strings to test/prod depending on environment
            SetDBConnections();

        }

        // Fills Material & Size Combo box on init :: Complete
        private void loadMaterialComboBox()
        {
            ProgramMaterialCombo.Items.Add("SS");
            ProgramMaterialCombo.Items.Add("CS");

            ProgramSizeCombo.Items.Add("06");
            ProgramSizeCombo.Items.Add("08");
            ProgramSizeCombo.Items.Add("10");
            ProgramSizeCombo.Items.Add("12");
            ProgramSizeCombo.Items.Add("16");
            ProgramSizeCombo.Items.Add("20");
            ProgramSizeCombo.Items.Add("24");
            ProgramSizeCombo.Items.Add("28");
            ProgramSizeCombo.Items.Add("32");
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
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
               
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

            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }

            sendMarkedToDB();

        }

        // sends info about marked part to DB :: Complete
        private void sendMarkedToDB()
        {
            try { isConnected = axMBActX2.Comm.Online(); }
            catch { }
            using (SqlConnection cn = new SqlConnection(HHI_PUMIConnectionString))
            {
                try
                {
                    cn.Open(); 

                    // Prepare the SQL command with parameters
                    string sql = @"
            INSERT INTO LaserMarkings (DateTimeMarked, PartNum, ProductionNumber, HeatNumber, IsConnected)
            VALUES (@DateTimeMarked, @PartNum, @ProductionNumber, @HeatNumber, @IsConnected)";

                    using (SqlCommand cmd2 = new SqlCommand(sql, cn))
                    {
                        cmd2.CommandTimeout = 120; // Set a long timeout in case of really complex queries

                        DataGridViewRow row = this.OrdersGridView.SelectedRows[0];
                        string ProductionNumber = "";
                        try { ProductionNumber = row.Cells["ProductionNumber"].Value.ToString(); }
                        catch { }

                        // Add parameters to the command
                        cmd2.Parameters.AddWithValue("@DateTimeMarked", DateTime.Now); // Set to current date and time
                        if (!partNumsFliped)
                        {
                            cmd2.Parameters.AddWithValue("@PartNum", PartNumAndRevBox.Text);
                        } else
                        {
                            cmd2.Parameters.AddWithValue("@PartNum", CustPartNumAndRevBox.Text);
                        }
                        cmd2.Parameters.AddWithValue("@ProductionNumber", ProductionNumber); // Replace with actual production order
                        cmd2.Parameters.AddWithValue("@HeatNumber", HeatBox.Text);
                        cmd2.Parameters.AddWithValue("@IsConnected", isConnected); // Replace with actual test flag value

                        // Execute the command
                        cmd2.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error)
                    Console.WriteLine($"Error: {ex.Message}");
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
            save.Enabled = true;
            Mark_Part.BackColor = SystemColors.ControlLight ;
            string SelectedPN = "";
            double diam;
            double wall;
            string partnum;
            string mtl;
            bool fileFound;
            string PNSub = " ";
            string orderRev = "0";
  
            DateTime thisDay = DateTime.Today;
            string customdate = thisDay.ToString("yyyy-MM-dd");

            DateBox.Text = customdate;

            if (OrdersGridView.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.OrdersGridView.SelectedRows[0];
                SelectedPN = row.Cells["Part_Number"].Value.ToString();
            }
           
            if (SelectedPN.Contains("_"))
            {
                PNSub = SelectedPN.Split('_')[0];
                orderRev = SelectedPN.Split('_')[1];
            }

            // SelectedPN: PN as shown in table (may have rev)
            // PNSub: PN
            // orderRev: rev

            
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

            // P case (what means)
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
                            CheckForCustomProgram(selectedPN + "_" + orderRev);
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
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Drawing not found for "+pNSub+" from "+folderName+" folder. Please create before continuing." );
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
                            MessageBox.Show("No Tube Part Numbers Found");
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
                    MessageBox.Show("Error looking up BOM/ determining the diameter and wall thickness.\n\n" + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error looking up BOM/ determining the diameter and wall thickness.\n\n" + ex.Message);
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
                if (diameter == 2.000)
                {
                    ProgramSizeCombo.SelectedItem = "32";
                    
                }else if (diameter == 1.500)
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
                }else if (diameter == 0.500)
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
                if (diameter == 2.000)
                {
                    ProgramSizeCombo.SelectedItem = "32";

                }
                else if (diameter == 1.500)
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
            try { if (axMBActX2.Block(2).IsEditable) { axMBActX2.Block(2).Text = DateBox.Text; } }
            catch { axMBActX2.Block(2).Text = " "; }
            try { if (axMBActX2.Block(3).IsEditable) { axMBActX2.Block(3).Text = PartNumAndRevBox.Text; } }
            catch { axMBActX2.Block(3).Text = " "; }
            try { if (axMBActX2.Block(4).IsEditable) { axMBActX2.Block(4).Text = CustPartNumAndRevBox.Text; } }
            catch { axMBActX2.Block(4).Text = " "; }
            try { if (axMBActX2.Block(5).IsEditable) { axMBActX2.Block(5).Text = DescLine1Box.Text; } }
            catch { axMBActX2.Block(5).Text = " "; }
            try { if (axMBActX2.Block(6).IsEditable) { axMBActX2.Block(6).Text = DescLine2Box.Text; } }
            catch { axMBActX2.Block(6).Text = " "; }
            try { if (axMBActX2.Block(7).IsEditable) { axMBActX2.Block(7).Text = HeatBox.Text; } }
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
            catch { MessageBox.Show("No Image Block (2) exists or is not editable"); }
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

            string FilePath = $@"\\UMISSERVER2\UMI\Engineering\LaserMarkingProfiles\{PartNumAndRevBox}.MA2";

            try
            {

                if (System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);

                    axMBActX2.SaveJob($@"U:\Engineering\LaserMarkingProfiles\" + PartNumAndRevBox.Text + ".MA2");
                }
                else
                {

                    axMBActX2.SaveJob($@"U:\Engineering\LaserMarkingProfiles\" + PartNumAndRevBox.Text + ".MA2");
                }


            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }

        }

        // Opens Marker Builder plus into panel :: Complete
        private void btnOpenMarkerBuilder_MouseClick(object sender, MouseEventArgs e)
        {
            // save the file (use normal save)... will guarentee file exists
            save_Click(sender, e);

            // open file in marker builder
            string filePath = $@"U:\Engineering\LaserMarkingProfiles\{PartNumAndRevBox.Text.Trim()}.MA2";
            string markingBuilderPath = @"C:\Program Files (x86)\KEYENCE\MarkingBuilderPlus_Ver2\MarkingBuilderPlus.exe";

            try
            {
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

    }
}


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
        IEdmFolder5 aFolder;
        IEdmPos5 aPos;
        IEdmCard6 aCard;
        IEdmCardControl7 aControl;
        IEdmFolder5 ppoRetParentFolder;
        private IEdmBom bom;
        private IEdmBomMgr2 bomMgr;
        private IEdmBomView3 bomView;
        string fileExt;


        bool GenericProgram = true;

        public MainWindow()
        {
            InitializeComponent();

            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

            InitializeMarker();

            USBConnection();

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

            SelectedBlockCombo.Items.Add("Date");
            SelectedBlockCombo.Items.Add("Logo");
            SelectedBlockCombo.Items.Add("Part No.");
            SelectedBlockCombo.Items.Add("Cust PN");
            SelectedBlockCombo.Items.Add("Desc 1");
            SelectedBlockCombo.Items.Add("Desc 2");
            SelectedBlockCombo.Items.Add("Box Size");
            SelectedBlockCombo.Items.Add("Box Position");

        }

        private void OnApplicationExit(object sender, EventArgs e)
        {

        }


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

        private void USBConnection()
        {
            //int ControllerSerialNo = 12345;
            //try
            //{
            //    //If the application is exited without disconnected, the instance may exclusively use the online connection. In this case, release the online connection.
            //if (axMBActX1.Comm.IsOnline)
            //    {
            //        axMBActX1.Comm.Offline();
            //    }
            //    axMBActX1.Comm.ConnectionType = MBPLib2.ConnectionTypes.CONNECTION_USB;
            //    axMBActX1.Comm.UsbControllerSerialNo = ControllerSerialNo;
            //    bool is_success = axMBActX1.Comm.Online();
            //}
            //catch (System.Runtime.InteropServices.COMException error)
            //{
            //    MessageBox.Show(error.Message);
            //}




        }

        public static string OpenConnect(string connStr)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connStr))
                {
                    cn.Open();
                    //MessageBox.Show(connStr);
                    return connStr;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in OpenConnect() Cant open with connection string.\n" + ex.Message);
                return connStr;
            }
        }

        private void updateOpenOrderItems()
        {
            using (SqlConnection cn = new SqlConnection(OpenConnect(SAPConnectionString)))
            {
                try
                {
                    cn.Open();  // Open connection using the SQL connection string above
                    SqlCommand cmd2 = new SqlCommand("", cn);    //Declare text command for server connection
                    cmd2.CommandTimeout = 120; //set a long timeout in case of really complex queries 2019-04-30

                    cmd2.Parameters.AddWithValue("@Search", "%%");
                    /*
                    // Code used before
                    cmd2.CommandText = "" +

                    //" DECLARE @temp TABLE (SO_Number VARCHAR(255),Order_Date VARCHAR(255),Promise_Date VARCHAR(255),Customer VARCHAR(255),Part_Number VARCHAR(255),Open_Qty VARCHAR(255),Price VARCHAR(255),Open_Amount VARCHAR(255))    " +
                    //" INSERT @temp EXEC CheckOpenOrders                                                                                                                                                                                  " +
                    //" SELECT Part_Number,Open_Qty,  Customer, Order_Date,Promise_Date, SO_Number  FROM @temp                                                                                                                                                                                                 " +
                    //" where Part_Number like '80%' and LEN(Part_Number) <9  and (SO_Number like '%' + @Search + '%' or Part_Number like '%' + @Search + '%')                                                                                                                                                                                   ";

                    

                    " DECLARE @temp TABLE (SO_Number VARCHAR(255),Order_Date VARCHAR(255),Promise_Date VARCHAR(255),Customer VARCHAR(255),Part_Number VARCHAR(255),Open_Qty VARCHAR(255),Price VARCHAR(255),Open_Amount VARCHAR(255))    " +
                    " INSERT @temp EXEC CheckOpenOrders                                                                                                                                                                                  " +
                    " SELECT Part_Number,Open_Qty,  Customer, Order_Date,Promise_Date, SO_Number  FROM @temp                                                                                                                                                                                                 " +
                    " where Part_Number like '8%' and LEN(Part_Number) <9  and (SO_Number like '%' + @Search + '%' or Part_Number like '%' + @Search + '%')                                                                                                                                                                                   ";
                    */
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
        private void OrdersGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = OrdersGridView.Columns[e.ColumnIndex].Name;
            DataTable dataTable = OrdersGridView.DataSource as DataTable;

            if (dataTable != null)
            {
                DataView dataView = dataTable.DefaultView;
                dataView.Sort = $"{columnName} ASC";
                OrdersGridView.DataSource = dataView.ToTable();
            }
        }

        private void GetOrderTubePNBTN_Click(object sender, EventArgs e)
        {
            OrdersGridView.Columns.Add("Material", "Material");
            OrdersGridView.Columns["Material"].DisplayIndex = 1; ;

            foreach (DataGridViewRow row in OrdersGridView.Rows)
            {
                //MessageBox.Show(""+  (row.Cells["Part_Number"].Value.ToString()).Substring(0,5) );
                try {
                    row.Cells["Material"].Value = SimplyGetTubeMaterialFromPN((row.Cells["Part_Number"].Value.ToString()).Substring(0, 5));
                }
                catch {}
                
            }
        }

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

                    bomMgr = (IEdmBomMgr2)(IEdmBomMgr)vault2.CreateUtility(EdmUtility.EdmUtil_BomMgr);
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

                        //foreach (EdmBomColumn COLUMN in BomColumns)
                        //{
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
                                //ColumnCount += 1;
                            }
                        //}
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

        //Sets connection strings based on whether we are debugging or not
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
                
                axMBActX2.Comm.ConnectionType =
                MBPLib2.ConnectionTypes.CONNECTION_USB;
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

        private void markerConnectButton_Click(object sender, EventArgs e)
        {
            AttemptToConnectToLaser();
        }

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

            //try
            //{
            //    //If the application is exited without disconnected, the
            //    //instance may exclusively use the online connection.In this
            //    //case, release the online connection.
            //    if (axMBActX1.Comm.IsOnline)
            //    {
            //        axMBActX1.Comm.Offline();
            //    }
                
            //}
            //catch (System.Runtime.InteropServices.COMException error)
            //{
            //    MessageBox.Show(error.Message);
            //}
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            updateOpenOrderItems();
        }


        private void Map_Surface_Click(object sender, EventArgs e)
        {
            axMBActX2.Operation.StartXYTracking(0);

            double diffX;
            double diffY;
            double angle;
            int corr;
            int time;
            bool result;
            axMBActX2.Operation.GetXYTrackingResult(0, out diffX, out diffY, out angle, out corr, out time, out result);
            MessageBox.Show(" diffX: " + diffX + " diffY: " + diffY + " angle: " + angle + " corr: " + corr + " time: " + time + " result: " + result);
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


            //axMBActX1.IsBlockingCommunication = false;
            //axMBActX2.IsBlockingCommunication = true;
            

            try
            {

                axMBActX2.SaveControllerJob(0);
                
                //int JobNo;
                //JobNo = axMBActX2.Operation.GetCurrentJobNo();

                axMBActX2.IsBlockingCommunication = true;

                axMBActX2.Operation.SetCurrentJobNo(0);

                axMBActX2.Operation.StartMarking();


                //MessageBox.Show("Marking Done");

                //MessageBox.Show("JOb Number "+JobNo);



                //try
                //{
                //    axMBActX1.Operation.StartGuideLaserMarking(MBPLib2.GuideLaserTypes.GUIDELASER_BLOCKFRAME);
                //}
                //catch (System.Runtime.InteropServices.COMException error)
                //{
                //    MessageBox.Show(error.Message);
                //}


                //Get_Z_Click performs the same operation 
                //get result
                double diffZ;
                double tiltX;
                double tiltY;
                int stab;
                int time;
                bool isWithinTolerance;
                bool result;
                bool result2;
                double time2;

                //axMBActX2.Operation.GetZTrackingResult(0, out diffZ, out tiltX, out tiltY, out stab, out time, out isWithinTolerance, out result);
                axMBActX2.Operation.GetMarkingResult(out result2,out time2);
                //MessageBox.Show(" diffZ: " + diffZ + " tiltX: " + tiltX + " tiltY: " + tiltY + " stab: " + stab + " time: " + time + " isWithinTolerance: " + isWithinTolerance + " result: " + result + " result2: " + result2 + " time2: " + time2);


                
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }

            

            // axMBActX1.Operation.StartZTracking(0);

        }
        
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


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                axMBActX2.Operation.StartCameraScanningMode2(ShutterStateTypes.SHUTTERSTATE_OPEN);
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                axMBActX2.Operation.FinishCameraScanningMode();
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        

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

        private void OpenJobButton_Click(object sender, EventArgs e)
        {
            try
            {
                //@"\\UMISSERVER2\UMI\Engineering\MaterialQuotes\";

                //if (axMBActX1.OpenJob(@"C: \Users\treeves/Desktop\LazerJunk\CaliperMarker2.MA1\"))
                if (axMBActX2.OpenJob(""))
                {
                    JobTitleLabel.Text = axMBActX2.Job.Title;
                }
                List<string> tempList = new List<string>();
                for (int a = 0; a <= 10; a++)
                {
                    
                }
                
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message + error);
            }
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

        private void SetMarkingConditionButton_Click(object sender, EventArgs e)
        {
            try
            {
                axMBActX2.Block(2).SetMarkingCondition(5);
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }
            
        }

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

            // TK case (what means)
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

            // From 8xxxx will add rev?
            else if (SelectedPN[0] == '8' && SelectedPN.Length == 5)
            {
                File1List.Items.Clear();

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



                    dr["Part_Number"] = poPartNo != "" ? poPartNo.ToString() : "N/A";
                    dr["Customer"] = poCustomer != "" ? poCustomer.ToString() : "N/A";
                    dr["CustomerPN"] = poCustomerPN != "" ? poCustomerPN.ToString() : "N/A";
                    dr["Rev"] = poRevision != "" ? poRevision.ToString() : "N/A";
                    dr["Description"] = poDescription != "" ? poDescription.ToString() : "N/A";

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


            /*
            if (SelectedPN[0] == '8' && SelectedPN.Length >= 7 && SelectedPN.Contains("_"))
            {
                CheckForCustomProgram(SelectedPN);
                if (GenericProgram)
                {
                    string[] numbers = SelectedPN.Split('_');
                    GetTubePartNumberFromPDMBom(numbers[0], out diam, out wall, out partnum, out mtl);
                    OpenGenericProgram();
                    
                    FillTubeDetails(numbers[0], numbers[1]);

                }
            }
            else if (SelectedPN[0] == '8' && SelectedPN.Length == 5)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Part_Number");
                dt.Columns.Add("Customer");
                dt.Columns.Add("CustomerPN");
                dt.Columns.Add("Rev");
                dt.Columns.Add("Description");
                DataRow dr = null;
                aFile = (IEdmFile7)vault1.GetFileFromPath($@"C:\UMIS\UMi Parts\80000\{SelectedPN}.slddrw", out ppoRetParentFolder);
               
                try
                {

                    IEdmEnumeratorVariable10 enumVariable = (IEdmEnumeratorVariable10)aFile.GetEnumeratorVariable();
                    bool getVarSuccess = enumVariable.GetVarAsText("PartNo", "@", ppoRetParentFolder.ID, out object poPartNo);
                    getVarSuccess = enumVariable.GetVarAsText("Customer Name", "@", ppoRetParentFolder.ID, out object poCustomer);
                    getVarSuccess = enumVariable.GetVarAsText("CustomerPN", "@", ppoRetParentFolder.ID, out object poCustomerPN);
                    getVarSuccess = enumVariable.GetVarAsText("Revision", "@", ppoRetParentFolder.ID, out object poRevision);
                    getVarSuccess = enumVariable.GetVarAsText("Description", "@", ppoRetParentFolder.ID, out object poDescription);


                    dr = dt.NewRow();
                  
                    dr["Part_Number"] = poPartNo != null ? poPartNo.ToString() : "N/A";
                    dr["Customer"] = poCustomer != null ? poCustomer.ToString() : "N/A";
                    dr["CustomerPN"] = poCustomerPN != null ? poCustomerPN.ToString() : "N/A";
                    dr["Rev"] = poRevision != null ? poRevision.ToString() : "N/A";
                    dr["Description"] = poDescription != null ? poDescription.ToString() : "N/A";

                    dt.Rows.Add(dr);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                
                OrdersGridView.DataSource = dt;
                DataGridViewRow row = this.OrdersGridView.SelectedRows[0];
                OrderRev = row.Cells["Rev"].Value.ToString();
               
                CheckForCustomProgram(SelectedPN + "_" + OrderRev);
                if (GenericProgram)
                {
                    
                    GetTubePartNumberFromPDMBom(SelectedPN, out diam, out wall, out partnum, out mtl);
                    OpenGenericProgram();
                    FillTubeDetails(SelectedPN, OrderRev);
                }
            }
            else
            {
                CheckForCustomProgram(SelectedPN);
                if (GenericProgram)
                {
                    OpenGenericProgram();
                    FillTubeDetails(SelectedPN, OrderRev);
                }
            }

            */

            //CheckForCustomProgram(PNSub);
            //if (GenericProgram == true)
            //{
            //    GetTubePartNumberFromPDMBom(SelectedPN, out diam, out wall, out partnum, out mtl);  
            //    OpenGenericProgram();
            //    Console.WriteLine("Here1");
            //    FillTubeDetails(SelectedPN, orderRev); // needs to happen after the program is opened
            //}

        }

        private void GetTubeKitFromPDMBom(string partNumber, DataTable dt)
        {
            
            try
            {
                File1List.Items.Clear();

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
                    bomMgr = (IEdmBomMgr2)(IEdmBomMgr)vault2.CreateUtility(EdmUtility.EdmUtil_BomMgr);
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
                        axMBActX2.Context = ContextTypes.CONTEXT_EDITING;
                        axMBActX2.OpenJob(FilePath);
                        try
                        {
                            JobTitleLabel.Text = axMBActX2.Job.Title;
                            partNum = axMBActX2.Block(3).Text;
                            customerNum = axMBActX2.Block(4).Text;
                            description = axMBActX2.Block(5).Text;
                            description2 = axMBActX2.Block(6).Text;
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
                            DataGridViewRow row = this.OrdersGridView.SelectedRows[0];
                            string myPN = row.Cells["Part_Number"].Value.ToString();
                            string myRev = row.Cells["Rev"].Value.ToString();
                            PartNumAndRevBox.Text = partNum;
                            PartNumAndRevBox.Text = (myPN + "_" + myRev);
                            CustPartNumAndRevBox.Text = customerNum;
                            DescLine1Box.Text = description;
                            DescLine2Box.Text = description2;

                            if (axMBActX2.Block(8).IsMarkingEnable)
                            {
                                QRCheckBox.Checked = true;
                            }
                            else
                            {
                                QRCheckBox.Checked = false;
                            }
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
                File1List.Items.Clear();

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

                        bomMgr = (IEdmBomMgr2)(IEdmBomMgr)vault2.CreateUtility(EdmUtility.EdmUtil_BomMgr);
                        EdmBomInfo[] derivedBOMs;

                        aFile.GetDerivedBOMs(out derivedBOMs);

                        // MessageBox.Show(""+derivedBOMs.Length);

                        ////**************
                        //int arrSize = 0;
                        //EdmBomVersion[] ppoVersions = null;
                        //int i = 0;

                        //int id = 0;
                        //string str = "";
                        //string verstr = "";
                        //int verArrSize = 0;
                        //arrSize = derivedBOMs.Length;
                        //int userID = 0;
                        //bool canSeeLayout = false;

                        ////userID = vault2.GetLoggedInWindowsUserID(vault2.Name);

                        //while (i < arrSize)
                        //{
                        //    id = derivedBOMs[i].mlBomID;
                        //    bom = (IEdmBom)vault2.GetObject(EdmObjectType.EdmObject_BOM, id);
                        //    str = "Named BOM: " + derivedBOMs[i].mbsBomName + "\r\n" + "Type of BOM as defined in EdmBomType: " + derivedBOMs[i].meType + "\\n" + "Check-out user: " + bom.CheckOutUserID + "\r\n" + "Current state: " + bom.CurrentState.Name + "\r\n" + "Current version: " + bom.CurrentVersion + "\r\n" + "ID: " + bom.FileID + "\r\n" + "Is checked out: " + bom.IsCheckedOut;
                        //    MessageBox.Show(str);
                        //    bom.GetVersions(out ppoVersions);
                        //    verArrSize = ppoVersions.Length;
                        //    int j = 0;
                        //    while (j < verArrSize)
                        //    {
                        //        verstr = "BOM version: " + "\r\n" + "Type as defined in EdmBomVersionType: " + ppoVersions[j].meType + "\r\n" + "Version number: " + ppoVersions[j].mlVersion + "\r\n" + "Date: " + ppoVersions[j].moDate + "\r\n" + "Label: " + ppoVersions[j].mbsTag + "\r\n" + "Comment: " + ppoVersions[j].mbsComment;
                        //        MessageBox.Show(verstr);
                        //        j = j + 1;
                        //    }
                        //    i = i + 1;
                        //}



                        //*****************

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
                            //string StringDiameter = "";
                            //string StringWallThickness = "";
                            //StringDiameter = TubesFound[0].Substring(TubesFound[0].Length - 7, 4);
                            //StringWallThickness = TubesFound[0].Substring(TubesFound[0].Length - 3, 3);
                            //MessageBox.Show("Diameter " + Convert.ToDouble(StringDiameter) / 1000 + "\nWall: " + Convert.ToDouble(StringWallThickness) / 1000);
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

        private void FillTubeDetails(string TubePartNumber, string OrderRev)
        {
            /*
            int customerId =0;
            using (SqlConnection cn = new SqlConnection(OpenConnect(UMIConnectionString)))
            {
                try
                {
                    cn.Open();  // Open connection using the SQL connection string above
                    SqlCommand cmd2 = new SqlCommand("", cn);    //Declare text command for server connection
                    cmd2.CommandTimeout = 120; //set a long timeout in case of really complex queries 2019-04-30

                    cmd2.Parameters.AddWithValue("@Search", TubePartNumber);

                    cmd2.Parameters.AddWithValue("@rev", OrderRev);

                    if (OrderRev == " ")
                    {
                        cmd2.CommandText = "" +

                        " select * FROM [HydraulicHoseInfo_prod].[dbo].[TubeAssemblies] where PartNo = @Search";
                    }
                    else
                    {
                        cmd2.CommandText = "" +

                        " select * FROM [HydraulicHoseInfo_prod].[dbo].[TubeAssemblies] where PartNo = @Search AND Rev = @rev";
                    }                                                                                                   

                    SqlDataReader reader2 = cmd2.ExecuteReader();  //SET up reader to read values out of command.

                    
                    while (reader2.Read())
                    {
                        try
                        {
                            //if ((reader2["Rev"]).ToString() == "" )
                            
                            if (OrderRev == " ") // USE REV FROM ORDER FOR NOW
                            {
                                PartNumAndRevBox.Text = (reader2["PartNo"]).ToString();
                            }
                            else
                            {
                                PartNumAndRevBox.Text = (reader2["PartNo"]).ToString() + "_" + (reader2["Rev"]).ToString();
                            }
                            if ((reader2["CustomerRev"]).ToString() == "")
                            {
                                CustPartNumAndRevBox.Text = (reader2["CustomerPN"]).ToString();
                            }
                            else{
                                CustPartNumAndRevBox.Text = (reader2["CustomerPN"]).ToString() + "_" + (reader2["CustomerRev"]).ToString();
                            }
                            
                            string desc = (reader2["Description"]).ToString();
                            //string desc = "123456789ABCDEFGHIJKLMNOPQRSTUVWXZY123456789AA";
                            int DescLengthAllow =32;
                            if (desc.Length <= DescLengthAllow)
                            {
                                DescLine1Box.Text = desc;
                            }
                            else if(desc.Length > DescLengthAllow && desc.Length <= (DescLengthAllow*2))
                            {
                                DescLine1Box.Text = desc.Substring(0, DescLengthAllow);
                                DescLine2Box.Text = desc.Substring(DescLengthAllow, desc.Length- DescLengthAllow);
                            }
                            else
                            {
                                DescLine1Box.Text = desc.Substring(0, DescLengthAllow);
                                DescLine2Box.Text = desc.Substring(DescLengthAllow, DescLengthAllow);
                                
                            }
                            customerId = Convert.ToInt32((reader2["Customer_id"]).ToString());
                            UpdateCurrentProgramBlocks(customerId);
                        }
                        catch
                        {

                        }
                        
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting open orders" + ex);
                }
            }
            */
            try
            {
                File1List.Items.Clear();

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
                using (SqlConnection cn = new SqlConnection(OpenConnect(HHI_PUMIConnectionString)))
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

        private void UpdateCurrentProgramBlocks(int customerId)
        {
            
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //Edit the block No. 2
                axMBActX2.Block(2).IsMarkingEnable = true;
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void ProgramSizeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ProgramMaterialCombo.SelectedIndex = -1;
        }

        private void CheckForExistingProgram(string selectedPN)
        {
            string partialName = "999999999999";
            try
            {
                partialName = selectedPN.Substring(0, 5);
            }
            catch
            {
                MessageBox.Show("Something went wrong when trimming your string.");
            }
            

            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"U:\Engineering\LaserMarkingProfiles");
            FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" + partialName + "*.*");

            if(filesInDir.Count() > 1)
            {

            }else if(filesInDir.Count() == 0)
            {

            }
            else
            {

            }

            OpenProgram op = new OpenProgram();
            
            op.OpenExistingProgramListBox.Items.AddRange(filesInDir);
            
            op.Show();
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

        private void axMBActX1_EvError(object sender, EventArgs e)
        {
            MessageBox.Show("Error CALLED: "+e);
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
                if (Count >0)
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

        private void PartNumAndRevBox_TextChanged(object sender, EventArgs e)
        {
            UpdateCurrentProgramBlocks(0);
        }

        private void CustPartNumAndRevBox_TextChanged(object sender, EventArgs e)
        {
            UpdateCurrentProgramBlocks(0);
        }

        private void DescLine1Box_TextChanged(object sender, EventArgs e)
        {
            UpdateCurrentProgramBlocks(0);
        }

        private void DescLine2Box_TextChanged(object sender, EventArgs e)
        {
            UpdateCurrentProgramBlocks(0);
        }

        private void QRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (QRCheckBox.Checked)
            {
                QRCodeDataBox.ReadOnly = false;
                axMBActX2.Block(8).IsMarkingEnable = true;
                try
                {
                    if (axMBActX2.Block(8).IsEditable)
                    {
                        axMBActX2.Block(8).Text = "umisolutions.ca";// + QRCodeDataBox.Text;
                    }
                }
                catch (System.Runtime.InteropServices.COMException error)
                {
                    MessageBox.Show(error.Message);
                }
            }
            else
            {
                QRCodeDataBox.ReadOnly = true;
                axMBActX2.Block(8).IsMarkingEnable = false;
            }
        }

        private void QRCodeDataBox_TextChanged(object sender, EventArgs e)
        {
            
            try { if (axMBActX2.Block(8).IsEditable) { axMBActX2.Block(8).Text = QRCodeDataBox.Text; } }
            catch { axMBActX2.Block(8).Text = "umisolutions.ca"; }
        }

        private void FlipPartNumbersButton_Click(object sender, EventArgs e)
        {
            string tempPN = PartNumAndRevBox.Text;
            string tempCustPN = CustPartNumAndRevBox.Text;

            PartNumAndRevBox.Text = tempCustPN;
            CustPartNumAndRevBox.Text = tempPN;

            //80006 rev 2 
            //80139 rev 0
            //80140 rev 0
        }

        private void XMoveNegButton_Click(object sender, EventArgs e)
        {
            MoveBlock("XN", (SelectedBlockCombo.SelectedItem).ToString());
        }

        private void MoveBlock(string Direction, string Block)
        {
            int BlockNum = 999;




            if (Block == "Date")
            {
                BlockNum = 2;
            }else if (Block == "Logo")
            {
                BlockNum = 1;
            }
            else if (Block == "Part No.")
            {
                BlockNum = 3;
            }
            else if (Block == "Cust PN")
            {
                BlockNum = 4;
            }
            else if (Block == "Desc 1")
            {
                BlockNum = 5;
            }
            else if (Block == "Desc 2")
            {
                BlockNum = 6;
            }
            else if (Block == "Box Size" || Block == "Box Position")
            {
                BlockNum = 0;
            }

            //axMBActX2.Block(0).X = 0;
            //axMBActX2.Block(0).Y = 0;
         
            if (Block != "" && BlockNum != 999)
            {
                if (Direction == "XP")
                {
                    try
                    {
                       if (axMBActX2.Block(BlockNum).IsEditable)
                       {
                            if (BlockNum == 0 || BlockNum == 1)
                            {
                                if (Block == "Box Position")
                                {
                                    axMBActX2.Block(BlockNum).X += 1;
                                }
                                else
                                {
                                    axMBActX2.Block(BlockNum).LogoWidth += 1;
                                }
                            }
                            else
                            {
                                //axMBActX2.Block(BlockNum).X += 1; // Edit the block No. 2                                
                                axMBActX2.Block(BlockNum).CharWidth += 1;
                            }
                           
                        }
                       
                       
                    }
                    catch (System.Runtime.InteropServices.COMException error)
                    {
                        MessageBox.Show(error.Message);
                    }
                }else if (Direction == "XN")
                {
                    try
                    {
                        if (BlockNum == 0 || BlockNum == 1)
                        {
                            if (Block == "Box Position")
                            {
                                axMBActX2.Block(BlockNum).X -= 1;
                            }
                            else
                            {
                                axMBActX2.Block(BlockNum).LogoWidth -= 1; 
                            }
                        }
                        else
                        {
                            //axMBActX2.Block(BlockNum).X -= 1; // Edit the block No. 2
                            axMBActX2.Block(BlockNum).CharWidth -= 1;
                        }
                        

                    }
                    catch (System.Runtime.InteropServices.COMException error)
                    {
                        MessageBox.Show(error.Message);
                    }
                }
                else if (Direction == "YP")
                {
                    try
                    {
                        if (BlockNum == 0 || BlockNum == 1)
                        {
                            if (Block == "Box Position")
                            {
                                axMBActX2.Block(BlockNum).Y += 1;
                            }
                            else
                            {
                                axMBActX2.Block(BlockNum).LogoHeight += 0.5;
                            }
                        }
                        else
                        {
                            //axMBActX2.Block(BlockNum).Y += 1; // Edit the block No. 2
                            axMBActX2.Block(BlockNum).CharHeight += 0.5;
                        }
                        

                    }
                    catch (System.Runtime.InteropServices.COMException error)
                    {
                        MessageBox.Show(error.Message);
                    }
                }
                else if (Direction == "YN")
                {
                    try
                    {
                        if (BlockNum == 0 || BlockNum == 1)
                        {
                            if (Block == "Box Position")
                            {
                                axMBActX2.Block(BlockNum).Y -= 1;
                            }
                            else
                            {
                                axMBActX2.Block(BlockNum).LogoHeight -= 0.5;
                            }
                        }
                        else
                        {
                            //axMBActX2.Block(BlockNum).Y -= 1; // Edit the block No. 2
                            axMBActX2.Block(BlockNum).CharHeight -= 0.5;
                        }
                        

                    }
                    catch (System.Runtime.InteropServices.COMException error)
                    {
                        MessageBox.Show(error.Message);
                    }
                }
            }
            //axMBActX2.Block(0).X = 0;
            //axMBActX2.Block(0).Y = 0;

        }

        private void YMoveNegButton_Click(object sender, EventArgs e)
        {
            MoveBlock("YN", (SelectedBlockCombo.SelectedItem).ToString());
        }
        private void XPositiveButton_Click(object sender, EventArgs e)
        {
            MoveBlock("XP", (SelectedBlockCombo.SelectedItem).ToString());
            
        }

        private void YMovePositiveButton_Click(object sender, EventArgs e)
        {
            MoveBlock("YP", (SelectedBlockCombo.SelectedItem).ToString());
            try
            {
                axMBActX2.Block(1).Y += 1; // Edit the block No. 2
            }
            catch (System.Runtime.InteropServices.COMException error)
            {
                MessageBox.Show(error.Message);
            }
        }

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

        private void ProgramMaterialCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

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
                        axMBActX2.Block(8).IsMarkingEnable = false;
                        axMBActX2.Block(3).X = 10;

                    }
                    
                }
                catch (System.Runtime.InteropServices.COMException error)
                {
                    MessageBox.Show(error.Message + error);
                }
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

        private void button4_Click(object sender, EventArgs e)
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

                if (aFile != null)
                {

                    bomMgr = (IEdmBomMgr2)(IEdmBomMgr)vault2.CreateUtility(EdmUtility.EdmUtil_BomMgr);
                    EdmBomInfo[] derivedBOMs;
                    aFile.GetDerivedBOMs(out derivedBOMs);
                    int i = 0;
                    int arrSize = derivedBOMs.Length;
                    string str = "";
                    int id;
                    while (i < arrSize)
                    {
                        id = derivedBOMs[i].mlBomID;
                        bom = (IEdmBom)vault2.GetObject(EdmObjectType.EdmObject_BOM, id);
                        str = "Derived BOM: " + derivedBOMs[i].mbsBomName + " " + bom.CheckOutUserID + " " + bom.CurrentState.Name + " " + bom.CurrentVersion + " " + bom.FileID + " " + bom.IsCheckedOut;

                        //MessageBox.Show(str);
                        i = i + 1;
                    }
                    

                    bomView = (IEdmBomView3)(IEdmBomView2)bom.GetView(0);
                    

                    EdmBomColumn[] ppoColumns = null;
                    bomView.GetColumns(out ppoColumns);

                    object[] ppoRows = null;
                    IEdmBomCell ppoRow = default(IEdmBomCell);
                    bomView.GetRows(out ppoRows);

                    object cellItem = null;
                    object cmop = null;
                    string config = null;
                    bool nnn= false;
                    

                    

                    i = 0;
                    arrSize = ppoColumns.Length;
                    str = "";
                    while (i < arrSize)
                    {
                        str = "BOM Column " + i + ": " + "\n" + "Header: " + ppoColumns[i].mbsCaption + "\n" + "Column type as defined in EdmBomColumnType: " + ppoColumns[i].meType + "\n" + "ID: " + ppoColumns[i].mlColumnID + "\n" + "Flags: " + ppoColumns[i].mlFlags + "\n" + "Variable ID: " + ppoColumns[i].mlVariableID + "\n" + "Variable type as defined in EdmVariableType: " + ppoColumns[i].mlVariableType + "\n" + "Column width: " + ppoColumns[i].mlWidth;
                        //MessageBox.Show(str);
                        i = i + 1;
                    }




                    
                    
                    i = 0;
                    arrSize = ppoRows.Length;
                    str = "";
                    while (i < arrSize)
                    {
                        ppoRow = (IEdmBomCell)ppoRows[i];
                        str = "BOM Row " + i + ": " + "\n" + "Item ID: " + ppoRow.GetItemID() + "\n" + "Path name: " + ppoRow.GetPathName() + "\n" + "Tree level: " + ppoRow.GetTreeLevel() + "\n" + " Is locked? " + ppoRow.IsLocked();
                        //MessageBox.Show(str);
                        i = i + 1;
                    }

                    string parts = "";
                    foreach (IEdmBomCell rowe in ppoRows)
                    {
                        int abc = 0;
                        foreach (EdmBomColumn d in ppoColumns)
                        {
                            
                            
                            if(abc<= (ppoColumns.Count() - 1))
                            {
                                rowe.GetVar(ppoColumns[abc].mlVariableID, ppoColumns[abc].meType, out cellItem, out cmop, out config, out nnn);
                                if (abc == 1)
                                {
                                    parts += cellItem + "\n";
                                }
                                MessageBox.Show("Col Position: " + abc + "\n Col ml variable: " + ppoColumns[abc].mlVariableID + "\n cell item :" + cellItem + "\n Col Type for [abc] :" + ppoColumns[abc].meType);
                                abc += 1;



                            }

                            
                        }


                    }

                    foreach (IEdmBomCell rowe in ppoRows)
                    {
                        int abc = 1;
                        //foreach (EdmBomColumn d in ppoColumns)
                        //{
                            rowe.GetVar(ppoColumns[1].mlVariableID, ppoColumns[1].meType, out cellItem, out cmop, out config, out nnn);

                            if (abc == 1)
                            {
                                parts += cellItem + "\n";
                            }
                            MessageBox.Show("222222Col Position: " + abc + "\n Col ml variable: " + ppoColumns[abc].mlVariableID + "\n cell item :" + cellItem + "\n Col Type for [2] :" + ppoColumns[2].meType);
                            abc += 1;
                        //}


                    }
                    MessageBox.Show(parts);
                    

                    

                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                MessageBox.Show("HRESULT = 0x" + ex.ErrorCode.ToString("X") + " " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                File1List.Items.Clear();

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


                aFile = (IEdmFile7)vault1.GetFileFromPath(@"C:\UMIS\UMi Parts\80000\81024.slddrw", out ppoRetParentFolder);
                File1List.Items.Add(aFile.Name);


                ////Set the initial directory in the Select File dialog
                //OpenFileDialog1.InitialDirectory = vault1.RootFolderPath;

                ////Show the Select File dialog
                //System.Windows.Forms.DialogResult DialogResult;
                //DialogResult = OpenFileDialog1.ShowDialog();

                //if (!(DialogResult == System.Windows.Forms.DialogResult.OK))
                //{
                //    // do nothing
                //}
                //else
                //{
                //    foreach (string FileName in OpenFileDialog1.FileNames)
                //    {
                //        File1List.Items.Add(FileName);
                //        aFile = (IEdmFile7)vault1.GetFileFromPath(FileName, out ppoRetParentFolder);
                //        k = FileName.LastIndexOf(".");
                //        fileExt = FileName.Substring(k + 1, (FileName.Length) - k - 1);
                //        aPos = aFile.GetFirstFolderPosition();
                //        aFolder = aFile.GetNextFolder(aPos);
                //    }
                //}

            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                MessageBox.Show("HRESULT = 0x" + ex.ErrorCode.ToString("X") + " " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
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


                if ((aFile != null))
                {
                    IEdmEnumeratorVariable10 EnumVarObj = default(IEdmEnumeratorVariable10);
                    EnumVarObj = (IEdmEnumeratorVariable10)aFile.GetEnumeratorVariable();
                    

                    // Get configurations
                    EdmStrLst5 cfgList = default(EdmStrLst5);
                    cfgList = aFile.GetConfigurations();

                    IEdmPos5 pos = default(IEdmPos5);
                    pos = cfgList.GetHeadPosition();
                    string cfgName = null;

                    Dictionary<string, Dictionary<string, string>> myDictDict = new Dictionary<string, Dictionary<string, string>>();

                    object[] varz = null;
                    string[] configz = null;
                    EdmGetVarData poRetDat = new EdmGetVarData();
                    EnumVarObj.GetVersionVars(aFile.GetLocalVersionNo(ppoRetParentFolder.ID), ppoRetParentFolder.ID, out varz, out configz, poRetDat);

                    string str = null;
                    str = "File variable data for " + aFile.Name + "\r\n";
                    str = str + "Version: " + poRetDat.mlVersion + "\r\n";
                    str = str + "Latest version: " + poRetDat.mlLatestVersion + "\r\n";
                    str = str + "Revision: " + poRetDat.mbsRevision + "\r\n";
                    str = str + "State: " + poRetDat.mbsState + "\r\n";
                    str = str + "Workflow: " + poRetDat.mbsWorkflow + "r\\n";
                    str = str + "Category: " + poRetDat.mbsCategory + "\r\n";
                    str = str + "SQL Server date format code: " + poRetDat.mlDateFmt + "\r\n";
                    str = str + "EdmGetVarDataFlags: " + poRetDat.mlEdmGetVarDataFlags;

                    //MessageBox.Show(str);
                    foreach (IEdmVariableValue6 b in varz)
                    {
                        MessageBox.Show("" + b.VariableName + "  " + b.GetValue("@"));
                    }



                    while (!pos.IsNull)
                    {

                        cfgName = cfgList.GetNext(pos);
                        
                        object VarObj = null;

                        EnumVarObj.GetVar("PartNo", cfgName, out VarObj);
                        string VarVal = VarObj?.ToString();

                        EnumVarObj.GetVar("Description", cfgName, out VarObj);
                        string Description = VarObj?.ToString();

                        EnumVarObj.GetVar("Revision", cfgName, out VarObj);
                        string revision = VarObj?.ToString();

                        //EnumVarObj.GetVar("DrawnBy", "@", out VarObj);
                        
                        EnumVarObj.GetVarAsText("Customer Name", "@",(ppoRetParentFolder.ID), out VarObj);
                        string Customer = VarObj?.ToString();

                        EnumVarObj.GetVar("CustomerPN", cfgName, out VarObj);
                        string CustomerPN = VarObj?.ToString();

                        Dictionary<string, string> values = new Dictionary<string, string>();

                        values.Add("PartNo", VarVal);
                        values.Add("Revision", revision);
                        values.Add("Description", Description);
                        MessageBox.Show("PartNo: "+ VarVal+ "\nRev: " + revision + "\nDescription: " + Description + "\nCustomer: " + Customer + "\nCustomerPN: " + CustomerPN);

                    }




                    //    // Get the selected file's data card
                    //    aCard = (IEdmCard6)aFolder.GetCard(fileExt);
                    //cardID = aFolder.GetCardID(fileExt);
                    
                    //aCard.GetSize(out plWidth, out plHeight);
                    
                    //str = "File: " + aFile.Name + "\r\n" + "Card ID: " + cardID + ", EdmCardType: " + aCard.CardType + ", Width: " + plWidth + ", Height: " + plHeight;
                    //MessageBox.Show(str);

                    //aPos = aCard.GetFirstControlPosition();
                    //while (!(aPos.IsNull))
                    //{
                    //    aControl = (IEdmCardControl7)(IEdmCardControl6)aCard.GetNextControl(aPos);
                    //    contType = (int)aControl.ControlType;
                    //    try
                    //    {
                    //        string[] a;
                    //        aControl.GetControlVariableList(aFile.ID, out a);
                            
                    //        //int i = 0;    
                    //        foreach (string b in a)
                    //        {
                    //            MessageBox.Show("Look    " + aControl.Name + " " + b );
                    //        }
                    //    }
                    //    catch
                    //    {

                    //    }


                    //    bool ret = false;
                    //    string[] variableItemsList = null;
                    //    if (((contType == 7) | (contType == 8) | (contType == 9) | (contType == 10)))
                    //    {
                    //        str = "List values associated with drop-down card control: " + aControl.VariableID.ToString();
                    //        ret = aControl.GetControlVariableList(aFile.ID, out variableItemsList);

                    //        foreach (string listValue in variableItemsList)
                    //        {
                    //            str = str + "\r\n" + listValue;
                    //        }
                    //        MessageBox.Show(str);
                    //    }



                    //    // Get the edit box controls in the card
                    //    if (contType == 4)
                    //    {
                    //        str = "";
                    //        aControl.GetParentInfo(out plParentCtrlID, out plPageNo);
                    //        aControl.GetPosition(out plX, out plY, out plWidth, out plHeight);
                    //        varType = (int)aControl.GetValidation(out poMin, out poMax);

                    //        str = "Card control: " + aControl.Name;
                    //        str = str + "\r\n" + "Variable ID: " + aControl.VariableID + "\t\n" + "EdmCardControlType: " + contType + "\r\n" + "Is multi-line? " + aControl.IsMultiLine + "\r\n" + "Is read-only? " + aControl.IsReadOnly + "\r\n" + "Show in preview? " + aControl.ShowInPreview;
                    //        str = str + "\r\n" + "Location on card: [" + plX + ", " + plY + "], Width: " + plWidth + ", Height: " + plHeight;
                    //        str = str + "\r\n" + "Parent control ID (0, if none): " + plParentCtrlID;
                    //        str = str + "\r\n" + "Tab index: " + plPageNo;
                    //        str = str + "\r\n" + "EdmVariableType: " + varType;

                    //        str = str + "\r\n" + "Updates all configurations? " + aControl.UpdatesAllConfigurations.ToString();



                    //        MessageBox.Show(str);
                    //    }
                    //}
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                MessageBox.Show("HRESULT = 0x" + ex.ErrorCode.ToString("X") + " " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        
    }

        private void AllPartNumBtn_Click(object sender, EventArgs e)
        {
            /*
            //Populate 80000 series from sql database
            using (SqlConnection cn = new SqlConnection(OpenConnect(HHI_PUMIConnectionString)))
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
                    " FROM TubeAssemblies ta                                                                                                         " +
                    "left join Customers c on c.id = Customer_id                                                                                      " +
                   " where PartNo like '8%'                                             " +
                    " order by PartNo desc                                                                                                            ";
                    DataTable dt = new DataTable();                                                                      
                    dt.Load(cmd2.ExecuteReader());

                    OrdersGridView.DataSource = dt;
                    
                }
                catch { }
            }
            */
            //Populate 80000 from pdm
            try
            { 
                File1List.Items.Clear();

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

                //DirectoryInfo di = new DirectoryInfo(path);
                //Queue<FileInfo> unvisited = new Queue<FileInfo>();

                //foreach (var fi in di.EnumerateFiles("*.slddrw"))
                //{
                //    if (fi.Name != null && fi != null)
                //    {
                //        unvisited.Enqueue(fi);
                //    }
                //}

                //while (unvisited.Count > 0)
                //{
                //    var filed = unvisited.Dequeue();
                //    dr = dt.NewRow();
                //    dr["Part_Number"] = Path.GetFileNameWithoutExtension(filed.Name);
                //    dt.Rows.Add(dr);
                //}
                OrdersGridView.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void runlengthCheck() //used to checking all SW boms!!!!!!!!!!!!!!!!!!
        {
            
            string SelectedPN = "";
            string SelectedRev = "";
            string SelectedCustomerPN = "";
            double len;
            string partnum;
            string mtl;

            string printout = "";
            foreach(DataGridViewRow rowwwww in OrdersGridView.Rows)
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
                printout += PNSub + "\t" + len +"\n";
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
                File1List.Items.Clear();

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
                if (aFile != null)
                {
                    File1List.Items.Add(aFile.Name);
                }
                else
                {
                    //MessageBox.Show("File not found in 80,000 folder.");

                }



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
                        bomMgr = (IEdmBomMgr2)(IEdmBomMgr)vault2.CreateUtility(EdmUtility.EdmUtil_BomMgr);
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

        private void GetLengthsBtn_Click(object sender, EventArgs e) //used to checking all SW boms!!!!!!!!!!!!!!!!!!
        {
            using (SqlConnection cn = new SqlConnection(OpenConnect(HHI_PUMIConnectionString)))
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

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                if (axMBActX2.Block(1).IsEditable)
                {

                    axMBActX2.Block(1).IsLogoAspectRatioKeep = true;
                    //axMBActX2.Block(1).LogoWidth = 10.250; //in mm
                    DirectoryInfo directory = new DirectoryInfo(@"U:\Engineering\CUSTOMERLOGO\");
                    axMBActX2.Block(1).Text = "" + directory.ToString() + "Multiflow.MHL";

                }

            }
            catch { axMBActX2.Block(1).Text = " "; }
        }

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
        private void Desc2Box_CheckedChanged(object sender, EventArgs e)
        {
            if (Desc2Box.Checked)
            {
                axMBActX2.Block(6).IsMarkingEnable = true;
            }
            else
            {
                axMBActX2.Block(6).IsMarkingEnable = false;
            }
            
        }


    }
}


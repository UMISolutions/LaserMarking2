
using System.Collections.Generic;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LaserMarking
{
    partial class MainWindow
    {
        

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

       #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.axMBActX2 = new AxMBPLib2.AxMBActX();
            this.OrdersGridView = new System.Windows.Forms.DataGridView();
            this.Get_Z = new System.Windows.Forms.Button();
            this.Mark_Part = new System.Windows.Forms.Button();
            this.markerConnectButton = new System.Windows.Forms.Button();
            this.MarkerDisconnectButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.EditingContextButton = new System.Windows.Forms.Button();
            this.ControllerContextButton = new System.Windows.Forms.Button();
            this.LightOnButton = new System.Windows.Forms.Button();
            this.LightOffButton = new System.Windows.Forms.Button();
            this.JobTitleLabel = new System.Windows.Forms.Label();
            this.ProgramMaterialCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ProgramSizeCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CameraFinderViewButton = new System.Windows.Forms.Button();
            this.ShowFileListButton = new System.Windows.Forms.Button();
            this.Errors_Btn = new System.Windows.Forms.Button();
            this.ClearErrors_Btn = new System.Windows.Forms.Button();
            this.DateBox = new System.Windows.Forms.TextBox();
            this.PartNumAndRevBox = new System.Windows.Forms.TextBox();
            this.CustPartNumAndRevBox = new System.Windows.Forms.TextBox();
            this.DescLine1Box = new System.Windows.Forms.TextBox();
            this.DescLine2Box = new System.Windows.Forms.TextBox();
            this.QRCodeDataBox = new System.Windows.Forms.TextBox();
            this.FlipPartNumbersButton = new System.Windows.Forms.Button();
            this.SetCameraPosition = new System.Windows.Forms.Button();
            this.OpenControllerJob = new System.Windows.Forms.Button();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.SelectedMaterialPN = new System.Windows.Forms.Label();
            this.GetOrderTubePNBTN = new System.Windows.Forms.Button();
            this.AllPartNumBtn = new System.Windows.Forms.Button();
            this.GetLengthsBtn = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.Desc2Box = new System.Windows.Forms.CheckBox();
            this.btnOpenMarkerBuilder = new System.Windows.Forms.Button();
            this.btnRefreshTag = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.HeatBox = new System.Windows.Forms.TextBox();
            this.HeatCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.widthBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.heightBox = new System.Windows.Forms.TextBox();
            this.wp10 = new System.Windows.Forms.Button();
            this.wm10 = new System.Windows.Forms.Button();
            this.wp1 = new System.Windows.Forms.Button();
            this.wm1 = new System.Windows.Forms.Button();
            this.hp10 = new System.Windows.Forms.Button();
            this.hp1 = new System.Windows.Forms.Button();
            this.hm10 = new System.Windows.Forms.Button();
            this.hm1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.PlusOne = new System.Windows.Forms.Button();
            this.PlusTen = new System.Windows.Forms.Button();
            this.MinusTen = new System.Windows.Forms.Button();
            this.MinusOne = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label14 = new System.Windows.Forms.Label();
            this.POTxtBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.Desc1Box = new System.Windows.Forms.CheckBox();
            this.PN2Box = new System.Windows.Forms.CheckBox();
            this.PN1Box = new System.Windows.Forms.CheckBox();
            this.DateCheckBox = new System.Windows.Forms.CheckBox();
            this.QRCheckBox = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.LabelBox = new System.Windows.Forms.CheckBox();
            this.ImageBox = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.LogoComboBox = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabLaserMarking = new System.Windows.Forms.TabPage();
            this.tabBrazing = new System.Windows.Forms.TabPage();
            this.btnLoadResults = new System.Windows.Forms.Button();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvBrazeParameters = new System.Windows.Forms.DataGridView();
            this.cbFittingPN = new System.Windows.Forms.ComboBox();
            this.btnAddBrazeEntry = new System.Windows.Forms.Button();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.txtBrazeRings = new System.Windows.Forms.TextBox();
            this.txtJointClearance = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.txtPowerLevel = new System.Windows.Forms.TextBox();
            this.txtConePN = new System.Windows.Forms.TextBox();
            this.txtTubeEndStyle = new System.Windows.Forms.TextBox();
            this.txtFittingPN = new System.Windows.Forms.TextBox();
            this.lblMeasuredJointClearance = new System.Windows.Forms.Label();
            this.lblBrazeRings = new System.Windows.Forms.Label();
            this.lblComments = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblPowerLevel = new System.Windows.Forms.Label();
            this.lblConePN = new System.Windows.Forms.Label();
            this.lblTubeEndStyle = new System.Windows.Forms.Label();
            this.lblFittingPN = new System.Windows.Forms.Label();
            this.lblTubePN = new System.Windows.Forms.Label();
            this.lblAddBrazeEntry = new System.Windows.Forms.Label();
            this.txtTubePN = new System.Windows.Forms.TextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.axMBActX2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrdersGridView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabLaserMarking.SuspendLayout();
            this.tabBrazing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBrazeParameters)).BeginInit();
            this.SuspendLayout();
            // 
            // axMBActX2
            // 
            this.axMBActX2.Enabled = true;
            this.axMBActX2.Location = new System.Drawing.Point(517, 7);
            this.axMBActX2.Margin = new System.Windows.Forms.Padding(4);
            this.axMBActX2.Name = "axMBActX2";
            this.axMBActX2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMBActX2.OcxState")));
            this.axMBActX2.Size = new System.Drawing.Size(686, 738);
            this.axMBActX2.TabIndex = 0;
            this.axMBActX2.UseWaitCursor = true;
            this.axMBActX2.EvMarkingEnd += new AxMBPLib2._DMBActXEvents_EvMarkingEndEventHandler(this.axMBActX1_EvMarkingEnd);
            // 
            // OrdersGridView
            // 
            this.OrdersGridView.AllowUserToAddRows = false;
            this.OrdersGridView.AllowUserToDeleteRows = false;
            this.OrdersGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OrdersGridView.Location = new System.Drawing.Point(17, 44);
            this.OrdersGridView.Margin = new System.Windows.Forms.Padding(4);
            this.OrdersGridView.Name = "OrdersGridView";
            this.OrdersGridView.ReadOnly = true;
            this.OrdersGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.OrdersGridView.Size = new System.Drawing.Size(556, 481);
            this.OrdersGridView.TabIndex = 1;
            this.OrdersGridView.Click += new System.EventHandler(this.OrdersGridView_Click);
            // 
            // Get_Z
            // 
            this.Get_Z.Location = new System.Drawing.Point(1648, 823);
            this.Get_Z.Margin = new System.Windows.Forms.Padding(4);
            this.Get_Z.Name = "Get_Z";
            this.Get_Z.Size = new System.Drawing.Size(69, 28);
            this.Get_Z.TabIndex = 2;
            this.Get_Z.Text = "Get Z";
            this.Get_Z.UseVisualStyleBackColor = true;
            this.Get_Z.Click += new System.EventHandler(this.Get_Z_Click);
            // 
            // Mark_Part
            // 
            this.Mark_Part.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Mark_Part.Location = new System.Drawing.Point(1682, 217);
            this.Mark_Part.Margin = new System.Windows.Forms.Padding(4);
            this.Mark_Part.Name = "Mark_Part";
            this.Mark_Part.Size = new System.Drawing.Size(313, 64);
            this.Mark_Part.TabIndex = 4;
            this.Mark_Part.Text = "Mark Part";
            this.Mark_Part.UseVisualStyleBackColor = false;
            this.Mark_Part.Click += new System.EventHandler(this.Mark_Part_Click);
            // 
            // markerConnectButton
            // 
            this.markerConnectButton.Location = new System.Drawing.Point(1648, 786);
            this.markerConnectButton.Margin = new System.Windows.Forms.Padding(4);
            this.markerConnectButton.Name = "markerConnectButton";
            this.markerConnectButton.Size = new System.Drawing.Size(161, 28);
            this.markerConnectButton.TabIndex = 5;
            this.markerConnectButton.Text = "Connect USB Marker";
            this.markerConnectButton.UseVisualStyleBackColor = true;
            this.markerConnectButton.Click += new System.EventHandler(this.markerConnectButton_Click);
            // 
            // MarkerDisconnectButton
            // 
            this.MarkerDisconnectButton.Location = new System.Drawing.Point(1652, 754);
            this.MarkerDisconnectButton.Margin = new System.Windows.Forms.Padding(4);
            this.MarkerDisconnectButton.Name = "MarkerDisconnectButton";
            this.MarkerDisconnectButton.Size = new System.Drawing.Size(173, 28);
            this.MarkerDisconnectButton.TabIndex = 6;
            this.MarkerDisconnectButton.Text = "Disconnect USB Marker";
            this.MarkerDisconnectButton.UseVisualStyleBackColor = true;
            this.MarkerDisconnectButton.Click += new System.EventHandler(this.MarkerDisconnectButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(83, 0);
            this.RefreshButton.Margin = new System.Windows.Forms.Padding(4);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(128, 28);
            this.RefreshButton.TabIndex = 7;
            this.RefreshButton.Text = "Refresh Orders";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // EditingContextButton
            // 
            this.EditingContextButton.Location = new System.Drawing.Point(1834, 754);
            this.EditingContextButton.Margin = new System.Windows.Forms.Padding(4);
            this.EditingContextButton.Name = "EditingContextButton";
            this.EditingContextButton.Size = new System.Drawing.Size(117, 28);
            this.EditingContextButton.TabIndex = 10;
            this.EditingContextButton.Text = "Editing Context";
            this.EditingContextButton.UseVisualStyleBackColor = true;
            this.EditingContextButton.Click += new System.EventHandler(this.EditingContextButton_Click);
            // 
            // ControllerContextButton
            // 
            this.ControllerContextButton.Location = new System.Drawing.Point(1818, 786);
            this.ControllerContextButton.Margin = new System.Windows.Forms.Padding(4);
            this.ControllerContextButton.Name = "ControllerContextButton";
            this.ControllerContextButton.Size = new System.Drawing.Size(135, 28);
            this.ControllerContextButton.TabIndex = 11;
            this.ControllerContextButton.Text = "Controller Context";
            this.ControllerContextButton.UseVisualStyleBackColor = true;
            this.ControllerContextButton.Click += new System.EventHandler(this.ControllerContextButton_Click);
            // 
            // LightOnButton
            // 
            this.LightOnButton.Location = new System.Drawing.Point(1707, 143);
            this.LightOnButton.Margin = new System.Windows.Forms.Padding(4);
            this.LightOnButton.Name = "LightOnButton";
            this.LightOnButton.Size = new System.Drawing.Size(123, 65);
            this.LightOnButton.TabIndex = 12;
            this.LightOnButton.Text = "Light On";
            this.LightOnButton.UseVisualStyleBackColor = true;
            this.LightOnButton.Click += new System.EventHandler(this.LightOnButton_Click);
            // 
            // LightOffButton
            // 
            this.LightOffButton.Location = new System.Drawing.Point(1838, 143);
            this.LightOffButton.Margin = new System.Windows.Forms.Padding(4);
            this.LightOffButton.Name = "LightOffButton";
            this.LightOffButton.Size = new System.Drawing.Size(123, 66);
            this.LightOffButton.TabIndex = 13;
            this.LightOffButton.Text = "Light Off";
            this.LightOffButton.UseVisualStyleBackColor = true;
            this.LightOffButton.Click += new System.EventHandler(this.LightOffButton_Click);
            // 
            // JobTitleLabel
            // 
            this.JobTitleLabel.AutoSize = true;
            this.JobTitleLabel.Location = new System.Drawing.Point(397, 902);
            this.JobTitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.JobTitleLabel.Name = "JobTitleLabel";
            this.JobTitleLabel.Size = new System.Drawing.Size(93, 17);
            this.JobTitleLabel.TabIndex = 15;
            this.JobTitleLabel.Text = "Program Title";
            // 
            // ProgramMaterialCombo
            // 
            this.ProgramMaterialCombo.FormattingEnabled = true;
            this.ProgramMaterialCombo.Location = new System.Drawing.Point(177, 787);
            this.ProgramMaterialCombo.Margin = new System.Windows.Forms.Padding(4);
            this.ProgramMaterialCombo.Name = "ProgramMaterialCombo";
            this.ProgramMaterialCombo.Size = new System.Drawing.Size(160, 24);
            this.ProgramMaterialCombo.TabIndex = 19;
            this.ProgramMaterialCombo.SelectedIndexChanged += new System.EventHandler(this.ProgramMaterialCombo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 794);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "Material";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(367, 790);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "Size";
            // 
            // ProgramSizeCombo
            // 
            this.ProgramSizeCombo.FormattingEnabled = true;
            this.ProgramSizeCombo.Location = new System.Drawing.Point(411, 787);
            this.ProgramSizeCombo.Margin = new System.Windows.Forms.Padding(4);
            this.ProgramSizeCombo.Name = "ProgramSizeCombo";
            this.ProgramSizeCombo.Size = new System.Drawing.Size(160, 24);
            this.ProgramSizeCombo.TabIndex = 23;
            this.ProgramSizeCombo.SelectedIndexChanged += new System.EventHandler(this.ProgramSizeCombo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(294, 902);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 20);
            this.label3.TabIndex = 24;
            this.label3.Text = "Program:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 3);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 20);
            this.label4.TabIndex = 25;
            this.label4.Text = "Orders:";
            // 
            // CameraFinderViewButton
            // 
            this.CameraFinderViewButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CameraFinderViewButton.Location = new System.Drawing.Point(1743, 75);
            this.CameraFinderViewButton.Margin = new System.Windows.Forms.Padding(4);
            this.CameraFinderViewButton.Name = "CameraFinderViewButton";
            this.CameraFinderViewButton.Size = new System.Drawing.Size(181, 60);
            this.CameraFinderViewButton.TabIndex = 26;
            this.CameraFinderViewButton.Text = "CameraFinderView";
            this.CameraFinderViewButton.UseVisualStyleBackColor = false;
            this.CameraFinderViewButton.Click += new System.EventHandler(this.CameraFinderViewButton_Click);
            // 
            // ShowFileListButton
            // 
            this.ShowFileListButton.Location = new System.Drawing.Point(1822, 858);
            this.ShowFileListButton.Margin = new System.Windows.Forms.Padding(4);
            this.ShowFileListButton.Name = "ShowFileListButton";
            this.ShowFileListButton.Size = new System.Drawing.Size(120, 28);
            this.ShowFileListButton.TabIndex = 27;
            this.ShowFileListButton.Text = "Show File List";
            this.ShowFileListButton.UseVisualStyleBackColor = true;
            this.ShowFileListButton.Click += new System.EventHandler(this.ShowFileListButton_Click);
            // 
            // Errors_Btn
            // 
            this.Errors_Btn.Location = new System.Drawing.Point(1758, 858);
            this.Errors_Btn.Margin = new System.Windows.Forms.Padding(4);
            this.Errors_Btn.Name = "Errors_Btn";
            this.Errors_Btn.Size = new System.Drawing.Size(60, 28);
            this.Errors_Btn.TabIndex = 28;
            this.Errors_Btn.Text = "Errors";
            this.Errors_Btn.UseVisualStyleBackColor = true;
            this.Errors_Btn.Click += new System.EventHandler(this.Errors_Btn_Click);
            // 
            // ClearErrors_Btn
            // 
            this.ClearErrors_Btn.Location = new System.Drawing.Point(1648, 858);
            this.ClearErrors_Btn.Margin = new System.Windows.Forms.Padding(4);
            this.ClearErrors_Btn.Name = "ClearErrors_Btn";
            this.ClearErrors_Btn.Size = new System.Drawing.Size(101, 28);
            this.ClearErrors_Btn.TabIndex = 29;
            this.ClearErrors_Btn.Text = "Clear Errors";
            this.ClearErrors_Btn.UseVisualStyleBackColor = true;
            this.ClearErrors_Btn.Click += new System.EventHandler(this.ClearErrors_Btn_Click);
            // 
            // DateBox
            // 
            this.DateBox.Location = new System.Drawing.Point(367, 598);
            this.DateBox.Margin = new System.Windows.Forms.Padding(4);
            this.DateBox.Name = "DateBox";
            this.DateBox.ReadOnly = true;
            this.DateBox.Size = new System.Drawing.Size(204, 22);
            this.DateBox.TabIndex = 32;
            // 
            // PartNumAndRevBox
            // 
            this.PartNumAndRevBox.Location = new System.Drawing.Point(367, 627);
            this.PartNumAndRevBox.Margin = new System.Windows.Forms.Padding(4);
            this.PartNumAndRevBox.Name = "PartNumAndRevBox";
            this.PartNumAndRevBox.Size = new System.Drawing.Size(204, 22);
            this.PartNumAndRevBox.TabIndex = 33;
            this.PartNumAndRevBox.TextChanged += new System.EventHandler(this.BlockText_TextChanged);
            // 
            // CustPartNumAndRevBox
            // 
            this.CustPartNumAndRevBox.Location = new System.Drawing.Point(367, 659);
            this.CustPartNumAndRevBox.Margin = new System.Windows.Forms.Padding(4);
            this.CustPartNumAndRevBox.Name = "CustPartNumAndRevBox";
            this.CustPartNumAndRevBox.Size = new System.Drawing.Size(204, 22);
            this.CustPartNumAndRevBox.TabIndex = 34;
            this.CustPartNumAndRevBox.TextChanged += new System.EventHandler(this.BlockText_TextChanged);
            // 
            // DescLine1Box
            // 
            this.DescLine1Box.Location = new System.Drawing.Point(219, 691);
            this.DescLine1Box.Margin = new System.Windows.Forms.Padding(4);
            this.DescLine1Box.Name = "DescLine1Box";
            this.DescLine1Box.Size = new System.Drawing.Size(352, 22);
            this.DescLine1Box.TabIndex = 35;
            this.DescLine1Box.TextChanged += new System.EventHandler(this.BlockText_TextChanged);
            // 
            // DescLine2Box
            // 
            this.DescLine2Box.Location = new System.Drawing.Point(219, 723);
            this.DescLine2Box.Margin = new System.Windows.Forms.Padding(4);
            this.DescLine2Box.Name = "DescLine2Box";
            this.DescLine2Box.Size = new System.Drawing.Size(352, 22);
            this.DescLine2Box.TabIndex = 36;
            this.DescLine2Box.TextChanged += new System.EventHandler(this.BlockText_TextChanged);
            // 
            // QRCodeDataBox
            // 
            this.QRCodeDataBox.Location = new System.Drawing.Point(1967, 41);
            this.QRCodeDataBox.Margin = new System.Windows.Forms.Padding(4);
            this.QRCodeDataBox.Name = "QRCodeDataBox";
            this.QRCodeDataBox.ReadOnly = true;
            this.QRCodeDataBox.Size = new System.Drawing.Size(37, 22);
            this.QRCodeDataBox.TabIndex = 38;
            this.QRCodeDataBox.Visible = false;
            this.QRCodeDataBox.TextChanged += new System.EventHandler(this.QRCodeDataBox_TextChanged);
            // 
            // FlipPartNumbersButton
            // 
            this.FlipPartNumbersButton.Location = new System.Drawing.Point(66, 627);
            this.FlipPartNumbersButton.Margin = new System.Windows.Forms.Padding(4);
            this.FlipPartNumbersButton.Name = "FlipPartNumbersButton";
            this.FlipPartNumbersButton.Size = new System.Drawing.Size(72, 52);
            this.FlipPartNumbersButton.TabIndex = 39;
            this.FlipPartNumbersButton.Text = "FLIPPN";
            this.FlipPartNumbersButton.UseVisualStyleBackColor = true;
            this.FlipPartNumbersButton.Click += new System.EventHandler(this.FlipPartNumbersButton_Click);
            // 
            // SetCameraPosition
            // 
            this.SetCameraPosition.Location = new System.Drawing.Point(1648, 894);
            this.SetCameraPosition.Margin = new System.Windows.Forms.Padding(4);
            this.SetCameraPosition.Name = "SetCameraPosition";
            this.SetCameraPosition.Size = new System.Drawing.Size(117, 28);
            this.SetCameraPosition.TabIndex = 43;
            this.SetCameraPosition.Text = "CameraPosition";
            this.SetCameraPosition.UseVisualStyleBackColor = true;
            this.SetCameraPosition.Click += new System.EventHandler(this.SetCameraPosition_Click);
            // 
            // OpenControllerJob
            // 
            this.OpenControllerJob.Location = new System.Drawing.Point(1726, 823);
            this.OpenControllerJob.Margin = new System.Windows.Forms.Padding(4);
            this.OpenControllerJob.Name = "OpenControllerJob";
            this.OpenControllerJob.Size = new System.Drawing.Size(135, 28);
            this.OpenControllerJob.TabIndex = 44;
            this.OpenControllerJob.Text = "OPN CNTRL JOB";
            this.OpenControllerJob.UseVisualStyleBackColor = true;
            this.OpenControllerJob.Click += new System.EventHandler(this.OpenControllerJob_Click);
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.FileName = "openFileDialog1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(54, 902);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 20);
            this.label5.TabIndex = 49;
            this.label5.Text = "Tube:";
            // 
            // SelectedMaterialPN
            // 
            this.SelectedMaterialPN.AutoSize = true;
            this.SelectedMaterialPN.Location = new System.Drawing.Point(126, 902);
            this.SelectedMaterialPN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SelectedMaterialPN.Name = "SelectedMaterialPN";
            this.SelectedMaterialPN.Size = new System.Drawing.Size(106, 17);
            this.SelectedMaterialPN.TabIndex = 50;
            this.SelectedMaterialPN.Text = "304LTS______";
            // 
            // GetOrderTubePNBTN
            // 
            this.GetOrderTubePNBTN.Location = new System.Drawing.Point(326, 0);
            this.GetOrderTubePNBTN.Margin = new System.Windows.Forms.Padding(4);
            this.GetOrderTubePNBTN.Name = "GetOrderTubePNBTN";
            this.GetOrderTubePNBTN.Size = new System.Drawing.Size(240, 28);
            this.GetOrderTubePNBTN.TabIndex = 51;
            this.GetOrderTubePNBTN.Text = "Get Order Tube PN (45 seconds)";
            this.GetOrderTubePNBTN.UseVisualStyleBackColor = true;
            this.GetOrderTubePNBTN.Click += new System.EventHandler(this.GetOrderTubePNBTN_Click);
            // 
            // AllPartNumBtn
            // 
            this.AllPartNumBtn.Location = new System.Drawing.Point(219, 0);
            this.AllPartNumBtn.Margin = new System.Windows.Forms.Padding(4);
            this.AllPartNumBtn.Name = "AllPartNumBtn";
            this.AllPartNumBtn.Size = new System.Drawing.Size(100, 28);
            this.AllPartNumBtn.TabIndex = 53;
            this.AllPartNumBtn.Text = "8xxxx";
            this.AllPartNumBtn.UseVisualStyleBackColor = true;
            this.AllPartNumBtn.Click += new System.EventHandler(this.AllPartNumBtn_Click);
            // 
            // GetLengthsBtn
            // 
            this.GetLengthsBtn.Location = new System.Drawing.Point(1864, 824);
            this.GetLengthsBtn.Margin = new System.Windows.Forms.Padding(4);
            this.GetLengthsBtn.Name = "GetLengthsBtn";
            this.GetLengthsBtn.Size = new System.Drawing.Size(103, 28);
            this.GetLengthsBtn.TabIndex = 54;
            this.GetLengthsBtn.Text = "GetTubeLengths";
            this.GetLengthsBtn.UseVisualStyleBackColor = true;
            this.GetLengthsBtn.Click += new System.EventHandler(this.GetLengthsBtn_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(17, 533);
            this.save.Margin = new System.Windows.Forms.Padding(4);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(101, 70);
            this.save.TabIndex = 55;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // Desc2Box
            // 
            this.Desc2Box.AutoSize = true;
            this.Desc2Box.Location = new System.Drawing.Point(580, 757);
            this.Desc2Box.Margin = new System.Windows.Forms.Padding(4);
            this.Desc2Box.Name = "Desc2Box";
            this.Desc2Box.Size = new System.Drawing.Size(18, 17);
            this.Desc2Box.TabIndex = 58;
            this.Desc2Box.UseVisualStyleBackColor = true;
            this.Desc2Box.CheckedChanged += new System.EventHandler(this.Desc2Box_CheckedChanged);
            // 
            // btnOpenMarkerBuilder
            // 
            this.btnOpenMarkerBuilder.Location = new System.Drawing.Point(17, 820);
            this.btnOpenMarkerBuilder.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenMarkerBuilder.Name = "btnOpenMarkerBuilder";
            this.btnOpenMarkerBuilder.Size = new System.Drawing.Size(272, 74);
            this.btnOpenMarkerBuilder.TabIndex = 59;
            this.btnOpenMarkerBuilder.Text = "Open In Marker Builder";
            this.btnOpenMarkerBuilder.UseVisualStyleBackColor = true;
            this.btnOpenMarkerBuilder.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnOpenMarkerBuilder_MouseClick);
            // 
            // btnRefreshTag
            // 
            this.btnRefreshTag.Location = new System.Drawing.Point(297, 820);
            this.btnRefreshTag.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefreshTag.Name = "btnRefreshTag";
            this.btnRefreshTag.Size = new System.Drawing.Size(276, 74);
            this.btnRefreshTag.TabIndex = 60;
            this.btnRefreshTag.Text = "Reload (After Saving in Marker Builder)";
            this.btnRefreshTag.UseVisualStyleBackColor = true;
            this.btnRefreshTag.Click += new System.EventHandler(this.btnRefreshTag_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(265, 602);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 17);
            this.label6.TabIndex = 61;
            this.label6.Text = "Date (Block 2)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(167, 630);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(200, 17);
            this.label7.TabIndex = 62;
            this.label7.Text = "Primary Part Number (Block 3)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(145, 662);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(220, 17);
            this.label8.TabIndex = 63;
            this.label8.Text = "Secondary Part Number (Block 4)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(66, 694);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(151, 17);
            this.label9.TabIndex = 64;
            this.label9.Text = "Description 1 (Block 5)";
            // 
            // HeatBox
            // 
            this.HeatBox.Location = new System.Drawing.Point(219, 755);
            this.HeatBox.Margin = new System.Windows.Forms.Padding(4);
            this.HeatBox.Name = "HeatBox";
            this.HeatBox.Size = new System.Drawing.Size(352, 22);
            this.HeatBox.TabIndex = 65;
            this.HeatBox.TextChanged += new System.EventHandler(this.BlockText_TextChanged);
            // 
            // HeatCheckBox
            // 
            this.HeatCheckBox.AutoSize = true;
            this.HeatCheckBox.Location = new System.Drawing.Point(580, 788);
            this.HeatCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.HeatCheckBox.Name = "HeatCheckBox";
            this.HeatCheckBox.Size = new System.Drawing.Size(18, 17);
            this.HeatCheckBox.TabIndex = 66;
            this.HeatCheckBox.UseVisualStyleBackColor = true;
            this.HeatCheckBox.CheckedChanged += new System.EventHandler(this.HeatCheckBox_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(33, 33);
            this.panel1.TabIndex = 67;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Block 0",
            "Block 1",
            "Block 2",
            "Block 3",
            "Block 4",
            "Block 5",
            "Block 6",
            "Block 7"});
            this.comboBox1.Location = new System.Drawing.Point(1844, 324);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(96, 24);
            this.comboBox1.TabIndex = 69;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1653, 411);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 17);
            this.label10.TabIndex = 70;
            this.label10.Text = "Width";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1649, 492);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 17);
            this.label11.TabIndex = 72;
            this.label11.Text = "Height";
            // 
            // widthBox
            // 
            this.widthBox.Enabled = false;
            this.widthBox.Location = new System.Drawing.Point(1707, 384);
            this.widthBox.Margin = new System.Windows.Forms.Padding(4);
            this.widthBox.Name = "widthBox";
            this.widthBox.Size = new System.Drawing.Size(132, 22);
            this.widthBox.TabIndex = 73;
            this.widthBox.TextChanged += new System.EventHandler(this.widthBox_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label12.Location = new System.Drawing.Point(1703, 325);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(127, 25);
            this.label12.TabIndex = 75;
            this.label12.Text = "Block Editor";
            // 
            // heightBox
            // 
            this.heightBox.Enabled = false;
            this.heightBox.Location = new System.Drawing.Point(1703, 466);
            this.heightBox.Margin = new System.Windows.Forms.Padding(4);
            this.heightBox.Name = "heightBox";
            this.heightBox.Size = new System.Drawing.Size(132, 22);
            this.heightBox.TabIndex = 76;
            this.heightBox.TextChanged += new System.EventHandler(this.heightBox_TextChanged);
            // 
            // wp10
            // 
            this.wp10.Enabled = false;
            this.wp10.Location = new System.Drawing.Point(4, 4);
            this.wp10.Margin = new System.Windows.Forms.Padding(4);
            this.wp10.Name = "wp10";
            this.wp10.Size = new System.Drawing.Size(72, 28);
            this.wp10.TabIndex = 77;
            this.wp10.Text = "+ 10%";
            this.wp10.UseVisualStyleBackColor = true;
            this.wp10.Click += new System.EventHandler(this.wp10_Click);
            // 
            // wm10
            // 
            this.wm10.Enabled = false;
            this.wm10.Location = new System.Drawing.Point(4, 40);
            this.wm10.Margin = new System.Windows.Forms.Padding(4);
            this.wm10.Name = "wm10";
            this.wm10.Size = new System.Drawing.Size(72, 28);
            this.wm10.TabIndex = 78;
            this.wm10.Text = "- 10%";
            this.wm10.UseVisualStyleBackColor = true;
            this.wm10.Click += new System.EventHandler(this.wm10_Click);
            // 
            // wp1
            // 
            this.wp1.Enabled = false;
            this.wp1.Location = new System.Drawing.Point(84, 4);
            this.wp1.Margin = new System.Windows.Forms.Padding(4);
            this.wp1.Name = "wp1";
            this.wp1.Size = new System.Drawing.Size(72, 28);
            this.wp1.TabIndex = 79;
            this.wp1.Text = "+ 1%";
            this.wp1.UseVisualStyleBackColor = true;
            this.wp1.Click += new System.EventHandler(this.wp1_Click);
            // 
            // wm1
            // 
            this.wm1.Enabled = false;
            this.wm1.Location = new System.Drawing.Point(84, 40);
            this.wm1.Margin = new System.Windows.Forms.Padding(4);
            this.wm1.Name = "wm1";
            this.wm1.Size = new System.Drawing.Size(72, 28);
            this.wm1.TabIndex = 80;
            this.wm1.Text = "- 1%";
            this.wm1.UseVisualStyleBackColor = true;
            this.wm1.Click += new System.EventHandler(this.wm1_Click);
            // 
            // hp10
            // 
            this.hp10.Enabled = false;
            this.hp10.Location = new System.Drawing.Point(4, 4);
            this.hp10.Margin = new System.Windows.Forms.Padding(4);
            this.hp10.Name = "hp10";
            this.hp10.Size = new System.Drawing.Size(72, 28);
            this.hp10.TabIndex = 81;
            this.hp10.Text = "+ 10%";
            this.hp10.UseVisualStyleBackColor = true;
            this.hp10.Click += new System.EventHandler(this.hp10_Click);
            // 
            // hp1
            // 
            this.hp1.Enabled = false;
            this.hp1.Location = new System.Drawing.Point(84, 4);
            this.hp1.Margin = new System.Windows.Forms.Padding(4);
            this.hp1.Name = "hp1";
            this.hp1.Size = new System.Drawing.Size(72, 28);
            this.hp1.TabIndex = 82;
            this.hp1.Text = "+ 1%";
            this.hp1.UseVisualStyleBackColor = true;
            this.hp1.Click += new System.EventHandler(this.hp1_Click);
            // 
            // hm10
            // 
            this.hm10.Enabled = false;
            this.hm10.Location = new System.Drawing.Point(4, 40);
            this.hm10.Margin = new System.Windows.Forms.Padding(4);
            this.hm10.Name = "hm10";
            this.hm10.Size = new System.Drawing.Size(72, 28);
            this.hm10.TabIndex = 83;
            this.hm10.Text = "- 10%";
            this.hm10.UseVisualStyleBackColor = true;
            this.hm10.Click += new System.EventHandler(this.hm10_Click);
            // 
            // hm1
            // 
            this.hm1.Enabled = false;
            this.hm1.Location = new System.Drawing.Point(84, 40);
            this.hm1.Margin = new System.Windows.Forms.Padding(4);
            this.hm1.Name = "hm1";
            this.hm1.Size = new System.Drawing.Size(72, 28);
            this.hm1.TabIndex = 84;
            this.hm1.Text = "- 1%";
            this.hm1.UseVisualStyleBackColor = true;
            this.hm1.Click += new System.EventHandler(this.hm1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.wp10, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.wp1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.wm10, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.wm1, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1844, 363);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(160, 73);
            this.tableLayoutPanel1.TabIndex = 85;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.hp10, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.hp1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.hm1, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.hm10, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1844, 443);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(160, 73);
            this.tableLayoutPanel2.TabIndex = 86;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(152, 34);
            this.label13.TabIndex = 87;
            this.label13.Text = "DO NOT REMOVE --> \r\nIt\'s important\r\n";
            this.label13.Visible = false;
            // 
            // PlusOne
            // 
            this.PlusOne.Enabled = false;
            this.PlusOne.Location = new System.Drawing.Point(129, 4);
            this.PlusOne.Margin = new System.Windows.Forms.Padding(4);
            this.PlusOne.Name = "PlusOne";
            this.PlusOne.Size = new System.Drawing.Size(57, 28);
            this.PlusOne.TabIndex = 0;
            this.PlusOne.Text = "+ 1";
            this.PlusOne.UseVisualStyleBackColor = true;
            this.PlusOne.Click += new System.EventHandler(this.PlusOne_Click);
            // 
            // PlusTen
            // 
            this.PlusTen.Enabled = false;
            this.PlusTen.Location = new System.Drawing.Point(197, 4);
            this.PlusTen.Margin = new System.Windows.Forms.Padding(4);
            this.PlusTen.Name = "PlusTen";
            this.PlusTen.Size = new System.Drawing.Size(57, 28);
            this.PlusTen.TabIndex = 1;
            this.PlusTen.Text = "+ 10";
            this.PlusTen.UseVisualStyleBackColor = true;
            this.PlusTen.Click += new System.EventHandler(this.PlusTen_Click);
            // 
            // MinusTen
            // 
            this.MinusTen.Enabled = false;
            this.MinusTen.Location = new System.Drawing.Point(4, 4);
            this.MinusTen.Margin = new System.Windows.Forms.Padding(4);
            this.MinusTen.Name = "MinusTen";
            this.MinusTen.Size = new System.Drawing.Size(52, 27);
            this.MinusTen.TabIndex = 2;
            this.MinusTen.Text = "- 10";
            this.MinusTen.UseVisualStyleBackColor = true;
            this.MinusTen.Click += new System.EventHandler(this.MinusTen_Click);
            // 
            // MinusOne
            // 
            this.MinusOne.Enabled = false;
            this.MinusOne.Location = new System.Drawing.Point(64, 4);
            this.MinusOne.Margin = new System.Windows.Forms.Padding(4);
            this.MinusOne.Name = "MinusOne";
            this.MinusOne.Size = new System.Drawing.Size(57, 27);
            this.MinusOne.TabIndex = 3;
            this.MinusOne.Text = "- 1";
            this.MinusOne.UseVisualStyleBackColor = true;
            this.MinusOne.Click += new System.EventHandler(this.MinusOne_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1758, 520);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(105, 17);
            this.label15.TabIndex = 92;
            this.label15.Text = "Move on X-Axis";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel3.Controls.Add(this.MinusTen, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.MinusOne, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.PlusTen, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.PlusOne, 2, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(1686, 541);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(267, 39);
            this.tableLayoutPanel3.TabIndex = 93;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1647, 285);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(213, 17);
            this.label14.TabIndex = 94;
            this.label14.Text = "Marking For Production Number:";
            // 
            // POTxtBox
            // 
            this.POTxtBox.Location = new System.Drawing.Point(1862, 281);
            this.POTxtBox.Margin = new System.Windows.Forms.Padding(4);
            this.POTxtBox.Name = "POTxtBox";
            this.POTxtBox.Size = new System.Drawing.Size(132, 22);
            this.POTxtBox.TabIndex = 95;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(66, 761);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(152, 17);
            this.label16.TabIndex = 96;
            this.label16.Text = "Heat Number (Block 7)";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(66, 726);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(151, 17);
            this.label17.TabIndex = 97;
            this.label17.Text = "Description 2 (Block 6)";
            // 
            // Desc1Box
            // 
            this.Desc1Box.AutoSize = true;
            this.Desc1Box.Location = new System.Drawing.Point(579, 721);
            this.Desc1Box.Margin = new System.Windows.Forms.Padding(4);
            this.Desc1Box.Name = "Desc1Box";
            this.Desc1Box.Size = new System.Drawing.Size(18, 17);
            this.Desc1Box.TabIndex = 98;
            this.Desc1Box.UseVisualStyleBackColor = true;
            this.Desc1Box.CheckedChanged += new System.EventHandler(this.Desc1Box_CheckedChanged);
            // 
            // PN2Box
            // 
            this.PN2Box.AutoSize = true;
            this.PN2Box.Location = new System.Drawing.Point(580, 689);
            this.PN2Box.Margin = new System.Windows.Forms.Padding(4);
            this.PN2Box.Name = "PN2Box";
            this.PN2Box.Size = new System.Drawing.Size(18, 17);
            this.PN2Box.TabIndex = 99;
            this.PN2Box.UseVisualStyleBackColor = true;
            this.PN2Box.CheckedChanged += new System.EventHandler(this.PN2Box_CheckedChanged);
            // 
            // PN1Box
            // 
            this.PN1Box.AutoSize = true;
            this.PN1Box.Location = new System.Drawing.Point(580, 656);
            this.PN1Box.Margin = new System.Windows.Forms.Padding(4);
            this.PN1Box.Name = "PN1Box";
            this.PN1Box.Size = new System.Drawing.Size(18, 17);
            this.PN1Box.TabIndex = 100;
            this.PN1Box.UseVisualStyleBackColor = true;
            this.PN1Box.CheckedChanged += new System.EventHandler(this.PN1Box_CheckedChanged);
            // 
            // DateCheckBox
            // 
            this.DateCheckBox.AutoSize = true;
            this.DateCheckBox.Location = new System.Drawing.Point(579, 628);
            this.DateCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.DateCheckBox.Name = "DateCheckBox";
            this.DateCheckBox.Size = new System.Drawing.Size(18, 17);
            this.DateCheckBox.TabIndex = 101;
            this.DateCheckBox.UseVisualStyleBackColor = true;
            this.DateCheckBox.CheckedChanged += new System.EventHandler(this.DateCheckBox_CheckedChanged);
            // 
            // QRCheckBox
            // 
            this.QRCheckBox.AutoSize = true;
            this.QRCheckBox.Location = new System.Drawing.Point(1807, 21);
            this.QRCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.QRCheckBox.Name = "QRCheckBox";
            this.QRCheckBox.Size = new System.Drawing.Size(148, 21);
            this.QRCheckBox.TabIndex = 37;
            this.QRCheckBox.Text = "QR Code (Block 8)";
            this.QRCheckBox.UseVisualStyleBackColor = true;
            this.QRCheckBox.Visible = false;
            this.QRCheckBox.CheckedChanged += new System.EventHandler(this.QRCheckBox_CheckedChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(383, 547);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(183, 17);
            this.label18.TabIndex = 102;
            this.label18.Text = "Label Background (Block 0)";
            // 
            // LabelBox
            // 
            this.LabelBox.AutoSize = true;
            this.LabelBox.Location = new System.Drawing.Point(579, 574);
            this.LabelBox.Margin = new System.Windows.Forms.Padding(4);
            this.LabelBox.Name = "LabelBox";
            this.LabelBox.Size = new System.Drawing.Size(18, 17);
            this.LabelBox.TabIndex = 103;
            this.LabelBox.UseVisualStyleBackColor = true;
            this.LabelBox.CheckedChanged += new System.EventHandler(this.LabelBox_CheckedChanged);
            // 
            // ImageBox
            // 
            this.ImageBox.AutoSize = true;
            this.ImageBox.Location = new System.Drawing.Point(579, 599);
            this.ImageBox.Margin = new System.Windows.Forms.Padding(4);
            this.ImageBox.Name = "ImageBox";
            this.ImageBox.Size = new System.Drawing.Size(18, 17);
            this.ImageBox.TabIndex = 104;
            this.ImageBox.UseVisualStyleBackColor = true;
            this.ImageBox.CheckedChanged += new System.EventHandler(this.ImageBox_CheckedChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(126, 574);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(142, 17);
            this.label19.TabIndex = 105;
            this.label19.Text = "Image/Logo (Block 1)";
            // 
            // LogoComboBox
            // 
            this.LogoComboBox.FormattingEnabled = true;
            this.LogoComboBox.Location = new System.Drawing.Point(281, 569);
            this.LogoComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.LogoComboBox.Name = "LogoComboBox";
            this.LogoComboBox.Size = new System.Drawing.Size(287, 24);
            this.LogoComboBox.TabIndex = 106;
            this.LogoComboBox.SelectedIndexChanged += new System.EventHandler(this.LogoComboBox_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabLaserMarking);
            this.tabControl1.Controls.Add(this.tabBrazing);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(2002, 948);
            this.tabControl1.TabIndex = 107;
            // 
            // tabLaserMarking
            // 
            this.tabLaserMarking.Controls.Add(this.axMBActX2);
            this.tabLaserMarking.Controls.Add(this.OrdersGridView);
            this.tabLaserMarking.Controls.Add(this.Get_Z);
            this.tabLaserMarking.Controls.Add(this.Mark_Part);
            this.tabLaserMarking.Controls.Add(this.markerConnectButton);
            this.tabLaserMarking.Controls.Add(this.MarkerDisconnectButton);
            this.tabLaserMarking.Controls.Add(this.RefreshButton);
            this.tabLaserMarking.Controls.Add(this.EditingContextButton);
            this.tabLaserMarking.Controls.Add(this.ControllerContextButton);
            this.tabLaserMarking.Controls.Add(this.LightOnButton);
            this.tabLaserMarking.Controls.Add(this.LightOffButton);
            this.tabLaserMarking.Controls.Add(this.JobTitleLabel);
            this.tabLaserMarking.Controls.Add(this.ProgramMaterialCombo);
            this.tabLaserMarking.Controls.Add(this.label1);
            this.tabLaserMarking.Controls.Add(this.label2);
            this.tabLaserMarking.Controls.Add(this.ProgramSizeCombo);
            this.tabLaserMarking.Controls.Add(this.label3);
            this.tabLaserMarking.Controls.Add(this.label4);
            this.tabLaserMarking.Controls.Add(this.CameraFinderViewButton);
            this.tabLaserMarking.Controls.Add(this.ShowFileListButton);
            this.tabLaserMarking.Controls.Add(this.Errors_Btn);
            this.tabLaserMarking.Controls.Add(this.ClearErrors_Btn);
            this.tabLaserMarking.Controls.Add(this.PartNumAndRevBox);
            this.tabLaserMarking.Controls.Add(this.CustPartNumAndRevBox);
            this.tabLaserMarking.Controls.Add(this.DescLine1Box);
            this.tabLaserMarking.Controls.Add(this.DescLine2Box);
            this.tabLaserMarking.Controls.Add(this.DateBox);
            this.tabLaserMarking.Controls.Add(this.QRCheckBox);
            this.tabLaserMarking.Controls.Add(this.QRCodeDataBox);
            this.tabLaserMarking.Controls.Add(this.FlipPartNumbersButton);
            this.tabLaserMarking.Controls.Add(this.SetCameraPosition);
            this.tabLaserMarking.Controls.Add(this.OpenControllerJob);
            this.tabLaserMarking.Controls.Add(this.label5);
            this.tabLaserMarking.Controls.Add(this.SelectedMaterialPN);
            this.tabLaserMarking.Controls.Add(this.GetOrderTubePNBTN);
            this.tabLaserMarking.Controls.Add(this.AllPartNumBtn);
            this.tabLaserMarking.Controls.Add(this.GetLengthsBtn);
            this.tabLaserMarking.Controls.Add(this.save);
            this.tabLaserMarking.Controls.Add(this.Desc2Box);
            this.tabLaserMarking.Controls.Add(this.btnOpenMarkerBuilder);
            this.tabLaserMarking.Controls.Add(this.btnRefreshTag);
            this.tabLaserMarking.Controls.Add(this.label6);
            this.tabLaserMarking.Controls.Add(this.label7);
            this.tabLaserMarking.Controls.Add(this.label8);
            this.tabLaserMarking.Controls.Add(this.label9);
            this.tabLaserMarking.Controls.Add(this.HeatBox);
            this.tabLaserMarking.Controls.Add(this.HeatCheckBox);
            this.tabLaserMarking.Controls.Add(this.panel1);
            this.tabLaserMarking.Controls.Add(this.comboBox1);
            this.tabLaserMarking.Controls.Add(this.label10);
            this.tabLaserMarking.Controls.Add(this.label11);
            this.tabLaserMarking.Controls.Add(this.widthBox);
            this.tabLaserMarking.Controls.Add(this.label12);
            this.tabLaserMarking.Controls.Add(this.heightBox);
            this.tabLaserMarking.Controls.Add(this.tableLayoutPanel1);
            this.tabLaserMarking.Controls.Add(this.tableLayoutPanel2);
            this.tabLaserMarking.Controls.Add(this.label13);
            this.tabLaserMarking.Controls.Add(this.label15);
            this.tabLaserMarking.Controls.Add(this.tableLayoutPanel3);
            this.tabLaserMarking.Controls.Add(this.label14);
            this.tabLaserMarking.Controls.Add(this.POTxtBox);
            this.tabLaserMarking.Controls.Add(this.label16);
            this.tabLaserMarking.Controls.Add(this.label17);
            this.tabLaserMarking.Controls.Add(this.Desc1Box);
            this.tabLaserMarking.Controls.Add(this.PN2Box);
            this.tabLaserMarking.Controls.Add(this.PN1Box);
            this.tabLaserMarking.Controls.Add(this.DateCheckBox);
            this.tabLaserMarking.Controls.Add(this.label18);
            this.tabLaserMarking.Controls.Add(this.LabelBox);
            this.tabLaserMarking.Controls.Add(this.ImageBox);
            this.tabLaserMarking.Controls.Add(this.label19);
            this.tabLaserMarking.Controls.Add(this.LogoComboBox);
            this.tabLaserMarking.ImageIndex = 2;
            this.tabLaserMarking.Location = new System.Drawing.Point(4, 25);
            this.tabLaserMarking.Name = "tabLaserMarking";
            this.tabLaserMarking.Padding = new System.Windows.Forms.Padding(3);
            this.tabLaserMarking.Size = new System.Drawing.Size(1994, 919);
            this.tabLaserMarking.TabIndex = 0;
            this.tabLaserMarking.Text = "Laser Marking";
            this.tabLaserMarking.UseVisualStyleBackColor = true;
            // 
            // tabBrazing
            // 
            this.tabBrazing.Controls.Add(this.btnLoadResults);
            this.tabBrazing.Controls.Add(this.tbSearch);
            this.tabBrazing.Controls.Add(this.lblSearch);
            this.tabBrazing.Controls.Add(this.splitContainer1);
            this.tabBrazing.ImageIndex = 0;
            this.tabBrazing.Location = new System.Drawing.Point(4, 25);
            this.tabBrazing.Name = "tabBrazing";
            this.tabBrazing.Padding = new System.Windows.Forms.Padding(3);
            this.tabBrazing.Size = new System.Drawing.Size(1994, 919);
            this.tabBrazing.TabIndex = 1;
            this.tabBrazing.Text = "Brazing";
            this.tabBrazing.UseVisualStyleBackColor = true;
            // 
            // btnLoadResults
            // 
            this.btnLoadResults.Location = new System.Drawing.Point(337, 2);
            this.btnLoadResults.Name = "btnLoadResults";
            this.btnLoadResults.Size = new System.Drawing.Size(188, 29);
            this.btnLoadResults.TabIndex = 1;
            this.btnLoadResults.Text = "Reload";
            this.btnLoadResults.UseVisualStyleBackColor = true;
            this.btnLoadResults.Click += new System.EventHandler(this.btnLoadResults_Click);
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(67, 6);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(264, 22);
            this.tbSearch.TabIndex = 2;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(8, 8);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(53, 17);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Search";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 40);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvBrazeParameters);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cbFittingPN);
            this.splitContainer1.Panel2.Controls.Add(this.btnAddBrazeEntry);
            this.splitContainer1.Panel2.Controls.Add(this.txtComments);
            this.splitContainer1.Panel2.Controls.Add(this.txtBrazeRings);
            this.splitContainer1.Panel2.Controls.Add(this.txtJointClearance);
            this.splitContainer1.Panel2.Controls.Add(this.txtHeight);
            this.splitContainer1.Panel2.Controls.Add(this.txtDuration);
            this.splitContainer1.Panel2.Controls.Add(this.txtPowerLevel);
            this.splitContainer1.Panel2.Controls.Add(this.txtConePN);
            this.splitContainer1.Panel2.Controls.Add(this.txtTubeEndStyle);
            this.splitContainer1.Panel2.Controls.Add(this.txtFittingPN);
            this.splitContainer1.Panel2.Controls.Add(this.lblMeasuredJointClearance);
            this.splitContainer1.Panel2.Controls.Add(this.lblBrazeRings);
            this.splitContainer1.Panel2.Controls.Add(this.lblComments);
            this.splitContainer1.Panel2.Controls.Add(this.lblHeight);
            this.splitContainer1.Panel2.Controls.Add(this.lblDuration);
            this.splitContainer1.Panel2.Controls.Add(this.lblPowerLevel);
            this.splitContainer1.Panel2.Controls.Add(this.lblConePN);
            this.splitContainer1.Panel2.Controls.Add(this.lblTubeEndStyle);
            this.splitContainer1.Panel2.Controls.Add(this.lblFittingPN);
            this.splitContainer1.Panel2.Controls.Add(this.lblTubePN);
            this.splitContainer1.Panel2.Controls.Add(this.lblAddBrazeEntry);
            this.splitContainer1.Panel2.Controls.Add(this.txtTubePN);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1826, 879);
            this.splitContainer1.SplitterDistance = 345;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvBrazeParameters
            // 
            this.dgvBrazeParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBrazeParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBrazeParameters.Location = new System.Drawing.Point(0, 0);
            this.dgvBrazeParameters.Name = "dgvBrazeParameters";
            this.dgvBrazeParameters.RowTemplate.Height = 24;
            this.dgvBrazeParameters.Size = new System.Drawing.Size(1826, 345);
            this.dgvBrazeParameters.TabIndex = 0;
            // 
            // cbFittingPN
            // 
            this.cbFittingPN.Enabled = false;
            this.cbFittingPN.FormattingEnabled = true;
            this.cbFittingPN.Location = new System.Drawing.Point(369, 106);
            this.cbFittingPN.Name = "cbFittingPN";
            this.cbFittingPN.Size = new System.Drawing.Size(121, 24);
            this.cbFittingPN.TabIndex = 22;
            this.cbFittingPN.Visible = false;
            // 
            // btnAddBrazeEntry
            // 
            this.btnAddBrazeEntry.Location = new System.Drawing.Point(8, 368);
            this.btnAddBrazeEntry.Name = "btnAddBrazeEntry";
            this.btnAddBrazeEntry.Size = new System.Drawing.Size(338, 28);
            this.btnAddBrazeEntry.TabIndex = 21;
            this.btnAddBrazeEntry.Text = "Add Braze Entry";
            this.btnAddBrazeEntry.UseVisualStyleBackColor = true;
            this.btnAddBrazeEntry.Click += new System.EventHandler(this.btnAddBrazeEntry_Click);
            // 
            // txtComments
            // 
            this.txtComments.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtComments.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtComments.Location = new System.Drawing.Point(125, 304);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(223, 58);
            this.txtComments.TabIndex = 20;
            // 
            // txtBrazeRings
            // 
            this.txtBrazeRings.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtBrazeRings.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtBrazeRings.Location = new System.Drawing.Point(125, 276);
            this.txtBrazeRings.Name = "txtBrazeRings";
            this.txtBrazeRings.Size = new System.Drawing.Size(223, 22);
            this.txtBrazeRings.TabIndex = 19;
            // 
            // txtJointClearance
            // 
            this.txtJointClearance.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtJointClearance.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtJointClearance.Location = new System.Drawing.Point(125, 248);
            this.txtJointClearance.Name = "txtJointClearance";
            this.txtJointClearance.Size = new System.Drawing.Size(223, 22);
            this.txtJointClearance.TabIndex = 18;
            // 
            // txtHeight
            // 
            this.txtHeight.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtHeight.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtHeight.Location = new System.Drawing.Point(125, 220);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(223, 22);
            this.txtHeight.TabIndex = 17;
            // 
            // txtDuration
            // 
            this.txtDuration.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtDuration.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtDuration.Location = new System.Drawing.Point(125, 192);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(223, 22);
            this.txtDuration.TabIndex = 16;
            // 
            // txtPowerLevel
            // 
            this.txtPowerLevel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtPowerLevel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtPowerLevel.Location = new System.Drawing.Point(125, 164);
            this.txtPowerLevel.Name = "txtPowerLevel";
            this.txtPowerLevel.Size = new System.Drawing.Size(223, 22);
            this.txtPowerLevel.TabIndex = 15;
            // 
            // txtConePN
            // 
            this.txtConePN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtConePN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtConePN.Location = new System.Drawing.Point(125, 136);
            this.txtConePN.Name = "txtConePN";
            this.txtConePN.Size = new System.Drawing.Size(223, 22);
            this.txtConePN.TabIndex = 14;
            // 
            // txtTubeEndStyle
            // 
            this.txtTubeEndStyle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtTubeEndStyle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtTubeEndStyle.Enabled = false;
            this.txtTubeEndStyle.Location = new System.Drawing.Point(125, 108);
            this.txtTubeEndStyle.Name = "txtTubeEndStyle";
            this.txtTubeEndStyle.Size = new System.Drawing.Size(223, 22);
            this.txtTubeEndStyle.TabIndex = 13;
            // 
            // txtFittingPN
            // 
            this.txtFittingPN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtFittingPN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFittingPN.Location = new System.Drawing.Point(125, 79);
            this.txtFittingPN.Name = "txtFittingPN";
            this.txtFittingPN.Size = new System.Drawing.Size(223, 22);
            this.txtFittingPN.TabIndex = 12;
            // 
            // lblMeasuredJointClearance
            // 
            this.lblMeasuredJointClearance.AutoSize = true;
            this.lblMeasuredJointClearance.Location = new System.Drawing.Point(15, 248);
            this.lblMeasuredJointClearance.Name = "lblMeasuredJointClearance";
            this.lblMeasuredJointClearance.Size = new System.Drawing.Size(106, 17);
            this.lblMeasuredJointClearance.TabIndex = 9;
            this.lblMeasuredJointClearance.Text = "Joint Clearance";
            // 
            // lblBrazeRings
            // 
            this.lblBrazeRings.AutoSize = true;
            this.lblBrazeRings.Location = new System.Drawing.Point(8, 276);
            this.lblBrazeRings.Name = "lblBrazeRings";
            this.lblBrazeRings.Size = new System.Drawing.Size(113, 17);
            this.lblBrazeRings.TabIndex = 10;
            this.lblBrazeRings.Text = "# of Braze Rings";
            // 
            // lblComments
            // 
            this.lblComments.AutoSize = true;
            this.lblComments.Location = new System.Drawing.Point(39, 304);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(74, 17);
            this.lblComments.TabIndex = 11;
            this.lblComments.Text = "Comments";
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(64, 220);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(49, 17);
            this.lblHeight.TabIndex = 8;
            this.lblHeight.Text = "Height";
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(38, 192);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(83, 17);
            this.lblDuration.TabIndex = 7;
            this.lblDuration.Text = "Duration (s)";
            // 
            // lblPowerLevel
            // 
            this.lblPowerLevel.AutoSize = true;
            this.lblPowerLevel.Location = new System.Drawing.Point(10, 164);
            this.lblPowerLevel.Name = "lblPowerLevel";
            this.lblPowerLevel.Size = new System.Drawing.Size(111, 17);
            this.lblPowerLevel.TabIndex = 6;
            this.lblPowerLevel.Text = "Power Level (%)";
            // 
            // lblConePN
            // 
            this.lblConePN.AutoSize = true;
            this.lblConePN.Location = new System.Drawing.Point(49, 136);
            this.lblConePN.Name = "lblConePN";
            this.lblConePN.Size = new System.Drawing.Size(64, 17);
            this.lblConePN.TabIndex = 5;
            this.lblConePN.Text = "Cone PN";
            // 
            // lblTubeEndStyle
            // 
            this.lblTubeEndStyle.AutoSize = true;
            this.lblTubeEndStyle.Location = new System.Drawing.Point(8, 108);
            this.lblTubeEndStyle.Name = "lblTubeEndStyle";
            this.lblTubeEndStyle.Size = new System.Drawing.Size(105, 17);
            this.lblTubeEndStyle.TabIndex = 4;
            this.lblTubeEndStyle.Text = "Tube End Style";
            // 
            // lblFittingPN
            // 
            this.lblFittingPN.AutoSize = true;
            this.lblFittingPN.Location = new System.Drawing.Point(44, 82);
            this.lblFittingPN.Name = "lblFittingPN";
            this.lblFittingPN.Size = new System.Drawing.Size(69, 17);
            this.lblFittingPN.TabIndex = 3;
            this.lblFittingPN.Text = "Fitting PN";
            // 
            // lblTubePN
            // 
            this.lblTubePN.AutoSize = true;
            this.lblTubePN.Location = new System.Drawing.Point(49, 53);
            this.lblTubePN.Name = "lblTubePN";
            this.lblTubePN.Size = new System.Drawing.Size(64, 17);
            this.lblTubePN.TabIndex = 2;
            this.lblTubePN.Text = "Tube PN";
            // 
            // lblAddBrazeEntry
            // 
            this.lblAddBrazeEntry.AutoSize = true;
            this.lblAddBrazeEntry.Font = new System.Drawing.Font("Impact", 15.75F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddBrazeEntry.Location = new System.Drawing.Point(8, 11);
            this.lblAddBrazeEntry.Name = "lblAddBrazeEntry";
            this.lblAddBrazeEntry.Size = new System.Drawing.Size(199, 34);
            this.lblAddBrazeEntry.TabIndex = 1;
            this.lblAddBrazeEntry.Text = "Add Braze Entry";
            // 
            // txtTubePN
            // 
            this.txtTubePN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtTubePN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtTubePN.Location = new System.Drawing.Point(125, 50);
            this.txtTubePN.Name = "txtTubePN";
            this.txtTubePN.Size = new System.Drawing.Size(223, 22);
            this.txtTubePN.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "brazing128x128.png");
            this.imageList1.Images.SetKeyName(1, "laser128x128.png");
            this.imageList1.Images.SetKeyName(2, "lasernew128x128.png");
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2002, 948);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainWindow";
            this.Text = "Laser Marking";
            ((System.ComponentModel.ISupportInitialize)(this.axMBActX2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrdersGridView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabLaserMarking.ResumeLayout(false);
            this.tabLaserMarking.PerformLayout();
            this.tabBrazing.ResumeLayout(false);
            this.tabBrazing.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBrazeParameters)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxMBPLib2.AxMBActX axMBActX2;
        private System.Windows.Forms.DataGridView OrdersGridView;
        private System.Windows.Forms.Button Get_Z;
        private System.Windows.Forms.Button Mark_Part;
        private System.Windows.Forms.Button markerConnectButton;
        private System.Windows.Forms.Button MarkerDisconnectButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Button EditingContextButton;
        private System.Windows.Forms.Button ControllerContextButton;
        private System.Windows.Forms.Button LightOnButton;
        private System.Windows.Forms.Button LightOffButton;
        private System.Windows.Forms.Label JobTitleLabel;
        private System.Windows.Forms.ComboBox ProgramMaterialCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ProgramSizeCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button CameraFinderViewButton;
        private System.Windows.Forms.Button ShowFileListButton;
        private System.Windows.Forms.Button Errors_Btn;
        private System.Windows.Forms.Button ClearErrors_Btn;
        private System.Windows.Forms.TextBox DateBox;
        private System.Windows.Forms.TextBox PartNumAndRevBox;
        private System.Windows.Forms.TextBox CustPartNumAndRevBox;
        private System.Windows.Forms.TextBox DescLine1Box;
        private System.Windows.Forms.TextBox DescLine2Box;
        private System.Windows.Forms.TextBox QRCodeDataBox;
        private System.Windows.Forms.Button FlipPartNumbersButton;
        private System.Windows.Forms.Button SetCameraPosition;
        private System.Windows.Forms.Button OpenControllerJob;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label SelectedMaterialPN;
        private System.Windows.Forms.Button GetOrderTubePNBTN;
        private System.Windows.Forms.Button AllPartNumBtn;
        private System.Windows.Forms.Button GetLengthsBtn;
        private System.Windows.Forms.Button save;
        private CheckBox Desc2Box;
        private Button btnOpenMarkerBuilder;
        private Button btnRefreshTag;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox HeatBox;
        private CheckBox HeatCheckBox;
        private Panel panel1;
        private ComboBox comboBox1;
        private Label label10;
        private Label label11;
        private TextBox widthBox;
        private Label label12;
        private TextBox heightBox;
        private Button wp10;
        private Button wm10;
        private Button wp1;
        private Button wm1;
        private Button hp10;
        private Button hp1;
        private Button hm10;
        private Button hm1;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label13;
        private Button PlusOne;
        private Button PlusTen;
        private Button MinusTen;
        private Button MinusOne;
        private Label label15;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label14;
        private TextBox POTxtBox;
        private Label label16;
        private Label label17;
        private CheckBox Desc1Box;
        private CheckBox PN2Box;
        private CheckBox PN1Box;
        private CheckBox DateCheckBox;
        private CheckBox QRCheckBox;
        private Label label18;
        private CheckBox LabelBox;
        private CheckBox ImageBox;
        private Label label19;
        private ComboBox LogoComboBox;
        private TabControl tabControl1;
        private TabPage tabLaserMarking;
        private TabPage tabBrazing;
        private ImageList imageList1;
        private SplitContainer splitContainer1;
        private DataGridView dgvBrazeParameters;
        private TextBox tbSearch;
        private Label lblSearch;
        private Button btnLoadResults;
        private TextBox txtTubePN;
        private Label lblTubePN;
        private Label lblAddBrazeEntry;
        private Label lblDuration;
        private Label lblPowerLevel;
        private Label lblConePN;
        private Label lblTubeEndStyle;
        private Label lblFittingPN;
        private Label lblBrazeRings;
        private Label lblMeasuredJointClearance;
        private Label lblComments;
        private Label lblHeight;
        private TextBox txtFittingPN;
        private TextBox txtComments;
        private TextBox txtBrazeRings;
        private TextBox txtJointClearance;
        private TextBox txtHeight;
        private TextBox txtDuration;
        private TextBox txtPowerLevel;
        private TextBox txtConePN;
        private TextBox txtTubeEndStyle;
        private Button btnAddBrazeEntry;
        private ComboBox cbFittingPN;
        //private System.Windows.Forms.Button saveBtn;
    }
    public partial class frmSelectBOM : Form
    {
        private ComboBox comboBoxBOMs;
        private Button buttonBOM;
        private Label selectBOMLabel;

        public frmSelectBOM()
        {
            this.comboBoxBOMs = new ComboBox();
            this.buttonBOM = new Button();
            this.selectBOMLabel = new Label();
            this.SuspendLayout();

            this.comboBoxBOMs.Location = new System.Drawing.Point(39, 85);
            this.comboBoxBOMs.Name = "BOMs";
            this.comboBoxBOMs.Size = new System.Drawing.Size(230, 21);


            this.buttonBOM.Location = new System.Drawing.Point(39, 110);
            this.buttonBOM.Name = "buttonBOM";
            this.buttonBOM.Text = "Continue";
            this.buttonBOM.Size = new System.Drawing.Size(230, 21);
            this.buttonBOM.Click += new System.EventHandler(this.buttonBOM_Click);


            this.selectBOMLabel.Location = new System.Drawing.Point(39, 40);
            this.selectBOMLabel.AutoSize = true;
            this.selectBOMLabel.Size = new System.Drawing.Size(91, 13);
            this.selectBOMLabel.Text = "Multiple BOMs were found in the drawing. \nPlease select the BOM containing tube part \nnumbers and continue.";


            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.buttonBOM);
            this.Controls.Add(this.comboBoxBOMs);
            this.Controls.Add(this.selectBOMLabel);
            this.Name = "frmSelectBOM";
            this.Text = "frmSelectBOM";

            this.ResumeLayout(false);

        }
        public string selectedBOM { get; set; }
        public void SelectBOM(List<string> BOMnames) //initialize
        {
            Cursor = Cursors.WaitCursor;

            foreach (string BOMname in BOMnames)
            {
                Console.WriteLine(BOMname);
                comboBoxBOMs.Items.Add(BOMname);
            }
            Cursor = Cursors.Default;
        }

        private void loadForm(object sender, EventArgs e)
        {

        }

        private void buttonBOM_Click(object sender, EventArgs e)
        {
            if (comboBoxBOMs.SelectedIndex == -1)
            {
                MessageBox.Show("must select a BOM to continue", "SELECT BOM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.selectedBOM = comboBoxBOMs.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}


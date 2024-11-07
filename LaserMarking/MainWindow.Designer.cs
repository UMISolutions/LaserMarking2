
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxA = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxA2 = new System.Windows.Forms.TextBox();
            this.textBoxB = new System.Windows.Forms.TextBox();
            this.textBoxC = new System.Windows.Forms.TextBox();
            this.textBoxB2 = new System.Windows.Forms.TextBox();
            this.textBoxC2 = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.axMBActX2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrdersGridView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // axMBActX2
            // 
            this.axMBActX2.Enabled = true;
            this.axMBActX2.Location = new System.Drawing.Point(456, 12);
            this.axMBActX2.Name = "axMBActX2";
            this.axMBActX2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMBActX2.OcxState")));
            this.axMBActX2.Size = new System.Drawing.Size(775, 775);
            this.axMBActX2.TabIndex = 0;
            this.axMBActX2.UseWaitCursor = true;
            this.axMBActX2.EvMarkingEnd += new AxMBPLib2._DMBActXEvents_EvMarkingEndEventHandler(this.axMBActX1_EvMarkingEnd);
            // 
            // OrdersGridView
            // 
            this.OrdersGridView.AllowUserToAddRows = false;
            this.OrdersGridView.AllowUserToDeleteRows = false;
            this.OrdersGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OrdersGridView.Location = new System.Drawing.Point(14, 41);
            this.OrdersGridView.Name = "OrdersGridView";
            this.OrdersGridView.ReadOnly = true;
            this.OrdersGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.OrdersGridView.Size = new System.Drawing.Size(417, 410);
            this.OrdersGridView.TabIndex = 1;
            this.OrdersGridView.Click += new System.EventHandler(this.OrdersGridView_Click);
            // 
            // Get_Z
            // 
            this.Get_Z.Location = new System.Drawing.Point(1237, 687);
            this.Get_Z.Name = "Get_Z";
            this.Get_Z.Size = new System.Drawing.Size(52, 23);
            this.Get_Z.TabIndex = 2;
            this.Get_Z.Text = "Get Z";
            this.Get_Z.UseVisualStyleBackColor = true;
            this.Get_Z.Click += new System.EventHandler(this.Get_Z_Click);
            // 
            // Mark_Part
            // 
            this.Mark_Part.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Mark_Part.Location = new System.Drawing.Point(1262, 195);
            this.Mark_Part.Name = "Mark_Part";
            this.Mark_Part.Size = new System.Drawing.Size(235, 52);
            this.Mark_Part.TabIndex = 4;
            this.Mark_Part.Text = "Mark Part";
            this.Mark_Part.UseVisualStyleBackColor = false;
            this.Mark_Part.Click += new System.EventHandler(this.Mark_Part_Click);
            // 
            // markerConnectButton
            // 
            this.markerConnectButton.Location = new System.Drawing.Point(1237, 657);
            this.markerConnectButton.Name = "markerConnectButton";
            this.markerConnectButton.Size = new System.Drawing.Size(121, 23);
            this.markerConnectButton.TabIndex = 5;
            this.markerConnectButton.Text = "Connect USB Marker";
            this.markerConnectButton.UseVisualStyleBackColor = true;
            this.markerConnectButton.Click += new System.EventHandler(this.markerConnectButton_Click);
            // 
            // MarkerDisconnectButton
            // 
            this.MarkerDisconnectButton.Location = new System.Drawing.Point(1240, 631);
            this.MarkerDisconnectButton.Name = "MarkerDisconnectButton";
            this.MarkerDisconnectButton.Size = new System.Drawing.Size(130, 23);
            this.MarkerDisconnectButton.TabIndex = 6;
            this.MarkerDisconnectButton.Text = "Disconnect USB Marker";
            this.MarkerDisconnectButton.UseVisualStyleBackColor = true;
            this.MarkerDisconnectButton.Click += new System.EventHandler(this.MarkerDisconnectButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(69, 12);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(96, 23);
            this.RefreshButton.TabIndex = 7;
            this.RefreshButton.Text = "Refresh Orders";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // EditingContextButton
            // 
            this.EditingContextButton.Location = new System.Drawing.Point(1376, 631);
            this.EditingContextButton.Name = "EditingContextButton";
            this.EditingContextButton.Size = new System.Drawing.Size(88, 23);
            this.EditingContextButton.TabIndex = 10;
            this.EditingContextButton.Text = "Editing Context";
            this.EditingContextButton.UseVisualStyleBackColor = true;
            this.EditingContextButton.Click += new System.EventHandler(this.EditingContextButton_Click);
            // 
            // ControllerContextButton
            // 
            this.ControllerContextButton.Location = new System.Drawing.Point(1364, 657);
            this.ControllerContextButton.Name = "ControllerContextButton";
            this.ControllerContextButton.Size = new System.Drawing.Size(101, 23);
            this.ControllerContextButton.TabIndex = 11;
            this.ControllerContextButton.Text = "Controller Context";
            this.ControllerContextButton.UseVisualStyleBackColor = true;
            this.ControllerContextButton.Click += new System.EventHandler(this.ControllerContextButton_Click);
            // 
            // LightOnButton
            // 
            this.LightOnButton.Location = new System.Drawing.Point(1281, 135);
            this.LightOnButton.Name = "LightOnButton";
            this.LightOnButton.Size = new System.Drawing.Size(92, 53);
            this.LightOnButton.TabIndex = 12;
            this.LightOnButton.Text = "Light On";
            this.LightOnButton.UseVisualStyleBackColor = true;
            this.LightOnButton.Click += new System.EventHandler(this.LightOnButton_Click);
            // 
            // LightOffButton
            // 
            this.LightOffButton.Location = new System.Drawing.Point(1379, 135);
            this.LightOffButton.Name = "LightOffButton";
            this.LightOffButton.Size = new System.Drawing.Size(92, 54);
            this.LightOffButton.TabIndex = 13;
            this.LightOffButton.Text = "Light Off";
            this.LightOffButton.UseVisualStyleBackColor = true;
            this.LightOffButton.Click += new System.EventHandler(this.LightOffButton_Click);
            // 
            // JobTitleLabel
            // 
            this.JobTitleLabel.AutoSize = true;
            this.JobTitleLabel.Location = new System.Drawing.Point(299, 755);
            this.JobTitleLabel.Name = "JobTitleLabel";
            this.JobTitleLabel.Size = new System.Drawing.Size(69, 13);
            this.JobTitleLabel.TabIndex = 15;
            this.JobTitleLabel.Text = "Program Title";
            // 
            // ProgramMaterialCombo
            // 
            this.ProgramMaterialCombo.FormattingEnabled = true;
            this.ProgramMaterialCombo.Location = new System.Drawing.Point(134, 661);
            this.ProgramMaterialCombo.Name = "ProgramMaterialCombo";
            this.ProgramMaterialCombo.Size = new System.Drawing.Size(121, 21);
            this.ProgramMaterialCombo.TabIndex = 19;
            this.ProgramMaterialCombo.SelectedIndexChanged += new System.EventHandler(this.ProgramMaterialCombo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 667);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Material";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(277, 664);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Size";
            // 
            // ProgramSizeCombo
            // 
            this.ProgramSizeCombo.FormattingEnabled = true;
            this.ProgramSizeCombo.Location = new System.Drawing.Point(310, 661);
            this.ProgramSizeCombo.Name = "ProgramSizeCombo";
            this.ProgramSizeCombo.Size = new System.Drawing.Size(121, 21);
            this.ProgramSizeCombo.TabIndex = 23;
            this.ProgramSizeCombo.SelectedIndexChanged += new System.EventHandler(this.ProgramSizeCombo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(222, 755);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 24;
            this.label3.Text = "Program:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 16);
            this.label4.TabIndex = 25;
            this.label4.Text = "Orders:";
            // 
            // CameraFinderViewButton
            // 
            this.CameraFinderViewButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CameraFinderViewButton.Location = new System.Drawing.Point(1308, 80);
            this.CameraFinderViewButton.Name = "CameraFinderViewButton";
            this.CameraFinderViewButton.Size = new System.Drawing.Size(136, 49);
            this.CameraFinderViewButton.TabIndex = 26;
            this.CameraFinderViewButton.Text = "CameraFinderView";
            this.CameraFinderViewButton.UseVisualStyleBackColor = false;
            this.CameraFinderViewButton.Click += new System.EventHandler(this.CameraFinderViewButton_Click);
            // 
            // ShowFileListButton
            // 
            this.ShowFileListButton.Location = new System.Drawing.Point(1367, 716);
            this.ShowFileListButton.Name = "ShowFileListButton";
            this.ShowFileListButton.Size = new System.Drawing.Size(90, 23);
            this.ShowFileListButton.TabIndex = 27;
            this.ShowFileListButton.Text = "Show File List";
            this.ShowFileListButton.UseVisualStyleBackColor = true;
            this.ShowFileListButton.Click += new System.EventHandler(this.ShowFileListButton_Click);
            // 
            // Errors_Btn
            // 
            this.Errors_Btn.Location = new System.Drawing.Point(1319, 716);
            this.Errors_Btn.Name = "Errors_Btn";
            this.Errors_Btn.Size = new System.Drawing.Size(45, 23);
            this.Errors_Btn.TabIndex = 28;
            this.Errors_Btn.Text = "Errors";
            this.Errors_Btn.UseVisualStyleBackColor = true;
            this.Errors_Btn.Click += new System.EventHandler(this.Errors_Btn_Click);
            // 
            // ClearErrors_Btn
            // 
            this.ClearErrors_Btn.Location = new System.Drawing.Point(1237, 716);
            this.ClearErrors_Btn.Name = "ClearErrors_Btn";
            this.ClearErrors_Btn.Size = new System.Drawing.Size(76, 23);
            this.ClearErrors_Btn.TabIndex = 29;
            this.ClearErrors_Btn.Text = "Clear Errors";
            this.ClearErrors_Btn.UseVisualStyleBackColor = true;
            this.ClearErrors_Btn.Click += new System.EventHandler(this.ClearErrors_Btn_Click);
            // 
            // DateBox
            // 
            this.DateBox.Location = new System.Drawing.Point(277, 508);
            this.DateBox.Name = "DateBox";
            this.DateBox.ReadOnly = true;
            this.DateBox.Size = new System.Drawing.Size(154, 20);
            this.DateBox.TabIndex = 32;
            // 
            // PartNumAndRevBox
            // 
            this.PartNumAndRevBox.Location = new System.Drawing.Point(277, 531);
            this.PartNumAndRevBox.Name = "PartNumAndRevBox";
            this.PartNumAndRevBox.Size = new System.Drawing.Size(154, 20);
            this.PartNumAndRevBox.TabIndex = 33;
            this.PartNumAndRevBox.TextChanged += new System.EventHandler(this.BlockText_TextChanged);
            // 
            // CustPartNumAndRevBox
            // 
            this.CustPartNumAndRevBox.Location = new System.Drawing.Point(277, 557);
            this.CustPartNumAndRevBox.Name = "CustPartNumAndRevBox";
            this.CustPartNumAndRevBox.Size = new System.Drawing.Size(154, 20);
            this.CustPartNumAndRevBox.TabIndex = 34;
            this.CustPartNumAndRevBox.TextChanged += new System.EventHandler(this.BlockText_TextChanged);
            // 
            // DescLine1Box
            // 
            this.DescLine1Box.Location = new System.Drawing.Point(166, 583);
            this.DescLine1Box.Name = "DescLine1Box";
            this.DescLine1Box.Size = new System.Drawing.Size(265, 20);
            this.DescLine1Box.TabIndex = 35;
            this.DescLine1Box.TextChanged += new System.EventHandler(this.BlockText_TextChanged);
            // 
            // DescLine2Box
            // 
            this.DescLine2Box.Location = new System.Drawing.Point(166, 609);
            this.DescLine2Box.Name = "DescLine2Box";
            this.DescLine2Box.Size = new System.Drawing.Size(265, 20);
            this.DescLine2Box.TabIndex = 36;
            this.DescLine2Box.TextChanged += new System.EventHandler(this.BlockText_TextChanged);
            // 
            // QRCodeDataBox
            // 
            this.QRCodeDataBox.Location = new System.Drawing.Point(1475, 33);
            this.QRCodeDataBox.Name = "QRCodeDataBox";
            this.QRCodeDataBox.ReadOnly = true;
            this.QRCodeDataBox.Size = new System.Drawing.Size(29, 20);
            this.QRCodeDataBox.TabIndex = 38;
            this.QRCodeDataBox.Visible = false;
            this.QRCodeDataBox.TextChanged += new System.EventHandler(this.QRCodeDataBox_TextChanged);
            // 
            // FlipPartNumbersButton
            // 
            this.FlipPartNumbersButton.Location = new System.Drawing.Point(51, 531);
            this.FlipPartNumbersButton.Name = "FlipPartNumbersButton";
            this.FlipPartNumbersButton.Size = new System.Drawing.Size(54, 42);
            this.FlipPartNumbersButton.TabIndex = 39;
            this.FlipPartNumbersButton.Text = "FLIPPN";
            this.FlipPartNumbersButton.UseVisualStyleBackColor = true;
            this.FlipPartNumbersButton.Click += new System.EventHandler(this.FlipPartNumbersButton_Click);
            // 
            // SetCameraPosition
            // 
            this.SetCameraPosition.Location = new System.Drawing.Point(1237, 745);
            this.SetCameraPosition.Name = "SetCameraPosition";
            this.SetCameraPosition.Size = new System.Drawing.Size(88, 23);
            this.SetCameraPosition.TabIndex = 43;
            this.SetCameraPosition.Text = "CameraPosition";
            this.SetCameraPosition.UseVisualStyleBackColor = true;
            this.SetCameraPosition.Click += new System.EventHandler(this.SetCameraPosition_Click);
            // 
            // OpenControllerJob
            // 
            this.OpenControllerJob.Location = new System.Drawing.Point(1295, 687);
            this.OpenControllerJob.Name = "OpenControllerJob";
            this.OpenControllerJob.Size = new System.Drawing.Size(101, 23);
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
            this.label5.Location = new System.Drawing.Point(42, 755);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 49;
            this.label5.Text = "Tube:";
            // 
            // SelectedMaterialPN
            // 
            this.SelectedMaterialPN.AutoSize = true;
            this.SelectedMaterialPN.Location = new System.Drawing.Point(96, 755);
            this.SelectedMaterialPN.Name = "SelectedMaterialPN";
            this.SelectedMaterialPN.Size = new System.Drawing.Size(81, 13);
            this.SelectedMaterialPN.TabIndex = 50;
            this.SelectedMaterialPN.Text = "304LTS______";
            // 
            // GetOrderTubePNBTN
            // 
            this.GetOrderTubePNBTN.Location = new System.Drawing.Point(251, 12);
            this.GetOrderTubePNBTN.Name = "GetOrderTubePNBTN";
            this.GetOrderTubePNBTN.Size = new System.Drawing.Size(180, 23);
            this.GetOrderTubePNBTN.TabIndex = 51;
            this.GetOrderTubePNBTN.Text = "Get Order Tube PN (45 seconds)";
            this.GetOrderTubePNBTN.UseVisualStyleBackColor = true;
            this.GetOrderTubePNBTN.Click += new System.EventHandler(this.GetOrderTubePNBTN_Click);
            // 
            // AllPartNumBtn
            // 
            this.AllPartNumBtn.Location = new System.Drawing.Point(171, 12);
            this.AllPartNumBtn.Name = "AllPartNumBtn";
            this.AllPartNumBtn.Size = new System.Drawing.Size(75, 23);
            this.AllPartNumBtn.TabIndex = 53;
            this.AllPartNumBtn.Text = "8xxxx";
            this.AllPartNumBtn.UseVisualStyleBackColor = true;
            this.AllPartNumBtn.Click += new System.EventHandler(this.AllPartNumBtn_Click);
            // 
            // GetLengthsBtn
            // 
            this.GetLengthsBtn.Location = new System.Drawing.Point(1399, 688);
            this.GetLengthsBtn.Name = "GetLengthsBtn";
            this.GetLengthsBtn.Size = new System.Drawing.Size(77, 23);
            this.GetLengthsBtn.TabIndex = 54;
            this.GetLengthsBtn.Text = "GetTubeLengths";
            this.GetLengthsBtn.UseVisualStyleBackColor = true;
            this.GetLengthsBtn.Click += new System.EventHandler(this.GetLengthsBtn_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(14, 455);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(76, 57);
            this.save.TabIndex = 55;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // Desc2Box
            // 
            this.Desc2Box.AutoSize = true;
            this.Desc2Box.Location = new System.Drawing.Point(435, 615);
            this.Desc2Box.Name = "Desc2Box";
            this.Desc2Box.Size = new System.Drawing.Size(15, 14);
            this.Desc2Box.TabIndex = 58;
            this.Desc2Box.UseVisualStyleBackColor = true;
            this.Desc2Box.CheckedChanged += new System.EventHandler(this.Desc2Box_CheckedChanged);
            // 
            // btnOpenMarkerBuilder
            // 
            this.btnOpenMarkerBuilder.Location = new System.Drawing.Point(14, 688);
            this.btnOpenMarkerBuilder.Name = "btnOpenMarkerBuilder";
            this.btnOpenMarkerBuilder.Size = new System.Drawing.Size(204, 60);
            this.btnOpenMarkerBuilder.TabIndex = 59;
            this.btnOpenMarkerBuilder.Text = "Open In Marker Builder";
            this.btnOpenMarkerBuilder.UseVisualStyleBackColor = true;
            this.btnOpenMarkerBuilder.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnOpenMarkerBuilder_MouseClick);
            // 
            // btnRefreshTag
            // 
            this.btnRefreshTag.Location = new System.Drawing.Point(224, 688);
            this.btnRefreshTag.Name = "btnRefreshTag";
            this.btnRefreshTag.Size = new System.Drawing.Size(207, 60);
            this.btnRefreshTag.TabIndex = 60;
            this.btnRefreshTag.Text = "Reload (After Saving in Marker Builder)";
            this.btnRefreshTag.UseVisualStyleBackColor = true;
            this.btnRefreshTag.Click += new System.EventHandler(this.btnRefreshTag_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(200, 511);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 61;
            this.label6.Text = "Date (Block 2)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(127, 534);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 13);
            this.label7.TabIndex = 62;
            this.label7.Text = "Primary Part Number (Block 3)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(110, 560);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(165, 13);
            this.label8.TabIndex = 63;
            this.label8.Text = "Secondary Part Number (Block 4)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(51, 586);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 13);
            this.label9.TabIndex = 64;
            this.label9.Text = "Description 1 (Block 5)";
            // 
            // HeatBox
            // 
            this.HeatBox.Location = new System.Drawing.Point(166, 635);
            this.HeatBox.Name = "HeatBox";
            this.HeatBox.Size = new System.Drawing.Size(265, 20);
            this.HeatBox.TabIndex = 65;
            this.HeatBox.TextChanged += new System.EventHandler(this.BlockText_TextChanged);
            // 
            // HeatCheckBox
            // 
            this.HeatCheckBox.AutoSize = true;
            this.HeatCheckBox.Location = new System.Drawing.Point(435, 640);
            this.HeatCheckBox.Name = "HeatCheckBox";
            this.HeatCheckBox.Size = new System.Drawing.Size(15, 14);
            this.HeatCheckBox.TabIndex = 66;
            this.HeatCheckBox.UseVisualStyleBackColor = true;
            this.HeatCheckBox.CheckedChanged += new System.EventHandler(this.HeatCheckBox_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(1479, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(25, 27);
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
            this.comboBox1.Location = new System.Drawing.Point(1384, 282);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(73, 21);
            this.comboBox1.TabIndex = 69;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1240, 334);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 70;
            this.label10.Text = "Width";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1237, 400);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 72;
            this.label11.Text = "Height";
            // 
            // widthBox
            // 
            this.widthBox.Enabled = false;
            this.widthBox.Location = new System.Drawing.Point(1281, 331);
            this.widthBox.Name = "widthBox";
            this.widthBox.Size = new System.Drawing.Size(100, 20);
            this.widthBox.TabIndex = 73;
            this.widthBox.TextChanged += new System.EventHandler(this.widthBox_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label12.Location = new System.Drawing.Point(1278, 283);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 20);
            this.label12.TabIndex = 75;
            this.label12.Text = "Block Editor";
            // 
            // heightBox
            // 
            this.heightBox.Enabled = false;
            this.heightBox.Location = new System.Drawing.Point(1278, 397);
            this.heightBox.Name = "heightBox";
            this.heightBox.Size = new System.Drawing.Size(100, 20);
            this.heightBox.TabIndex = 76;
            this.heightBox.TextChanged += new System.EventHandler(this.heightBox_TextChanged);
            // 
            // wp10
            // 
            this.wp10.Enabled = false;
            this.wp10.Location = new System.Drawing.Point(3, 3);
            this.wp10.Name = "wp10";
            this.wp10.Size = new System.Drawing.Size(54, 23);
            this.wp10.TabIndex = 77;
            this.wp10.Text = "+ 10%";
            this.wp10.UseVisualStyleBackColor = true;
            this.wp10.Click += new System.EventHandler(this.wp10_Click);
            // 
            // wm10
            // 
            this.wm10.Enabled = false;
            this.wm10.Location = new System.Drawing.Point(3, 32);
            this.wm10.Name = "wm10";
            this.wm10.Size = new System.Drawing.Size(54, 23);
            this.wm10.TabIndex = 78;
            this.wm10.Text = "- 10%";
            this.wm10.UseVisualStyleBackColor = true;
            this.wm10.Click += new System.EventHandler(this.wm10_Click);
            // 
            // wp1
            // 
            this.wp1.Enabled = false;
            this.wp1.Location = new System.Drawing.Point(63, 3);
            this.wp1.Name = "wp1";
            this.wp1.Size = new System.Drawing.Size(54, 23);
            this.wp1.TabIndex = 79;
            this.wp1.Text = "+ 1%";
            this.wp1.UseVisualStyleBackColor = true;
            this.wp1.Click += new System.EventHandler(this.wp1_Click);
            // 
            // wm1
            // 
            this.wm1.Enabled = false;
            this.wm1.Location = new System.Drawing.Point(63, 32);
            this.wm1.Name = "wm1";
            this.wm1.Size = new System.Drawing.Size(54, 23);
            this.wm1.TabIndex = 80;
            this.wm1.Text = "- 1%";
            this.wm1.UseVisualStyleBackColor = true;
            this.wm1.Click += new System.EventHandler(this.wm1_Click);
            // 
            // hp10
            // 
            this.hp10.Enabled = false;
            this.hp10.Location = new System.Drawing.Point(3, 3);
            this.hp10.Name = "hp10";
            this.hp10.Size = new System.Drawing.Size(54, 23);
            this.hp10.TabIndex = 81;
            this.hp10.Text = "+ 10%";
            this.hp10.UseVisualStyleBackColor = true;
            this.hp10.Click += new System.EventHandler(this.hp10_Click);
            // 
            // hp1
            // 
            this.hp1.Enabled = false;
            this.hp1.Location = new System.Drawing.Point(63, 3);
            this.hp1.Name = "hp1";
            this.hp1.Size = new System.Drawing.Size(54, 23);
            this.hp1.TabIndex = 82;
            this.hp1.Text = "+ 1%";
            this.hp1.UseVisualStyleBackColor = true;
            this.hp1.Click += new System.EventHandler(this.hp1_Click);
            // 
            // hm10
            // 
            this.hm10.Enabled = false;
            this.hm10.Location = new System.Drawing.Point(3, 32);
            this.hm10.Name = "hm10";
            this.hm10.Size = new System.Drawing.Size(54, 23);
            this.hm10.TabIndex = 83;
            this.hm10.Text = "- 10%";
            this.hm10.UseVisualStyleBackColor = true;
            this.hm10.Click += new System.EventHandler(this.hm10_Click);
            // 
            // hm1
            // 
            this.hm1.Enabled = false;
            this.hm1.Location = new System.Drawing.Point(63, 32);
            this.hm1.Name = "hm1";
            this.hm1.Size = new System.Drawing.Size(54, 23);
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1384, 314);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(120, 59);
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1384, 379);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(120, 59);
            this.tableLayoutPanel2.TabIndex = 86;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1360, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 26);
            this.label13.TabIndex = 87;
            this.label13.Text = "DO NOT REMOVE --> \r\nIt\'s important\r\n";
            this.label13.Visible = false;
            // 
            // PlusOne
            // 
            this.PlusOne.Enabled = false;
            this.PlusOne.Location = new System.Drawing.Point(103, 3);
            this.PlusOne.Name = "PlusOne";
            this.PlusOne.Size = new System.Drawing.Size(43, 23);
            this.PlusOne.TabIndex = 0;
            this.PlusOne.Text = "+ 1";
            this.PlusOne.UseVisualStyleBackColor = true;
            this.PlusOne.Click += new System.EventHandler(this.PlusOne_Click);
            // 
            // PlusTen
            // 
            this.PlusTen.Enabled = false;
            this.PlusTen.Location = new System.Drawing.Point(154, 3);
            this.PlusTen.Name = "PlusTen";
            this.PlusTen.Size = new System.Drawing.Size(43, 23);
            this.PlusTen.TabIndex = 1;
            this.PlusTen.Text = "+ 10";
            this.PlusTen.UseVisualStyleBackColor = true;
            this.PlusTen.Click += new System.EventHandler(this.PlusTen_Click);
            // 
            // MinusTen
            // 
            this.MinusTen.Enabled = false;
            this.MinusTen.Location = new System.Drawing.Point(3, 3);
            this.MinusTen.Name = "MinusTen";
            this.MinusTen.Size = new System.Drawing.Size(42, 22);
            this.MinusTen.TabIndex = 2;
            this.MinusTen.Text = "- 10";
            this.MinusTen.UseVisualStyleBackColor = true;
            this.MinusTen.Click += new System.EventHandler(this.MinusTen_Click);
            // 
            // MinusOne
            // 
            this.MinusOne.Enabled = false;
            this.MinusOne.Location = new System.Drawing.Point(51, 3);
            this.MinusOne.Name = "MinusOne";
            this.MinusOne.Size = new System.Drawing.Size(46, 22);
            this.MinusOne.TabIndex = 3;
            this.MinusOne.Text = "- 1";
            this.MinusOne.UseVisualStyleBackColor = true;
            this.MinusOne.Click += new System.EventHandler(this.MinusOne_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1319, 441);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(81, 13);
            this.label15.TabIndex = 92;
            this.label15.Text = "Move on X-Axis";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel3.Controls.Add(this.MinusTen, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.MinusOne, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.PlusTen, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.PlusOne, 2, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(1265, 458);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(200, 32);
            this.tableLayoutPanel3.TabIndex = 93;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1236, 250);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(160, 13);
            this.label14.TabIndex = 94;
            this.label14.Text = "Marking For Production Number:";
            // 
            // POTxtBox
            // 
            this.POTxtBox.Location = new System.Drawing.Point(1397, 247);
            this.POTxtBox.Name = "POTxtBox";
            this.POTxtBox.Size = new System.Drawing.Size(100, 20);
            this.POTxtBox.TabIndex = 95;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(51, 640);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(115, 13);
            this.label16.TabIndex = 96;
            this.label16.Text = "Heat Number (Block 7)";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(51, 612);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(114, 13);
            this.label17.TabIndex = 97;
            this.label17.Text = "Description 2 (Block 6)";
            // 
            // Desc1Box
            // 
            this.Desc1Box.AutoSize = true;
            this.Desc1Box.Location = new System.Drawing.Point(434, 586);
            this.Desc1Box.Name = "Desc1Box";
            this.Desc1Box.Size = new System.Drawing.Size(15, 14);
            this.Desc1Box.TabIndex = 98;
            this.Desc1Box.UseVisualStyleBackColor = true;
            this.Desc1Box.CheckedChanged += new System.EventHandler(this.Desc1Box_CheckedChanged);
            // 
            // PN2Box
            // 
            this.PN2Box.AutoSize = true;
            this.PN2Box.Location = new System.Drawing.Point(435, 560);
            this.PN2Box.Name = "PN2Box";
            this.PN2Box.Size = new System.Drawing.Size(15, 14);
            this.PN2Box.TabIndex = 99;
            this.PN2Box.UseVisualStyleBackColor = true;
            this.PN2Box.CheckedChanged += new System.EventHandler(this.PN2Box_CheckedChanged);
            // 
            // PN1Box
            // 
            this.PN1Box.AutoSize = true;
            this.PN1Box.Location = new System.Drawing.Point(435, 533);
            this.PN1Box.Name = "PN1Box";
            this.PN1Box.Size = new System.Drawing.Size(15, 14);
            this.PN1Box.TabIndex = 100;
            this.PN1Box.UseVisualStyleBackColor = true;
            this.PN1Box.CheckedChanged += new System.EventHandler(this.PN1Box_CheckedChanged);
            // 
            // DateCheckBox
            // 
            this.DateCheckBox.AutoSize = true;
            this.DateCheckBox.Location = new System.Drawing.Point(434, 510);
            this.DateCheckBox.Name = "DateCheckBox";
            this.DateCheckBox.Size = new System.Drawing.Size(15, 14);
            this.DateCheckBox.TabIndex = 101;
            this.DateCheckBox.UseVisualStyleBackColor = true;
            this.DateCheckBox.CheckedChanged += new System.EventHandler(this.DateCheckBox_CheckedChanged);
            // 
            // QRCheckBox
            // 
            this.QRCheckBox.AutoSize = true;
            this.QRCheckBox.Location = new System.Drawing.Point(1356, 36);
            this.QRCheckBox.Name = "QRCheckBox";
            this.QRCheckBox.Size = new System.Drawing.Size(115, 17);
            this.QRCheckBox.TabIndex = 37;
            this.QRCheckBox.Text = "QR Code (Block 8)";
            this.QRCheckBox.UseVisualStyleBackColor = true;
            this.QRCheckBox.Visible = false;
            this.QRCheckBox.CheckedChanged += new System.EventHandler(this.QRCheckBox_CheckedChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(289, 466);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(139, 13);
            this.label18.TabIndex = 102;
            this.label18.Text = "Label Background (Block 0)";
            // 
            // LabelBox
            // 
            this.LabelBox.AutoSize = true;
            this.LabelBox.Location = new System.Drawing.Point(434, 466);
            this.LabelBox.Name = "LabelBox";
            this.LabelBox.Size = new System.Drawing.Size(15, 14);
            this.LabelBox.TabIndex = 103;
            this.LabelBox.UseVisualStyleBackColor = true;
            this.LabelBox.CheckedChanged += new System.EventHandler(this.LabelBox_CheckedChanged);
            // 
            // ImageBox
            // 
            this.ImageBox.AutoSize = true;
            this.ImageBox.Location = new System.Drawing.Point(434, 487);
            this.ImageBox.Name = "ImageBox";
            this.ImageBox.Size = new System.Drawing.Size(15, 14);
            this.ImageBox.TabIndex = 104;
            this.ImageBox.UseVisualStyleBackColor = true;
            this.ImageBox.CheckedChanged += new System.EventHandler(this.ImageBox_CheckedChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(96, 488);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(110, 13);
            this.label19.TabIndex = 105;
            this.label19.Text = "Image/Logo (Block 1)";
            // 
            // LogoComboBox
            // 
            this.LogoComboBox.FormattingEnabled = true;
            this.LogoComboBox.Location = new System.Drawing.Point(212, 484);
            this.LogoComboBox.Name = "LogoComboBox";
            this.LogoComboBox.Size = new System.Drawing.Size(216, 21);
            this.LogoComboBox.TabIndex = 106;
            this.LogoComboBox.SelectedIndexChanged += new System.EventHandler(this.LogoComboBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1240, 496);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 107;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxA
            // 
            this.textBoxA.Location = new System.Drawing.Point(1240, 525);
            this.textBoxA.Name = "textBoxA";
            this.textBoxA.Size = new System.Drawing.Size(100, 20);
            this.textBoxA.TabIndex = 108;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1401, 496);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 109;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBoxA2
            // 
            this.textBoxA2.Location = new System.Drawing.Point(1397, 525);
            this.textBoxA2.Name = "textBoxA2";
            this.textBoxA2.Size = new System.Drawing.Size(100, 20);
            this.textBoxA2.TabIndex = 110;
            // 
            // textBoxB
            // 
            this.textBoxB.Location = new System.Drawing.Point(1240, 551);
            this.textBoxB.Name = "textBoxB";
            this.textBoxB.Size = new System.Drawing.Size(100, 20);
            this.textBoxB.TabIndex = 111;
            // 
            // textBoxC
            // 
            this.textBoxC.Location = new System.Drawing.Point(1240, 577);
            this.textBoxC.Name = "textBoxC";
            this.textBoxC.Size = new System.Drawing.Size(100, 20);
            this.textBoxC.TabIndex = 112;
            // 
            // textBoxB2
            // 
            this.textBoxB2.Location = new System.Drawing.Point(1397, 551);
            this.textBoxB2.Name = "textBoxB2";
            this.textBoxB2.Size = new System.Drawing.Size(100, 20);
            this.textBoxB2.TabIndex = 113;
            // 
            // textBoxC2
            // 
            this.textBoxC2.Location = new System.Drawing.Point(1397, 577);
            this.textBoxC2.Name = "textBoxC2";
            this.textBoxC2.Size = new System.Drawing.Size(100, 20);
            this.textBoxC2.TabIndex = 114;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "0000",
            "0001",
            "0002",
            "0003",
            "0004",
            "0005",
            "0006",
            "0007",
            "0008",
            "0009",
            "0010",
            "0011",
            "0012",
            "0013",
            "0014",
            "0015",
            "0016",
            "0017",
            "0018",
            "0019",
            "0020",
            "0021",
            "0022",
            "0023",
            "0024",
            "0025",
            "0026",
            "0027",
            "0028",
            "0029",
            "0030",
            "0031",
            "0032",
            "0033",
            "0034",
            "0035",
            "0036",
            "0037",
            "0038",
            "0039",
            "0040",
            "0041",
            "0042",
            "0043",
            "0044",
            "0045",
            "0046",
            "0047",
            "0048",
            "0049",
            "0050",
            "0051",
            "0052",
            "0053",
            "0054",
            "0055",
            "0056",
            "0057",
            "0058",
            "0059",
            "0060",
            "0061",
            "0062",
            "0063",
            "0064",
            "0065",
            "0066",
            "0067",
            "0068",
            "0069",
            "0070",
            "0071",
            "0072",
            "0073",
            "0074",
            "0075",
            "0076",
            "0077",
            "0078",
            "0079",
            "0080",
            "0081",
            "0082",
            "0083",
            "0084",
            "0085",
            "0086",
            "0087",
            "0088",
            "0089",
            "0090",
            "0091",
            "0092",
            "0093",
            "0094",
            "0095",
            "0096",
            "0097",
            "0098",
            "0099",
            "0100",
            "0101",
            "0102",
            "0103",
            "0104",
            "0105",
            "0106",
            "0107",
            "0108",
            "0109",
            "0110",
            "0111",
            "0112",
            "0113",
            "0114",
            "0115",
            "0116",
            "0117",
            "0118",
            "0119",
            "0120",
            "0121",
            "0122",
            "0123",
            "0124",
            "0125",
            "0126",
            "0127",
            "0128",
            "0129",
            "0130",
            "0131",
            "0132",
            "0133",
            "0134",
            "0135",
            "0136",
            "0137",
            "0138",
            "0139",
            "0140",
            "0141",
            "0142",
            "0143",
            "0144",
            "0145",
            "0146",
            "0147",
            "0148",
            "0149",
            "0150",
            "0151",
            "0152",
            "0153",
            "0154",
            "0155",
            "0156",
            "0157",
            "0158",
            "0159",
            "0160",
            "0161",
            "0162",
            "0163",
            "0164",
            "0165",
            "0166",
            "0167",
            "0168",
            "0169",
            "0170",
            "0171",
            "0172",
            "0173",
            "0174",
            "0175",
            "0176",
            "0177",
            "0178",
            "0179",
            "0180",
            "0181",
            "0182",
            "0183",
            "0184",
            "0185",
            "0186",
            "0187",
            "0188",
            "0189",
            "0190",
            "0191",
            "0192",
            "0193",
            "0194",
            "0195",
            "0196",
            "0197",
            "0198",
            "0199",
            "0200",
            "0201",
            "0202",
            "0203",
            "0204",
            "0205",
            "0206",
            "0207",
            "0208",
            "0209",
            "0210",
            "0211",
            "0212",
            "0213",
            "0214",
            "0215",
            "0216",
            "0217",
            "0218",
            "0219",
            "0220",
            "0221",
            "0222",
            "0223",
            "0224",
            "0225",
            "0226",
            "0227",
            "0228",
            "0229",
            "0230",
            "0231",
            "0232",
            "0233",
            "0234",
            "0235",
            "0236",
            "0237",
            "0238",
            "0239",
            "0240",
            "0241",
            "0242",
            "0243",
            "0244",
            "0245",
            "0246",
            "0247",
            "0248",
            "0249",
            "0250",
            "0251",
            "0252",
            "0253",
            "0254",
            "0255",
            "0256",
            "0257",
            "0258",
            "0259",
            "0260",
            "0261",
            "0262",
            "0263",
            "0264",
            "0265",
            "0266",
            "0267",
            "0268",
            "0269",
            "0270",
            "0271",
            "0272",
            "0273",
            "0274",
            "0275",
            "0276",
            "0277",
            "0278",
            "0279",
            "0280",
            "0281",
            "0282",
            "0283",
            "0284",
            "0285",
            "0286",
            "0287",
            "0288",
            "0289",
            "0290",
            "0291",
            "0292",
            "0293",
            "0294",
            "0295",
            "0296",
            "0297",
            "0298",
            "0299",
            "0300",
            "0301",
            "0302",
            "0303",
            "0304",
            "0305",
            "0306",
            "0307",
            "0308",
            "0309",
            "0310",
            "0311",
            "0312",
            "0313",
            "0314",
            "0315",
            "0316",
            "0317",
            "0318",
            "0319",
            "0320",
            "0321",
            "0322",
            "0323",
            "0324",
            "0325",
            "0326",
            "0327",
            "0328",
            "0329",
            "0330",
            "0331",
            "0332",
            "0333",
            "0334",
            "0335",
            "0336",
            "0337",
            "0338",
            "0339",
            "0340",
            "0341",
            "0342",
            "0343",
            "0344",
            "0345",
            "0346",
            "0347",
            "0348",
            "0349",
            "0350",
            "0351",
            "0352",
            "0353",
            "0354",
            "0355",
            "0356",
            "0357",
            "0358",
            "0359",
            "0360",
            "0361",
            "0362",
            "0363",
            "0364",
            "0365",
            "0366",
            "0367",
            "0368",
            "0369",
            "0370",
            "0371",
            "0372",
            "0373",
            "0374",
            "0375",
            "0376",
            "0377",
            "0378",
            "0379",
            "0380",
            "0381",
            "0382",
            "0383",
            "0384",
            "0385",
            "0386",
            "0387",
            "0388",
            "0389",
            "0390",
            "0391",
            "0392",
            "0393",
            "0394",
            "0395",
            "0396",
            "0397",
            "0398",
            "0399",
            "0400",
            "0401",
            "0402",
            "0403",
            "0404",
            "0405",
            "0406",
            "0407",
            "0408",
            "0409",
            "0410",
            "0411",
            "0412",
            "0413",
            "0414",
            "0415",
            "0416",
            "0417",
            "0418",
            "0419",
            "0420",
            "0421",
            "0422",
            "0423",
            "0424",
            "0425",
            "0426",
            "0427",
            "0428",
            "0429",
            "0430",
            "0431",
            "0432",
            "0433",
            "0434",
            "0435",
            "0436",
            "0437",
            "0438",
            "0439",
            "0440",
            "0441",
            "0442",
            "0443",
            "0444",
            "0445",
            "0446",
            "0447",
            "0448",
            "0449",
            "0450",
            "0451",
            "0452",
            "0453",
            "0454",
            "0455",
            "0456",
            "0457",
            "0458",
            "0459",
            "0460",
            "0461",
            "0462",
            "0463",
            "0464",
            "0465",
            "0466",
            "0467",
            "0468",
            "0469",
            "0470",
            "0471",
            "0472",
            "0473",
            "0474",
            "0475",
            "0476",
            "0477",
            "0478",
            "0479",
            "0480",
            "0481",
            "0482",
            "0483",
            "0484",
            "0485",
            "0486",
            "0487",
            "0488",
            "0489",
            "0490",
            "0491",
            "0492",
            "0493",
            "0494",
            "0495",
            "0496",
            "0497",
            "0498",
            "0499",
            "0500",
            "0501",
            "0502",
            "0503",
            "0504",
            "0505",
            "0506",
            "0507",
            "0508",
            "0509",
            "0510",
            "0511",
            "0512",
            "0513",
            "0514",
            "0515",
            "0516",
            "0517",
            "0518",
            "0519",
            "0520",
            "0521",
            "0522",
            "0523",
            "0524",
            "0525",
            "0526",
            "0527",
            "0528",
            "0529",
            "0530",
            "0531",
            "0532",
            "0533",
            "0534",
            "0535",
            "0536",
            "0537",
            "0538",
            "0539",
            "0540",
            "0541",
            "0542",
            "0543",
            "0544",
            "0545",
            "0546",
            "0547",
            "0548",
            "0549",
            "0550",
            "0551",
            "0552",
            "0553",
            "0554",
            "0555",
            "0556",
            "0557",
            "0558",
            "0559",
            "0560",
            "0561",
            "0562",
            "0563",
            "0564",
            "0565",
            "0566",
            "0567",
            "0568",
            "0569",
            "0570",
            "0571",
            "0572",
            "0573",
            "0574",
            "0575",
            "0576",
            "0577",
            "0578",
            "0579",
            "0580",
            "0581",
            "0582",
            "0583",
            "0584",
            "0585",
            "0586",
            "0587",
            "0588",
            "0589",
            "0590",
            "0591",
            "0592",
            "0593",
            "0594",
            "0595",
            "0596",
            "0597",
            "0598",
            "0599",
            "0600",
            "0601",
            "0602",
            "0603",
            "0604",
            "0605",
            "0606",
            "0607",
            "0608",
            "0609",
            "0610",
            "0611",
            "0612",
            "0613",
            "0614",
            "0615",
            "0616",
            "0617",
            "0618",
            "0619",
            "0620",
            "0621",
            "0622",
            "0623",
            "0624",
            "0625",
            "0626",
            "0627",
            "0628",
            "0629",
            "0630",
            "0631",
            "0632",
            "0633",
            "0634",
            "0635",
            "0636",
            "0637",
            "0638",
            "0639",
            "0640",
            "0641",
            "0642",
            "0643",
            "0644",
            "0645",
            "0646",
            "0647",
            "0648",
            "0649",
            "0650",
            "0651",
            "0652",
            "0653",
            "0654",
            "0655",
            "0656",
            "0657",
            "0658",
            "0659",
            "0660",
            "0661",
            "0662",
            "0663",
            "0664",
            "0665",
            "0666",
            "0667",
            "0668",
            "0669",
            "0670",
            "0671",
            "0672",
            "0673"});
            this.comboBox2.Location = new System.Drawing.Point(1308, 603);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(112, 21);
            this.comboBox2.TabIndex = 115;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1509, 793);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.textBoxC2);
            this.Controls.Add(this.textBoxB2);
            this.Controls.Add(this.textBoxC);
            this.Controls.Add(this.textBoxB);
            this.Controls.Add(this.textBoxA2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBoxA);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LogoComboBox);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.ImageBox);
            this.Controls.Add(this.LabelBox);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.DateCheckBox);
            this.Controls.Add(this.PN1Box);
            this.Controls.Add(this.PN2Box);
            this.Controls.Add(this.Desc1Box);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.POTxtBox);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.heightBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.widthBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.HeatCheckBox);
            this.Controls.Add(this.HeatBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnRefreshTag);
            this.Controls.Add(this.btnOpenMarkerBuilder);
            this.Controls.Add(this.Desc2Box);
            this.Controls.Add(this.save);
            this.Controls.Add(this.GetLengthsBtn);
            this.Controls.Add(this.AllPartNumBtn);
            this.Controls.Add(this.GetOrderTubePNBTN);
            this.Controls.Add(this.SelectedMaterialPN);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.OpenControllerJob);
            this.Controls.Add(this.SetCameraPosition);
            this.Controls.Add(this.FlipPartNumbersButton);
            this.Controls.Add(this.QRCodeDataBox);
            this.Controls.Add(this.QRCheckBox);
            this.Controls.Add(this.DescLine2Box);
            this.Controls.Add(this.DescLine1Box);
            this.Controls.Add(this.CustPartNumAndRevBox);
            this.Controls.Add(this.PartNumAndRevBox);
            this.Controls.Add(this.DateBox);
            this.Controls.Add(this.ClearErrors_Btn);
            this.Controls.Add(this.Errors_Btn);
            this.Controls.Add(this.ShowFileListButton);
            this.Controls.Add(this.CameraFinderViewButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ProgramSizeCombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProgramMaterialCombo);
            this.Controls.Add(this.JobTitleLabel);
            this.Controls.Add(this.LightOffButton);
            this.Controls.Add(this.LightOnButton);
            this.Controls.Add(this.ControllerContextButton);
            this.Controls.Add(this.EditingContextButton);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.MarkerDisconnectButton);
            this.Controls.Add(this.markerConnectButton);
            this.Controls.Add(this.Mark_Part);
            this.Controls.Add(this.Get_Z);
            this.Controls.Add(this.OrdersGridView);
            this.Controls.Add(this.axMBActX2);
            this.Name = "MainWindow";
            this.Text = "Laser Marking";
            ((System.ComponentModel.ISupportInitialize)(this.axMBActX2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrdersGridView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private Button button1;
        private TextBox textBoxA;
        private Button button2;
        private TextBox textBoxA2;
        private TextBox textBoxB;
        private TextBox textBoxC;
        private TextBox textBoxB2;
        private TextBox textBoxC2;
        private ComboBox comboBox2;
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


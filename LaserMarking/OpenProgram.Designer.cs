
namespace LaserMarking
{
    partial class OpenProgram
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
            this.label2 = new System.Windows.Forms.Label();
            this.ShowAllProgramsCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OpenExistingProgramCancel = new System.Windows.Forms.Button();
            this.OpenExistingProgramListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "The following programs have been found:";
            // 
            // ShowAllProgramsCheckBox
            // 
            this.ShowAllProgramsCheckBox.AutoSize = true;
            this.ShowAllProgramsCheckBox.Location = new System.Drawing.Point(9, 255);
            this.ShowAllProgramsCheckBox.Name = "ShowAllProgramsCheckBox";
            this.ShowAllProgramsCheckBox.Size = new System.Drawing.Size(114, 17);
            this.ShowAllProgramsCheckBox.TabIndex = 8;
            this.ShowAllProgramsCheckBox.Text = "Show All Programs";
            this.ShowAllProgramsCheckBox.UseVisualStyleBackColor = true;
            this.ShowAllProgramsCheckBox.CheckedChanged += new System.EventHandler(this.ShowAllProgramsCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Program:";
            // 
            // OpenExistingProgramCancel
            // 
            this.OpenExistingProgramCancel.Location = new System.Drawing.Point(168, 274);
            this.OpenExistingProgramCancel.Name = "OpenExistingProgramCancel";
            this.OpenExistingProgramCancel.Size = new System.Drawing.Size(75, 23);
            this.OpenExistingProgramCancel.TabIndex = 6;
            this.OpenExistingProgramCancel.Text = "Cancel";
            this.OpenExistingProgramCancel.UseVisualStyleBackColor = true;
            this.OpenExistingProgramCancel.Click += new System.EventHandler(this.OpenExistingProgramCancel_Click);
            // 
            // OpenExistingProgramListBox
            // 
            this.OpenExistingProgramListBox.FormattingEnabled = true;
            this.OpenExistingProgramListBox.Location = new System.Drawing.Point(12, 76);
            this.OpenExistingProgramListBox.Name = "OpenExistingProgramListBox";
            this.OpenExistingProgramListBox.Size = new System.Drawing.Size(262, 173);
            this.OpenExistingProgramListBox.TabIndex = 5;
            this.OpenExistingProgramListBox.DoubleClick += new System.EventHandler(this.OpenExistingProgramListBox_DoubleClick);
            // 
            // OpenProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 319);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ShowAllProgramsCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OpenExistingProgramCancel);
            this.Controls.Add(this.OpenExistingProgramListBox);
            this.Name = "OpenProgram";
            this.Text = "OpenProgram";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ShowAllProgramsCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OpenExistingProgramCancel;
        public System.Windows.Forms.ListBox OpenExistingProgramListBox;
    }
}
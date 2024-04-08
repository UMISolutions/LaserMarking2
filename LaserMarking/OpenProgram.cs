using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaserMarking
{
    public partial class OpenProgram : Form
    {
        List<FileInfo> tempList = new List<FileInfo>();

        public OpenProgram()
        {
            InitializeComponent();
        
        }

        private void ShowAllProgramsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            
            if (ShowAllProgramsCheckBox.Checked == true)
            {
                foreach (FileInfo thing in OpenExistingProgramListBox.Items)
                {
                    tempList.Add(thing);
                }
                OpenExistingProgramListBox.Items.Clear();
                DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"U:\Engineering\LaserMarkingProfiles");
                FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles("*" +  "*.*");
                
                OpenExistingProgramListBox.Items.AddRange(filesInDir);

            }
            else
            {
                try
                {
                    OpenExistingProgramListBox.Items.Clear();
                    foreach(FileInfo thing in tempList)
                    {
                        OpenExistingProgramListBox.Items.Add(thing);
                    }
                   
                }
                catch
                {

                }
            }
        }

        private void OpenExistingProgramCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenExistingProgramListBox_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}

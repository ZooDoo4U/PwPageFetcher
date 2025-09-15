using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PwCdpPageScrapper
{
    public partial class frmGetFileName : Form
    {
        private string fileName = "";
        public frmGetFileName()
        {
            InitializeComponent();
        }

        private void cmdSelectFile_Click( object sender, EventArgs e )
        {
            string fileName = txtFileName.Text.Trim();

            if(!string.IsNullOrWhiteSpace( fileName ))
            {
                saveFileDlg.FileName = fileName;
            }

            saveFileDlg.Filter = "All Files (*.*)|*.*|Data files (*.dat)|*.dat";
            saveFileDlg.ValidateNames = true;
            saveFileDlg.CheckFileExists = false;
            saveFileDlg.CheckPathExists = true;
            
            if(saveFileDlg.ShowDialog() == DialogResult.OK)
            {
                this.fileName = saveFileDlg.FileName;
                txtFileName.Text = this.fileName;
            }         
        }

        private void cmdOk_Click( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cmdCancel_Click( object sender, EventArgs e )
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

namespace PwCdpPageScrapper
{
    partial class frmGetFileName
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDlg = new OpenFileDialog();
            this.saveFileDlg = new SaveFileDialog();
            this.label1 = new Label();
            this.txtFileName = new TextBox();
            this.cmdSelectFile = new Button();
            this.cmdOk = new Button();
            this.cmdCancel = new Button();
            this.SuspendLayout();
            // 
            // openFileDlg
            // 
            this.openFileDlg.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new Point( 28, 29 );
            this.label1.Name = "label1";
            this.label1.Size = new Size( 97, 18 );
            this.label1.TabIndex = 0;
            this.label1.Text = "File Name:";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new Point( 155, 26 );
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new Size( 617, 27 );
            this.txtFileName.TabIndex = 1;
            // 
            // cmdSelectFile
            // 
            this.cmdSelectFile.Location = new Point( 34, 87 );
            this.cmdSelectFile.Name = "cmdSelectFile";
            this.cmdSelectFile.Size = new Size( 149, 30 );
            this.cmdSelectFile.TabIndex = 2;
            this.cmdSelectFile.Text = "Select &File";
            this.cmdSelectFile.UseVisualStyleBackColor = true;
            this.cmdSelectFile.Click += this.cmdSelectFile_Click;
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new Point( 326, 87 );
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new Size( 149, 30 );
            this.cmdOk.TabIndex = 3;
            this.cmdOk.Text = "&OK";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += this.cmdOk_Click;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new Point( 618, 87 );
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new Size( 149, 30 );
            this.cmdCancel.TabIndex = 4;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += this.cmdCancel_Click;
            // 
            // frmGetFileName
            // 
            this.AutoScaleDimensions = new SizeF( 10F, 18F );
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size( 800, 142 );
            this.Controls.Add( this.cmdCancel );
            this.Controls.Add( this.cmdOk );
            this.Controls.Add( this.cmdSelectFile );
            this.Controls.Add( this.txtFileName );
            this.Controls.Add( this.label1 );
            this.Name = "frmGetFileName";
            this.Text = "frmGetFileName";
            this.ResumeLayout( false );
            this.PerformLayout();
        }

        #endregion
        private OpenFileDialog openFileDlg;
        private SaveFileDialog saveFileDlg;
        private Label label1;
        private Button cmdSelectFile;
        private Button cmdOk;
        private Button cmdCancel;
        public TextBox txtFileName;
    }
}
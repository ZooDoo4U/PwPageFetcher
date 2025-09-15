namespace PwCdpPageScrapper
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdLaunch = new Button();
            this.cmdExitProgram = new Button();
            this.txtPageSource = new TextBox();
            this.cmdSubmit = new Button();
            this.cmdSave = new Button();
            this.cmdSavePageJS = new Button();
            this.txtJsonCode = new TextBox();
            this.label1 = new Label();
            this.lblHtmlModelName = new Label();
            this.SuspendLayout();
            // 
            // cmdLaunch
            // 
            this.cmdLaunch.Location = new Point( 16, 9 );
            this.cmdLaunch.Name = "cmdLaunch";
            this.cmdLaunch.Size = new Size( 151, 29 );
            this.cmdLaunch.TabIndex = 3;
            this.cmdLaunch.Text = "&Scrap Browser";
            this.cmdLaunch.UseVisualStyleBackColor = true;
            this.cmdLaunch.Click += this.cmdLaunch_Click;
            // 
            // cmdExitProgram
            // 
            this.cmdExitProgram.Location = new Point( 709, 9 );
            this.cmdExitProgram.Name = "cmdExitProgram";
            this.cmdExitProgram.Size = new Size( 75, 29 );
            this.cmdExitProgram.TabIndex = 4;
            this.cmdExitProgram.Text = "E&xit";
            this.cmdExitProgram.UseVisualStyleBackColor = true;
            this.cmdExitProgram.Click += this.cmdExitProgram_Click;
            // 
            // txtPageSource
            // 
            this.txtPageSource.Anchor =  AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtPageSource.Location = new Point( 16, 47 );
            this.txtPageSource.Multiline = true;
            this.txtPageSource.Name = "txtPageSource";
            this.txtPageSource.ScrollBars = ScrollBars.Both;
            this.txtPageSource.Size = new Size( 770, 102 );
            this.txtPageSource.TabIndex = 5;
            // 
            // cmdSubmit
            // 
            this.cmdSubmit.Location = new Point( 196, 9 );
            this.cmdSubmit.Name = "cmdSubmit";
            this.cmdSubmit.Size = new Size( 113, 29 );
            this.cmdSubmit.TabIndex = 6;
            this.cmdSubmit.Text = "Get Poms";
            this.cmdSubmit.UseVisualStyleBackColor = true;
            this.cmdSubmit.Visible = false;
            this.cmdSubmit.Click += this.cmdGetPoms_Click;
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new Point( 336, 9 );
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new Size( 170, 29 );
            this.cmdSave.TabIndex = 7;
            this.cmdSave.Text = "S&ave Raw Html";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Visible = false;
            this.cmdSave.Click += this.cmdSave_Click;
            // 
            // cmdSavePageJS
            // 
            this.cmdSavePageJS.Location = new Point( 362, 9 );
            this.cmdSavePageJS.Name = "cmdSavePageJS";
            this.cmdSavePageJS.Size = new Size( 152, 29 );
            this.cmdSavePageJS.TabIndex = 8;
            this.cmdSavePageJS.Text = "Save JS Code";
            this.cmdSavePageJS.UseVisualStyleBackColor = true;
            this.cmdSavePageJS.Click += this.cmdSavePageJS_Click;
            // 
            // txtJsonCode
            // 
            this.txtJsonCode.Anchor =  AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.txtJsonCode.Location = new Point( 16, 188 );
            this.txtJsonCode.Multiline = true;
            this.txtJsonCode.Name = "txtJsonCode";
            this.txtJsonCode.ScrollBars = ScrollBars.Both;
            this.txtJsonCode.Size = new Size( 770, 250 );
            this.txtJsonCode.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new Point( 16, 160 );
            this.label1.Name = "label1";
            this.label1.Size = new Size( 113, 18 );
            this.label1.TabIndex = 10;
            this.label1.Text = "Pom JS Code";
            this.label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblHtmlModelName
            // 
            this.lblHtmlModelName.Location = new Point( 141, 152 );
            this.lblHtmlModelName.Name = "lblHtmlModelName";
            this.lblHtmlModelName.Size = new Size( 647, 35 );
            this.lblHtmlModelName.TabIndex = 11;
            this.lblHtmlModelName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new SizeF( 10F, 18F );
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size( 800, 450 );
            this.Controls.Add( this.lblHtmlModelName );
            this.Controls.Add( this.label1 );
            this.Controls.Add( this.txtJsonCode );
            this.Controls.Add( this.cmdSavePageJS );
            this.Controls.Add( this.cmdSave );
            this.Controls.Add( this.cmdSubmit );
            this.Controls.Add( this.txtPageSource );
            this.Controls.Add( this.cmdExitProgram );
            this.Controls.Add( this.cmdLaunch );
            this.Name = "frmMain";
            this.Text = "Form1";
            this.ResumeLayout( false );
            this.PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtUrl;
        private Button cmdLaunch;
        private Button cmdExitProgram;
        private TextBox txtPageSource;
        private Button button1;
        private Button cmdSubmit;
        private Button cmdSave;
        private Button button2;
        private Button cmdSavePageJS;
        private TextBox textBox1;
        private TextBox txtJsonCode;
        private Label lblHtmlModelName;
    }
}

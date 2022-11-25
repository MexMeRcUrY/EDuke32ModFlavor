namespace MBlood_ModDocumentation
{
    partial class MbloodDocGenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MbloodDocGenForm));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.txtSourceFolderPath = new System.Windows.Forms.TextBox();
            this.btnSourceFolderPath = new System.Windows.Forms.Button();
            this.btnModCSVPath = new System.Windows.Forms.Button();
            this.txtModCSVPath = new System.Windows.Forms.TextBox();
            this.btnModOutputPath = new System.Windows.Forms.Button();
            this.txtModOutputPath = new System.Windows.Forms.TextBox();
            this.richTxtOutputMsg = new System.Windows.Forms.RichTextBox();
            this.lblOutputMsg = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chkDebug = new System.Windows.Forms.CheckBox();
            this.chkListExportIntems = new System.Windows.Forms.CheckedListBox();
            this.lblExportoptions = new System.Windows.Forms.Label();
            this.ChkDeleteOutputFolder = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSourceFolderPath
            // 
            this.txtSourceFolderPath.Location = new System.Drawing.Point(160, 39);
            this.txtSourceFolderPath.Name = "txtSourceFolderPath";
            this.txtSourceFolderPath.Size = new System.Drawing.Size(515, 23);
            this.txtSourceFolderPath.TabIndex = 0;
            // 
            // btnSourceFolderPath
            // 
            this.btnSourceFolderPath.Location = new System.Drawing.Point(21, 38);
            this.btnSourceFolderPath.Name = "btnSourceFolderPath";
            this.btnSourceFolderPath.Size = new System.Drawing.Size(133, 23);
            this.btnSourceFolderPath.TabIndex = 1;
            this.btnSourceFolderPath.Text = "Files Source Folder";
            this.btnSourceFolderPath.UseVisualStyleBackColor = true;
            this.btnSourceFolderPath.Click += new System.EventHandler(this.btnSourceFolderPath_Click);
            // 
            // btnModCSVPath
            // 
            this.btnModCSVPath.Location = new System.Drawing.Point(21, 78);
            this.btnModCSVPath.Name = "btnModCSVPath";
            this.btnModCSVPath.Size = new System.Drawing.Size(133, 23);
            this.btnModCSVPath.TabIndex = 3;
            this.btnModCSVPath.Text = "CSV File";
            this.btnModCSVPath.UseVisualStyleBackColor = true;
            this.btnModCSVPath.Click += new System.EventHandler(this.btnModCSVPath_Click);
            // 
            // txtModCSVPath
            // 
            this.txtModCSVPath.Location = new System.Drawing.Point(160, 79);
            this.txtModCSVPath.Name = "txtModCSVPath";
            this.txtModCSVPath.Size = new System.Drawing.Size(515, 23);
            this.txtModCSVPath.TabIndex = 2;
            // 
            // btnModOutputPath
            // 
            this.btnModOutputPath.Location = new System.Drawing.Point(21, 119);
            this.btnModOutputPath.Name = "btnModOutputPath";
            this.btnModOutputPath.Size = new System.Drawing.Size(133, 23);
            this.btnModOutputPath.TabIndex = 5;
            this.btnModOutputPath.Text = "MOD Output Folder";
            this.btnModOutputPath.UseVisualStyleBackColor = true;
            this.btnModOutputPath.Click += new System.EventHandler(this.btnModOutputPath_Click);
            // 
            // txtModOutputPath
            // 
            this.txtModOutputPath.Location = new System.Drawing.Point(160, 120);
            this.txtModOutputPath.Name = "txtModOutputPath";
            this.txtModOutputPath.Size = new System.Drawing.Size(515, 23);
            this.txtModOutputPath.TabIndex = 4;
            // 
            // richTxtOutputMsg
            // 
            this.richTxtOutputMsg.Location = new System.Drawing.Point(192, 200);
            this.richTxtOutputMsg.Name = "richTxtOutputMsg";
            this.richTxtOutputMsg.ReadOnly = true;
            this.richTxtOutputMsg.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTxtOutputMsg.Size = new System.Drawing.Size(483, 208);
            this.richTxtOutputMsg.TabIndex = 6;
            this.richTxtOutputMsg.Text = "";
            // 
            // lblOutputMsg
            // 
            this.lblOutputMsg.AutoSize = true;
            this.lblOutputMsg.Location = new System.Drawing.Point(192, 182);
            this.lblOutputMsg.Name = "lblOutputMsg";
            this.lblOutputMsg.Size = new System.Drawing.Size(102, 15);
            this.lblOutputMsg.TabIndex = 7;
            this.lblOutputMsg.Text = "Output Messages:";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(192, 411);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(336, 40);
            this.btnGenerate.TabIndex = 8;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(598, 413);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(99, 36);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 200);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(167, 157);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // chkDebug
            // 
            this.chkDebug.AutoSize = true;
            this.chkDebug.Location = new System.Drawing.Point(12, 430);
            this.chkDebug.Name = "chkDebug";
            this.chkDebug.Size = new System.Drawing.Size(61, 19);
            this.chkDebug.TabIndex = 11;
            this.chkDebug.Text = "Debug";
            this.chkDebug.UseVisualStyleBackColor = true;
            // 
            // chkListExportIntems
            // 
            this.chkListExportIntems.FormattingEnabled = true;
            this.chkListExportIntems.Items.AddRange(new object[] {
            "Tiles",
            "Models",
            "Voxels",
            "Map-Hacks",
            "Music",
            "Sounds",
            "CutScenes"});
            this.chkListExportIntems.Location = new System.Drawing.Point(683, 25);
            this.chkListExportIntems.Name = "chkListExportIntems";
            this.chkListExportIntems.Size = new System.Drawing.Size(110, 130);
            this.chkListExportIntems.TabIndex = 12;
            // 
            // lblExportoptions
            // 
            this.lblExportoptions.AutoSize = true;
            this.lblExportoptions.Location = new System.Drawing.Point(687, 8);
            this.lblExportoptions.Name = "lblExportoptions";
            this.lblExportoptions.Size = new System.Drawing.Size(86, 15);
            this.lblExportoptions.TabIndex = 13;
            this.lblExportoptions.Text = "Export Options";
            // 
            // ChkDeleteOutputFolder
            // 
            this.ChkDeleteOutputFolder.AutoSize = true;
            this.ChkDeleteOutputFolder.Checked = true;
            this.ChkDeleteOutputFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkDeleteOutputFolder.Location = new System.Drawing.Point(160, 149);
            this.ChkDeleteOutputFolder.Name = "ChkDeleteOutputFolder";
            this.ChkDeleteOutputFolder.Size = new System.Drawing.Size(246, 19);
            this.ChkDeleteOutputFolder.TabIndex = 14;
            this.ChkDeleteOutputFolder.Text = "Delete output TEMP files after ZIP Created";
            this.ChkDeleteOutputFolder.UseVisualStyleBackColor = true;
            // 
            // MbloodDocGenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ChkDeleteOutputFolder);
            this.Controls.Add(this.lblExportoptions);
            this.Controls.Add(this.chkListExportIntems);
            this.Controls.Add(this.chkDebug);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.lblOutputMsg);
            this.Controls.Add(this.richTxtOutputMsg);
            this.Controls.Add(this.btnModOutputPath);
            this.Controls.Add(this.txtModOutputPath);
            this.Controls.Add(this.btnModCSVPath);
            this.Controls.Add(this.txtModCSVPath);
            this.Controls.Add(this.btnSourceFolderPath);
            this.Controls.Add(this.txtSourceFolderPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MbloodDocGenForm";
            this.Text = "MBlood Documentation - > Mod Generator";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FolderBrowserDialog folderBrowserDialog;
        private TextBox txtSourceFolderPath;
        private Button btnSourceFolderPath;
        private Button btnModCSVPath;
        private TextBox txtModCSVPath;
        private Button btnModOutputPath;
        private TextBox txtModOutputPath;
        private RichTextBox richTxtOutputMsg;
        private Label lblOutputMsg;
        private Button btnGenerate;
        private Button btnClose;
        private OpenFileDialog openFileDialog1;
        private PictureBox pictureBox1;
        private CheckBox chkDebug;
        private CheckedListBox chkListExportIntems;
        private Label lblExportoptions;
        private CheckBox ChkDeleteOutputFolder;
    }
}
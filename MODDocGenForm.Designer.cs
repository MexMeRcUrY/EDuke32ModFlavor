namespace MBlood_ModDocumentation
{
    partial class MODDocGenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MODDocGenForm));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.TabTemplateExample = new System.Windows.Forms.TabControl();
            this.tabModGen = new System.Windows.Forms.TabPage();
            this.ChkDeleteOutputFolder = new System.Windows.Forms.CheckBox();
            this.lblExportoptions = new System.Windows.Forms.Label();
            this.chkListExportIntems = new System.Windows.Forms.CheckedListBox();
            this.chkDebug = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblOutputMsg = new System.Windows.Forms.Label();
            this.richTxtOutputMsg = new System.Windows.Forms.RichTextBox();
            this.btnModOutputPath = new System.Windows.Forms.Button();
            this.txtModOutputPath = new System.Windows.Forms.TextBox();
            this.btnModCSVPath = new System.Windows.Forms.Button();
            this.txtModCSVPath = new System.Windows.Forms.TextBox();
            this.btnSourceFolderPath = new System.Windows.Forms.Button();
            this.txtSourceFolderPath = new System.Windows.Forms.TextBox();
            this.tabDEFReverseEng = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.lblDEFCSVOutputMsg = new System.Windows.Forms.Label();
            this.richDEF2CSVOutputMsg = new System.Windows.Forms.RichTextBox();
            this.btnDEFGenCSV = new System.Windows.Forms.Button();
            this.txtDEFCSVOutput = new System.Windows.Forms.TextBox();
            this.btnDEFCSVOutput = new System.Windows.Forms.Button();
            this.txtDEFSource = new System.Windows.Forms.TextBox();
            this.btnDEFSource = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnExportTemplate = new System.Windows.Forms.Button();
            this.txtTemplateOutput = new System.Windows.Forms.TextBox();
            this.btnTemplateOut = new System.Windows.Forms.Button();
            this.TabTemplateExample.SuspendLayout();
            this.tabModGen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabDEFReverseEng.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // TabTemplateExample
            // 
            this.TabTemplateExample.Controls.Add(this.tabModGen);
            this.TabTemplateExample.Controls.Add(this.tabDEFReverseEng);
            this.TabTemplateExample.Controls.Add(this.tabPage1);
            this.TabTemplateExample.Location = new System.Drawing.Point(1, 2);
            this.TabTemplateExample.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabTemplateExample.Name = "TabTemplateExample";
            this.TabTemplateExample.SelectedIndex = 0;
            this.TabTemplateExample.Size = new System.Drawing.Size(1157, 783);
            this.TabTemplateExample.TabIndex = 15;
            // 
            // tabModGen
            // 
            this.tabModGen.Controls.Add(this.ChkDeleteOutputFolder);
            this.tabModGen.Controls.Add(this.lblExportoptions);
            this.tabModGen.Controls.Add(this.chkListExportIntems);
            this.tabModGen.Controls.Add(this.chkDebug);
            this.tabModGen.Controls.Add(this.pictureBox1);
            this.tabModGen.Controls.Add(this.btnClose);
            this.tabModGen.Controls.Add(this.btnGenerate);
            this.tabModGen.Controls.Add(this.lblOutputMsg);
            this.tabModGen.Controls.Add(this.richTxtOutputMsg);
            this.tabModGen.Controls.Add(this.btnModOutputPath);
            this.tabModGen.Controls.Add(this.txtModOutputPath);
            this.tabModGen.Controls.Add(this.btnModCSVPath);
            this.tabModGen.Controls.Add(this.txtModCSVPath);
            this.tabModGen.Controls.Add(this.btnSourceFolderPath);
            this.tabModGen.Controls.Add(this.txtSourceFolderPath);
            this.tabModGen.Location = new System.Drawing.Point(4, 34);
            this.tabModGen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabModGen.Name = "tabModGen";
            this.tabModGen.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabModGen.Size = new System.Drawing.Size(1149, 745);
            this.tabModGen.TabIndex = 0;
            this.tabModGen.Text = "Mod Generator";
            this.tabModGen.UseVisualStyleBackColor = true;
            // 
            // ChkDeleteOutputFolder
            // 
            this.ChkDeleteOutputFolder.AutoSize = true;
            this.ChkDeleteOutputFolder.Checked = true;
            this.ChkDeleteOutputFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkDeleteOutputFolder.Location = new System.Drawing.Point(226, 215);
            this.ChkDeleteOutputFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChkDeleteOutputFolder.Name = "ChkDeleteOutputFolder";
            this.ChkDeleteOutputFolder.Size = new System.Drawing.Size(369, 29);
            this.ChkDeleteOutputFolder.TabIndex = 29;
            this.ChkDeleteOutputFolder.Text = "Delete output TEMP files after ZIP Created";
            this.ChkDeleteOutputFolder.UseVisualStyleBackColor = true;
            // 
            // lblExportoptions
            // 
            this.lblExportoptions.AutoSize = true;
            this.lblExportoptions.Location = new System.Drawing.Point(979, -20);
            this.lblExportoptions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExportoptions.Name = "lblExportoptions";
            this.lblExportoptions.Size = new System.Drawing.Size(132, 25);
            this.lblExportoptions.TabIndex = 28;
            this.lblExportoptions.Text = "Export Options";
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
            this.chkListExportIntems.Location = new System.Drawing.Point(973, 8);
            this.chkListExportIntems.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkListExportIntems.Name = "chkListExportIntems";
            this.chkListExportIntems.Size = new System.Drawing.Size(155, 200);
            this.chkListExportIntems.TabIndex = 27;
            // 
            // chkDebug
            // 
            this.chkDebug.AutoSize = true;
            this.chkDebug.Location = new System.Drawing.Point(14, 683);
            this.chkDebug.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDebug.Name = "chkDebug";
            this.chkDebug.Size = new System.Drawing.Size(92, 29);
            this.chkDebug.TabIndex = 26;
            this.chkDebug.Text = "Debug";
            this.chkDebug.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 300);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(305, 342);
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(823, 655);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(137, 67);
            this.btnClose.TabIndex = 24;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(335, 655);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(480, 67);
            this.btnGenerate.TabIndex = 23;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // lblOutputMsg
            // 
            this.lblOutputMsg.AutoSize = true;
            this.lblOutputMsg.Location = new System.Drawing.Point(271, 270);
            this.lblOutputMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOutputMsg.Name = "lblOutputMsg";
            this.lblOutputMsg.Size = new System.Drawing.Size(156, 25);
            this.lblOutputMsg.TabIndex = 22;
            this.lblOutputMsg.Text = "Output Messages:";
            // 
            // richTxtOutputMsg
            // 
            this.richTxtOutputMsg.Location = new System.Drawing.Point(335, 300);
            this.richTxtOutputMsg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTxtOutputMsg.Name = "richTxtOutputMsg";
            this.richTxtOutputMsg.ReadOnly = true;
            this.richTxtOutputMsg.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTxtOutputMsg.Size = new System.Drawing.Size(624, 344);
            this.richTxtOutputMsg.TabIndex = 21;
            this.richTxtOutputMsg.Text = "";
            // 
            // btnModOutputPath
            // 
            this.btnModOutputPath.Location = new System.Drawing.Point(27, 165);
            this.btnModOutputPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnModOutputPath.Name = "btnModOutputPath";
            this.btnModOutputPath.Size = new System.Drawing.Size(190, 38);
            this.btnModOutputPath.TabIndex = 20;
            this.btnModOutputPath.Text = "MOD Output Folder";
            this.btnModOutputPath.UseVisualStyleBackColor = true;
            this.btnModOutputPath.Click += new System.EventHandler(this.btnModOutputPath_Click);
            // 
            // txtModOutputPath
            // 
            this.txtModOutputPath.Location = new System.Drawing.Point(226, 167);
            this.txtModOutputPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtModOutputPath.Name = "txtModOutputPath";
            this.txtModOutputPath.Size = new System.Drawing.Size(734, 31);
            this.txtModOutputPath.TabIndex = 19;
            // 
            // btnModCSVPath
            // 
            this.btnModCSVPath.Location = new System.Drawing.Point(27, 97);
            this.btnModCSVPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnModCSVPath.Name = "btnModCSVPath";
            this.btnModCSVPath.Size = new System.Drawing.Size(190, 38);
            this.btnModCSVPath.TabIndex = 18;
            this.btnModCSVPath.Text = "CSV File";
            this.btnModCSVPath.UseVisualStyleBackColor = true;
            this.btnModCSVPath.Click += new System.EventHandler(this.btnModCSVPath_Click);
            // 
            // txtModCSVPath
            // 
            this.txtModCSVPath.Location = new System.Drawing.Point(226, 98);
            this.txtModCSVPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtModCSVPath.Name = "txtModCSVPath";
            this.txtModCSVPath.Size = new System.Drawing.Size(734, 31);
            this.txtModCSVPath.TabIndex = 17;
            // 
            // btnSourceFolderPath
            // 
            this.btnSourceFolderPath.Location = new System.Drawing.Point(27, 30);
            this.btnSourceFolderPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSourceFolderPath.Name = "btnSourceFolderPath";
            this.btnSourceFolderPath.Size = new System.Drawing.Size(190, 38);
            this.btnSourceFolderPath.TabIndex = 16;
            this.btnSourceFolderPath.Text = "Files Source Folder";
            this.btnSourceFolderPath.UseVisualStyleBackColor = true;
            this.btnSourceFolderPath.Click += new System.EventHandler(this.btnSourceFolderPath_Click);
            // 
            // txtSourceFolderPath
            // 
            this.txtSourceFolderPath.Location = new System.Drawing.Point(226, 32);
            this.txtSourceFolderPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSourceFolderPath.Name = "txtSourceFolderPath";
            this.txtSourceFolderPath.Size = new System.Drawing.Size(734, 31);
            this.txtSourceFolderPath.TabIndex = 15;
            // 
            // tabDEFReverseEng
            // 
            this.tabDEFReverseEng.Controls.Add(this.button1);
            this.tabDEFReverseEng.Controls.Add(this.lblDEFCSVOutputMsg);
            this.tabDEFReverseEng.Controls.Add(this.richDEF2CSVOutputMsg);
            this.tabDEFReverseEng.Controls.Add(this.btnDEFGenCSV);
            this.tabDEFReverseEng.Controls.Add(this.txtDEFCSVOutput);
            this.tabDEFReverseEng.Controls.Add(this.btnDEFCSVOutput);
            this.tabDEFReverseEng.Controls.Add(this.txtDEFSource);
            this.tabDEFReverseEng.Controls.Add(this.btnDEFSource);
            this.tabDEFReverseEng.Location = new System.Drawing.Point(4, 34);
            this.tabDEFReverseEng.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabDEFReverseEng.Name = "tabDEFReverseEng";
            this.tabDEFReverseEng.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabDEFReverseEng.Size = new System.Drawing.Size(1149, 745);
            this.tabDEFReverseEng.TabIndex = 1;
            this.tabDEFReverseEng.Text = "DEF --> CSV";
            this.tabDEFReverseEng.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(771, 572);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 60);
            this.button1.TabIndex = 25;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblDEFCSVOutputMsg
            // 
            this.lblDEFCSVOutputMsg.AutoSize = true;
            this.lblDEFCSVOutputMsg.Location = new System.Drawing.Point(223, 155);
            this.lblDEFCSVOutputMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDEFCSVOutputMsg.Name = "lblDEFCSVOutputMsg";
            this.lblDEFCSVOutputMsg.Size = new System.Drawing.Size(156, 25);
            this.lblDEFCSVOutputMsg.TabIndex = 24;
            this.lblDEFCSVOutputMsg.Text = "Output Messages:";
            // 
            // richDEF2CSVOutputMsg
            // 
            this.richDEF2CSVOutputMsg.Location = new System.Drawing.Point(223, 185);
            this.richDEF2CSVOutputMsg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richDEF2CSVOutputMsg.Name = "richDEF2CSVOutputMsg";
            this.richDEF2CSVOutputMsg.ReadOnly = true;
            this.richDEF2CSVOutputMsg.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richDEF2CSVOutputMsg.Size = new System.Drawing.Size(688, 344);
            this.richDEF2CSVOutputMsg.TabIndex = 23;
            this.richDEF2CSVOutputMsg.Text = "";
            // 
            // btnDEFGenCSV
            // 
            this.btnDEFGenCSV.Location = new System.Drawing.Point(223, 572);
            this.btnDEFGenCSV.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDEFGenCSV.Name = "btnDEFGenCSV";
            this.btnDEFGenCSV.Size = new System.Drawing.Size(306, 60);
            this.btnDEFGenCSV.TabIndex = 4;
            this.btnDEFGenCSV.Text = "Generate CSV";
            this.btnDEFGenCSV.UseVisualStyleBackColor = true;
            this.btnDEFGenCSV.Click += new System.EventHandler(this.btnDEFGenCSV_Click);
            // 
            // txtDEFCSVOutput
            // 
            this.txtDEFCSVOutput.Location = new System.Drawing.Point(223, 100);
            this.txtDEFCSVOutput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDEFCSVOutput.Name = "txtDEFCSVOutput";
            this.txtDEFCSVOutput.Size = new System.Drawing.Size(843, 31);
            this.txtDEFCSVOutput.TabIndex = 3;
            // 
            // btnDEFCSVOutput
            // 
            this.btnDEFCSVOutput.Location = new System.Drawing.Point(10, 98);
            this.btnDEFCSVOutput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDEFCSVOutput.Name = "btnDEFCSVOutput";
            this.btnDEFCSVOutput.Size = new System.Drawing.Size(204, 38);
            this.btnDEFCSVOutput.TabIndex = 2;
            this.btnDEFCSVOutput.Text = "CSV Output";
            this.btnDEFCSVOutput.UseVisualStyleBackColor = true;
            // 
            // txtDEFSource
            // 
            this.txtDEFSource.Location = new System.Drawing.Point(223, 30);
            this.txtDEFSource.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDEFSource.Name = "txtDEFSource";
            this.txtDEFSource.Size = new System.Drawing.Size(843, 31);
            this.txtDEFSource.TabIndex = 1;
            // 
            // btnDEFSource
            // 
            this.btnDEFSource.Location = new System.Drawing.Point(10, 28);
            this.btnDEFSource.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDEFSource.Name = "btnDEFSource";
            this.btnDEFSource.Size = new System.Drawing.Size(204, 38);
            this.btnDEFSource.TabIndex = 0;
            this.btnDEFSource.Text = ".DEF Source";
            this.btnDEFSource.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnExportTemplate);
            this.tabPage1.Controls.Add(this.txtTemplateOutput);
            this.tabPage1.Controls.Add(this.btnTemplateOut);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(1149, 745);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Template Example Export";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnExportTemplate
            // 
            this.btnExportTemplate.Location = new System.Drawing.Point(278, 143);
            this.btnExportTemplate.Name = "btnExportTemplate";
            this.btnExportTemplate.Size = new System.Drawing.Size(428, 59);
            this.btnExportTemplate.TabIndex = 2;
            this.btnExportTemplate.Text = "Export CSV Template";
            this.btnExportTemplate.UseVisualStyleBackColor = true;
            this.btnExportTemplate.Click += new System.EventHandler(this.btnExportTemplate_Click);
            // 
            // txtTemplateOutput
            // 
            this.txtTemplateOutput.Location = new System.Drawing.Point(278, 63);
            this.txtTemplateOutput.Name = "txtTemplateOutput";
            this.txtTemplateOutput.Size = new System.Drawing.Size(835, 31);
            this.txtTemplateOutput.TabIndex = 1;
            // 
            // btnTemplateOut
            // 
            this.btnTemplateOut.Location = new System.Drawing.Point(31, 63);
            this.btnTemplateOut.Name = "btnTemplateOut";
            this.btnTemplateOut.Size = new System.Drawing.Size(221, 34);
            this.btnTemplateOut.TabIndex = 0;
            this.btnTemplateOut.Text = "Template Output Folder";
            this.btnTemplateOut.UseVisualStyleBackColor = true;
            // 
            // MODDocGenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 785);
            this.Controls.Add(this.TabTemplateExample);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MODDocGenForm";
            this.Text = "EDuke32 MOD DOCUMENTATION (DEF) DEF language (CSV) comma separated value file";
            this.TabTemplateExample.ResumeLayout(false);
            this.tabModGen.ResumeLayout(false);
            this.tabModGen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabDEFReverseEng.ResumeLayout(false);
            this.tabDEFReverseEng.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FolderBrowserDialog folderBrowserDialog;
        private OpenFileDialog openFileDialog1;
        private TabControl TabTemplateExample;
        private TabPage tabModGen;
        private CheckBox ChkDeleteOutputFolder;
        private Label lblExportoptions;
        private CheckedListBox chkListExportIntems;
        private CheckBox chkDebug;
        private PictureBox pictureBox1;
        private Button btnClose;
        private Button btnGenerate;
        private Label lblOutputMsg;
        private RichTextBox richTxtOutputMsg;
        private Button btnModOutputPath;
        private TextBox txtModOutputPath;
        private Button btnModCSVPath;
        private TextBox txtModCSVPath;
        private Button btnSourceFolderPath;
        private TextBox txtSourceFolderPath;
        private TabPage tabDEFReverseEng;
        private Button button1;
        private Label lblDEFCSVOutputMsg;
        private RichTextBox richDEF2CSVOutputMsg;
        private Button btnDEFGenCSV;
        private TextBox txtDEFCSVOutput;
        private Button btnDEFCSVOutput;
        private TextBox txtDEFSource;
        private Button btnDEFSource;
        private TabPage tabPage1;
        private Button btnExportTemplate;
        private TextBox txtTemplateOutput;
        private Button btnTemplateOut;
    }
}
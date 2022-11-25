//-------------------------------------------------------------------------
/*
Copyright (C) 2010-2019 EDuke32 ,Nblood, MBlood developers and contributors

This file is part of MBlood.
MBlood is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License version 2
as published by the Free Software Foundation.
This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/
//-------------------------------------------------------------------------
using LumenWorks.Framework.IO.Csv;
using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using SearchOption = System.IO.SearchOption;
using System.IO.Compression;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Threading;

namespace MBlood_ModDocumentation
{
    /*
     * For Models the will default Scale 1 and Shade 1 if not specify 
     * */
    public partial class MbloodDocGenForm : Form
    {
        public MbloodDocGenForm()
        {
            InitializeComponent();
            //debug
            txtSourceFolderPath.Text = @"C:\Users\Mercury\Desktop\trash\autoload";
            txtModCSVPath.Text = @"C:\\Users\\Mercury\\Desktop\\trash\\MBlood HD - List.csv";
            txtModOutputPath.Text = @"C:\Users\Mercury\Desktop\trash\output";
            //defaults
            chkListExportIntems.SetItemCheckState(0, CheckState.Checked);
            chkListExportIntems.SetItemCheckState(1, CheckState.Unchecked);
            chkListExportIntems.SetItemCheckState(2, CheckState.Unchecked);
            chkListExportIntems.SetItemCheckState(3, CheckState.Unchecked);
            chkListExportIntems.SetItemCheckState(4, CheckState.Unchecked);
            chkListExportIntems.SetItemCheckState(5, CheckState.Unchecked);
            chkListExportIntems.SetItemCheckState(6, CheckState.Unchecked);


        }

        private void btnSourceFolderPath_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == folderBrowserDialog.ShowDialog())
            {
                txtSourceFolderPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnModOutputPath_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == folderBrowserDialog.ShowDialog())
            {
                txtModOutputPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnModCSVPath_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = String.Empty;
            openFileDialog1.Filter = "CSV file (*.csv)|*.csv";
            openFileDialog1.Title = "Open Mod file documentation";
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                txtModCSVPath.Text = openFileDialog1.FileName;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string SYSCHAR = "~";
            StringBuilder errorMsg = new StringBuilder();
            StringBuilder debugMsg = new StringBuilder();
            richTxtOutputMsg.Text = string.Empty;


            //rules
            /*
            1.- for safty the source and output folder cannot be the same
            2.- Mandatory columns are identified by a tilde '~' at the begining of the column name
            at lest the following columns must exist in the file:
            ~ModName ~Type	~TileNumber	~Category	~Description	~FileName	~FileExtension	~ImageMode	~PalletNumber	~FrameName	~Scale	~Shade
            
            */
            try
            {
                bool exportTiles = false;
                bool exportModels = false;
                bool exportVoxels = false;
                bool exportMapHacks = false;
                bool exportMusic = false;
                bool exportSounds = false;
                bool exportCutScenes = false;
                foreach (var expItem in chkListExportIntems.CheckedItems)
                {

                    switch (expItem.ToString().ToUpper())
                    {
                        case "TILES":
                            exportTiles = true;
                            break;
                        case "MODELS":
                            exportModels = true;
                            break;
                        case "VOXELS":
                            exportVoxels = true;
                            break;
                        case "MAP-HACKS":
                            exportMapHacks = true;
                            break;
                        case "MUSIC":
                            exportMusic = true;
                            break;
                        case "SOUNDS":
                            exportSounds = true;
                            break;
                        case "CUTSCENES":
                            exportCutScenes = true;
                            break;
                    }

                }
                //validate directory
                if (!Directory.Exists(txtSourceFolderPath.Text))
                {
                    errorMsg.AppendLine("Source Directory does not exist.");
                    errorMsg.AppendLine(txtSourceFolderPath.Text);
                }
                if (!File.Exists(txtModCSVPath.Text))
                {
                    errorMsg.AppendLine("CSV File does not exist.");
                    errorMsg.AppendLine(txtModCSVPath.Text);
                }
                if (txtModOutputPath.Text.Equals(txtSourceFolderPath.Text, StringComparison.OrdinalIgnoreCase))
                {
                    errorMsg.AppendLine("Source folder and Mod Ouput Folder cannot be the same.");
                }
                if (txtModOutputPath.Text.Trim() == string.Empty)
                {
                    errorMsg.AppendLine("Mod output folder is required.");
                }
                //read CSV

                foreach (Control ctl in this.Controls)
                {
                    ctl.Enabled = false;
                }
                if (errorMsg.ToString() != string.Empty)
                {
                    return;
                }

                using (CsvReader csv =
                new CsvReader(new StreamReader(txtModCSVPath.Text), true))
                {
                    int fieldCount = csv.FieldCount;
                    string[] headers = csv.GetFieldHeaders().Select(x => x.ToUpper()).ToArray<string>();
                    string[] mandtoryCSVFields = new string[] {
                        SYSCHAR+"MODNAME",
                        SYSCHAR+"TYPE",
                        SYSCHAR+"TILENUMBER",
                        SYSCHAR+"CATEGORY",
                        SYSCHAR+"DESCRIPTION",
                        SYSCHAR+"FILENAME",
                        SYSCHAR+"FILEEXTENSION",
                        SYSCHAR+"IMAGEMODE",
                        SYSCHAR+"PALLETNUMBER",
                        SYSCHAR+"SCALE",
                        SYSCHAR+"SHADE",
                        SYSCHAR+"FRAMENAME",
                        SYSCHAR+"SKINFILENAME",
                        SYSCHAR + "SKINFILEEXTENSION"}.Select(x => x.ToUpper()).ToArray<string>();

                    foreach (string m in mandtoryCSVFields)
                    {
                        if (!Array.Exists(headers, x => x.Contains(m, StringComparison.OrdinalIgnoreCase)))
                        {
                            errorMsg.AppendLine("Mandatory column(s) not found in CSV: " + m);
                        }
                    }
                    if (errorMsg.Length > 0)
                    {
                        throw new Exception();
                    }

                    string outputDirPath = string.Empty;
                    string outputFileName = string.Empty;
                    StringBuilder texturesDEF = new StringBuilder();
                    StringBuilder modelsDEF = new StringBuilder();
                    DataTable mainTbl = new DataTable();

                    #region WorkTable
                    foreach (string hed in headers)
                    {
                        if (hed.Equals(SYSCHAR + "TILENUMBER") || hed.Equals(SYSCHAR + "PALLETNUMBER"))
                        {
                            mainTbl.Columns.Add(new DataColumn(hed.Replace(SYSCHAR, String.Empty), typeof(int)));
                        }
                        else
                        {
                            mainTbl.Columns.Add(new DataColumn(hed.Replace(SYSCHAR, String.Empty)));
                        }
                    }
                    while (csv.ReadNextRecord())
                    {
                        DataRow r = mainTbl.NewRow();
                        bool skipRec = false;
                        for (int i = 0; i < fieldCount; i++)
                        {
                            debugMsg.AppendLine(headers[i]);
                            debugMsg.AppendLine(csv[i]);
                            string colName = headers[i].Replace(SYSCHAR, string.Empty);

                            if (headers[i].Equals(SYSCHAR + "TILENUMBER") || headers[i].Equals(SYSCHAR + "PALLETNUMBER"))
                            {
                                if (csv[i] != null && csv[i] != String.Empty)
                                {
                                    int numeric = 0;
                                    if (int.TryParse(csv[i], out numeric))
                                    {
                                        r[colName] = numeric;
                                    }
                                    else
                                    {
                                        skipRec = true;
                                        errorMsg.AppendLine("Error Parsing: " + headers[i] + " Value:" + csv[i]);
                                    }
                                }
                            }
                            else
                            {
                                r[colName] = csv[i];
                            }
                        }
                        if (!skipRec)
                            mainTbl.Rows.Add(r);
                    }
                    #endregion WorkTable
                    string[] allfiles = Directory.GetFiles(txtSourceFolderPath.Text, "*.*", SearchOption.AllDirectories);
                    DataView textureDataView = new DataView(mainTbl);
                    DataView modelDataView = new DataView(mainTbl);
                    DataView duplicateModelDataView = new DataView(mainTbl);
                    textureDataView.Sort = "MODNAME";
                    textureDataView.RowFilter = "TYPE = 'Sprite'";
                    modelDataView.Sort = "MODNAME, FILENAME";
                    modelDataView.RowFilter = "TYPE = 'Model'";

                    //DEF Processing
                    string defSpriteFile = string.Empty;
                    string defModelFile = string.Empty;
                    string currentOutputDirFolder = String.Empty;
                    string OutputDirFolder = String.Empty;
                    string? modName = String.Empty;
                    string curModel = string.Empty;
                    string curSkin = string.Empty;
                    
                    int sameModelCount = 0;
                    #region MODEL DEF
                    if (exportModels)
                        foreach (DataRowView dvr in modelDataView)
                        {
                            currentOutputDirFolder = String.Empty;
                            
                            //buidl path
                            DataRow dr = dvr.Row;
                            modName = dr["MODNAME"].ToString();
                            string? itemType = dr["TYPE"].ToString();
                            string? category = dr["CATEGORY"].ToString();
                            string? fileName = dr["FILENAME"].ToString();
                            string? extension = dr["FILEEXTENSION"].ToString();
                            string? imgMode = dr["IMAGEMODE"].ToString();
                            string? palletNum = dr["PALLETNUMBER"].ToString();
                            string? tileNum = dr["TILENUMBER"].ToString();
                            string? itemDescription = dr["DESCRIPTION"].ToString();
                            string? scale = dr["SCALE"].ToString();
                            string? shade = dr["SHADE"].ToString();
                            string? frameName = dr["FRAMENAME"].ToString();
                            string? skinFileName = dr["SKINFILENAME"].ToString();
                            string? skinExtension = dr["SKINFILEEXTENSION"].ToString();
                            
                            //control variable for mutliple palelts, multiple tiles same model
                            if (curModel == string.Empty || curModel != fileName)
                            {
                                sameModelCount = 0;
                                curModel = fileName??string.Empty;
                                curSkin = string.Empty;
                            }
                            sameModelCount++;
                            if (modName == null || modName == String.Empty)
                            {
                                errorMsg.AppendLine(SYSCHAR + "ModName cannot be null or Empty.");
                                continue;
                            }
                            if (itemType == null || itemType == String.Empty)
                            {
                                errorMsg.AppendLine(SYSCHAR + "Type cannot be null or Empty. Value Must Be Sprite or Model");
                                continue;
                            }
                            else
                            {
                                if (itemType.Equals("MODEL", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (frameName == null || frameName == String.Empty)
                                    {
                                        errorMsg.AppendLine(SYSCHAR + "FrameName cannot be null or Empty for Type Model.");
                                        continue;
                                    }
                                }
                            }
                            if (category == null || category == String.Empty)
                            {
                                errorMsg.AppendLine(SYSCHAR + "Category cannot be null or Empty.");
                                continue;
                            }
                            if (fileName == null || fileName == String.Empty)
                            {
                                errorMsg.AppendLine(SYSCHAR + "FILENAME cannot be null or Empty.");
                                continue;
                            }
                            if (extension == null || extension == String.Empty)
                            {
                                errorMsg.AppendLine(SYSCHAR + "FILEEXTENSION cannot be null or Empty.");
                                continue;
                            }else
                            {
                                switch(extension.ToUpper())
                                {
                                    case "MD3":
                                        break;
                                    case "VKX":
                                        break;
                                    default:
                                        errorMsg.AppendLine("Model Extention not supported: " + extension);
                                        continue;
                                        break;

                                }
                            }
                            if (itemDescription == null || itemDescription == String.Empty)
                            {
                                itemDescription = String.Empty;
                            }
                            if (scale == null || scale == String.Empty)
                            {
                                scale = "1";
                            }
                            if (shade == null || shade == String.Empty)
                            {
                                shade = "1";
                            }
                            if (imgMode == null || imgMode == String.Empty)
                            {
                                imgMode = ""; //default
                            }
                            else
                            {
                                imgMode = "";
                                if (imgMode.Equals("Indexed", StringComparison.OrdinalIgnoreCase))
                                {
                                    errorMsg.AppendLine(SYSCHAR + "Models cannot have indexed image mode.");
                                }
                            }
                            if (palletNum == null || palletNum == String.Empty)
                            {
                                palletNum = "0";
                                continue;
                            }
                            int intPal = 0;
                            int intTile = 0;
                            bool validPalNum = int.TryParse(palletNum, out intPal);
                            bool validTileNum = int.TryParse(tileNum, out intTile);
                            if (!validPalNum)
                            {
                                errorMsg.AppendLine("invalid ~PALLETNUMBER : " + palletNum);
                                continue;
                            }
                            else
                            {
                                palletNum = intPal.ToString();
                            }
                            if (!validTileNum)
                            {
                                errorMsg.AppendLine("invalid ~TILENUMBER : " + tileNum);
                                continue;
                            }
                            else
                            {
                                tileNum = intTile.ToString();
                            }
                            currentOutputDirFolder = Path.Combine(currentOutputDirFolder, txtModOutputPath.Text);
                            currentOutputDirFolder = Path.Combine(currentOutputDirFolder, modName);
                            OutputDirFolder = currentOutputDirFolder; // output Mod Root folder
                            currentOutputDirFolder = Path.Combine(currentOutputDirFolder, category);
                            defModelFile = category;
                            //find the rest of the categories
                            foreach (DataColumn dc in modelDataView.Table.Columns)
                            {
                                for (int i = 0; i < 51; i++)
                                {
                                    if (dc.ColumnName.Equals("Category" + i.ToString(), StringComparison.OrdinalIgnoreCase))
                                    {
                                        if (category == null || category == String.Empty)
                                        {
                                            continue;
                                        }
                                        category = dr[dc.ColumnName].ToString();
                                        currentOutputDirFolder = Path.Combine(currentOutputDirFolder, category);
                                        defModelFile = Path.Combine(defModelFile, category);
                                    }
                                }
                            }
                            string copyFileName = fileName + "." + extension;
                            string copySkinFileName = skinFileName + "." + skinExtension;
                            string fileFound = allfiles.Select(x => x).Where(x => x.IndexOf(copyFileName, StringComparison.OrdinalIgnoreCase) > -1).FirstOrDefault();
                            if (fileFound == null || fileFound == String.Empty)
                            {
                                errorMsg.AppendLine("file not found: " + copyFileName);
                                string? bestMatch = allfiles.Select(x => x).Where(x => x.IndexOf(fileName, StringComparison.OrdinalIgnoreCase) > -1).FirstOrDefault();
                                if (bestMatch != null && bestMatch != string.Empty)
                                    errorMsg.AppendLine("BestMatch: " + bestMatch);

                                continue;
                            }
                            string skinfound = allfiles.Select(x => x).Where(x => x.IndexOf(copySkinFileName, StringComparison.OrdinalIgnoreCase) > -1).FirstOrDefault();
                            if (skinfound == null || skinfound == String.Empty)
                            {
                                errorMsg.AppendLine("Skin file not found: " + copySkinFileName);
                                string? bestMatch = allfiles.Select(x => x).Where(x => x.IndexOf(copySkinFileName, StringComparison.OrdinalIgnoreCase) > -1).FirstOrDefault();
                                if (bestMatch != null && bestMatch != string.Empty)
                                    errorMsg.AppendLine("BestMatch: " + bestMatch);

                                continue;
                            }
                            copyFileName = Path.GetFileName(fileFound);
                            copySkinFileName = Path.GetFileName(skinfound);
                            //make idrectory
                            try
                            {
                                // Determine whether the directory exists.
                                if (!Directory.Exists(currentOutputDirFolder))
                                {
                                    // Try to create the directory.
                                    DirectoryInfo di = Directory.CreateDirectory(currentOutputDirFolder);
                                }
                            }
                            catch (Exception ex)
                            {
                                errorMsg.AppendLine("Directory creation failed: " + currentOutputDirFolder + "\n" + ex.Message);
                                continue;
                            }
                            //Copy File
                            string fileModelToCopy = string.Empty;
                            string defSkinFile = string.Empty;
                            try
                            {
                                defSkinFile = defModelFile;
                                fileModelToCopy = Path.Combine(currentOutputDirFolder, copyFileName);
                                debugMsg.AppendLine(fileModelToCopy);
                                File.Copy(fileFound, fileModelToCopy, true);
                                defModelFile = Path.Combine(defModelFile, copyFileName);

                                fileModelToCopy = string.Empty;
                                fileModelToCopy = Path.Combine(currentOutputDirFolder, copySkinFileName);
                                debugMsg.AppendLine(fileModelToCopy);
                                File.Copy(skinfound, fileModelToCopy, true);
                                defSkinFile = Path.Combine(defSkinFile, copySkinFileName);



                            }
                            catch (Exception ex)
                            {
                                errorMsg.AppendLine("File copy failed: " + fileModelToCopy + "\n" + ex.Message);
                                continue;
                            }
                            //add to DEF
                            switch (itemType.Trim().ToUpper())
                            {
                                case "SPRITE":
                                    //texture 6145 {  pal 0 { file "upscale2/tile6145.png" indexed }} 
                                    /*
                                    string texture = string.Format("texture {0} { pal {1} { file \"{2}\" {3} }}", tileNum, palletNum, fileToCopy, imgMode);
                                    if (itemDescription != String.Empty)
                                    {
                                        texturesDEF.AppendLine("//" + itemDescription);
                                    }
                                    texturesDEF.AppendLine(texture);
                                    */
                                    break;
                                case "MODEL":
                                    defModelFile = defModelFile.Replace(@"\", "/");
                                    //models can have different time images and also replace multiple tiles
                                    /*
                                     * model "models/Deco/759_BottleWisky_HD.md3" {
                                       scale .8 shade 1
                                       skin { pal 0 file "models/Deco/759_BottleWisky_HD.png" }
                                       skin { pal 13 file "models/Deco/759_BottleWisky13_HD.png" }
                                       skin { pal 11 file "models/Deco/759_BottleWisky11_HD.png" }
                                       frame { name "none00" tile 759 }
                                        }
                                        model "models/Deco/0550-0551_FlameVase.md3" {
                                                   scale 0.9 shade 0
                                                   skin { pal 0 file "models/Deco/0550_FlameVaseOFF.jpg" }
                                                   frame { name "shape0" tile 550 }
                                                   skin { pal 0 file "models/Deco/0551_FlameVaseON.jpg" }
                                                   frame { name "shape0" tile 551 }
                                        }
                                     * */
                                    if (itemDescription != String.Empty)
                                    {
                                        modelsDEF.AppendLine("//" + itemDescription);
                                    }
                                    duplicateModelDataView.Sort = "MODNAME, FILENAME";
                                    duplicateModelDataView.RowFilter = "MODNAME = '"+modName+"' AND TYPE = 'Model' AND FILENAME = '" + fileName + "'";
                                    
                                    
                                    curSkin += string.Format("skin {{ pal {0} file \"{1}\" }} frame {{ name \"{2}\" tile {3} }} ", palletNum, defSkinFile, frameName, tileNum);
                                    if (duplicateModelDataView.Count == sameModelCount)
                                    {
                                        string model = string.Format("model \"{0}\" {{ scale {1} shade {2} ", defModelFile, scale, shade);
                                        modelsDEF.AppendLine(model + curSkin + "}");
                                    }
                                    break;
                                case "VOXEL":
                                    break;
                                default:
                                    errorMsg.AppendLine("Type not supported: " + itemType);
                                    break;

                            }

                        }
                    #endregion MODEL DEF
                    if (exportTiles)
                        #region TILE DEF
                        foreach (DataRowView dvr in textureDataView)
                        {

                            currentOutputDirFolder = String.Empty;
                            //buidl path
                            DataRow dr = dvr.Row;
                            modName = dr["MODNAME"].ToString();
                            string? itemType = dr["TYPE"].ToString();
                            string? category = dr["CATEGORY"].ToString();
                            string? fileName = dr["FILENAME"].ToString();
                            string? extension = dr["FILEEXTENSION"].ToString();
                            string? imgMode = dr["IMAGEMODE"].ToString();
                            string? palletNum = dr["PALLETNUMBER"].ToString();
                            string? tileNum = dr["TILENUMBER"].ToString();
                            string? itemDescription = dr["DESCRIPTION"].ToString();
                            string? scale = dr["SCALE"].ToString();
                            string? shade = dr["SHADE"].ToString();
                            string? frameName = dr["FRAMENAME"].ToString();
                            //skip any documented tile that is not fully documented
                            if(tileNum == null || tileNum == string.Empty || itemType == null || itemType == string.Empty)
                            {
                                errorMsg.AppendLine("skipping un-documented tile");
                                continue;
                            }
                            else
                            {
                                if( fileName == null || fileName == string.Empty)
                                {
                                    errorMsg.AppendLine("skipping un-documented tile: " + tileNum);
                                    continue;
                                }
                            }

                            if (modName == null || modName == String.Empty)
                            {
                                errorMsg.AppendLine(SYSCHAR + "ModName cannot be null or Empty.");
                                continue;
                            }
                            if (itemType == null || itemType == String.Empty)
                            {
                                errorMsg.AppendLine(SYSCHAR + "Type cannot be null or Empty. Value Must Be Sprite or Model");
                                continue;
                            }

                            if (category == null || category == String.Empty)
                            {
                                errorMsg.AppendLine(SYSCHAR + "Category cannot be null or Empty.");
                                continue;
                            }
                            if (fileName == null || fileName == String.Empty)
                            {
                                errorMsg.AppendLine(SYSCHAR + "FILENAME cannot be null or Empty.");
                                continue;
                            }
                            if (extension == null || extension == String.Empty)
                            {
                                errorMsg.AppendLine(SYSCHAR + "FILEEXTENSION cannot be null or Empty.");
                                continue;
                            }
                            if (itemDescription == null || itemDescription == String.Empty)
                            {
                                itemDescription = String.Empty;
                            }
                            if (scale == null || scale == String.Empty)
                            {
                                scale = "1";
                            }
                            if (shade == null || shade == String.Empty)
                            {
                                shade = "1";
                            }
                            if (imgMode == null || imgMode == String.Empty)
                            {
                                imgMode = ""; //default
                            }
                            else
                            {
                                imgMode = "";
                                if (imgMode.Equals("Indexed", StringComparison.OrdinalIgnoreCase))
                                {
                                    imgMode = "indexed";
                                }
                            }
                            if (palletNum == null || palletNum == String.Empty)
                            {
                                palletNum = "0";
                            }
                            int intPal = 0;
                            int intTile = 0;
                            bool validPalNum = int.TryParse(palletNum, out intPal);
                            bool validTileNum = int.TryParse(tileNum, out intTile);
                            if (!validPalNum)
                            {
                                errorMsg.AppendLine("invalid ~PALLETNUMBER : " + palletNum);
                                continue;
                            }
                            else
                            {
                                palletNum = intPal.ToString();
                            }
                            if (!validTileNum)
                            {
                                errorMsg.AppendLine("invalid ~TILENUMBER : " + tileNum);
                                continue;
                            }
                            else
                            {
                                tileNum = intTile.ToString();
                            }
                            modelDataView.RowFilter = "";
                            modelDataView.RowFilter = "TILENUMBER = '" + tileNum + "'";
                            if (modelDataView.Count == 0)
                            {
                                errorMsg.AppendLine("Model found for tile " + tileNum + " import of sprite for tile not allowed.");
                                continue;
                            }
                            currentOutputDirFolder = Path.Combine(currentOutputDirFolder, txtModOutputPath.Text);
                            currentOutputDirFolder = Path.Combine(currentOutputDirFolder, modName);
                            OutputDirFolder = currentOutputDirFolder; // output Mod Root folder
                            currentOutputDirFolder = Path.Combine(currentOutputDirFolder, category);
                            defSpriteFile = category;
                            //find the rest of the categories
                            foreach (DataColumn dc in textureDataView.Table.Columns)
                            {
                                if (dc.ColumnName.StartsWith("Category", StringComparison.OrdinalIgnoreCase))
                                    for (int i = 0; i < 51; i++)
                                    {
                                        if (dc.ColumnName.Equals("Category" + i.ToString(), StringComparison.OrdinalIgnoreCase))
                                        {
                                            if (category == null || category == String.Empty)
                                            {
                                                continue;
                                            }
                                            category = dr[dc.ColumnName].ToString();
                                            currentOutputDirFolder = Path.Combine(currentOutputDirFolder, category);
                                            defSpriteFile = Path.Combine(defSpriteFile, category);
                                        }
                                    }
                            }

                            string copyFileName = fileName + "." + extension;
                            string fileFound = allfiles.Select(x => x).Where(x => x.IndexOf(copyFileName, StringComparison.OrdinalIgnoreCase) > -1).FirstOrDefault();
                            if (fileFound == null || fileFound == String.Empty)
                            {
                                errorMsg.AppendLine("file not found: " + copyFileName);
                                string bestMatch = allfiles.Select(x => x).Where(x => x.IndexOf(fileName, StringComparison.OrdinalIgnoreCase) > -1).FirstOrDefault();
                                if (bestMatch != null && bestMatch != string.Empty)
                                    errorMsg.AppendLine("BestMatch: " + bestMatch);

                                continue;
                            }
                            //make idrectory
                            try
                            {
                                // Determine whether the directory exists.
                                if (!Directory.Exists(currentOutputDirFolder))
                                {
                                    // Try to create the directory.
                                    DirectoryInfo di = Directory.CreateDirectory(currentOutputDirFolder);
                                }
                            }
                            catch (Exception ex)
                            {
                                errorMsg.AppendLine("Directory creation failed: " + currentOutputDirFolder + "\n" + ex.Message);
                                continue;
                            }
                            //Copy File
                            string fileToCopy = string.Empty;
                            try
                            {
                                copyFileName = Path.GetFileName(fileFound);
                                fileToCopy = Path.Combine(currentOutputDirFolder, copyFileName);
                                debugMsg.AppendLine(fileToCopy);
                                File.Copy(fileFound, fileToCopy, true);
                                defSpriteFile = Path.Combine(defSpriteFile, copyFileName);
                            }
                            catch (Exception ex)
                            {
                                errorMsg.AppendLine("File copy failed: " + fileToCopy + "\n" + ex.Message);
                                continue;
                            }
                            //add to DEF
                            switch (itemType.Trim().ToUpper())
                            {
                                case "SPRITE":
                                    //texture 6145 {  pal 0 { file "upscale2/tile6145.png" indexed }} 
                                    defSpriteFile = defSpriteFile.Replace(@"\", "/");
                                    string texture = string.Format("texture {0} {{ pal {1} {{ file \"{2}\" {3} }}}}", tileNum.ToString(), palletNum.ToString(), defSpriteFile, imgMode);
                                    if (itemDescription != String.Empty)
                                    {
                                        texturesDEF.AppendLine("//" + itemDescription);
                                    }
                                    texturesDEF.AppendLine(texture);
                                    break;
                                default:
                                    errorMsg.AppendLine("Type not supported: " + itemType);
                                    break;

                            }

                            //delete directory after ZIP was created
                            // Delete the directory.
                            //di.Delete();
                            //Console.WriteLine("The directory was deleted successfully.");
                        }
                    #endregion TILE DEF


                    File.WriteAllText(Path.Combine(OutputDirFolder, "Blood.def"), "//MODELS\n" + modelsDEF + "\n//TEXTURES\n" +texturesDEF.ToString());
                    string zipFileName = Path.Combine(txtModOutputPath.Text, modName + ".zip");
                    string modRoot = Path.Combine(txtModOutputPath.Text, modName);
                    if(File.Exists(zipFileName))
                    {
                        File.Delete(zipFileName);
                    }
                    ZipFile.CreateFromDirectory(modRoot, zipFileName);
                    //delete directory after ZIP was created
                    if (ChkDeleteOutputFolder.Checked)
                    {
                        string deleteTempFolder = Path.Combine(txtModOutputPath.Text, modName);
                        Directory.Delete(deleteTempFolder,true);
                    }
                }

            }
            catch (Exception ex)
            {
                errorMsg.AppendLine(ex.Message);
            }
            finally
            {
                foreach (Control ctl in this.Controls)
                {
                    ctl.Enabled = true;
                }
                if (chkDebug.Checked)
                {
                    richTxtOutputMsg.Text = debugMsg.ToString();
                }

                richTxtOutputMsg.Text += errorMsg.ToString();
                if (errorMsg.ToString() != String.Empty)
                {
                    richTxtOutputMsg.AppendText("Completed With Errors.");
                }
                else
                {
                    richTxtOutputMsg.AppendText("Completed successfully.");
                }
            }

        }
    }
}
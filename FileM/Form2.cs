using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;

namespace FileM
{
    public partial class Form2 : Form
    {
        #region forms
        public Form2()
        {
            InitializeComponent();
            LoadSett();
            MainSettings.Default.S_SettOpen = true;
            
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainSettings.Default.S_SettOpen = false;
            MainSettings.Default.P_Path0 = textBox_P10.Text.ToString();
            MainSettings.Default.P_Path1 = textBox_P20.Text.ToString();
            MainSettings.Default.P_Path2 = textBox_P30.Text.ToString();
            MainSettings.Default.P_Path3 = textBox_P40.Text.ToString();
            MainSettings.Default.P_Path4 = textBox_P50.Text.ToString();
            MainSettings.Default.P_Path5 = textBox_P60.Text.ToString();
            MainSettings.Default.P_Path6 = textBox_P70.Text.ToString();
            MainSettings.Default.P_Path7 = textBox_P80.Text.ToString();
            /////////////////////////////////////////////////////////
            MainSettings.Default.P_Filter0 = textBox_P11.Text.ToString();
            MainSettings.Default.P_Filter1 = textBox_P21.Text.ToString();
            MainSettings.Default.P_Filter2 = textBox_P31.Text.ToString();
            MainSettings.Default.P_Filter3 = textBox_P41.Text.ToString();
            MainSettings.Default.P_Filter4 = textBox_P51.Text.ToString();
            MainSettings.Default.P_Filter5 = textBox_P61.Text.ToString();
            MainSettings.Default.P_Filter6 = textBox_P71.Text.ToString();
            MainSettings.Default.P_Filter7 = textBox_P81.Text.ToString();
            ///////////////////////////////////////////////////////////
            MainSettings.Default.P_Sub0 = checkBox_P1.Checked;
            MainSettings.Default.P_Sub1 = checkBox_P2.Checked;
            MainSettings.Default.P_Sub2 = checkBox_P3.Checked;
            MainSettings.Default.P_Sub3 = checkBox_P4.Checked;
            MainSettings.Default.P_Sub4 = checkBox_P5.Checked;
            MainSettings.Default.P_Sub5 = checkBox_P6.Checked;
            MainSettings.Default.P_Sub6 = checkBox_P7.Checked;
            MainSettings.Default.P_Sub7 = checkBox_P8.Checked;
            /////////////////////////////////////////////////
            MainSettings.Default.B_DataForm = textBox_B4.Text.ToString();
            MainSettings.Default.B_FolderName = textBox_B1.Text.ToString();
            MainSettings.Default.B_Prefix = textBox_B2.Text.ToString();
            MainSettings.Default.B_Sufix = textBox_B3.Text.ToString();
            MainSettings.Default.L_DataForm = textBox_L4.Text.ToString();
            MainSettings.Default.L_Extension = textBox_L3.Text.ToString();
            MainSettings.Default.L_Filename = textBox_L2.Text.ToString();
            MainSettings.Default.L_FolderName = textBox_L1.Text.ToString();
            if (textBox_V1.Text.ToString() != String.Empty) MainSettings.Default.S_MaxRow = Convert.ToInt32(textBox_V1.Text.ToString());
            MainSettings.Default.Save();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            LoadSett();
        }
        private void LoadSett()
        {
            MainSettings.Default.Reload();
            checkBox_B1.Checked = MainSettings.Default.B_bBackup;
            checkBox_L1.Checked = MainSettings.Default.L_bAutosave;
            checkBox_L2.Checked = MainSettings.Default.L_bError;
            checkBox_V1.Checked = MainSettings.Default.S_MostTop;
            checkBox_V2.Checked = MainSettings.Default.S_Minimize;
            checkBox_V3.Checked = MainSettings.Default.F_bALLDRIVES;
            checkBox_V4.Checked = MainSettings.Default.S_bExcCurrDir;
            checkBox_V5.Checked = MainSettings.Default.F_bExcAPPDATA;
            checkBox_V6.Checked = MainSettings.Default.F_bExcWINDOWS;
            ////////////////////////////////////////////////////////
            textBox_B1.Text = MainSettings.Default.B_FolderName;
            textBox_B2.Text = MainSettings.Default.B_Prefix;
            textBox_B3.Text = MainSettings.Default.B_Sufix;
            textBox_B4.Text = MainSettings.Default.B_DataForm;
            ////////////////////////////////////////////////////////
            textBox_L1.Text = MainSettings.Default.L_FolderName;
            textBox_L2.Text = MainSettings.Default.L_Filename;
            textBox_L3.Text = MainSettings.Default.L_Extension;
            textBox_L4.Text = MainSettings.Default.L_DataForm;
            textBox_V1.Text = MainSettings.Default.S_MaxRow.ToString();
            ///////////////////////////////////////////////////
            textBox_B1.Enabled = MainSettings.Default.B_bBackup;
            textBox_B2.Enabled = MainSettings.Default.B_bBackup;
            textBox_B3.Enabled = MainSettings.Default.B_bBackup;
            textBox_B4.Enabled = MainSettings.Default.B_bBackup;
            textBox_L1.Enabled = MainSettings.Default.L_bAutosave;
            textBox_L2.Enabled = MainSettings.Default.L_bAutosave;
            textBox_L3.Enabled = MainSettings.Default.L_bAutosave;
            textBox_L4.Enabled = MainSettings.Default.L_bAutosave;
            ////////////////////////////////////////////////////
            checkBox_P1.Checked = MainSettings.Default.P_Sub0;
            checkBox_P2.Checked = MainSettings.Default.P_Sub1;
            checkBox_P3.Checked = MainSettings.Default.P_Sub2;
            checkBox_P4.Checked = MainSettings.Default.P_Sub3;
            checkBox_P5.Checked = MainSettings.Default.P_Sub4;
            checkBox_P6.Checked = MainSettings.Default.P_Sub5;
            checkBox_P7.Checked = MainSettings.Default.P_Sub6;
            checkBox_P8.Checked = MainSettings.Default.P_Sub7;
            /////////////////////////////////////////////////
            textBox_P10.Text = MainSettings.Default.P_Path0;
            textBox_P20.Text = MainSettings.Default.P_Path1;
            textBox_P30.Text = MainSettings.Default.P_Path2;
            textBox_P40.Text = MainSettings.Default.P_Path3;
            textBox_P50.Text = MainSettings.Default.P_Path4;
            textBox_P60.Text = MainSettings.Default.P_Path5;
            textBox_P70.Text = MainSettings.Default.P_Path6;
            textBox_P80.Text = MainSettings.Default.P_Path7;
            //////////////////////////////////////////////
            textBox_P11.Text = MainSettings.Default.P_Filter0;
            textBox_P21.Text = MainSettings.Default.P_Filter1;
            textBox_P31.Text = MainSettings.Default.P_Filter2;
            textBox_P41.Text = MainSettings.Default.P_Filter3;
            textBox_P51.Text = MainSettings.Default.P_Filter4;
            textBox_P61.Text = MainSettings.Default.P_Filter5;
            textBox_P71.Text = MainSettings.Default.P_Filter6;
            textBox_P81.Text = MainSettings.Default.P_Filter7;
            ////////////////////////////////////////////////
            textBox_P10.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P20.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P30.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P40.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P50.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P60.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P70.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P80.Enabled = !MainSettings.Default.F_bALLDRIVES;
            /////////////////////////////////////////////////
            textBox_P11.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P21.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P31.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P41.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P51.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P61.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P71.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P81.Enabled = !MainSettings.Default.F_bALLDRIVES;
            /////////////////////////////////////////////////
            checkBox_P1.Enabled = !MainSettings.Default.F_bALLDRIVES;
            checkBox_P2.Enabled = !MainSettings.Default.F_bALLDRIVES;
            checkBox_P3.Enabled = !MainSettings.Default.F_bALLDRIVES;
            checkBox_P4.Enabled = !MainSettings.Default.F_bALLDRIVES;
            checkBox_P5.Enabled = !MainSettings.Default.F_bALLDRIVES;
            checkBox_P6.Enabled = !MainSettings.Default.F_bALLDRIVES;
            checkBox_P7.Enabled = !MainSettings.Default.F_bALLDRIVES;
            checkBox_P8.Enabled = !MainSettings.Default.F_bALLDRIVES;
        }
        #endregion
        #region CheckBox
        private void checkBox_V2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_V2.Checked == false) MainSettings.Default.S_Minimize = false;
            if (checkBox_V2.Checked == true) MainSettings.Default.S_Minimize = true;
        }
        private void checkBox_V1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_V1.Checked == false) MainSettings.Default.S_MostTop = false;
            if (checkBox_V1.Checked == true) MainSettings.Default.S_MostTop = true;
        }
        private void checkBox_L1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_L1.Checked == false)
            {
                MainSettings.Default.L_bAutosave = false;
                textBox_L4.Enabled = false;
                textBox_L3.Enabled = false;
                textBox_L2.Enabled = false;
                textBox_L1.Enabled = false;
                checkBox_L2.Enabled = false;
            }
            if (checkBox_L1.Checked == true)
            {
                MainSettings.Default.L_bAutosave = true;
                textBox_L4.Enabled = true;
                textBox_L3.Enabled = true;
                textBox_L2.Enabled = true;
                textBox_L1.Enabled = true;
                checkBox_L2.Enabled = true;
            }
        }
        private void checkBox_V3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_V3.Checked == false) MainSettings.Default.F_bALLDRIVES = false;
            if (checkBox_V3.Checked == true) MainSettings.Default.F_bALLDRIVES = true;
            textBox_P10.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P20.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P30.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P40.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P50.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P60.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P70.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P80.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P11.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P21.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P31.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P41.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P51.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P61.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P71.Enabled = !MainSettings.Default.F_bALLDRIVES;
            textBox_P81.Enabled = !MainSettings.Default.F_bALLDRIVES;
            checkBox_P1.Enabled = !MainSettings.Default.F_bALLDRIVES;
            checkBox_P2.Enabled = !MainSettings.Default.F_bALLDRIVES;
            checkBox_P3.Enabled = !MainSettings.Default.F_bALLDRIVES;
            checkBox_P4.Enabled = !MainSettings.Default.F_bALLDRIVES;
            checkBox_P5.Enabled = !MainSettings.Default.F_bALLDRIVES;
            checkBox_P6.Enabled = !MainSettings.Default.F_bALLDRIVES;
            checkBox_P7.Enabled = !MainSettings.Default.F_bALLDRIVES;
            checkBox_P8.Enabled = !MainSettings.Default.F_bALLDRIVES;
        }
        private void checkBox_L2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_L2.Checked == false) MainSettings.Default.L_bError = false;
            if (checkBox_L2.Checked == true) MainSettings.Default.L_bError = true;
        }
        private void checkBox_V5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_V5.Checked == false) MainSettings.Default.F_bExcAPPDATA = false;
            if (checkBox_V5.Checked == true) MainSettings.Default.F_bExcAPPDATA = true;
        }
        private void checkBox_V6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_V6.Checked == false) MainSettings.Default.F_bExcWINDOWS = false;
            if (checkBox_V6.Checked == true) MainSettings.Default.F_bExcWINDOWS = true;
        }
        private void checkBox_V4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_V4.Checked == false) MainSettings.Default.S_bExcCurrDir = false;
            if (checkBox_V4.Checked == true) MainSettings.Default.S_bExcCurrDir = true;
        }
        private void checkBox_B1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_B1.Checked == false)
            {
                MainSettings.Default.B_bBackup = false;
                //MainSettings.Default.S_bExcCurrDir = false;
                checkBox_V4.Checked = false;
            }
            if (checkBox_B1.Checked == true)
            {
                MainSettings.Default.B_bBackup = true;
                //MainSettings.Default.S_bExcCurrDir = true;
                checkBox_V4.Checked = true;
            }
            textBox_B2.Enabled = MainSettings.Default.B_bBackup;
            textBox_B3.Enabled = MainSettings.Default.B_bBackup;
            textBox_B4.Enabled = MainSettings.Default.B_bBackup;
            textBox_B1.Enabled = MainSettings.Default.B_bBackup;
        }

        #endregion
        #region path1
        private void textBox_P10_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FolderDialog_1.ShowDialog();
            if (result == DialogResult.OK)
                textBox_P10.Text = FolderDialog_1.SelectedPath;
        }
        private void textBox_P11_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FileDialog_1.ShowDialog();
            if (result == DialogResult.OK)
                textBox_P11.Text = FileDialog_1.SafeFileName;
        }
        private void label_P1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FileDialog_1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox_P10.Text = Path.GetDirectoryName(FileDialog_1.FileName);
                textBox_P11.Text = FileDialog_1.SafeFileName;
            }
        }
        #endregion
        #region path2
        private void textBox_P20_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FolderDialog_1.ShowDialog();
            if (result == DialogResult.OK)
                textBox_P20.Text = FolderDialog_1.SelectedPath;
        }
        private void textBox_P21_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FileDialog_1.ShowDialog();
            if (result == DialogResult.OK)
                textBox_P21.Text = FileDialog_1.SafeFileName;
        }
        private void label_P2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FileDialog_1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox_P20.Text = Path.GetDirectoryName(FileDialog_1.FileName);
                textBox_P21.Text = FileDialog_1.SafeFileName;
            }
        }
        #endregion
        #region path3
        private void textBox_P30_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FolderDialog_1.ShowDialog();
            if (result == DialogResult.OK)
                textBox_P30.Text = FolderDialog_1.SelectedPath;
        }
        private void textBox_P31_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FileDialog_1.ShowDialog();
            if (result == DialogResult.OK)
                textBox_P31.Text = FileDialog_1.SafeFileName;
        }
        private void label_P3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FileDialog_1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox_P30.Text = Path.GetDirectoryName(FileDialog_1.FileName);
                textBox_P31.Text = FileDialog_1.SafeFileName;
            }
        }
        #endregion
        #region path4
        private void textBox_P40_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FolderDialog_1.ShowDialog();
            if (result == DialogResult.OK)
                textBox_P40.Text = FolderDialog_1.SelectedPath;
        }
        private void textBox_P41_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FileDialog_1.ShowDialog();
            if (result == DialogResult.OK)
                textBox_P41.Text = FileDialog_1.SafeFileName;
        }
        private void label_P4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FileDialog_1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox_P40.Text = Path.GetDirectoryName(FileDialog_1.FileName);
                textBox_P41.Text = FileDialog_1.SafeFileName;
            }
        }
        #endregion
        #region path5
        private void textBox_P50_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FolderDialog_1.ShowDialog();
            if (result == DialogResult.OK)
                textBox_P50.Text = FolderDialog_1.SelectedPath;
        }
        private void textBox_P51_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FileDialog_1.ShowDialog();
            if (result == DialogResult.OK)
                textBox_P51.Text = FileDialog_1.SafeFileName;
        }
        private void label_P5_MouseDoubleClick(object sender, EventArgs e)
        {
            DialogResult result = FileDialog_1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox_P50.Text = Path.GetDirectoryName(FileDialog_1.FileName);
                textBox_P51.Text = FileDialog_1.SafeFileName;
            }
        }
        #endregion
        #region path6
        private void textBox_P60_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FolderDialog_1.ShowDialog();
            if (result == DialogResult.OK)
                textBox_P60.Text = FolderDialog_1.SelectedPath;
        }
        private void textBox_P61_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FileDialog_1.ShowDialog();
            if (result == DialogResult.OK)
                textBox_P61.Text = FileDialog_1.SafeFileName;
        }
        private void label_P6_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FileDialog_1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox_P60.Text = Path.GetDirectoryName(FileDialog_1.FileName);
                textBox_P61.Text = FileDialog_1.SafeFileName;
            }
        }
        #endregion
        #region path7
        private void textBox_P70_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FolderDialog_1.ShowDialog();
            if (result == DialogResult.OK)
                textBox_P70.Text = FolderDialog_1.SelectedPath;
        }
        private void textBox_P71_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FileDialog_1.ShowDialog();
            if (result == DialogResult.OK)
                textBox_P71.Text = FileDialog_1.SafeFileName;
        }
        private void label_P7_MouseDoubleClick(object sender, EventArgs e)
        {
            DialogResult result = FileDialog_1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox_P70.Text = Path.GetDirectoryName(FileDialog_1.FileName);
                textBox_P71.Text = FileDialog_1.SafeFileName;
            }
        }
        #endregion
        #region path8
        private void textBox_P80_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FolderDialog_1.ShowDialog();
            if (result == DialogResult.OK)
                textBox_P80.Text = FolderDialog_1.SelectedPath;
        }
        private void textBox_P81_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult result = FileDialog_1.ShowDialog();
            if (result == DialogResult.OK)
                textBox_P81.Text = FileDialog_1.SafeFileName;
        }
        private void label_P8_MouseDoubleClick(object sender, EventArgs e)
        {
            DialogResult result = FileDialog_1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox_P80.Text = Path.GetDirectoryName(FileDialog_1.FileName);
                textBox_P81.Text = FileDialog_1.SafeFileName;
            }
        }
        #endregion
        #region Restart button
        private void resetSettToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainSettings.Default.Reset();
            MainSettings.Default.P_Path0 = Environment.CurrentDirectory;
            MainSettings.Default.P_Filter0 = "*";
            MainSettings.Default.P_Sub0 = true;
            MainSettings.Default.Save();
            LoadSett();
        }
        #endregion
        #region Path
        private void label_P1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.label_P1, "[P1-P8] Double-Click -> Select: Path & File");
        }

        private void textBox_P10_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.textBox_P10, "[P1-P8] Double-Click -> Select: Path");
        }

        private void textBox_P11_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.textBox_P11, "[P1-P8] Double-Click -> Select: File");
        }

        private void checkBox_P1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.checkBox_P1, "[P1-P8] Subdir for higher folders");
        }
        #endregion
        #region Log
        private void checkBox_L1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.checkBox_L1, "Enables logging");
        }

        private void checkBox_L2_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.checkBox_L2, "Enables error logging");
        }

        private void textBox_L1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.textBox_L1, "Name of addonal log folder\nDefault: N/A");
        }

        private void textBox_L2_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.textBox_L2, "Name of log file\nDefault: [log]");
        }

        private void textBox_L3_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.textBox_L3, "Name of extention\nDefault: [.txt]");
        }

        private void textBox_L4_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.textBox_L4, "Date format in log\nDefault: [ddMMyyyy]");
        }
        #endregion
        #region Backup
        private void checkBox_B1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.checkBox_B1, "Enables backuping");
        }

        private void textBox_B1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.textBox_B1, "Name of addonal backup folder\nDefault: N/A");
        }

        private void textBox_B2_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.textBox_B2, "Name of file prefix\nDefault: [b_]");
        }

        private void textBox_B3_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.textBox_B3, "Name of file sufix\nDefault: [_]");
        }

        private void textBox_B4_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.textBox_B4, "Date format in backup\nDefault: [ddMMyyyyHHmmss]");
        }
        #endregion
        #region VA
        private void checkBox_V1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.checkBox_V1, "Stays always on top");
        }

        private void checkBox_V2_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.checkBox_V2, "Minimizes to tray when minimize");
        }

        private void checkBox_V3_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.checkBox_V3, "Sets watcher to all drive leters [A-Z]");
        }

        private void checkBox_V4_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.checkBox_V4, "Exclude [" + Environment.CurrentDirectory + "]");
        }

        private void checkBox_V5_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.checkBox_V5, "Exclude [C:\\User\\Appdata]");
        }

        private void checkBox_V6_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.checkBox_V6, "Exclude [C:\\Windows]");
        }

        private void textBox_V1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.textBox_V1, "Max rows in main window\nDefault: [64]");
        }
        #endregion
    }
}

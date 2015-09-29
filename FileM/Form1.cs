#region Using
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
#endregion
namespace FileM
{
    public partial class Form1 : Form
    {
        #region VAR
        public bool[] sub = new bool[8];               
        public DateTime dt = new DateTime();
        public int _x = 0;
        public int count = 0;                                           
        public int limiter = 0;                                      
        public int row_count = 0;                                       
        public string lastName = "";                                    
        public string[] args = System.Environment.GetCommandLineArgs(); 
        public string[] drives;                                         
        public string[] filters = new string[8];
        public string[] paths = new string[8];                                                     
        public FileSystemWatcher[] Watcher = new FileSystemWatcher[16]; 
        #endregion
        #region Form
        public Form1()
        {
            InitializeComponent();
            notifyIcon1.Visible = false;
            LoadPath();
            InitWatcher(16);
            SetWatcher();
            MainSettings.Default.Reload();
            if (MainSettings.Default.S_FirstTimeSetup == true)
            {
                MainSettings.Default.Reset();
                MainSettings.Default.S_FirstTimeSetup = false;
                MainSettings.Default.P_Path0 = Environment.CurrentDirectory;
                MainSettings.Default.Save();
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
                this.Invoke((MethodInvoker)delegate
                {
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
                });
            if (WindowState == FormWindowState.Minimized && MainSettings.Default.S_Minimize == true)
            {
                Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int a = 0; a != 16; a++)
            {
                Watcher[a].Changed -= new FileSystemEventHandler(OnChanged);
                Watcher[a].Created -= new FileSystemEventHandler(OnChanged);
                Watcher[a].Deleted -= new FileSystemEventHandler(OnChanged);
                Watcher[a].Renamed -= new RenamedEventHandler(OnRenamed);
                Watcher[a].Error -= new ErrorEventHandler(OnError);
            }
            MainSettings.Default.Save();
        }
        #endregion
        #region MyFunc
        public void LoadPath()
        {
            paths[0] = MainSettings.Default.P_Path0;
            paths[1] = MainSettings.Default.P_Path1;
            paths[2] = MainSettings.Default.P_Path2;
            paths[3] = MainSettings.Default.P_Path3;
            paths[4] = MainSettings.Default.P_Path4;
            paths[5] = MainSettings.Default.P_Path5;
            paths[6] = MainSettings.Default.P_Path6;
            paths[7] = MainSettings.Default.P_Path7;
            filters[0] = MainSettings.Default.P_Filter0;
            filters[1] = MainSettings.Default.P_Filter1;
            filters[2] = MainSettings.Default.P_Filter2;
            filters[3] = MainSettings.Default.P_Filter3;
            filters[4] = MainSettings.Default.P_Filter4;
            filters[5] = MainSettings.Default.P_Filter5;
            filters[6] = MainSettings.Default.P_Filter6;
            filters[7] = MainSettings.Default.P_Filter7;
            sub[0] = MainSettings.Default.P_Sub0;
            sub[1] = MainSettings.Default.P_Sub1;
            sub[2] = MainSettings.Default.P_Sub2;
            sub[3] = MainSettings.Default.P_Sub3;
            sub[4] = MainSettings.Default.P_Sub4;
            sub[5] = MainSettings.Default.P_Sub5;
            sub[6] = MainSettings.Default.P_Sub6;
            sub[7] = MainSettings.Default.P_Sub7;
        }
        public string ToShort(string e, int f)
        {
            if (e.Length > f)
            {
                e = e.Remove(f);
                e = e.Insert(f, "..");
            }
            return e;
        }
        public string MakeUnique(string path)
        {
            string dir = Path.GetDirectoryName(path);
            string fileName = Path.GetFileNameWithoutExtension(path);
            string fileExt = Path.GetExtension(path);

            for (int i = 1; ; ++i)
            {
                if (!File.Exists(Path.Combine(dir, fileName + "_" + i + fileExt)))
                {
                    return Path.Combine(dir, fileName + "_" + i + fileExt);
                }
            }
        }
        public void InitWatcher(int b)
        {
            for (int a = 0; (a != b)&&(a<32); a++)
            {
                Watcher[a] = new FileSystemWatcher();
                Watcher[a].NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.Security | NotifyFilters.Size;
                Watcher[a].Changed += new FileSystemEventHandler(OnChanged);
                Watcher[a].Created += new FileSystemEventHandler(OnChanged);
                Watcher[a].Deleted += new FileSystemEventHandler(OnChanged);
                Watcher[a].Renamed += new RenamedEventHandler(OnRenamed);
                Watcher[a].Error += new ErrorEventHandler(OnError);
                Watcher[a].EnableRaisingEvents = false;
            }
        }
        public void SetWatcher()
        {
            LoadPath();
            DriveInfo[] Drive = DriveInfo.GetDrives();
            drives = Environment.GetLogicalDrives();
            if (MainSettings.Default.F_bALLDRIVES == true)
                limiter = drives.Count();
            if (MainSettings.Default.F_bALLDRIVES == false)
                limiter = 8;

            for (_x = 0; _x != limiter; _x++)
            {
                if (MainSettings.Default.F_bALLDRIVES == false && (paths[_x] != string.Empty && filters[_x] != string.Empty))
                {
                    Watcher[_x].Path = paths[_x];
                    Watcher[_x].Filter = filters[_x];
                    Watcher[_x].IncludeSubdirectories = sub[_x];
                }
                if (MainSettings.Default.F_bALLDRIVES == true && (Drive[_x].DriveType.ToString() != "CDRom"))
                {
                    Watcher[_x].Path = drives[_x];
                    Watcher[_x].Filter = "*";
                    Watcher[_x].IncludeSubdirectories = true;
                }
                Watcher[_x].EnableRaisingEvents = false;
            }
        }
        public bool IsBackup(string name)
        {
            if (name != string.Empty)
            {
                if (name[0] == 'b' && name[1] == '_')
                    return true;
                else return false;
            }
            else return false;
        }
        public void AddToTitle(int il)
        {
                if (MainSettings.Default.F_bALLDRIVES == true)
                {
                    notifyIcon1.Text = "FileM [A-Z] -> " + ToShort(il.ToString(), 8);
                    this.Invoke((MethodInvoker)delegate{this.Text = "FileM [A-Z] -> " + ToShort(il.ToString(), 8);});
                }
                else if (MainSettings.Default.F_bALLDRIVES == false)
                {
                    notifyIcon1.Text = "FileM [Path] -> " + ToShort(il.ToString(), 8);
                    this.Invoke((MethodInvoker)delegate{this.Text = "FileM [Path] -> " + ToShort(il.ToString(), 8);});
                }
                this.Invoke((MethodInvoker)delegate{button4.Text = ("Clear (" + ToShort(il.ToString(), 8) + ")");});
        }
        public void SaveOutput(string f, string name, string date, string ext)
        {
            if (MainSettings.Default.L_bAutosave == true)
            {
                #region Autosave + folder names
                if (MainSettings.Default.L_FolderName != String.Empty)
                {
                    if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory + Path.DirectorySeparatorChar + MainSettings.Default.L_FolderName)))
                        Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory + Path.DirectorySeparatorChar + MainSettings.Default.L_FolderName));
                    StreamWriter txt = new StreamWriter(Path.Combine(Environment.CurrentDirectory + Path.DirectorySeparatorChar + MainSettings.Default.L_FolderName + Path.DirectorySeparatorChar + name + date + ext), true);
                    txt.WriteLine(dt.ToLocalTime().ToString("HH:mm:ss: ") + f);
                    txt.Close();
                }
                if (MainSettings.Default.L_FolderName == String.Empty)
                {
                    StreamWriter txt = new StreamWriter(Path.Combine(Environment.CurrentDirectory + Path.DirectorySeparatorChar + name + date + ext), true);
                    txt.WriteLine(dt.ToLocalTime().ToString("HH:mm:ss: ") + f);
                    txt.Close();
                }
                #endregion
            }
        }
        public void SaveErrOutput(string f, string name, string date, string ext)
        {
            if (MainSettings.Default.L_bAutosave == true && MainSettings.Default.L_bError == true)
            {
                #region Autosave + folder names
                if (MainSettings.Default.L_FolderName != String.Empty)
                {
                    if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory + Path.DirectorySeparatorChar + MainSettings.Default.L_FolderName)))
                        Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory + Path.DirectorySeparatorChar + MainSettings.Default.L_FolderName));
                    StreamWriter txt = new StreamWriter(Path.Combine(Environment.CurrentDirectory + Path.DirectorySeparatorChar + MainSettings.Default.L_FolderName + Path.DirectorySeparatorChar + "err_" + name + date + ext), true);
                    txt.WriteLine(dt.ToLocalTime().ToString("HH:mm:ss: ") + f);
                    txt.Close();
                }
                if (MainSettings.Default.L_FolderName == String.Empty)
                {
                    StreamWriter txt = new StreamWriter(Path.Combine(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "err_" + name + date + ext), true);
                    txt.WriteLine(dt.ToLocalTime().ToString("HH:mm:ss: ") + f);
                    txt.Close();
                }
                #endregion
            }
        }
        #endregion
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            #region VAR
            DirectoryInfo di = new DirectoryInfo(e.FullPath);
            DriveInfo[] Drive = DriveInfo.GetDrives();
            dt = System.DateTime.UtcNow;
            string filename = "file";                                                                   
            string ext = "";                                                                            
            string LOG_date = "";                                                                       
            string prefix = "";                                                                         
            string sufix = "";                                                                          
            string BUP_date = "";                                                                       
            string[] dir = di.FullName.Split(Path.DirectorySeparatorChar);                              
            if (row_count > MainSettings.Default.S_MaxRow)
                row_count = MainSettings.Default.S_MaxRow;   
            if (MainSettings.Default.B_Prefix != string.Empty)
                prefix = MainSettings.Default.B_Prefix;                                                 
            if (MainSettings.Default.B_Sufix != string.Empty)
                sufix = MainSettings.Default.B_Sufix;                                                   
            if (MainSettings.Default.B_DataForm != string.Empty)
                BUP_date = dt.ToLocalTime().ToString(MainSettings.Default.B_DataForm);                  
            if (MainSettings.Default.L_Filename != string.Empty)
                filename = MainSettings.Default.L_Filename;                                             
            if (MainSettings.Default.L_Extension != string.Empty)
                ext = MainSettings.Default.L_Extension;                                                 
            if (MainSettings.Default.L_DataForm != string.Empty)
                LOG_date = dt.ToLocalTime().ToString(MainSettings.Default.L_DataForm);                  
            #endregion
            try
            {
                #region return
                if (MainSettings.Default.S_bExcCurrDir)
                {
                    if (di.FullName.Contains(Environment.CurrentDirectory)) return;
                    if (di.FullName.Equals(Environment.CurrentDirectory)) return;
                }
                if (dir.Length > 2) 
                    if (dir[1] == "Windows" && MainSettings.Default.F_bExcWINDOWS == true) 
                        return;
                if (dir.Length > 4) 
                    if ((dir[3] == "AppData" || dir[3] == "Application Data") && MainSettings.Default.F_bExcAPPDATA == true) 
                        return;
                #endregion
                lastName = di.Name;
                row_count++;
                count++;
                AddToTitle(count);
                this.Invoke((MethodInvoker)delegate{dataGridView1.Rows.Add(dt.ToLocalTime().ToString("HH:mm:ss"), e.ChangeType.ToString(), e.FullPath);});
                this.Invoke((MethodInvoker)delegate{dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;});
                if (dataGridView1.RowCount > MainSettings.Default.S_MaxRow) 
                    this.Invoke((MethodInvoker)delegate{dataGridView1.Rows.Remove(dataGridView1.Rows[0]);});
                #region backup
                if (e.ChangeType.ToString() == "Changed" && MainSettings.Default.B_bBackup == true)
                {
                    try
                    {
                        if (MainSettings.Default.B_FolderName != String.Empty)
                        {
                            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory + Path.DirectorySeparatorChar + MainSettings.Default.B_FolderName + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(di.Name))))
                                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory + Path.DirectorySeparatorChar + MainSettings.Default.B_FolderName + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(di.Name)));
                            File.Copy(e.FullPath, MakeUnique(Path.Combine(Environment.CurrentDirectory + Path.DirectorySeparatorChar + MainSettings.Default.B_FolderName + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(di.Name) + Path.DirectorySeparatorChar + prefix + Path.GetFileNameWithoutExtension(di.Name) + sufix + BUP_date + di.Extension.ToString())));
                        }
                        if (MainSettings.Default.B_FolderName == String.Empty)
                        {
                            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(di.Name))))
                                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(di.Name)));
                            File.Copy(e.FullPath, MakeUnique(Path.Combine(Environment.CurrentDirectory + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(di.Name) + Path.DirectorySeparatorChar + prefix + Path.GetFileNameWithoutExtension(di.Name) + sufix + BUP_date + di.Extension.ToString())));
                        }
                    }
                    #region Catch
                    catch (UnauthorizedAccessException f)
                    {
                        SaveErrOutput(f.ToString(), filename, LOG_date, ext);
                    }
                    catch (FileNotFoundException f)
                    {
                        SaveErrOutput(f.ToString(), filename, LOG_date, ext);
                    }
                    catch (Exception f)
                    {
                        SaveErrOutput(f.ToString(), filename, LOG_date, ext);
                    }
                    #endregion
                }
                #endregion
                SaveOutput(e.ChangeType.ToString() + " " + e.FullPath, filename, LOG_date, ext);
            }
            catch(Exception f)
            {
                SaveErrOutput(f.ToString(), filename, LOG_date, ext);
            }
        }
        private void OnRenamed(object source, RenamedEventArgs e)
        {
            #region VAR
            dt = System.DateTime.UtcNow;
            DirectoryInfo di = new DirectoryInfo(e.FullPath);
            string[] dir = e.FullPath.Split(Path.DirectorySeparatorChar);
            string filename = "file";                                                  
            string ext = "";                                                            
            string LOG_date = "";                                                       
            if (MainSettings.Default.L_Filename != string.Empty) filename = MainSettings.Default.L_Filename;                             
            if (MainSettings.Default.L_Extension != string.Empty) ext = MainSettings.Default.L_Extension;                                 
            if (MainSettings.Default.L_DataForm != string.Empty) LOG_date = dt.ToLocalTime().ToString(MainSettings.Default.L_DataForm);
            #endregion
            try
            {
                #region return
                if (dir.Length > 2) 
                    if (dir[1] == "Windows" && MainSettings.Default.F_bExcWINDOWS == true) 
                        return;
                if (dir.Length > 4) 
                    if ((dir[3] == "AppData" || dir[3] == "Application Data") && MainSettings.Default.F_bExcAPPDATA == true) 
                        return;
                #endregion
                this.Invoke((MethodInvoker)delegate { dataGridView1.Rows.Add(dt.ToLocalTime().ToString("HH:mm:ss"), e.ChangeType.ToString(), (e.OldFullPath + " -> " + di.Name)); });
                this.Invoke((MethodInvoker)delegate { dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1; });
                if (dataGridView1.RowCount > MainSettings.Default.S_MaxRow) 
                    this.Invoke((MethodInvoker)delegate { dataGridView1.Rows.Remove(dataGridView1.Rows[0]); });
                count++;
                AddToTitle(count);
                SaveOutput(e.ChangeType.ToString() + " " + e.OldFullPath + " -> " + di.Name, filename, LOG_date, ext);
            }
            catch (Exception f)
            {
                SaveErrOutput(f.ToString(), filename, LOG_date, ext);
            }
        }
        private void OnError(object source, ErrorEventArgs f)
        {
            #region VAR
            dt = System.DateTime.UtcNow;
            string filename = "file";                                                  
            string ext = "";                                                            
            string LOG_date = "";                                                       
            if (MainSettings.Default.L_Filename != string.Empty) 
                filename = MainSettings.Default.L_Filename;                             
            if (MainSettings.Default.L_Extension != string.Empty) 
                ext = MainSettings.Default.L_Extension;                                 
            if (MainSettings.Default.L_DataForm != string.Empty) 
                LOG_date = dt.ToLocalTime().ToString(MainSettings.Default.L_DataForm);
            #endregion
            try
            {
                SaveErrOutput(f.ToString(), filename, LOG_date, ext);
            }
            catch (Exception g)
            {
                SaveErrOutput(g.ToString(), filename, LOG_date, ext);
            }
        }
        #region Buttons
        private void button1_Click(object sender, EventArgs e)
        {
            LoadPath();
            this.Invoke((MethodInvoker)delegate{ this.TopMost = MainSettings.Default.S_MostTop; });
            DriveInfo[] Drive = DriveInfo.GetDrives();
            drives = Environment.GetLogicalDrives();
            if (MainSettings.Default.S_SettOpen == true) 
                return;
            if (MainSettings.Default.F_bALLDRIVES == true)
                limiter = drives.Count();
            if (MainSettings.Default.F_bALLDRIVES == false)
                limiter = 8;
            for (_x = 0; _x != limiter; _x++)
            {
                if (MainSettings.Default.F_bALLDRIVES == false && (paths[_x] != string.Empty && filters[_x] != string.Empty))
                {
                    Watcher[_x].Path = paths[_x];
                    Watcher[_x].Filter = filters[_x];
                    Watcher[_x].IncludeSubdirectories = sub[_x];
                    Watcher[_x].EnableRaisingEvents = true;
                }
                if (MainSettings.Default.F_bALLDRIVES == true && (Drive[_x].DriveType.ToString() != "CDRom"))
                {
                    Watcher[_x].Path = drives[_x];
                    Watcher[_x].Filter = "*";
                    Watcher[_x].IncludeSubdirectories = true;
                    Watcher[_x].EnableRaisingEvents = true;
                }
            }
            button1.Enabled = false;
            button3.Enabled = true;
            button5.Enabled = false;
            AddToTitle(count);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MainSettings.Default.Save();
            Application.Exit();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate{this.TopMost = false;});
            DriveInfo[] Drive = DriveInfo.GetDrives();
            if (MainSettings.Default.F_bALLDRIVES == true)
                limiter = drives.Count();
            if (MainSettings.Default.F_bALLDRIVES == false)
                limiter = 8;
            for (_x = 0; _x != limiter; _x++)
            {
                if (MainSettings.Default.F_bALLDRIVES == false && (paths[_x] != string.Empty && filters[_x] != string.Empty))
                    Watcher[_x].EnableRaisingEvents = false;
                if (MainSettings.Default.F_bALLDRIVES == true && (Drive[_x].DriveType.ToString() != "CDRom"))
                    Watcher[_x].EnableRaisingEvents = false;
            }
            button1.Enabled = true;
            button3.Enabled = false;
            button5.Enabled = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            count = 0;
            this.Invoke((MethodInvoker)delegate{dataGridView1.Rows.Clear();});
            AddToTitle(count);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (MainSettings.Default.S_SettOpen == false)
            {
                Form2 f2 = new Form2();
                f2.ShowDialog();
            }
        }
        #endregion
        private void showProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }
        private void exitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainSettings.Default.Save();
            Application.Exit();
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }
    }
}

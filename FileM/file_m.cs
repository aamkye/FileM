using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileM
{
    class file_m
    {

        private DateTime DT;
        public FileSystemWatcher[] FSW;
        private bool[] sub_folder;
        private int size = 0;
        private int count = 0;
        private int limiter = 0;
        private int row_count = 0;
        private string lastName = "";
        private string[] args = System.Environment.GetCommandLineArgs();
        private string[] drives;
        private string[] filters = new string[8];
        private string[] paths = new string[8];

        public bool AddEvent(FileSystemWatcher T1)
        {
            T1 = new FileSystemWatcher();
            T1.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.Security | NotifyFilters.Size;
            T1.Changed += new FileSystemEventHandler(OnChanged);
            T1.Created += new FileSystemEventHandler(OnChanged);
            T1.Deleted += new FileSystemEventHandler(OnChanged);
            T1.Renamed += new RenamedEventHandler(OnRenamed);
            T1.Error += new ErrorEventHandler(OnError);
            T1.EnableRaisingEvents = false;
        }
        public file_m(int size)
        {
            this.size = size;
            DT = new DateTime();
            FSW = new FileSystemWatcher[64];
            sub_folder = new bool[64];
        }

    }
}

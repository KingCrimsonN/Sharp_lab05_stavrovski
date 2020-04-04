﻿

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;

namespace Sharp_lab05_stavrovskyi.Models
{
    class MyProcess
    {
        #region Fields

        private Process _process;
        private int _id;
        private string _name;
        private bool _isActive;
        private float _ram;
        private int _threads;
        private string _user;
        private string _filePath;
        private DateTime _startTime;
        private PerformanceCounter _pCounter;
        #endregion


        #region Properties

        public Process Process
        {
            get { return _process; }
        }

        public bool IsActive
        {
            get { return _isActive; }
        }

        public int ID
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        
        public string User
        {
            get { return _user; }
        }

        public double CPU
        {
            get { return Math.Round((_pCounter.NextValue() / Environment.ProcessorCount)*100,2); }
        }

        public double RAM
        {
            get { return _ram; }
        }

        public int Threads
        {
            get { return _threads; }
        }

        public DateTime StartTime
        {
            get { return _startTime; }
        }

        public string FilePath
        {
            get { return _filePath; }
        }

        #endregion

        internal MyProcess(Process pr)
        {
            _process = pr;
            Update();
        }

        public async void Update()
        {
            //_pCounter.NextValue();
            await Task.Run((() => {
                _name = _process.ProcessName;
                _pCounter = new PerformanceCounter("Process", "% Processor Time", _name);
                _pCounter.NextValue();
                _filePath = _process.MainModule.FileName;
                _id = _process.Id;
            }));
            _threads = _process.Threads.Count;
            _isActive = _process.Responding;
            _ram = (float)Math.Round((double)(_process.PrivateMemorySize64/1024/1024),2);
            _startTime = _process.StartTime;
            _user = GetUser(_process);

        }

        private static string GetUser(Process process)
        {
            IntPtr processHandle = IntPtr.Zero;
            try
            {
                OpenProcessToken(process.Handle, 8, out processHandle);
                WindowsIdentity wi = new WindowsIdentity(processHandle);
                string user = wi.Name;
                return user.Contains(@"\") ? user.Substring(user.IndexOf(@"\") + 1) : user;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (processHandle != IntPtr.Zero)
                {
                    CloseHandle(processHandle);
                }
            }
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

    }
}

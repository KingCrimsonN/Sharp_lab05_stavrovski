using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Sharp_lab05_stavrovskyi.Models;

namespace Sharp_lab05_stavrovskyi.Tools.Managers
{
    internal static class StationManager
    {
        public static event Action StopThreads;

        private static List<int> _deniedList = new List<int>();

        private static List<MyProcess> _processList = new List<MyProcess>();

        private static MyProcess _selectedProcess;

        //internal static Person CurrentUser { get; set; }

        internal static List<MyProcess> ProcessList
        {
            get
            {
                return _processList;
            }
        }

        internal static MyProcess SelectedProcess
        {
            get
            {
                return _selectedProcess; 
            }
            set { _selectedProcess = value; }
        }

        internal static void Initialize()
        {
            _processList = new List<MyProcess>();
            Process[] p = Process.GetProcesses();
            foreach (Process process in p)
            {
                if (!_deniedList.Contains(process.Id) && CheckModule(process))
                {
                    _processList.Add(new MyProcess(process));
                }
            }
            //AddProcesses();
        }

        public static void Update()
        {
            AddProcesses();
        }

        private static void AddProcesses()
        {
            //_processList = new List<MyProcess>();
            Process[] p = Process.GetProcesses();
            foreach (var process in p)
            {

                if (!_deniedList.Contains(process.Id) && CheckModule(process) && !HasProcess(process.Id))
                {
                    _processList.Add(new MyProcess(process));
                }

            }
        }

        private static bool HasProcess(int id)
        {
            MyProcess temp;
            try
            {
                foreach (MyProcess item in _processList)
                {
                    if (item.Process.HasExited)
                    {
                        _processList.Remove(item);
                        continue;
                    }

                    if (id == item.ID)
                    {
                        return true;
                    }
                }

                return false;   
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        private static bool CheckModule(Process process)
        {
            try
            {
                var m = process.MainModule;
                return true;
            }
            catch (Win32Exception e)
            {
                _deniedList.Add(process.Id);
                return false;
            }
            catch (System.InvalidOperationException)
            {
                _deniedList.Add(process.Id);
                return false;
            }

        }

        public static void RemoveProcess(MyProcess p)
        {
            _processList.Remove(p);
        }

        internal static void CloseApp()
        {
            //MessageBox.Show("ShutDown");
            StopThreads?.Invoke();
            Environment.Exit(1);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Sharp_lab05_stavrovskyi.Models;

namespace Sharp_lab05_stavrovskyi.Tools.Managers
{
    internal static class StationManager
    {
        public static event Action StopThreads;

        private static List<int> _deniedList = new List<int>();

        private static List<MyProcess> _processList = new List<MyProcess>();

        //internal static Person CurrentUser { get; set; }

        internal static List<MyProcess> ProcessList
        {
            get
            {
                return _processList;
            }
        }

        internal static void Initialize()
        {

            _processList = new List<MyProcess>();
            foreach (Process process in Process.GetProcesses())
            {
                //Debug.WriteLine("WRITETEITHEITHEIHTIHETHIETIHEHTIEHTIEHITHIEHITHEIH");
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
            try
            {
                foreach (MyProcess item in _processList)
                {
                    if (id == item.ID)
                    {
                        return true;
                    }
                }

                return false;   
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("HasProcessError" + id);
                return true;

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
                Debug.WriteLine("ID : " + process.Id);
                //Console.WriteLine("WIN32EXFCEPTIONS");
                return false;
            }
            catch (System.InvalidOperationException)
            {
                _deniedList.Add(process.Id);
                return false;
            }

        }

        internal static void CloseApp()
        {
            //MessageBox.Show("ShutDown");
            StopThreads?.Invoke();
            Environment.Exit(1);
        }
    }
}

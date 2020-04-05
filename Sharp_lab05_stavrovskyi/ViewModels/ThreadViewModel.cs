using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Sharp_lab05_stavrovskyi.Models;
using Sharp_lab05_stavrovskyi.Tools;

namespace Sharp_lab05_stavrovskyi.ViewModels
{
    internal class ThreadViewModel : BaseViewModel
    {
        private ObservableCollection<MyThread> _threads;

        public string ProcessName
        {
            get;
        }

        public ObservableCollection<MyThread> Threads
        {
            get
            {

                return _threads;

            }
            private set
            {
                _threads = value;
                OnPropertyChanged();
            }
        }

        public Action CloseAction { get; set; }

        internal ThreadViewModel(MyProcess process)
        {
            Threads = new ObservableCollection<MyThread>();
            ObservableCollection<MyThread> temp = new ObservableCollection<MyThread>();
            ProcessName = process.Name;
            int id = process.ID;
            foreach (ProcessThread thread in process.ThreadCollection)
            {
                temp.Add(new MyThread(thread));
            }
            Threads = temp;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Sharp_lab05_stavrovskyi.Models;
using Sharp_lab05_stavrovskyi.Tools;

namespace Sharp_lab05_stavrovskyi.ViewModels
{
    internal class ModuleViewModel : BaseViewModel
    {
        private ObservableCollection<MyModule> _modules;

        public ObservableCollection<MyModule> Modules
        {
            get
            {

                return _modules;

            }
            private set
            {
                _modules = value;
                OnPropertyChanged();
            }
        }

        public Action CloseAction { get; set; }

        internal ModuleViewModel(MyProcess process)
        {
            Modules = new ObservableCollection<MyModule>();
            ObservableCollection<MyModule> temp = new ObservableCollection<MyModule>();
            foreach (ProcessModule module in process.ModuleCollection)
            {
                temp.Add(new MyModule(module));
            }
            Modules = temp;
        }
    }
}

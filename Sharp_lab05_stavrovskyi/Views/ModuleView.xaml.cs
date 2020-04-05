using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Sharp_lab05_stavrovskyi.Tools.Managers;
using Sharp_lab05_stavrovskyi.ViewModels;

namespace Sharp_lab05_stavrovskyi.Views
{
    /// <summary>
    /// Interaction logic for ModuleView.xaml
    /// </summary>
    public partial class ModuleView : Window
    {
        public ModuleView()
        {
            InitializeComponent();
            ModuleViewModel mvm = new ModuleViewModel(StationManager.SelectedProcess);
            DataContext = mvm;
            if (mvm.CloseAction == null)
                mvm.CloseAction = new Action(this.Close);

        }
    }
}

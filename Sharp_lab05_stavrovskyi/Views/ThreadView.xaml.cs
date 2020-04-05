using System;
using System.Windows;
using Sharp_lab05_stavrovskyi.Tools.Managers;
using Sharp_lab05_stavrovskyi.ViewModels;

namespace Sharp_lab05_stavrovskyi.Views
{
    /// <summary>
    /// Interaction logic for ThreadView.xaml
    /// </summary>
    public partial class ThreadView : Window
    {
        public ThreadView()
        {
            InitializeComponent();
            ThreadViewModel tvm = new ThreadViewModel(StationManager.SelectedProcess);
            DataContext = tvm;
            if (tvm.CloseAction == null)
                tvm.CloseAction = new Action(this.Close);
        }
    }
}

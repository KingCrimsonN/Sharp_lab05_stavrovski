using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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

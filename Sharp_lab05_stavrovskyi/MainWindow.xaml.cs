
using System.ComponentModel;

using System.Windows;
using System.Windows.Controls;

using Sharp_lab05_stavrovskyi.Tools;
using Sharp_lab05_stavrovskyi.Tools.Managers;
using Sharp_lab05_stavrovskyi.ViewModels;

namespace Sharp_lab05_stavrovskyi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IContentOwner
    {

        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }

        public MainWindow()
        {

            DataContext = new ProcessViewModel();
            StationManager.Initialize();
            InitializeComponent();
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            StationManager.CloseApp();
        }
    }
}

using System.Windows.Controls;
using Sharp_lab05_stavrovskyi.ViewModels;

namespace Sharp_lab05_stavrovskyi.Views
{
    /// <summary>
    /// Interaction logic for ProcessView.xaml
    /// </summary>
    public partial class ProcessView : UserControl
    {
        public ProcessView()
        {
            DataContext = new ProcessViewModel();
            InitializeComponent();
        }
    }
}

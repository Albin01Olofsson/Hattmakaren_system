using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views1
{
    public partial class LagerPage : Page
    {
        public LagerPage()
        {
            InitializeComponent();
            DataContext = new LagerViewModel();
        }
    }
}
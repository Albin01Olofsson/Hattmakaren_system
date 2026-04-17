using System.Windows.Controls;
using WpfApp1.ViewModels;


namespace WpfApp1.Views1
{
    public partial class BestallningarPage : Page
    {
        public BestallningarPage()
        {
            InitializeComponent();
            DataContext = new BestallningarViewModel();
        }
    }
}
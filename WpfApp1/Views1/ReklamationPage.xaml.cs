using BL.Services;
using DAL;
using DAL.Repositorys;
using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views1
{
    public partial class ReklamationPage : Page
    {
        public ReklamationPage()
        {
            InitializeComponent();

            var context = new DBcontext();

            DataContext = new ReklamationViewModel(
                new ReklamationService(new ReklamationRepository(context)),
                new OrderService(new OrderRepo(context), context));
        }
    }
}

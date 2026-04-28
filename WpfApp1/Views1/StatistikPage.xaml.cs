using BL.Services;
using DAL;
using DAL.Repositorys;
using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views1
{
    public partial class StatistikPage : Page
    {
        public StatistikPage()
        {
            InitializeComponent();

            var context = new DBcontext();

            DataContext = new StatistikViewModel(
                new MaterialService(new MaterialRepo(context)),
                new ProduktService(new ProduktRepo(context)),
                new KundService(new KundRepo(context)),
                new OrderService(new OrderRepo(context), context),
                context);
        }
    }
}

using BL.Interfaces;
using BL.Services;
using DAL.Repositorys;
using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ViewModels;

namespace WpfApp1.Views1
{
    /// <summary>
    /// Interaction logic for OrderBeskrivningPage.xaml
    /// </summary>
    public partial class OrderBeskrivningPage : Page
    {
        public OrderBeskrivningPage(Order o)
        {
            InitializeComponent();
            IProduktService produktService = new ProduktService(new ProduktRepo(new DBcontext()));
            DataContext = new OrderBeskrivningVM(o, produktService);
;       }
    }
}

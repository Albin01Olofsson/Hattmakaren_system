using BL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
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
using System.Windows.Shapes;
using WpfApp1.ViewModels;

namespace WpfApp1.Views1
{
    /// <summary>
    /// Interaction logic for SkapaAktivitet.xaml
    /// </summary>
    public partial class SkapaAktivitet : Window
    {
        public Planering CreatedPlanering { get; private set; }
        public SkapaAktivitet()
        {
            InitializeComponent();
            var user = Session.CurrentUser;
            var serviceProvider = ((App)Application.Current).ServiceProvider;
            var service = serviceProvider.GetRequiredService<IOrderService>();
            var vm = new PlaneringViewModel(user, service);
            DataContext = vm;
        }
    }
}

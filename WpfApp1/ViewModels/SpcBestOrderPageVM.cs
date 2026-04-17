using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    public partial class SpcBestOrderPageVM : ObservableObject
    {
        private IOrderService _orderService;
        private IProduktService _produktService;
        public SpcBestOrderPageVM(IOrderService orderService, IProduktService produktService)
        {
            _orderService = orderService;
            _produktService = produktService;
        }
    }
}

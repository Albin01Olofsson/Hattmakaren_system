using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using BL.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;


namespace WpfApp1.ViewModels
{
    public partial class OrderVM : ObservableObject
    {
        private IOrderService _service;

        [ObservableProperty]
        private ObservableCollection<Order> orderList;
        public OrderVM(IOrderService s)
        {
            _service = s;
        }

        [RelayCommand]
        private void Sök()
        {
            var sökResultat = _service.GetOrderList();
        }
    }
}

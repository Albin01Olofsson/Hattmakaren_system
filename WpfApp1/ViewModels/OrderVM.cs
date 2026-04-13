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

        public int AntalOrders => OrderList.Count;
        public OrderVM(IOrderService s)
        {
            _service = s;
            OrderList = new ObservableCollection<Order>(_service.GetOrdersWithNavProps());

            //var sökResultat = _service.GetOrdersWithNavProps();
            //OrderList = new ObservableCollection<Order>(sökResultat);

            //OnPropertyChanged(nameof(AntalOrders));
        }

        [RelayCommand]
        private void Sök()
        {
            //var sökResultat = _service.GetOrdersWithNavProps();
            //OrderList = new ObservableCollection<Order>(sökResultat);

            //OnPropertyChanged(nameof(AntalOrders));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        [ObservableProperty]
        private String sökString = string.Empty;

        public int AntalOrders => OrderList.Count;

        public OrderVM(IOrderService s)
        {
            _service = s;
            OrderList = new ObservableCollection<Order>(_service.GetOrdersWithNavProps());
        }

        [RelayCommand]
        private void Sök()
        {
            var sökResultat = _service.GetOrdersWithNavProps();

            if (!string.IsNullOrWhiteSpace(SökString))
            {
                sökResultat = sökResultat.Where(
                    o => o.Kund.Namn.StartsWith(SökString, StringComparison.OrdinalIgnoreCase) || 
                    o.OrderID.ToString().StartsWith(SökString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            OrderList = new ObservableCollection<Order>(sökResultat);
            OnPropertyChanged(nameof(AntalOrders));
        }


        

    }
}

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

        [ObservableProperty]
        public DateTime? datumTillFilter;

        [ObservableProperty]
        public DateTime? datumFrånFilter;

        [ObservableProperty]
        public string orderStartareFilter;

        [ObservableProperty]
        public string klarFilter = "Ospecificerat";

        [ObservableProperty]
        public string specialBeställningFilter = "Ospecificerat";



        public OrderVM(IOrderService s)
        {
            _service = s;
            OrderList = new ObservableCollection<Order>(_service.GetOrdersWithNavProps());
        }

        [RelayCommand]
        private void Sök()
        {
            var sökResultat = _service.GetOrdersWithNavProps();

            //Fylla orderlistan som visas på sökta ordrar, med filtreringen

            //Sökstring
            if (!string.IsNullOrWhiteSpace(SökString))
            {
                sökResultat = sökResultat.Where(
                    o => o.Kund.Namn.StartsWith(SökString, StringComparison.OrdinalIgnoreCase) || 
                    o.OrderID.ToString().StartsWith(SökString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            //Datum från Filter
            if (DatumFrånFilter.HasValue)
            {
                sökResultat = sökResultat.Where(o => o.Datum.Date >= DatumFrånFilter.Value.Date).ToList();
            }

            //Datum till Filter
            if (DatumTillFilter.HasValue)
            {
                sökResultat = sökResultat.Where(o => o.Datum.Date <= DatumTillFilter.Value.Date).ToList();
            }

            //Order startare Filter
            if (OrderStartareFilter != "Alla")
            {
                sökResultat = sökResultat.Where(o => o.StartadAv.Namn == OrderStartareFilter).ToList();
            }

            //Klar status Filter
            if (KlarFilter == "Klar")
            {
                sökResultat = sökResultat.Where(o => o.Färdig == true).ToList();
            }
            else if (KlarFilter == "Ej Klar")
            {
                sökResultat = sökResultat.Where(o => o.Färdig == false).ToList();
            }

            //Specialbeställning Filter
            //if(SpecialBeställning == "Ja")
            //{
            //    sökResultat = sökResultat.Where(o => o.IsSpecialBeställning == true).ToList();
            //}else if(SpecialBeställning == "Nej")
            //{
            //    sökResultat = sökResultat.Where(o => o.IsSpecialBeställning == false).ToList();
            //}


            OrderList = new ObservableCollection<Order>(sökResultat);
            OnPropertyChanged(nameof(AntalOrders));
        }


        

    }
}

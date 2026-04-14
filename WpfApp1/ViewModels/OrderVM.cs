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
        public string orderStartareFilter = "Alla";

        [ObservableProperty]
        public string klarFilter = "Ospecificerat";

        [ObservableProperty]
        public string specialBeställningFilter = "Ospecificerat";


        //Lista för att fylla Order Startares namn som options i Combobox
        [ObservableProperty]
        public ObservableCollection<string> orderStartareNamnList;

        public OrderVM(IOrderService s)
        {
            _service = s;
            OrderList = new ObservableCollection<Order>(_service.GetOrdersWithNavProps());

            OrderStartareNamnList = new ObservableCollection<string>();

            OrderStartareNamnList.Add("Alla");

            foreach(string namn in _service.GetOrderStartareNamnList().OrderBy(n => n))
            {
                OrderStartareNamnList.Add(namn);
            }
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
            if (SpecialBeställningFilter == "Ja")
            {
                sökResultat = sökResultat.Where(o => o.IsSpecialbeställning == true).ToList();
            }
            else if (SpecialBeställningFilter == "Nej")
            {
                sökResultat = sökResultat.Where(o => o.IsSpecialbeställning == false).ToList();
            }


            OrderList = new ObservableCollection<Order>(sökResultat);
            OnPropertyChanged(nameof(AntalOrders));
        }


        

    }
}

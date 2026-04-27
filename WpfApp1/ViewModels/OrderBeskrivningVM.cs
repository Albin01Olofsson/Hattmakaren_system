using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Models;
using System.Collections.ObjectModel;

namespace WpfApp1.ViewModels
{
    public partial class OrderBeskrivningVM : ObservableObject
    {
        private Order ValdOrder;
        private IProduktService _service;

        [ObservableProperty]
        private int ordernsID;

        [ObservableProperty]
        private string kundNamn;

        [ObservableProperty]
        private decimal pris;

        [ObservableProperty]
        private Decimal rabatt;

        [ObservableProperty]
        private DateTime? datum;

        [ObservableProperty]
        private string orderStatus;

        [ObservableProperty]
        private ObservableCollection<Produkt> produkLista;

        [ObservableProperty]
        private ObservableCollection<SpecialBeställning> specialBeställningarLista;

        [ObservableProperty]
        private string orderStartareNamn;

        [ObservableProperty]
        private bool färdig;

        [ObservableProperty]
        private bool isSpecialbeställning;

        [ObservableProperty]
        private string? bildKälla = string.Empty;

        public OrderBeskrivningVM(Order o, IProduktService s)
        {
            _service = s;
            ValdOrder = o;

            OrdernsID = ValdOrder.OrderID;
            KundNamn = ValdOrder.Kund.Namn;
            Pris = ValdOrder.Pris;
            Rabatt = ValdOrder.Rabatt;
            Datum = ValdOrder.Datum;
            ProdukLista = new ObservableCollection<Produkt>(ValdOrder.OrderRader.Select(or => or.Produkt));
            SpecialBeställningarLista = new ObservableCollection<SpecialBeställning>(ValdOrder.OrderRader.Select(or => or.Produkt).OfType<SpecialBeställning>());
            OrderStartareNamn = ValdOrder.StartadAv.Namn;
            Färdig = ValdOrder.Färdig;
            IsSpecialbeställning = ValdOrder.IsSpecialbeställning;
        }
    }
}

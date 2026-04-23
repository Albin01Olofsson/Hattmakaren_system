using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DAL.Intefaces;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private ObservableCollection<Produkt> produkLista;
        //Funktionaliteten att visa produktlista i programmet funkar, men måste vänta på att riktiga ordar skapas,
        //eftersom att man inte kan lägga till produkter i produktlista på en order för exempeldata

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
            OrderStartareNamn = ValdOrder.StartadAv.Namn;
            Färdig = ValdOrder.Färdig;
            IsSpecialbeställning = ValdOrder.IsSpecialbeställning;
            BildKälla = "C:\\Users\\david\\Desktop\\CvBuddy-G16-master\\Hattmakaren_system\\DAL\\Bilder\\bildsaknas.png\"";
        }
    }
}

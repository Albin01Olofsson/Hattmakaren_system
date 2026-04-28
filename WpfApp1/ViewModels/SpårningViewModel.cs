using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using BL.Interfaces;
using BL.Services;
using Models;
using CommunityToolkit.Mvvm.Input;

namespace WpfApp1.ViewModels
{
    public partial class SpårningViewModel : ObservableObject
    {

        private readonly IOrderService _orderService;
        private readonly IFraktjaktService _fraktService;

        [ObservableProperty]
        private ObservableCollection<Order> plockadeOrdrar = new();
        [ObservableProperty]
        private ObservableCollection<SpårningsPunkt> uppdateringar = new();

        [ObservableProperty]
        private double lat = 59.3293;
        [ObservableProperty]
        private double lng = 18.0686;

        [ObservableProperty]
        private Order? valdOrder;

        public SpårningViewModel(IOrderService orderService, IFraktjaktService fraktService)
        {
            _orderService = orderService;
            _fraktService = fraktService;
            //SkapaTestOrder();
            LaddaOrdrar();
        }

        public async Task LaddaOrdrar()
        {
            var alla = await _orderService.GetOrdersWithNavProps();


            PlockadeOrdrar.Clear();
            foreach (var o in alla)
            {
                PlockadeOrdrar.Add(o);
            }
        }

        public async Task HämtaHistorikFrånFraktjakt(string sändningsnummer)
        {
            var historik = await _fraktService.HämtaHistorik(sändningsnummer);

            Uppdateringar.Clear();

            if (historik != null)
            {
                foreach (var punkt in historik)
                {
                    Uppdateringar.Add(punkt);
                }

                var senaste = historik.First().Meddelande;

                await _orderService.UppdateraFraktStatus(sändningsnummer, senaste);
            }
        }



        public void SkapaTestOrder()
        {
            var testOrder = new Order()
            {
                OrderID = 999,
                Status = "Ute för leverans",
                Kund = new Kund { Namn = "Test Testsson" }
            };

            if (testOrder.Frakt == null)
            {
                testOrder.Frakt = new ObservableCollection<Frakt>();
            }
            testOrder.Frakt.Add(new Frakt
            {
                Sändningsnummer = "SHIPPING-123",
                Status = "påväg"

            });

            PlockadeOrdrar.Add(testOrder);
        }



        partial void OnValdOrderChanged(Order? value)
        {
            // Kolla att både ordern och frakt-listan faktiskt finns
            if (value?.Frakt != null && value.Frakt.Any())
            {
                var snr = value.Frakt.First().Sändningsnummer;
                if (!string.IsNullOrEmpty(snr))
                {
                    _ = HämtaHistorikFrånFraktjakt(snr);
                }
            }
        }
    

        [RelayCommand]
        public async Task MarkeraSomSkickad()
        {
            if (ValdOrder != null && ValdOrder.Frakt.Any())
            {
                var sndNr = ValdOrder.Frakt.First().Sändningsnummer;
                await _orderService.UppdateraFraktStatus(sndNr, "Skickad");
                await HämtaHistorikFrånFraktjakt(sndNr);
                await LaddaOrdrar();
            }
        }
    }
}


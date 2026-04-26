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

namespace WpfApp1.ViewModels
{
    public partial class SpårningViewModel : ObservableObject
    {

        private readonly IOrderService _orderService;
        private readonly FraktjaktSimulator _simulator = new FraktjaktSimulator();

        [ObservableProperty]
        private ObservableCollection<Order> plockadeOrdrar = new();
        [ObservableProperty]
        private ObservableCollection<SpårningsPunkt> uppdateringar = new();

        [ObservableProperty]
        private double lat = 59.3293;
        [ObservableProperty]
        private double lng = 18.0686;

        [ObservableProperty]
        public partial Order? valdOrder { get; set; }

        public SpårningViewModel(IOrderService orderService)
        {
            _orderService = orderService;
            SkapaTestOrder();
            //LaddaOrdrar();
        }

        public async void LaddaOrdrar()
        {
            var alla = await _orderService.GetOrdersWithNavProps();
            var underTransport = alla.Where(o => o.Status == "Skickad" || o.Status == "Ute för leverans" || o.Status == "Levererad").ToList();

            PlockadeOrdrar.Clear();
            foreach (var o in underTransport)
            {
                PlockadeOrdrar.Add(o);
            }
        }
        
        public async Task HämtaHistorikFrånFraktjakt(string sändningsnummer)
        {
            var historik = await _simulator.HämtaHistorik(sändningsnummer);

            Uppdateringar.Clear();

            if (historik != null)
            {
                foreach (var punkt in historik)
                {
                    Uppdateringar.Add(punkt);
                }
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

            if(testOrder.Frakt == null)
            {
                testOrder.Frakt = new ObservableCollection<Frakt>();
            }
            testOrder.Frakt.Add(new Frakt
            {
                Sändningsnummer = "SHIPPING-123",
                status = "påväg"

            });

            PlockadeOrdrar.Add(testOrder);
        }

    }
}

using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System.Collections.ObjectModel;

namespace WpfApp1.ViewModels
{
    public partial class PlaneringViewModel : ObservableObject
    {
        private readonly IPlaneringsYtaService _service;
        private readonly IOrderService _orderService;

        public event Action<Planering> PlaneringAdded;

        // Denna sätts manuellt i SkapaAktivitet.xaml.cs
        [ObservableProperty]
        private Användare _user;

        // Samlingar som binds till UI
        public ObservableCollection<Order> AllaOrdrar { get; } = new();
        public ObservableCollection<Produkt> AllaProdukter { get; } = new();

        [ObservableProperty]
        private Order valdOrder;

        [ObservableProperty]
        private Produkt valdProdukt;

        [ObservableProperty]
        private DateTime? valdStartTid;

        [ObservableProperty]
        private int valdStartTimme;

        public PlaneringViewModel(IOrderService orderService, IPlaneringsYtaService planeringsService)
        {
            _orderService = orderService;
            _service = planeringsService;

            LaddaOrdrar();
        }

        public void LaddaOrdrar()
        {
            AllaOrdrar.Clear();

            var ordrarFrånDB = _orderService.GetOrdersWithNavProps();
            
            foreach (var order in ordrarFrånDB)
            {
                AllaOrdrar.Add(order);
            }
        }
        partial void OnValdOrderChanged(Order value)
        {
            AllaProdukter.Clear();
            if (value == null)
                return;

            var produkter = _service.HämtaLedigaProdukter(value.OrderID);
            foreach(var produkt in produkter)
            {
                AllaProdukter.Add(produkt);
            }
        }

        [RelayCommand]
        private void SparaAktivitet()
        {
            if (ValdProdukt == null || User == null || !ValdStartTid.HasValue)
                return;
            
            var startTid = valdStartTid.Value.Date.AddHours(ValdStartTimme);

            var planering = _service.PlaneraArbete(User.AnvändarID, ValdProdukt.ProduktID, startTid);

            PlaneringAdded?.Invoke(planering);

            ValdProdukt = null;
        }
    }
}
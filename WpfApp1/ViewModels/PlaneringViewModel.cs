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

        //[ObservableProperty]
        //private OrderRad valdOrderRad;

        [ObservableProperty]
        private DateTime? valdStartTid;

        [ObservableProperty]
        private int? valdStartTimme;

        [ObservableProperty]
        private DateTime? valdSlutTid;

        [ObservableProperty]
        private int? valdSlutTimme;

        [ObservableProperty]
        private string planeringsNamn;

        [ObservableProperty]
        private string valdTyp = "Order";

        [ObservableProperty]
        private string ordrarFel;
        [ObservableProperty]
        private string produkterFel;
        [ObservableProperty]
        private string startTidFel;
        [ObservableProperty]
        private string timmarFel;

        public PlaneringViewModel(IOrderService orderService, IPlaneringsYtaService planeringsService)
        {
            _orderService = orderService;
            _service = planeringsService;

            _ = LaddaOrdrar();
        }

        public async Task LaddaOrdrar()
        {
            AllaOrdrar.Clear();

            var ordrarFrånDB = await _orderService.GetOrdersWithNavProps();

            foreach (var order in ordrarFrånDB)
            {
                AllaOrdrar.Add(order);
            }
        }
        partial void OnValdOrderChanged(Order value)
        {
            _ = LaddaProdukter(value);// fire-and-forget kör async utan await
        }
        public async Task LaddaProdukter(Order value)
        {
            AllaProdukter.Clear();
            if (value == null)
                return;

            var produkter = await _service.HämtaLedigaProdukter(value.OrderID);
            foreach (var produkt in produkter)
            {
                AllaProdukter.Add(produkt);
            }
        }
        [RelayCommand]
        private async Task SparaAktivitet()
        {
            OrdrarFel = "";
            ProdukterFel = "";
            StartTidFel = "";
            TimmarFel = "";

            bool hasError = false;

            if (ValdOrder == null)
            {
                OrdrarFel = "Välj order";
                hasError = true;
            }

            if (ValdProdukt == null)
            {
                ProdukterFel = "Välj produkt";
                hasError = true;
            }

            if (!ValdStartTid.HasValue || !ValdStartTimme.HasValue ||
                !ValdSlutTid.HasValue || !ValdSlutTimme.HasValue)
            {
                StartTidFel = "Välj tid";
                hasError = true;
            }
            var startTid = ValdStartTid.Value.Date.AddHours(ValdStartTimme.Value);
            var slutTid = ValdSlutTid.Value.Date.AddHours(ValdSlutTimme.Value);

            if (slutTid <= startTid)
            {
                TimmarFel = "Arbetet måste sluta efter att det har börjat";
                return;
            }

            if (User == null || hasError)
                return;



            var orderRad = HämtaOrderRad();

            if (orderRad == null)
            {
                ProdukterFel = "Kunde inte hitta orderrad för vald produkt";
                return;
            }

            var planering = new Planering
            {
                AnvändarID = User.AnvändarID,
                OrderRadID = orderRad.OrderRadID,
                StartTid = startTid,
                SlutTid = slutTid,

                PlaneringsNamn = string.IsNullOrWhiteSpace(PlaneringsNamn)
                    ? ValdProdukt.Namn
                    : PlaneringsNamn,

                Status = "Ej påbörjat"
            };

            await _service.Add(planering);

            PlaneringAdded?.Invoke(planering);

            ValdProdukt = null;
            ValdOrder = null;
        }
        private OrderRad HämtaOrderRad()
        {
            return ValdOrder?
                .OrderRader?
                .FirstOrDefault(or => or.ProduktID == ValdProdukt?.ProduktID);
        }

    }
}
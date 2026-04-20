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
        private int? valdStartTimme;
       

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

            LaddaOrdrar();
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
            bool HasErrors = false;

            if (ValdOrder == null)
            {
                OrdrarFel = "Vänligen välj en order!";
                HasErrors = true;
            }
            if(ValdProdukt == null)
            {
                ProdukterFel = "Vänligen välj en produkt!";
                HasErrors = true;
            }
            if(ValdStartTid == null)
            {
                StartTidFel = "Vänligen välj vilken dag bokningen startar!";
                HasErrors = true;
            }
            if(ValdStartTimme == null)
            {
                TimmarFel = "Vänligen välj vilken tid bokningen börjar!";
                HasErrors = true;
            }
            
            if (ValdOrder == null || ValdProdukt == null || User == null || !ValdStartTid.HasValue || HasErrors)
                return;
            
            var startTid = valdStartTid.Value.Date.AddHours(ValdStartTimme.Value);

            var planering = await _service.PlaneraArbete(User.AnvändarID, ValdProdukt.ProduktID, startTid);

            PlaneringAdded?.Invoke(planering);

            ValdProdukt = null;
            OrdrarFel = "";
            ProdukterFel = "";
            StartTidFel = "";
            TimmarFel = "";
        }
    }
}
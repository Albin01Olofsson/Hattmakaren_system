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
        private Order _valdOrder;

        [ObservableProperty]
        private Produkt _valdProdukt;

        // KONSTRUKTOR: Tar nu endast emot tjänster som finns i App.xaml.cs
        public PlaneringViewModel(IOrderService orderService, IPlaneringsYtaService planeringsService)
        {
            _orderService = orderService;
            _service = planeringsService;

            LaddaOrdrar();
        }

        // Körs automatiskt när en order väljs i ComboBoxen
        partial void OnValdOrderChanged(Order value)
        {
            if (value != null)
            {
                LaddaProdukter(value.OrderID);
            }
            else
            {
                AllaProdukter.Clear();
            }
        }

        public void LaddaOrdrar()
        {
            var ordrarFrånDB = _orderService.GetOrdersWithNavProps();
            AllaOrdrar.Clear();
            foreach (var order in ordrarFrånDB)
            {
                AllaOrdrar.Add(order);
            }
        }

        public void LaddaProdukter(int orderid)
        {
            // Nu är _service inte längre null!
            var produkterFrånDB = _service.HämtaHattarFrånOrder(orderid);
            AllaProdukter.Clear();
            foreach (var produkt in produkterFrånDB)
            {
                AllaProdukter.Add(produkt);
            }
        }

        [RelayCommand]
        private void SparaAktivitet()
        {
            if (ValdProdukt != null && User != null)
            {
                _service.PlaneraArbete(User.AnvändarID, ValdProdukt.ProduktID, DateTime.Now);

                // Eventuellt stänga fönstret här eller skicka ett event
            }
        }
    }
}
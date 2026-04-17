using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System.Collections.ObjectModel;
using System.Security.RightsManagement;

namespace WpfApp1.ViewModels
{
    public partial class PlaneringViewModel :ObservableObject
    {
        private readonly IPlaneringsYtaService _service;
        private IOrderService _orderService;
        public event Action<Planering> PlaneringAdded;
        public Användare _user;

        [ObservableProperty]
        private ObservableCollection<Order> allaOrdrar;
        [ObservableProperty]
        private Order valdOrder;
        [ObservableProperty]
        private ObservableCollection<Produkt> allaProdukter;
        [ObservableProperty]
        private Produkt valdProdukt;

        public PlaneringViewModel(Användare user, IOrderService service)
        {
            _user = user;
            _orderService = service;
            LaddaOrdrar();
            LaddaProdukter(ValdOrder.OrderID);
        }

        public void LaddaOrdrar()
        {
            var ordrarFrånDB = _orderService.GetOrdersWithNavProps();
            AllaOrdrar.Clear();
            foreach(var oreder in ordrarFrånDB)
            {
                AllaOrdrar.Add(oreder);
            }
        }
        public void LaddaProdukter(int orderid)
        {
            var produkterFrånDB = _service.HämtaHattarFrånOrder(orderid);
            AllaProdukter.Clear();
            foreach(var produkt in produkterFrånDB)
            {
                AllaProdukter.Add(produkt);
            }
        }

        [RelayCommand]
        private void SparaAktivitet()
        {
            var planering = _service.PlaneraArbete(_user.AnvändarID, ValdProdukt.ProduktID, DateTime.Now);
            PlaneringAdded?.Invoke(planering);
        }

    }
}

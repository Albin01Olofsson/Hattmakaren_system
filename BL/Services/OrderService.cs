using BL.Interfaces;
using DAL.Intefaces;
using Models;

namespace BL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;

        public OrderService(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public List<Order> GetOrderList() => _orderRepo.GetAll();

        public Order GetOrder(int id) => _orderRepo.GetById(id);

        public void AddOrder(Order o) => _orderRepo.Add(o);

        public void UpdateOrder(Order o) => _orderRepo.Update(o);

        public void DeleteOrder(int id) => _orderRepo.Delete(id);

        public void SaveOrder() => _orderRepo.Save();
        public void skapaOrder(Order nyOrder)
        {
            //Kollar så att det som inte får vara null i DB inte är det, och att det finns minst en produkt i ordern. Annars kastas ett undantag.
            if (nyOrder.KundID == 0 || nyOrder.StartadAvID == 0 || nyOrder.Produkter == null || !nyOrder.Produkter.Any())
            {
                throw new ArgumentException("Ordern måste ha en kund, en startande användare och minst en produkt.");
            }
            foreach (var produkt in nyOrder.Produkter)
            {
                // Om det är en specialbeställning ser vi till att den är "false" vid start
                if (produkt is SpecialBeställning)
                {
                    produkt.Färdig = false;
                }

                // Vi sätter även datumet på produkten om ni har ett sådant fält, 
                // eller kopplar på annan logik.
            }

            try
            {
                nyOrder.Datum = DateTime.Now;
                _orderRepo.Add(nyOrder);
                _orderRepo.Save();

            }
            catch (Exception ex)
            {
                throw new Exception("Något gick fel när ordern skulle skapas. Kontrollera att alla fält är korrekt ifyllda och försök igen.", ex);
            }
        }
    }
}

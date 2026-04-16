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

        public List<Order> GetOrdersWithNavProps() => _orderRepo.GetOrdersAndNavPropertiesList();

        public List<string> GetOrderStartareNamnList()
        {
            return GetOrdersWithNavProps().Select(o => o.StartadAv.Namn).Distinct().ToList();
        }

        public Order GetOrder(int id) => _orderRepo.GetById(id);

        public void AddOrder(Order o) => _orderRepo.Add(o);

        public void UpdateOrder(Order o) => _orderRepo.Update(o);

        public void DeleteOrder(int id) => _orderRepo.Delete(id);

        public void SaveOrder() => _orderRepo.Save();
        public void skapaOrder(Order nyOrder)
        {
            // 1. Grundläggande validering (Fail-fast)
            if (nyOrder.KundID == 0 || nyOrder.StartadAvID == 0 || nyOrder.Produkter == null || !nyOrder.Produkter.Any())
            {
                throw new ArgumentException("Ordern måste ha en kund, en startande användare och minst en produkt.");
            }

            // Skapa en variabel för att räkna ut priset
            decimal totalPris = 0;

            // 2. Loopa igenom produkterna och räkna ihop priset
            foreach (var produkt in nyOrder.Produkter)
            {
                if (produkt is SpecialBeställning)
                {
                    produkt.Färdig = false;
                }

                // Lägg till hattens pris till totalen
                totalPris += produkt.pris;
            }

            // --- HÄR ÄR DET NYA FÖR RABATTEN ---
            // 3. Dra av rabatten från totalpriset
            totalPris -= nyOrder.Rabatt;



            // Säkerhetsspärr: Priset får aldrig bli mindre än 0 kr (om rabatten är högre än priset)
            if (totalPris < 0)
            {
                totalPris = 0;
            }

            if (nyOrder.IsPrio)
            {
                totalPris *= 1.20m;
            }

            // 4. Sätt det slutgiltiga, uträknade priset på ordern
            nyOrder.Pris = totalPris;

            // 5. Spara ordern till databasen
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

        public void MarkeraFärdig(int OrderID)
        {
            var order = _orderRepo.GetById(OrderID);
            if (order != null)
            {
                // Om den är true blir den false, om den är false blir den true
                order.Färdig = !order.Färdig;

                _orderRepo.Update(order);
                _orderRepo.Save();
            }
        }

        public void MarkeraSomPrio(int orderId)
        {

            var order = _orderRepo.GetById(orderId);

            if (order != null)
            {
                // 2. Kontrollera så vi inte lägger på 20% dubbelt
                if (!order.IsPrio)
                {
                    order.IsPrio = true;

                    // 3. Räkna ut det nya priset (+20%)
                    // 'm' står för decimal, vilket är standard för pengar
                    order.Pris = order.Pris * 1.20m;


                    _orderRepo.Update(order);
                    _orderRepo.Save();
                }
            }
        }

    }

}

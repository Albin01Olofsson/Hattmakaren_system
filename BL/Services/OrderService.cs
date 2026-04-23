using BL.Interfaces;
using DAL;
using DAL.Intefaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly DBcontext _context;

        public OrderService(IOrderRepository orderRepo, DBcontext context)
        {
            _orderRepo = orderRepo;
            _context = context;
        }

        public async Task<List<Order>> GetOrderList() => await _orderRepo.GetAll();

        public async Task<List<Order>> GetOrdersWithNavProps()
        {
            return await _orderRepo.GetOrdersAndNavPropertiesList().ToListAsync();
        }

        public async Task<List<string>> GetOrderStartareNamnList()
        {
            return (await GetOrdersWithNavProps())
                .Select(o => o.StartadAv.Namn)
                .Distinct()
                .OrderBy(n => n)
                .ToList();
        }

        public async Task<Order> GetOrder(int id) => await _orderRepo.GetById(id);

        public async Task<Order> GetFullOrder(int id) => await _orderRepo.GetMedDetaljer(id);

        public async Task AddOrder(Order o) => await _orderRepo.Add(o);

        public async Task UpdateOrder(Order o) => await _orderRepo.Update(o);

        public async Task DeleteOrder(int id) => await _orderRepo.Delete(id);

        public async Task SaveOrder() => await _orderRepo.Save();

        //public async Task skapaOrder(Order nyOrder)
        //{
        //    if (nyOrder.KundID == 0 || nyOrder.StartadAvID == 0 || nyOrder.Produkter == null || !nyOrder.Produkter.Any())
        //    {
        //        throw new ArgumentException("Ordern måste ha en kund, en startande användare och minst en produkt.");
        //    }

        //    decimal totalPris = 0;

        //    try
        //    {
        //        // Hämta produkterna igen från samma DbContext som ordern ska sparas i
        //        var produktIds = nyOrder.Produkter.Select(p => p.ProduktID).ToList();

        //        var produkterFrånDb = await _context.Produkter
        //            .Where(p => produktIds.Contains(p.ProduktID))
        //            .ToListAsync();

        //        if (produkterFrånDb.Count != produktIds.Count)
        //        {
        //            throw new Exception("En eller flera produkter kunde inte hittas i databasen.");
        //        }

        //        nyOrder.Produkter = produkterFrånDb;

        //        foreach (var produkt in nyOrder.Produkter)
        //        {
        //            if (produkt is SpecialBeställning)
        //            {
        //                produkt.Färdig = false;
        //            }

        //            totalPris += produkt.Pris;
        //        }

        //        totalPris -= nyOrder.Rabatt;

        //        if (totalPris < 0)
        //        {
        //            totalPris = 0;
        //        }

        //        if (nyOrder.IsPrio)
        //        {
        //            totalPris *= 1.20m;
        //        }

        //        nyOrder.Pris = totalPris;
        //        nyOrder.Datum = DateTime.Now;

        //        await _orderRepo.Add(nyOrder);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Något gick fel när ordern skulle skapas: {ex.Message}", ex);
        //    }
        //}
        public async Task skapaOrder(Order nyOrder, List<int> produktIds)
        {
            if (nyOrder.KundID == 0 || nyOrder.StartadAvID == 0 || produktIds == null || !produktIds.Any())
            {
                throw new ArgumentException("Ordern måste ha en kund, en startande användare och minst en produkt.");
            }

            decimal totalPris = 0;

            try
            {
                // 1. Gruppera ID:n för att räkna antalet av varje unik produkt
                // Om produktIds är [5, 5, 2] blir detta: { ProduktID = 5, Antal = 2 }, { ProduktID = 2, Antal = 1 }
                var grupperadeProdukter = produktIds
                    .GroupBy(id => id)
                    .Select(g => new { ProduktID = g.Key, Antal = g.Count() })
                    .ToList();

                // 2. Plocka ut en lista med BARA de unika ID-numren (t.ex. [5, 2])
                var unikaIds = grupperadeProdukter.Select(g => g.ProduktID).ToList();

                // 3. Fråga databasen efter de unika produkterna
                var produkterFrånDb = await _context.Produkter
                    .Where(p => unikaIds.Contains(p.ProduktID))
                    .ToListAsync();

                // Nu jämför vi antalet unika från DB med antalet unika vi frågade efter
                if (produkterFrånDb.Count != unikaIds.Count)
                {
                    throw new Exception("En eller flera produkter kunde inte hittas i databasen.");
                }

                // 4. Bygg upp OrderRader med rätt antal
                nyOrder.OrderRader = new List<OrderRad>();

                foreach (var grupp in grupperadeProdukter)
                {
                    var produktDb = produkterFrånDb.First(p => p.ProduktID == grupp.ProduktID);

                    nyOrder.OrderRader.Add(new OrderRad
                    {
                        ProduktID = produktDb.ProduktID,
                        Antal = grupp.Antal
                        // VIKTIGT: Vi skickar INTE in hela 'Produkt = produktDb' här, 
                        // vi låter Entity Framework knyta ihop relationen via ProduktID!
                    });

                    // Räkna ut priset: Produktens pris * Antalet vi vill köpa
                    totalPris += (produktDb.Pris * grupp.Antal);
                }

                // 5. Hantera Rabatt och Prio
                totalPris -= nyOrder.Rabatt;

                if (totalPris < 0)
                    totalPris = 0;

                if (nyOrder.IsPrio)
                    totalPris *= 1.20m;

                nyOrder.Pris = totalPris;
                nyOrder.Datum = DateTime.Now;

                // 6. Spara ordern
                await _orderRepo.Add(nyOrder);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Plockar ut InnerException om det finns, för att få den riktiga SQL-felkoden om det smäller
                string felorsak = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                throw new Exception(felorsak, ex);
            }
        }
        public async Task MarkeraFärdig(int OrderID)
        {
            var order = await _orderRepo.GetById(OrderID);
            if (order != null)
            {
                order.Färdig = !order.Färdig;

                await _orderRepo.Update(order);
                await _orderRepo.Save();
            }
        }

        public async Task MarkeraSomPrio(int orderId)
        {
            var order = await _orderRepo.GetById(orderId);

            if (order != null)
            {
                if (!order.IsPrio)
                {
                    order.IsPrio = true;
                    order.Pris = order.Pris * 1.20m;

                    await _orderRepo.Update(order);
                    await _orderRepo.Save();
                }
            }
        }

        public async Task<List<Order>> GetFilteredOrders(string sökString, DateTime? datumFrån, DateTime? datumTill, string orderStartare, string orderStatus, string specialFilter)
        {
            var query = _orderRepo.GetOrdersAndNavPropertiesList();

            if (!string.IsNullOrWhiteSpace(sökString))
            {
                query = query.Where(o =>
                    o.Kund.Namn.StartsWith(sökString) ||
                    o.OrderID.ToString().StartsWith(sökString));
            }

            if (datumFrån.HasValue)
            {
                query = query.Where(o => o.Datum >= datumFrån.Value);
            }

            if (datumTill.HasValue)
            {
                query = query.Where(o => o.Datum <= datumTill.Value);
            }

            if (orderStartare != "Alla")
            {
                query = query.Where(o => o.StartadAv.Namn == orderStartare);
            }

            if (orderStatus != "Alla" && orderStatus != "Ospecificerat" && !string.IsNullOrEmpty(orderStatus))
            {
                if (orderStatus == "Ej Påbörjad")
                {
                    // Fångar upp BÅDE den nya korrekta stavningen (d) och den gamla felstavningen (t) i databasen
                    // Jag lämnade kvar null-kollen också, den är alltid bra att ha som krockkudde!
                    query = query.Where(o => o.Status == "Ej Påbörjad" ||
                                             o.Status == "Ej påbörjat" ||
                                             o.Status == null ||
                                             o.Status == "");
                }
                else
                {
                    query = query.Where(o => o.Status == orderStatus);
                }
            }

            if (specialFilter == "Ja")
            {
                query = query.Where(o => o.IsSpecialbeställning);
            }
            else if (specialFilter == "Nej")
            {
                query = query.Where(o => !o.IsSpecialbeställning);
            }

            return await query.ToListAsync();
        }



        public async Task UppdateraOrderStatus(int orderId, string nyStatus)
        {
            try
            {
                // 1. Hämta den aktuella ordern från databasen via ditt Repository
                var order = await _orderRepo.GetById(orderId);

                if (order != null)
                {

                    order.Status = nyStatus;


                    await _orderRepo.Update(order);
                    await _orderRepo.Save();
                }
                else
                {

                    throw new Exception($"Kunde inte hitta order med ID {orderId}.");
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Ett fel uppstod när statusen skulle sparas: {ex.Message}", ex);
            }
        }
    }

}
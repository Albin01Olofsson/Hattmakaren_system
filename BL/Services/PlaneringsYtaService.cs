using BL.Interfaces;
using DAL.Intefaces;
using DAL.Repositorys;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BL.Services
{
    public class PlaneringsYtaService : IPlaneringsYtaService
    {
        private readonly IPlaneringsRepo _planeringsRepo;
        private readonly IOrderRepository _orderRepo;

        public PlaneringsYtaService(IPlaneringsRepo planeringsRepo, IOrderRepository orederRepo)
        {
            _planeringsRepo = planeringsRepo;
            _orderRepo = orederRepo;
        }
        public async Task Add(Planering planering)
        {
            await _planeringsRepo.Add(planering);
            await _planeringsRepo.Save();
        }
        // 1. Hämta hattar från en vald order så Judith kan välja en
        public async Task<List<Produkt>> HämtaHattarFrånOrder(int orderId)
        {
            var order = await _orderRepo.GetOrdersAndNavPropertiesList()
                                  .FirstOrDefaultAsync(o => o.OrderID == orderId);

            // Om ordern finns, returnera dess produkter, annars en tom lista
            return order?.Produkter ?? new List<Produkt>();
        }
        //public async Task LäggTillPlanering(Planering planering)
        //{
        //    await _planeringsRepo.
        //}
        // 2. Skapa själva bokningen i schemat
        public async Task<Planering> PlaneraArbete(int användarId, int produktId, DateTime startTid)
        {
            var slutTid = startTid.AddHours(2);

            var allaPlaneringar = await _planeringsRepo.GetAll();
            var finnsKrock = allaPlaneringar.Any(p => p.ProduktID == produktId &&
                          p.StartTid < slutTid &&
                          p.SlutTid > startTid);

            if (finnsKrock)
            {
                throw new Exception("Produkten är redan bokad denna tid!");
            }
            var nyBokning = new Planering
            {
                AnvändarID = användarId,
                ProduktID = produktId,
                StartTid = startTid,
                SlutTid = startTid.AddHours(2), // Vi sätter ett standardpass på 2 timmar
                PlaneringsNamn = "Planering",
                Status = "Ej påbörjat"
            };

            await _planeringsRepo.Add(nyBokning);
            await _planeringsRepo.Save();
            return nyBokning;
        }

        // 3. Hämta planeringar för att visa i schemat
        public async Task<Planering> HämtaPlaneringMedDetaljer(int planeringsID)
        {
            return await _planeringsRepo.HämtaPlaneringMedDetaljer(planeringsID);
        }

        // 4. Hämta alla planeringar för att visa i schemat
        public async Task<List<Planering>> HämtaAllaPlaneringar(DateTime veckaStart, DateTime veckaSlut)
        {
            return await _planeringsRepo.HämtaAllaPlaneringarMedDetaljer()
                .Where(p => p.StartTid >= veckaStart && p.StartTid < veckaSlut).ToListAsync();
        }

        // 5. Hämta planeringar för en specifik användare
        public async Task<List<Planering>> HämtaMinPlanering(int id)
        {

            var query = _planeringsRepo.HämtaAllaPlaneringarMedDetaljer();

            return await query.Where(p => p.AnvändarID == id).ToListAsync();
        }

        public async Task<List<Planering>> HämtaPlaneringar(bool alla, int userId)
        {
            var query = _planeringsRepo.HämtaAllaPlaneringarMedDetaljer();

            if (!alla)
            {
                // Vi lägger till WHERE-klausulen i receptet
                query = query.Where(p => p.AnvändarID == userId);
            }

            // NU först skickar vi frågan till SQL-servern
            return await query.ToListAsync();
        }

        public async Task TaBortPlanering(int planeringId)
        {
            var planering = await _planeringsRepo.GetById(planeringId);
            if (planering != null)
            {
                await _planeringsRepo.Delete(planering.PlaneringsID);
                await _planeringsRepo.Save();
            }
        }

        public async Task<List<Produkt>> HämtaLedigaProdukter(int orderId)
        {
            var order = await _orderRepo.GetOrdersAndNavPropertiesList()
                .FirstOrDefaultAsync(o => o.OrderID == orderId);

            if (order == null)
                return new List<Produkt>();

            var Allaplaneringar = await _planeringsRepo.GetAll();
            var upptagnaProdukterIds = Allaplaneringar
                .Select(p => p.ProduktID)
                .ToList();
            return order.Produkter
                .Where(p => !upptagnaProdukterIds.Contains(p.ProduktID))
                .ToList();
        }

        //public async Task<List<SchemaBlock>> HämtaSchema(DateTime start, DateTime slut, int userId, bool alla)
        //{
        //    var planeringar = await _planeringsRepo.HämtaAllaPlaneringarMedDetaljer()
        //        .Where(p => p.StartTid < slut && p.SlutTid > start)
        //        .ToListAsync();

        //    var aktiviteter = await _aktivitetsRepo.GetAll();

        //    if (!alla)
        //    {
        //        planeringar = planeringar.Where(p => p.AnvändarID == userId).ToList();

        //        aktiviteter = aktiviteter
        //            .Where(a => a.SkapadAvID == userId ||
        //                        a.Deltagare.Any(d => d.AnvändarID == userId))
        //            .ToList();
        //    }

        //    var result = new List<SchemaBlock>();

        //    // PLANERINGAR
        //    result.AddRange(planeringar.Select(p => new SchemaBlock
        //    {
        //        Id = p.PlaneringsID,
        //        Typ = "Planering",
        //        Titel = p.Produkt.Namn,
        //        StartTid = p.StartTid,
        //        SlutTid = p.SlutTid,
        //        OrderId = p.Produkt?.OrderID,
        //        ProduktId = p.ProduktID,
        //        Färg = GetFärg(p)
        //    }));

        //    // AKTIVITETER
        //    result.AddRange(aktiviteter.Select(a => new SchemaBlock
        //    {
        //        Id = a.AktivitetID,
        //        Typ = "Aktivitet",
        //        Titel = a.Namn,
        //        StartTid = a.StartTid,
        //        SlutTid = a.SlutTid,
        //        Färg = "#3A86FF"
        //    }));

        //    return result;
        //}
    }
}

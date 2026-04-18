using BL.Interfaces;
using DAL.Intefaces;
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
        public void Add(Planering planering)
        {
            _planeringsRepo.Add(planering);
            _planeringsRepo.Save();
        }
        // 1. Hämta hattar från en vald order så Judith kan välja en
        public List<Produkt> HämtaHattarFrånOrder(int orderId)
        {
            var order = _orderRepo.GetOrdersAndNavPropertiesList()
                                  .FirstOrDefault(o => o.OrderID == orderId);

            // Om ordern finns, returnera dess produkter, annars en tom lista
            return order?.Produkter ?? new List<Produkt>();
        }

        // 2. Skapa själva bokningen i schemat
        public Planering PlaneraArbete(int användarId, int produktId, DateTime startTid)
        {
            var slutTid = startTid.AddHours(2);

            var finnsKrock = _planeringsRepo.GetAll()
                .Any(p => p.ProduktID == produktId &&
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
                SlutTid = startTid.AddHours(2) // Vi sätter ett standardpass på 2 timmar
            };

            _planeringsRepo.Add(nyBokning);
            _planeringsRepo.Save();
            return nyBokning;
        }

        // 3. Hämta planeringar för att visa i schemat
        public Planering HämtaPlaneringMedDetaljer(int planeringsID)
        {
            return _planeringsRepo.HämtaPlaneringMedDetaljer(planeringsID);
        }

        // 4. Hämta alla planeringar för att visa i schemat
        public List<Planering> HämtaAllaPlaneringar()
        {
            return _planeringsRepo.HämtaAllaPlaneringarMedDetaljer();
        }

        // 5. Hämta planeringar för en specifik användare
        public List<Planering> HämtaMinPlanering(int id)
        {
            return _planeringsRepo.HämtaAllaPlaneringarMedDetaljer()
                                 .Where(p => p.AnvändarID == id)
                                 .ToList();
        }

        public void TaBortPlanering(int planeringId)
        {
            var planering = _planeringsRepo.GetById(planeringId);
            if (planering != null)
            {
                _planeringsRepo.Delete(planering.PlaneringsID);
                _planeringsRepo.Save();
            }
        }
    }
}

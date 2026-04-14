using BL.Interfaces;
using DAL.Intefaces;
using Models;

namespace BL.Services
{
    public class KundService : IKundService
    {
        private readonly IKundRepo _kundRepo;


        public KundService(IKundRepo kundRepo)
        {
            _kundRepo = kundRepo;
        }
        public List<Kund> HämtaAllaKunder()
        {

            return _kundRepo.GetAll();
        }

        public Kund GetByEmail(string email)
        {
            // Validera att email inte är null eller tomt
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            // Skicka frågan vidare till Repot (Databasen)
            return _kundRepo.GetByEmail(email);
        }


    }
}

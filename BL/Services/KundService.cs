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

        public Kund GetKundById(int id)
        {
            return _kundRepo.GetById(id);
        }

        public void AddKund(Kund kund)
        {
            // Validera att kund inte är null
            if (kund == null)
            {
                throw new ArgumentNullException(nameof(kund));
            }
            // Validera att kundens namn inte är null eller tomt
            if (string.IsNullOrWhiteSpace(kund.Namn))
            {
                throw new ArgumentException("Kundens namn kan inte vara tomt.", nameof(kund));
            }
            // Skicka vidare till Repot (Databasen) för att lägga till kunden
            _kundRepo.Add(kund);
        }

        public void UpdateKund(Kund kund)
        {
            // Validera att kund inte är null
            if (kund == null)
            {
                throw new ArgumentNullException(nameof(kund));
            }
            // Validera att kundens namn inte är null eller tomt
            if (string.IsNullOrWhiteSpace(kund.Namn))
            {
                throw new ArgumentException("Kundens namn kan inte vara tomt.", nameof(kund));
            }
            // Skicka vidare till Repot (Databasen) för att uppdatera kunden
            _kundRepo.Update(kund);
        }

        public void DeleteKund(int id)
        {
            // Validera att id är större än 0
            if (id <= 0)
            {
                throw new ArgumentException("Id måste vara större än 0.", nameof(id));
            }
            // Skicka vidare till Repot (Databasen) för att ta bort kunden
            _kundRepo.Delete(id);
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

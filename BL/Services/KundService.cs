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
        public async Task<List<Kund>> HämtaAllaKunder()
        {
            var kunder = await _kundRepo.GetAll();
            kunder = kunder.Where(k => k.Namn != "Borttagen kund").ToList();
            return kunder;
            //return await _kundRepo.GetAll();
        }

        public async Task<Kund> GetKundById(int id)
        {
            return await _kundRepo.GetById(id);
        }

        public async Task AddKund(Kund kund)
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
            await _kundRepo.Add(kund);
            await _kundRepo.Save();
        }

        public async Task UpdateKund(Kund kund)
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
            await _kundRepo.Update(kund);
            await _kundRepo.Save();
        }

        public async Task DeleteKund(int id)
        {
            // Validera att id är större än 0
            if (id <= 0)
            {
                throw new ArgumentException("Id måste vara större än 0.", nameof(id));
            }
            // Skicka vidare till Repot (Databasen) för att ta bort kunden
            await _kundRepo.Delete(id);
            await _kundRepo.Save();
        }

        public async Task<Kund> GetByEmail(string email)
        {
            // Validera att email inte är null eller tomt
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            // Skicka frågan vidare till Repot (Databasen)
            return await _kundRepo.GetByEmail(email);
        }

        public async Task AnonymiseraKund(int kundID)

        {

            var kund = await _kundRepo.GetById(kundID);



            if (kund != null)

            {

                kund.Namn = "Borttagen kund";

                kund.Email = "N/A";

                kund.Telefon = "N/A";

                kund.Adress = "N/A";

            }



            await _kundRepo.Update(kund);
            await _kundRepo.Save();

        }


    }

}

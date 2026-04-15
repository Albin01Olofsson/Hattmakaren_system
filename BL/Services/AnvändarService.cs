using BL.Interfaces;
using DAL.Intefaces;
using Models;

namespace BL.Services
{
    public class AnvändarService : IAnvändarService
    {
        private readonly IAnvändarRepo _användarRepo;

        public AnvändarService(IAnvändarRepo användarRepo)
        {
            _användarRepo = användarRepo;
        }

        // Metod för att hämta den som är inloggad eller en specifik användare
        public Användare HämtaAnvändareMedId(int id)
        {
            return _användarRepo.GetById(id);
        }

        public void LäggTillAnvändare (Användare användare)
        {
            if (användare == null)
            {
                throw new ArgumentNullException(nameof(användare));
            }
            // Validera att användarens namn inte är null eller tomt
            if (string.IsNullOrWhiteSpace(användare.Namn))
            {
                throw new ArgumentException("Användarens namn kan inte vara tomt.", nameof(användare));
            }
            användare.IsActive = true; // Sätt användaren som aktiv när den läggs till
            _användarRepo.Add(användare);
            _användarRepo.Save();
        }

        public void UpdateraAnvändare(Användare användare)
        {
            _användarRepo.Update(användare);
            _användarRepo.Save();
        }

        public void TaBortAnvändare(int id)
        {
            _användarRepo.Delete(id);
            _användarRepo.Save();
        }

    }
}

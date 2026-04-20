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
        public async Task<List<Användare>> HämtaAllaAnvändare()
        {
            var users = await _användarRepo.GetAll();
            return users.Where(a => a.IsActive).ToList();
        }

        // Metod för att hämta den som är inloggad eller en specifik användare
        public async Task<Användare> HämtaAnvändareMedId(int id)
        {
            return await _användarRepo.GetById(id);
        }

        public async Task LäggTillAnvändare(Användare användare)
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
            await _användarRepo.Add(användare);
            await _användarRepo.Save();
        }

        public async Task UpdateraAnvändare(Användare användare)
        {
            await _användarRepo.Update(användare);
            await _användarRepo.Save();
        }

        public async Task InaktiveraAnvändare(int id)
        {
            var användare = await _användarRepo.GetById(id);

            if (användare == null)
            {
                throw new Exception("Användare finns inte!");
            }
            användare.IsActive = false;
            await _användarRepo.Update(användare);
            await _användarRepo.Save();

        }

        public async Task TaBortAnvändare(int id)
        {
            await _användarRepo.Delete(id);
            await _användarRepo.Save();
        }

    }
}
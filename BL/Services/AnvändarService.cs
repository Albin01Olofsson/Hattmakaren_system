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
    }
}

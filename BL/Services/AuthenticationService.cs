using BL.Interfaces;
using DAL.Intefaces;
using Models;

namespace BL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAnvändarRepo _annvändarRepo;
        // håller koll på vem som är inloggad
        public Användare InloggadAnvändare { get; private set; }


        public AuthenticationService(IAnvändarRepo användarRepo)
        {
            _annvändarRepo = användarRepo;
        }
        public bool Login(string email, string lösenord)
        {
            var användare = _annvändarRepo.GetByEmail(email);
            if (användare == null)
                return false;

            // Kontrollera om lösenordet stämmer
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(lösenord, användare.Lösenord);

            if (isPasswordCorrect)
            {
                // Om lösenordet är rätt, kom ihåg användaren!
                InloggadAnvändare = användare;
                return true;
            }

            return false;
        }

    }
}

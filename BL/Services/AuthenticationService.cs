using BL.Interfaces;
using DAL.Intefaces;
using Models;



namespace BL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAnvändarRepo _annvändarRepo;
        public AuthenticationService(IAnvändarRepo användarRepo)
        {
            _annvändarRepo = användarRepo;
        }
        public async Task<Användare> Login(string email, string lösenord)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(lösenord))
            {
                return null;
            }
            var användare = await _annvändarRepo.GetByEmail(email);
            if (användare == null || !användare.IsActive)
            {
                return null;
            }


            var success = BCrypt.Net.BCrypt.Verify(lösenord, användare.Lösenord);
            if (!success)
            {
                return null;
            }
            return användare;
        }

    }
}

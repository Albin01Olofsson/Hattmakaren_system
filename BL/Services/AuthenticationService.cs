using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using DAL.Intefaces;
using BL.Interfaces;



namespace BL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAnvändarRepo _annvändarRepo;
        public AuthenticationService(IAnvändarRepo användarRepo)
        {
            _annvändarRepo = användarRepo;
        }
        public bool Login(string email, string lösenord)
        {
            if(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(lösenord))
            {
                return false;
            }
            var användare = _annvändarRepo.GetByEmail(email);
            if (användare == null)
            {
                return false;
            }
                

            return BCrypt.Net.BCrypt.Verify(lösenord, användare.Lösenord);
        }

    }
}

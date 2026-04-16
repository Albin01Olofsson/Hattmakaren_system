using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using DAL.Intefaces;
using BL.Interfaces;
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
        public Användare Login(string email, string lösenord)
        {
            if(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(lösenord))
            {
                return null;
            }
            var användare = _annvändarRepo.GetByEmail(email);
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

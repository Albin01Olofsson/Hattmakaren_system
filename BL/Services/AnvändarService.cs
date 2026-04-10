using BL.Interfaces;
using DAL.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class AnvändarService : IAnvändarService
    {
        private readonly IAnvändarRepo _användarRepo;

        public AnvändarService(IAnvändarRepo användarRepo)
        {
            _användarRepo = användarRepo;
        }
    }
}

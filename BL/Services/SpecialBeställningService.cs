using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using DAL.Intefaces;

namespace BL.Services
{
    public class SpecialBeställningService : ISpecialBeställningService
    {
        private readonly ISpecialBeställningsRepo _specialBeställningRepo;
        public SpecialBeställningService(ISpecialBeställningsRepo specialBeställningRepo)
        {
            _specialBeställningRepo = specialBeställningRepo;
        }
    }
}

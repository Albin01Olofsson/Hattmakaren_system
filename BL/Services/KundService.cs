using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using DAL.Intefaces;

namespace BL.Services
{
    public class KundService : IKundService
    {
        private readonly IKundRepo _kundRepo;
        public KundService(IKundRepo kundRepo)
        {
            _kundRepo = kundRepo;
        }
    }
}

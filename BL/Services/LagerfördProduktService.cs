using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using DAL.Intefaces;

namespace BL.Services
{
    public class LagerfördProduktService : ILagerfördProduktService
    {
        private readonly ILagerfördProduktRepository _lagerfördProduktRepo;

        public LagerfördProduktService(ILagerfördProduktRepository lagerfördProduktRepo)
        {
            _lagerfördProduktRepo = lagerfördProduktRepo;
        }
            
        
    }
}

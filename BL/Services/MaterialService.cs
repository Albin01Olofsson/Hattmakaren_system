using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using DAL.Intefaces;

namespace BL.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepo _materialRepo;
        public MaterialService(IMaterialRepo materialRepo)
        {
            _materialRepo = materialRepo;
        }
    }
}

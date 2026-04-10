using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using DAL.Intefaces;

namespace BL.Services
{
    public class MaterialBeställningService : IMaterialBeställningService
    {
        private readonly IMaterialBeställningRepository _materialBeställningRepo;
        public MaterialBeställningService(IMaterialBeställningRepository materialBeställningRepo)
        {
            _materialBeställningRepo = materialBeställningRepo;
        }
    }
}

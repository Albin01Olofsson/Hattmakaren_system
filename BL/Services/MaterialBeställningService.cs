using BL.Interfaces;
using DAL;
using DAL.Intefaces;
using DAL.Repositorys;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace BL.Services
{
    public class MaterialBeställningService : IMaterialBeställningService
    {
        private readonly DBcontext _context;

        public MaterialBeställningService(DBcontext context)
        {
            _context = context;
        }

        public void SkapaBestallning(MaterialBeställning bestallning)
        {
            foreach (var rad in bestallning.Rader)
            {
                // 🔥 SÄTT FK
                rad.MaterialId = rad.Material.MaterialID;

                // 🔥 SÄG TILL EF: detta material finns redan!
                _context.Entry(rad.Material).State = EntityState.Unchanged;
            }

            _context.MaterialBeställningar.Add(bestallning);
            _context.SaveChanges();
        }
    }
}
    


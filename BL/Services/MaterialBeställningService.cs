using BL.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BL.Services
{
    public class MaterialBeställningService : IMaterialBeställningService
    {
        private readonly DBcontext _context;

        public MaterialBeställningService(DBcontext context)
        {
            _context = context;
        }

        public async Task SkapaBestallning(MaterialBeställning bestallning)
        {
            foreach (var rad in bestallning.Rader)
            {
                // 🔥 SÄTT FK
                rad.MaterialId = rad.Material.MaterialID;

                // 🔥 SÄG TILL EF: detta material finns redan!
                _context.Entry(rad.Material).State = EntityState.Unchanged;
            }

            _context.MaterialBeställningar.Add(bestallning);
            await _context.SaveChangesAsync();
        }
    }
}



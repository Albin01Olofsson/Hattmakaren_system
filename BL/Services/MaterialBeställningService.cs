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
            if (bestallning == null || bestallning.Rader == null || !bestallning.Rader.Any())
                throw new Exception("Beställningen saknar materialrader.");

            foreach (var rad in bestallning.Rader)
            {
                if (rad.Material == null)
                    throw new Exception("Material saknas på en beställningsrad.");

                var dbMaterial = await _context.Material
                    .FirstOrDefaultAsync(m => m.MaterialID == rad.Material.MaterialID);

                if (dbMaterial == null)
                    throw new Exception($"Material med ID {rad.Material.MaterialID} hittades inte.");

                // Sätt foreign key
                rad.MaterialId = dbMaterial.MaterialID;

                // Koppla raden till materialet som redan finns i databasen
                rad.Material = dbMaterial;

                // Öka lagersaldo med antal som beställts
                dbMaterial.Lagerantal += rad.Antal;
            }

            _context.MaterialBeställningar.Add(bestallning);
            await _context.SaveChangesAsync();
        }
    }
}



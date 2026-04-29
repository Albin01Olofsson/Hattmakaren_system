using BL.Interfaces;
using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ProduktMaterialService : IProduktMaterialService
    {
        private readonly DBcontext _context;
        public ProduktMaterialService(DBcontext context)
        {
            _context = context;
        }
        public async Task SkapaProduktOchDraLager(Produkt produkt, List<ProduktMaterial> material)
        {
            foreach (var pm in material)
            {
                var m = await _context.Material.FindAsync(pm.MaterialID);

                if (m.Lagerantal < pm.Mängd)
                    throw new Exception($"Inte tillräckligt av {m.Namn}");

                m.Lagerantal -= (int)pm.Mängd;
            }

            produkt.ProduktMaterial = material;

            _context.Produkter.Add(produkt);
            await _context.SaveChangesAsync();
        }
    }
}

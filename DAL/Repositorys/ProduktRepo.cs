using DAL.Intefaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositorys
{
    public class ProduktRepo : DBRepository<Produkt>, IProduktRepo
    {
        public ProduktRepo(DBcontext context) : base(context)
        {
        }

        public async Task AddSpecBes(SpecialBeställning sb, List<int> materialIdn)
        {
            foreach(int materialId in materialIdn)
            {
                var materialIdDb = await _context.Material.FirstOrDefaultAsync(m => m.MaterialID == materialId);
                sb.MaterialLista.Add(materialIdDb);
            }

            await _context.SpecialBeställningar.AddAsync(sb);
            await Save();
        }
        
        public async Task AddProd(Produkt sb, List<int> materialIdn)
        {
            foreach(int materialId in materialIdn)
            {
                var materialIdDb = await _context.Material.FirstOrDefaultAsync(m => m.MaterialID == materialId);
                sb.MaterialLista.Add(materialIdDb);
            }

            await _context.Produkter.AddAsync(sb);
            await Save();
        }


        public async Task<List<Produkt>> GetAllaProdukter()
        {
            return await _context.Produkter
                .Include(p => p.MaterialLista)
                .Include(p => p.TillverkadAv)
                .ToListAsync();
        }
        public async Task AddRange(List<Produkt> produkter)
        {
            _context.Produkter.AddRange(produkter);
            await _context.SaveChangesAsync();
        }

        public async Task<Produkt> HämtaFörstaLedigaProdukt(int artikelId)
        {
            return await _context.Produkter
                .Where(p => p.ArtikelID == artikelId && !p.Färdig)
                .OrderBy(p => p.ProduktID)
                .FirstOrDefaultAsync();
        }
    }
}

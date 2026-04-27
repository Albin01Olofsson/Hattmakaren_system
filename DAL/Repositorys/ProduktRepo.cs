using DAL.Intefaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL.Repositorys
{
    public class ProduktRepo : DBRepository<Produkt>, IProduktRepo
    {
        public ProduktRepo(DBcontext context) : base(context)
        {
        }
        public async Task AddSpecBes(SpecialBeställning sb, List<ProduktMaterial> materialLista)
        {
            sb.ProduktMaterial = materialLista;

            await _context.SpecialBeställningar.AddAsync(sb);
            await Save();
        }

        public async Task AddProd(Produkt p, List<ProduktMaterial> materialLista)
        {
            p.ProduktMaterial = materialLista;

            await _context.Produkter.AddAsync(p);
            await Save();
        }

        public async Task<List<Produkt>> GetAllaProdukter()
        {
            return await _context.Produkter
                .Include(p => p.ProduktMaterial)
                    .ThenInclude(pm => pm.Material)
                .Include(p => p.TillverkadAv)
                .Include(p => p.OrderRader)
                    .ThenInclude(or => or.Order)
                .Include(p => p.OrderRader)
                    .ThenInclude(or => or.Planeringar)
                .ToListAsync();
        }
    }
}

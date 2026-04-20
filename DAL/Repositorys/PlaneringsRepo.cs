using DAL.Intefaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL.Repositorys
{
    public class PlaneringsRepo : DBRepository<Planering>, IPlaneringsRepo
    {
        public PlaneringsRepo(DBcontext context): base(context)
        {
             
        }

        public async Task<Planering> HämtaPlaneringMedDetaljer(int id)
        {
            return await _dbSet
                .Include(p => p.Användare)
                .Include(p => p.Produkt)
                .FirstOrDefaultAsync(p => p.PlaneringsID == id);
        }

        public async Task<List<Planering>> HämtaAllaPlaneringarMedDetaljer()
        {
            return await _dbSet
                .Include(p => p.Användare)
                .Include(p => p.Produkt)
                .ToListAsync();
        }

    }
}

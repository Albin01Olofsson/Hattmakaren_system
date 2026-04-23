using DAL.Intefaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL.Repositorys
{
    public class PlaneringsRepo : DBRepository<Planering>, IPlaneringsRepo
    {
        public PlaneringsRepo(DBcontext context) : base(context)
        {

        }

        public async Task<Planering> HämtaPlaneringMedDetaljer(int id)
        {
            return await _dbSet
                .Include(p => p.Användare)
                .Include(p => p.OrderRad)
                    .ThenInclude(or => or.Produkt)
                .Include(p => p.OrderRad)
                    .ThenInclude(or => or.Order)
                .FirstOrDefaultAsync(p => p.PlaneringsID == id);
        }

        public IQueryable<Planering> HämtaAllaPlaneringarMedDetaljer()
        {
            return _dbSet
                .Include(p => p.Användare)
                .Include(p => p.OrderRad)
                    .ThenInclude(or => or.Produkt)
                .Include(p => p.OrderRad)
                    .ThenInclude(or => or.Order)
                .AsNoTracking();
        }

    }
}

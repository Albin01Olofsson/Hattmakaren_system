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

        public Planering HämtaPlaneringMedDetaljer(int id)
        {
            return _dbSet
                .Include(p => p.Användare)
                .Include(p => p.Produkt)
                .FirstOrDefault(p => p.PlaneringsID == id);
        }

        public List<Planering> HämtaAllaPlaneringarMedDetaljer()
        {
            return _dbSet
                .Include(p => p.Användare)
                .Include(p => p.Produkt)
                .ToList();
        }

    }
}

using DAL.Intefaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL.Repositorys
{
    public class ReklamationRepository : DBRepository<Reklamation>, IReklamationRepository
    {
        public ReklamationRepository(DBcontext context) : base(context)
        {
        }

        public IQueryable<Reklamation> GetReklamationerMedDetaljer()
        {
            return _context.Reklamationer
                .Include(r => r.Order)
                .Include(r => r.Kund)
                .Include(r => r.Produkt)
                .Include(r => r.SkapadAv);
        }
    }
}

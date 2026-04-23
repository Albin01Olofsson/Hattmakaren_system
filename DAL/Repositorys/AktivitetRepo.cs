using DAL.Intefaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL.Repositorys
{
    public class AktivitetRepo: DBRepository<Aktivitet>, IAktivitetsRepo
    {
        public AktivitetRepo(DBcontext context) : base(context)
        {
            
        }
        public async Task<List<Aktivitet>> GetAllWithUsers()
        {
            return await _dbSet
                .Include(a => a.SkapadAv)
                .Include(a => a.Deltagare)
                .ToListAsync();
        }
        public async Task<List<Användare>> GetUsersByIds(List<int> ids)
        {
            return await _context.Användare
                .Where(u => ids.Contains(u.AnvändarID))
                .ToListAsync();
        }
    }
}

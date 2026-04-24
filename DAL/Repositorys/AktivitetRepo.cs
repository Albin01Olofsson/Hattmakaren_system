using DAL.Intefaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL.Repositorys
{
    public class AktivitetRepo : DBRepository<Aktivitet>, IAktivitetsRepo
    {
        public AktivitetRepo(DBcontext context) : base(context)
        {

        }
        public async Task<List<Aktivitet>> GetAllWithUsers()
        {
            return await _dbSet
                .AsNoTracking()
                .Include(a => a.SkapadAv)
                .Include(a => a.Deltagare)
                .ToListAsync();
        }
    }
}

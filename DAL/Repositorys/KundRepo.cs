using DAL.Intefaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL.Repositorys
{
    public class KundRepo : DBRepository<Kund>, IKundRepo
    {

        public KundRepo(DBcontext context) : base(context)
        {

        }
        public async Task<Kund> GetByEmail(string email)
        {
            return await _context.Kunder.FirstOrDefaultAsync(k => k.Email.ToLower() == email.ToLower());
        }
    }
}

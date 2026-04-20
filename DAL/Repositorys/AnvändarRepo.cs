using DAL.Intefaces;
using Microsoft.EntityFrameworkCore;
using Models;


namespace DAL.Repositorys
{
    public class AnvändarRepo : DBRepository<Användare>, IAnvändarRepo
    {
        public AnvändarRepo(DBcontext context) : base(context)
        {

        }

        public async Task<Användare> GetByEmail(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

    }
}
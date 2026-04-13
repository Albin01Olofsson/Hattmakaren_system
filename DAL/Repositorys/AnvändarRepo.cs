using DAL.Intefaces;
using Models;


namespace DAL.Repositorys
{
    public class AnvändarRepo : DBRepository<Användare>, IAnvändarRepo
    {
        public AnvändarRepo(DBcontext context) : base(context)
        {

        }

        public Användare GetByEmail(string email)
        {
            email = email.ToLower();
            return _dbSet.FirstOrDefault(u => u.Email.ToLower() == email);
        }

    }
}
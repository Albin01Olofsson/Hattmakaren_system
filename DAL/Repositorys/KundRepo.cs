using DAL.Intefaces;
using Models;

namespace DAL.Repositorys
{
    public class KundRepo : DBRepository<Kund>, IKundRepo
    {

        public KundRepo(DBcontext context) : base(context)
        {

        }
        public Kund GetByEmail(string email)
        {
            return _context.Kunder.FirstOrDefault(k => k.Email.ToLower() == email.ToLower());
        }
    }
}

using DAL.Intefaces;
using Models;

namespace DAL.Repositorys
{
    public class KundRepo : DBRepository<Kund>, IKundRepo
    {

        public KundRepo(DBcontext context) : base(context)
        {

        }
    
    }
}

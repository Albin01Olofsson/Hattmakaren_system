using DAL.Intefaces;
using Models;


namespace DAL.Repositorys
{
    public class SpecialBeställningsRepo : DBRepository<SpecialBeställning>, ISpecialBeställningsRepo
    {


        public SpecialBeställningsRepo(DBcontext context) : base(context)
        {

        }




    }
}

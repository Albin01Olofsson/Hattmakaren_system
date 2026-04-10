using DAL.Intefaces;
using Models;


namespace DAL.Repositorys
{
    public class AnvändarRepo : DBRepository<Användare>, IAnvändarRepo
    {
        public AnvändarRepo(DBcontext context) : base(context)
        {

        }
    }
}
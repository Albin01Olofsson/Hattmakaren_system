using Models;

namespace DAL.Intefaces
{
    public interface IKundRepo : IRepository<Kund>
    {
        public Kund GetByEmail(string email);
    }
}

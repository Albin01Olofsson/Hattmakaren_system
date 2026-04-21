using Models;

namespace DAL.Intefaces
{
    public interface IKundRepo : IRepository<Kund>
    {
        public Task<Kund> GetByEmail(string email);
    }
}

using Models;
namespace DAL.Intefaces
{
    public interface IAnvändarRepo : IRepository<Användare>
    {
        Task<Användare> GetByEmail(string email);
    }
}

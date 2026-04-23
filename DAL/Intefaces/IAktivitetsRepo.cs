using Models;

namespace DAL.Intefaces
{
    public interface IAktivitetsRepo : IRepository<Aktivitet>
    {
        Task<List<Aktivitet>> GetAllWithUsers();
        Task<List<Användare>> GetUsersByIds(List<int> ids);
    }
}

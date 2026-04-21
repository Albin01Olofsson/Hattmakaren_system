using Models;

namespace BL.Interfaces
{
    public interface IKundService
    {
        Task<List<Kund>> HämtaAllaKunder();

        Task<Kund> GetKundById(int id);

        Task AddKund(Kund kund);

        Task UpdateKund(Kund kund);

        Task DeleteKund(int id);

        Task<Kund> GetByEmail(string email);

        Task AnonymiseraKund(int kundID);
    }

}

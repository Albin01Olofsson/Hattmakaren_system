using Models;
using System.Collections.ObjectModel;

namespace BL.Interfaces
{
    public interface IKundService
    {
        ObservableCollection<Kund> Kunder { get; }
        Task<List<Kund>> HämtaAllaKunder();

        Task<Kund> GetKundById(int id);

        Task AddKund(Kund kund);

        Task UpdateKund(Kund kund);

        Task DeleteKund(int id);

        Task<Kund> GetByEmail(string email);

        Task AnonymiseraKund(int kundID);
    }

}

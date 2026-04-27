using Models;

namespace BL.Interfaces
{
    public interface IReklamationService
    {
        Task<List<Reklamation>> GetReklamationer();
        Task<List<Reklamation>> GetReklamationerMedDetaljer();
        Task<Reklamation> GetReklamation(int id);
        Task AddReklamation(Reklamation reklamation);
        Task UpdateReklamation(Reklamation reklamation);
        Task DeleteReklamation(int id);
    }
}

using Models;

namespace DAL.Intefaces
{
    public interface IReklamationRepository : IRepository<Reklamation>
    {
        IQueryable<Reklamation> GetReklamationerMedDetaljer();
    }
}

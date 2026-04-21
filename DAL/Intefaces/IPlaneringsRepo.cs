using Models;

namespace DAL.Intefaces
{
    public interface IPlaneringsRepo : IRepository<Planering>
    {
        Task<Planering> HämtaPlaneringMedDetaljer(int id);

        IQueryable<Planering> HämtaAllaPlaneringarMedDetaljer();

    }
}

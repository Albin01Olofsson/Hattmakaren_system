using Models;

namespace DAL.Intefaces
{
    public interface IPlaneringsRepo: IRepository<Planering>
    {
        Task<Planering> HämtaPlaneringMedDetaljer(int id);
        Task<List<Planering>> HämtaAllaPlaneringarMedDetaljer();
    }
}

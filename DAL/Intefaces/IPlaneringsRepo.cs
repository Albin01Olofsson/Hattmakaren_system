using Models;

namespace DAL.Intefaces
{
    public interface IPlaneringsRepo: IRepository<Planering>
    {
        Planering HämtaPlaneringMedDetaljer(int id);
        List<Planering> HämtaAllaPlaneringarMedDetaljer();
    }
}

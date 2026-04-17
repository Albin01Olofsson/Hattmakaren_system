using Models;

namespace BL.Interfaces
{
    public interface IPlaneringsYtaService
    {
        Planering HämtaPlaneringMedDetaljer(int planeringsID);
        List<Planering> HämtaAllaPlaneringar();
    }
}

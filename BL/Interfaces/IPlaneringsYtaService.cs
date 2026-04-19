using Models;

namespace BL.Interfaces
{
    public interface IPlaneringsYtaService
    {
        List<Produkt> HämtaHattarFrånOrder(int orderId);
        Planering PlaneraArbete(int användarId, int produktId, DateTime startTid);
        Planering HämtaPlaneringMedDetaljer(int planeringsID);
        List<Planering> HämtaAllaPlaneringar();
        List<Planering> HämtaMinPlanering(int id);
        List<Planering> HämtaPlaneringar(bool alla, int userId);
        void TaBortPlanering(int planeringId);
        List<Produkt> HämtaLedigaProdukter(int orderId);
    }
}

using Models;

namespace BL.Interfaces
{
    public interface IPlaneringsYtaService
    {
        Task<List<Produkt>> HämtaHattarFrånOrder(int orderId);
        Task<Planering> PlaneraArbete(int användarId, int produktId, DateTime startTid);
        Task<Planering> HämtaPlaneringMedDetaljer(int planeringsID);
        Task<List<Planering>> HämtaAllaPlaneringar(DateTime veckaStart, DateTime veckaSlut);
        Task<List<Planering>> HämtaMinPlanering(int id);
        Task<List<Planering>> HämtaPlaneringar(bool alla, int userId);
        Task TaBortPlanering(int planeringId);
        Task<List<Produkt>> HämtaLedigaProdukter(int orderId);
    }
}

using Models;

namespace BL.Interfaces
{
    public interface IAnvändarService
    {
        Task<List<Användare>> HämtaAllaAnvändare();
        Task<Användare> HämtaAnvändareMedId(int id);
        Task LäggTillAnvändare(Användare användare);
        Task UpdateraAnvändare(Användare användare);
        Task InaktiveraAnvändare(int id);
        Task TaBortAnvändare(int id);
    }
}

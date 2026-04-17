using Models;

namespace BL.Interfaces
{
    public interface IAnvändarService
    {
        public List<Användare> HämtaAllaAnvändare();
        public Användare HämtaAnvändareMedId(int id);
        void LäggTillAnvändare(Användare användare);
        void UpdateraAnvändare(Användare användare);
        void InaktiveraAnvändare(int id);
        void TaBortAnvändare(int id);
    }
}

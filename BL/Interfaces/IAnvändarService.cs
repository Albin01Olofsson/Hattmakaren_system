using Models;

namespace BL.Interfaces
{
    public interface IAnvändarService
    {
        public Användare HämtaAnvändareMedId(int id);
        void LäggTillAnvändare(Användare användare);
        void UpdateraAnvändare(Användare användare);
        void TaBortAnvändare(int id);
    }
}

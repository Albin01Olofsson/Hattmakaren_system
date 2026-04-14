using Models;

namespace BL.Interfaces
{
    public interface IKundService
    {
        public List<Kund> HämtaAllaKunder();

        Kund GetKundById(int id);

        void AddKund(Kund kund);

        void UpdateKund(Kund kund);

        void DeleteKund(int id);

        public Kund GetByEmail(string email);
    }
}

using Models;

namespace BL.Interfaces
{
    public interface IKundService
    {
        public List<Kund> HämtaAllaKunder();

        public Kund GetByEmail(string email);
    }
}

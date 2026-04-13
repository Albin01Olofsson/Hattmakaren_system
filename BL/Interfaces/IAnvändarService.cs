using Models;

namespace BL.Interfaces
{
    public interface IAnvändarService
    {
        public Användare HämtaAnvändareMedId(int id);
    }
}

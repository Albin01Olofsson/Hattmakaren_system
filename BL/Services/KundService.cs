using BL.Interfaces;
using DAL.Intefaces;
using Models;

namespace BL.Services
{
    public class KundService : IKundService
    {
        private readonly IKundRepo _kundRepo;
        public List<Kund> HämtaAllaKunder()
        {

            return _kundRepo.GetAll();
        }


    }
}

using BL.Interfaces;
using DAL.Intefaces;
using Models;

namespace BL.Services
{
    public class PlaneringsYtaService : IPlaneringsYtaService
    {
        private IPlaneringsRepo _planeringsRepo;

        public PlaneringsYtaService(IPlaneringsRepo planeringsRepo)
        {
            _planeringsRepo = planeringsRepo;
        }

        public Planering HämtaPlaneringMedDetaljer(int planeringsID)
        {
            return _planeringsRepo.HämtaPlaneringMedDetaljer(planeringsID);
        }

        public List<Planering> HämtaAllaPlaneringar()
        {
            return _planeringsRepo.HämtaAllaPlaneringarMedDetaljer();
        }


    }
}

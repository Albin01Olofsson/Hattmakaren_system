using BL.Interfaces;
using DAL.Intefaces;
using Models;

namespace BL.Services
{
    public class AktivitetService : IAktivitetService
    {
        private readonly IAktivitetsRepo _aktivitetsRepo;
        private readonly IOrderRepository _orderRepo;
        public AktivitetService(IAktivitetsRepo aktivitetsRepo, IOrderRepository orderRepo)
        {
            _aktivitetsRepo = aktivitetsRepo;
            _orderRepo = orderRepo;
        }

        public async Task LäggTillAktivitet(Aktivitet aktivitet)
        {
            await _aktivitetsRepo.Add(aktivitet);
            await _aktivitetsRepo.Save();
        }

        public async Task UpdateraAktivitet(Aktivitet aktivitet)
        {
            await _aktivitetsRepo.Update(aktivitet);
            await _aktivitetsRepo.Save();
        }

        public async Task<Aktivitet> HämtaAktivitetById(int id)
        {
            return await _aktivitetsRepo.GetById(id);
        }

        public async Task<List<Aktivitet>> HämtaAllaAktiviteter()
        {
            return await _aktivitetsRepo.GetAllWithUsers();
        }

        public async Task TaBortAktivitet(int id)
        {
            await _aktivitetsRepo.Delete(id);
        }
    }
}

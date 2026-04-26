using BL.Interfaces;
using DAL.Intefaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BL.Services
{
    public class ReklamationService : IReklamationService
    {
        private readonly IReklamationRepository _reklamationRepo;

        public ReklamationService(IReklamationRepository reklamationRepo)
        {
            _reklamationRepo = reklamationRepo;
        }

        public async Task<List<Reklamation>> GetReklamationer() => await _reklamationRepo.GetAll();

        public async Task<List<Reklamation>> GetReklamationerMedDetaljer()
        {
            return await _reklamationRepo.GetReklamationerMedDetaljer()
                .OrderByDescending(r => r.SkapadDatum)
                .ToListAsync();
        }

        public async Task<Reklamation> GetReklamation(int id) => await _reklamationRepo.GetById(id);

        public async Task AddReklamation(Reklamation reklamation)
        {
            await _reklamationRepo.Add(reklamation);
            await _reklamationRepo.Save();
        }

        public async Task UpdateReklamation(Reklamation reklamation) => await _reklamationRepo.Update(reklamation);

        public async Task DeleteReklamation(int id) => await _reklamationRepo.Delete(id);
    }
}

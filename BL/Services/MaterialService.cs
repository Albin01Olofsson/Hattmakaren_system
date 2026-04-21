using BL.Interfaces;
using DAL.Intefaces;
using Models;

namespace BL.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepo _materialRepo;
        public MaterialService(IMaterialRepo materialRepo)
        {
            _materialRepo = materialRepo;
        }

        public async Task<List<Material>> GetMaterialLista() => await _materialRepo.GetAll();
    }
}

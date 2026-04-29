using BL.Interfaces;
using DAL;
using DAL.Intefaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BL.Services
{
    public class ProduktService : IProduktService
    {
        private readonly IProduktRepo prodRepo;
        private readonly DBcontext _context;

        public ProduktService(IProduktRepo repository, DBcontext context)
        {
            prodRepo = repository;
            _context = context;
        }

        public async Task<List<Produkt>> GetProdukt() => await prodRepo.GetAll();

        public async Task<List<Produkt>> GetProdukter() => await prodRepo.GetAllaProdukter(); //För att få med inkluderade nav properties

        public async Task<Produkt> GetProduktId(int id) => await prodRepo.GetById(id);

        public async Task AddProdukt(Produkt p, List<ProduktMaterial> materialLista)
        {
            await KontrolleraOchDraMaterial(materialLista, p.Lagerantal);
            await prodRepo.AddProd(p, materialLista);
        }

        public async Task AddSpecialBeställning(SpecialBeställning sb, List<ProduktMaterial> materialLista)
        {
            await KontrolleraOchDraMaterial(materialLista, sb.Lagerantal);
            await prodRepo.AddSpecBes(sb, materialLista);
        }

        public async Task UpdateProdukt(Produkt p) => await prodRepo.Update(p);
        public async Task DeleteProdukt(int id) => await prodRepo.Delete(id);
        public async Task SaveProdukt() => await prodRepo.Save();

        public async Task TillverkaProdukt(int produktId, int antalAttTillverka)
        {
            var produkt = await prodRepo.GetById(produktId);

            if (produkt == null)
                throw new Exception("Produkt hittades inte");

            // Hämta produkt med material
            var fullProdukt = (await prodRepo.GetAllaProdukter())
                .First(p => p.ProduktID == produktId);

            foreach (var pm in fullProdukt.ProduktMaterial)
            {
                var material = pm.Material;

                decimal totalÅtgång = pm.Mängd * antalAttTillverka;

                if (material.Lagerantal < totalÅtgång)
                {
                    throw new Exception($"Material {material.Namn} räcker inte!");
                }

                material.Lagerantal -= (int)totalÅtgång;
            }

            // Lägg till i lager
            produkt.Lagerantal += antalAttTillverka;

            await prodRepo.Save();
        }

        private async Task KontrolleraOchDraMaterial(List<ProduktMaterial> materialLista, int antal)
        {
            foreach (var pm in materialLista)
            {
                var material = await _context.Material
    .FirstOrDefaultAsync(m => m.MaterialID == pm.MaterialID);

                if (material == null)
                    throw new Exception("Material saknas.");

                decimal behov = pm.Mängd * antal;

                if (material.Lagerantal < behov)
                {
                    throw new Exception(
                        $"För lite material: {material.Namn}. Behöver {behov}, finns {material.Lagerantal}"
                    );
                }
                material.Lagerantal -= (int)Math.Ceiling(behov);

                _context.Material.Update(material);
            }

            await _context.SaveChangesAsync(); 
        }
    }
}

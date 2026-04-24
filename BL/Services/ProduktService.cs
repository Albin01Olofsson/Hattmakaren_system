using BL.Interfaces;
using DAL.Intefaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ProduktService : IProduktService
    {
        private readonly IProduktRepo prodRepo;
        public ProduktService(IProduktRepo repository)
        {
            prodRepo = repository;
        }

        public async Task<List<Produkt>> GetProdukt() => await prodRepo.GetAll();

        public async Task<List<Produkt>> GetProdukter() => await prodRepo.GetAllaProdukter(); //För att få med inkluderade nav properties

        public async Task<Produkt> GetProduktId(int id) => await prodRepo.GetById(id);

        public async Task AddProdukt(Produkt p, List<int> materialIdn)
        {
            await prodRepo.AddProd(p, materialIdn);
            await prodRepo.Save();
        }
        public async Task<bool> LäggtillProdukt(Produkt p, List<int> materialIdn)
        {
            return await prodRepo.LäggtillProdukt(p, materialIdn);
        }

        public async Task AddSpecialBeställning(SpecialBeställning sb, List<int> materialIdn)
        {
            await prodRepo.AddSpecBes(sb, materialIdn);
            await prodRepo.Save();
        }

        public async Task UpdateProdukt(Produkt p) => await prodRepo.Update(p);
        public async Task DeleteProdukt(int id) => await prodRepo.Delete(id);
        public async Task SaveProdukt() => await prodRepo.Save();


    }
}

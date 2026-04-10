using DAL.Intefaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ProduktService : IProduktService
    {
        private readonly IProduktRepo prodRepo;
        public ProduktService(IProduktRepo repository)
        {
            prodRepo = repository;
        }

        public List<Produkt> GetProdukt() => prodRepo.GetAll();

        public Produkt GetProduktId(int id) => prodRepo.GetById(id);

        public void AddProdukt(Produkt p) => prodRepo.Add(p);

        public void UpdateProdukt(Produkt p) => prodRepo.Update(p);
        public void DeleteProdukt(int id) => prodRepo.Delete(id);
        public void SaveProdukt() => prodRepo.Save();


    }
}

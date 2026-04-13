using BL.Interfaces;
using DAL.Intefaces;
using DAL.Repositorys;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class MaterialBeställningService : IMaterialBeställningService
    {
        private readonly MaterialBeställningRepo _bestallningRepo;

        public MaterialBeställningService(MaterialBeställningRepo bestallningRepo)
        {
            _bestallningRepo = bestallningRepo;
        }

        public void SkapaBestallning(Material material, int antal)
        {
            if (material == null)
                throw new Exception("Material måste väljas.");

            if (antal <= 0)
                throw new Exception("Antal måste vara större än 0.");

            // 🔥 viktigt – EF tracking fix
            _bestallningRepo.AttachMaterial(material);

            var bestallning = new MaterialBeställning
            {
                MaterialLista = new List<Material> { material },
                TotalPris = material.Pris * antal,
                StartadAvID = 1 // ⚠️ kräver att user med ID=1 finns
            };

            _bestallningRepo.Add(bestallning);
            _bestallningRepo.Save();
        }
    }
}
    


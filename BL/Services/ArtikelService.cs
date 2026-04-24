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
    public class ArtikelService : IArtikelService
    {
        private readonly IArtikelRepo artRepo;
        private readonly IProduktRepo _produktRepo;

        public ArtikelService(IArtikelRepo repo, IProduktRepo produktRepo)
        {
            artRepo = repo;
            _produktRepo = produktRepo;
        }
        public async Task SkapaArtikelMedProdukter(Artikel artikel, int antalProdukter)
        {
            if (artikel == null)
                throw new ArgumentException("Artikel saknas");

            if (antalProdukter <= 0)
                throw new ArgumentException("Antal produkter måste vara minst 1");

            // 1. Spara artikel (viktigt för att få ID)
            await artRepo.Add(artikel);

            // 2. Skapa produkter
            var produkter = new List<Produkt>();

            for (int i = 0; i < antalProdukter; i++)
            {
                produkter.Add(new Produkt
                {
                    ArtikelID = artikel.ArtikelId,
                    Namn = artikel.Namn,
                    Pris = artikel.Pris,
                    Färg = artikel.Färg,
                    Modell = artikel.Modell,
                    Decoration = artikel.Decoration,
                    Färdig = false
                });
            }

            // 3. Spara produkter via repo
            await _produktRepo.AddRange(produkter);
        }
    }
}

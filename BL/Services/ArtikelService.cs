using DAL.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using BL.Interfaces;

namespace BL.Services
{
    public class ArtikelService: IArtikelService
    {
        private readonly IArtikelRepo _repo;
        public ArtikelService(IArtikelRepo repo)
        {
            _repo = repo;
        }
        public async Task<List<Artikel>> HämtaAllaArtiklar()
        {
            return await _repo.GetAll();
        }
        public async Task<Artikel> HämtaArtikelById(int id)
        {
            return await _repo.GetById(id);
        }
        public async Task LäggTillArtikel(Artikel artikel)
        {
            await _repo.Add(artikel);
            await _repo.Save();
        }
        public async Task UppdateraArtikel(Artikel artikel)
        {
            await _repo.Update(artikel);
            await _repo.Save();
        }
        public async Task RaderaArtikel(int id)
        {
            await _repo.Delete(id);
            await _repo.Save();
        }
    }
}

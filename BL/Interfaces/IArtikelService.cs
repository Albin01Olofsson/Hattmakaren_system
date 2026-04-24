using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    internal interface IArtikelService
    {
        Task<List<Artikel>> HämtaAllaArtiklar();
        Task<Artikel> HämtaArtikelById(int id);
        Task LäggTillArtikel(Artikel artikel);
        Task UppdateraArtikel(Artikel artikel);
        Task RaderaArtikel(int id);
    }
}

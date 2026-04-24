using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IArtikelService
    {
        Task SkapaArtikelMedProdukter(Artikel artikel, int antalProdukter);
    }
}

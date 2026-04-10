using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IProduktService
    {
        List<Produkt> GetProdukt();
        Produkt GetProduktId(int id);
        void AddProdukt(Produkt p);
        void UpdateProdukt(Produkt p);
        void DeleteProdukt(int id);
        void SaveProdukt();
    }
}

using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL.Intefaces
{
    public interface IProduktRepo : IRepository<Produkt>
    {
        List<Produkt> GetAllaProdukter(); //Med include

        void AddSpecBes(SpecialBeställning sb, List<int> materialIdn);
    }
}

using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Intefaces
{
    public interface IProduktRepo : IRepository<Produkt>
    {
        Task<List<Produkt>> GetAllaProdukter(); //Med include

        //Task AddSpecBes(SpecialBeställning sb, List<int> materialIdn);
        //Task AddProd(Produkt sb, List<int> materialIdn);
        Task AddSpecBes(SpecialBeställning sb, List<ProduktMaterial> materialLista);
        Task AddProd(Produkt p, List<ProduktMaterial> materialLista);
    }
}

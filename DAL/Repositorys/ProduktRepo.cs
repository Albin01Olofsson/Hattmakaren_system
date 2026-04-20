using DAL.Intefaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositorys
{
    public class ProduktRepo : DBRepository<Produkt>, IProduktRepo
    {
        public ProduktRepo(DBcontext context) : base(context)
        {
        }

        public void AddSpecBes(SpecialBeställning sb, List<int> materialIdn)
        {
            foreach(int materialId in materialIdn)
            {
                var materialIdDb = _context.Material.FirstOrDefault(m => m.MaterialID == materialId);
                sb.MaterialLista.Add(materialIdDb);
            }

            _context.SpecialBeställningar.Add(sb);
            Save();
        }


        public List<Produkt> GetAllaProdukter()
        {
            return _context.Produkter
                .Include(p => p.Order)
                .Include(p => p.MaterialLista)
                .Include(p => p.TillverkadAv)
                .ToList();
        }
    }
}

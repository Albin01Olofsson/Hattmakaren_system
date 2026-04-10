using DAL.Intefaces;
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
        public DBcontext _context;
        public ProduktRepo(DBcontext context) : base(context)
        {
            _context = context;
        }
    }
}

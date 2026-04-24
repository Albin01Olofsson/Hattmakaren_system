using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL.Intefaces;

namespace DAL.Repositorys
{
    public class ArtikelRepo : DBRepository<Artikel>, IArtikelRepo
    {
        public ArtikelRepo(DBcontext context) : base(context)
        {
             
        }
    }
}

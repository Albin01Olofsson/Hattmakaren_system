using DAL.Intefaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositorys
{
    public class MaterialBeställning : DBRepository<Order>, IMaterialBeställningRepository
    {
        public MaterialBeställning(DBcontext context) : base(context)
        {
        }
    }
}

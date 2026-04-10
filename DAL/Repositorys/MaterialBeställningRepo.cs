using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Intefaces;
using Models;

namespace DAL.Repositorys
{
    public class MaterialBeställningRepo : DBRepository<MaterialBeställning>, IMaterialBeställningRepository
    {
        public MaterialBeställningRepo(DBcontext context) : base(context)
        {
        }
    }
}

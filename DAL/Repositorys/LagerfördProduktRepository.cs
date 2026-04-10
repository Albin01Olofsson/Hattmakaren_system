using DAL.Intefaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositorys
{
    public class LagerfördProduktRepository : DBRepository<Order>, ILagerfördProduktRepository
    {
        public LagerfördProduktRepository(DBcontext context) : base(context)
        {
        }
    }
}

using DAL.Intefaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL.Repositorys
{
    public class OrderRepo : DBRepository<Order>, IOrderRepository
    {
        public OrderRepo(DBcontext context) : base(context)
        {


        }

        public Order HämtaMedDetaljer(int id)
        {
            // .Include gör att EF laddar in de relaterade objekten istället för att de är null
            return _dbSet
                .Include(o => o.Kund)
                .Include(o => o.StartadAv)
                .Include(o => o.Produkter)
                .FirstOrDefault(o => o.OrderID == id);
        }

    }
}
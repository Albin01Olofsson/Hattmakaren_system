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

        public async Task<List<Order>> GetOrdersAndNavPropertiesList()
        {
            return await _context.Ordrar
                .Include(o => o.Kund)
                .Include(o => o.StartadAv)
                .Include(o => o.Produkter)
                .ToListAsync();
        }

        public async Task<Order> GetMedDetaljer(int oid)
        {
            return await _context.Ordrar
                .Include(o => o.Kund)
                .Include(o => o.Produkter)
                .Include(o => o.StartadAv)
                .FirstOrDefaultAsync(o => o.OrderID == oid);
        }

        public async Task<Order> HämtaMedDetaljer(int id)
        {
            // .Include gör att EF laddar in de relaterade objekten istället för att de är null
            return await _dbSet
                .Include(o => o.Kund)
                .Include(o => o.StartadAv)
                .Include(o => o.Produkter)
                .FirstOrDefaultAsync(o => o.OrderID == id);
        }

    }
}
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

        public IQueryable<Order> GetOrdersAndNavPropertiesList()
        {
            return _context.Ordrar
                .AsNoTracking()
                .Include(o => o.Kund)
                .Include(o => o.StartadAv)
                .Include(o => o.OrderRader)
                    .ThenInclude(or => or.Produkt)
                    .Include(o => o.Frakt);
        }

        public async Task<Order> GetMedDetaljer(int oid)
        {
            return await _context.Ordrar
                .Include(o => o.Kund)
                .Include(o => o.StartadAv)
                .Include(o => o.OrderRader)
                    .ThenInclude(or => or.Produkt)
                    .Include(o => o.Frakt)
                .FirstOrDefaultAsync(o => o.OrderID == oid);
        }

        public async Task<Order> HämtaMedDetaljer(int id)
        {
            // .Include gör att EF laddar in de relaterade objekten istället för att de är null
            return await _dbSet
                .Include(o => o.Kund)
                .Include(o => o.StartadAv)
                .Include(o => o.OrderRader)
                    .ThenInclude(or => or.Produkt)
                .Include(o => o.OrderRader)
                    .ThenInclude(or => or.Planeringar)
                    .Include(o => o.Frakt)
                .FirstOrDefaultAsync(o => o.OrderID == id);
        }

    }
}
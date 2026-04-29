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
                .Include(o => o.Frakt)
                .Include(o => o.OrderRader)
                    .ThenInclude(or => or.Produkt);
        }

        public async Task<Order> GetMedDetaljer(int oid)
        {
            return await _context.Ordrar
                .Include(o => o.Kund)
                .Include(o => o.StartadAv)
                .Include(o => o.Frakt)
                .Include(o => o.OrderRader)
                    .ThenInclude(or => or.Produkt)
                .FirstOrDefaultAsync(o => o.OrderID == oid);
        }

        public async Task<Order> HämtaMedDetaljer(int id)
        {
            // .Include gör att EF laddar in de relaterade objekten istället för att de är null
            return await _dbSet
                .Include(o => o.Kund)
                .Include(o => o.StartadAv)
                .Include(o => o.Frakt)
                .Include(o => o.OrderRader)
                    .ThenInclude(or => or.Produkt)
                .Include(o => o.OrderRader)
                    .ThenInclude(or => or.Planeringar)
                .FirstOrDefaultAsync(o => o.OrderID == id);
        }

        public async Task<Frakt> GetFraktByOrderID(int orderID)
        {
            return await _context.Frakt.FirstOrDefaultAsync(f => f.OrderID == orderID);
        }

        public async Task<Frakt> GetFraktBySändningsnummer(string sändningsnummer)
        {
            return await _context.Frakt.Include(f => f.Order).ThenInclude(o => o.Kund).FirstOrDefaultAsync(f => f.Sändningsnummer == sändningsnummer);
        }

    }
}
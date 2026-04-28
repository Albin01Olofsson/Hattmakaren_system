using Models;

namespace DAL.Intefaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        public IQueryable<Order> GetOrdersAndNavPropertiesList();

        public Task<Order> HämtaMedDetaljer(int id);

        Task<Order> GetMedDetaljer(int oid);

        Task<Frakt> GetFraktByOrderID(int orderID);
        Task<Frakt> GetFraktBySändningsnummer(string sändningsnummer);
    }
}
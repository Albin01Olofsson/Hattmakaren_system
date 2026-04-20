using Models;

namespace DAL.Intefaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task<List<Order>> GetOrdersAndNavPropertiesList();

        public Task<Order> HämtaMedDetaljer(int id);

        Task<Order> GetMedDetaljer(int oid);
    }
}
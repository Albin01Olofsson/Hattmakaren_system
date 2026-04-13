using Models;

namespace DAL.Intefaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Order HämtaMedDetaljer(int id);
    }
}
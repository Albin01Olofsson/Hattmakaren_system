using Models;

namespace DAL.Intefaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        public List<Order> GetOrdersAndNavPropertiesList();

        public Order HämtaMedDetaljer(int id);

        Order GetMedDetaljer(int oid);
    }
}
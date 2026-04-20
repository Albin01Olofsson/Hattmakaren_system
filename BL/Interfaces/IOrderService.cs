using Models;

namespace BL.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrderList();

        Task<List<Order>> GetOrdersWithNavProps();

        Task<List<string>> GetOrderStartareNamnList();

        Task<Order> GetOrder(int id);

        Task<Order> GetFullOrder(int id);

        Task AddOrder(Order o);

        Task UpdateOrder(Order o);

        Task DeleteOrder(int id);

        Task SaveOrder();

        Task skapaOrder(Order nyOrder);

        Task MarkeraSomPrio(int OrderID);

        Task MarkeraFärdig(int OrderID);
    }
}

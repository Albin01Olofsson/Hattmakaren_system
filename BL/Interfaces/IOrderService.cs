using Models;

namespace BL.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetOrderList();

        List<Order> GetOrdersWithNavProps();

        Order GetOrder(int id);

        void AddOrder(Order o);

        void UpdateOrder(Order o);

        void DeleteOrder(int id);

        void SaveOrder();

        void skapaOrder(Order nyOrder);
    }
}

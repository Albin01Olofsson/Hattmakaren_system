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
        Task skapaOrder(Order nyOrder);//kan tas bort om inte funkar

        //Task skapaOrder(Order nyOrder);
        Task skapaOrder(Order nyOrder, List<int> produktIds);

        Task MarkeraSomPrio(int OrderID);

        Task MarkeraFärdig(int OrderID);
        Task<List<Order>> GetFilteredOrders(string sökString, DateTime? datumFrån, DateTime? datumTill, string orderStartare, string orderStatus, string specialFilter);

        Task UppdateraOrderStatus(int orderId, string nyStatus);
    }
}

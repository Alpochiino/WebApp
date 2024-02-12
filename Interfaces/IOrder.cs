using WebApp.Models;
namespace WebApp.Interfaces
{
    public interface IOrder
    {
        Task <IEnumerable<Order>> GetAllOrdersAsync();
        Task <Order> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);
    }
}

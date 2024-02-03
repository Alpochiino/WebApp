using WebApp.Interfaces;
using WebApp.Models;
using WebApp.Data;

namespace WebApp.Repository
{
    public class OrderRepository : IOrder
    {
        private readonly ApplicationDbContext context;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return context.Orders.OrderBy(x => x.OrderId);
        }

        public Order GetOrderById(int orderId)
        {
            return context.Orders.FirstOrDefault(order => order.OrderId == orderId);
        }

        public void AddOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            context.Orders.Update(order);
            context.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
        }
    }
}

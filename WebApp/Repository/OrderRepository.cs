using WebApp.Interfaces;
using WebApp.Models;
using WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Repository
{
    public class OrderRepository : IOrder
    {
        private readonly ApplicationDbContext context;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task <IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await context.Orders.OrderBy(x => x.OrderId).ToListAsync();
        }

        public async Task <Order> GetOrderByIdAsync(int orderId)
        {
            return await context.Orders.FirstOrDefaultAsync(order => order.OrderId == orderId);
        }

        public async Task AddOrderAsync(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            await context.SaveChangesAsync();
        }
    }
}

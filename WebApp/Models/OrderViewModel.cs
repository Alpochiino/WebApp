using WebApp.Models;

namespace WebApplic.Models
{
    public class OrderViewModel
    {
        public List<Order>? CurrentOrders { get; set; }
        public List<Order>? PreviousOrders { get; set; }
    }
}

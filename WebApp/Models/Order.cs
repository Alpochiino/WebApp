using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Order
    {
        [Display(Name = "Order")]
        public int OrderId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Total Price")]
        [Range(1, 10000)]
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }

        public int UserId { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }

    }
}

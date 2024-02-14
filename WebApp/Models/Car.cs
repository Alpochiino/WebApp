using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Car
    {
        [Display(Name = " ")]
        public int CarId { get; set; }

        [StringLength(60, MinimumLength = 1)]
        [Required]
        public required string Model { get; set; }

        [StringLength(60, MinimumLength = 1)]
        [Required]
        public required string Brand { get; set; }

        [DataType(DataType.Date)]
        public DateTime Year { get; set; }

        [Range(1, 10000)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:0.###}")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Images field is required.")]
        [NotMapped]
        [Display(Name = "Images")]
        public required List<IFormFile> ImageFiles { get; set; }

        public required List<string> ImagePath { get; set; }

        public bool IsAvailable { get; set; } = true;

        public CarStatus Status { get; set; }
    }

    public enum CarStatus
    {
        Available,
        Rented,
    }
}

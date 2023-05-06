using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace BleemsTest.Models
{
    public class Products
    {
        [Key] public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid price.")]
        public decimal ProductPrice { get; set; }
        public string Currency { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime Date { get; set; }= DateTime.Now;
        [NotMapped]
        public IFormFile Photo { get; set; }

    }
}

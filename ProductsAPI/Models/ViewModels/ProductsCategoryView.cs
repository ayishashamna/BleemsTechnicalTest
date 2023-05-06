using System.ComponentModel.DataAnnotations;

namespace ProductsAPI.Models.ViewModels
{
    public class ProductsCategoryView
    {
        [Key]public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public string Currency { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
    }
}

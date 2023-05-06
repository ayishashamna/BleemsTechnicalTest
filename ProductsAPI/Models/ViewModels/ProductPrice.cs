using Microsoft.EntityFrameworkCore;

namespace ProductsAPI.Models.ViewModels
{
    //[Keyless]
    public class ProductPrice
    {
        
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Price { get; set; }
    }
}

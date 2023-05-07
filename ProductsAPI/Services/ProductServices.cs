using ProductsAPI.Models;
using ProductsAPI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ProductsAPI.Services
{
    public class ProductServices
    {
        private readonly ApplicationDbContext _context;
        public ProductServices(ApplicationDbContext context) 
        {
            _context = context;
        }
        
        public async Task<List<ProductsInfo>> GetProductsInfo()
        {
            List<ProductsInfo> productsInfo = await (from product in _context.ProductsCategoryView
                                        select new ProductsInfo
                                        {
                                            ProductName = product.ProductName,
                                            ProductDescription = product.ProductDescription,
                                            CategoryName = product.CategoryName
                                        }).OrderBy(x => Guid.NewGuid()).ToListAsync();
            return productsInfo;
        }
        public async Task<ProductsInfo> GetProductById(int productId)
        {
            ProductsInfo productInfo = await (from product in _context.ProductsCategoryView
                                              where product.ProductId == productId
                                              select new ProductsInfo
                                              {
                                                  ProductName = product.ProductName,
                                                  ProductDescription = product.ProductDescription,
                                                  CategoryName = product.CategoryName
                                              }).FirstOrDefaultAsync();

            if (productInfo == null)
            {
                throw new Exception($"Product with ID {productId} not found.");
            }

            return productInfo;
        }
        public async Task<ProductPrice> GetProductDetailsByIdWithCurrency(int productId)
        {
            var results = await _context.ProductPrice.FromSqlRaw($"GetItemDetails {productId}").ToListAsync();
            return results.FirstOrDefault();
            
        }
    }
}

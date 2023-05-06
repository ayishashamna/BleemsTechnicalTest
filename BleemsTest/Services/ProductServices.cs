using BleemsTest.Models;
using BleemsTest.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BleemsTest.Services
{
    public class ProductServices
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;
        public ProductServices(ApplicationDbContext context , IWebHostEnvironment environment) 
        {
            _context = context;
            _environment = environment;
        }
        public async Task<ResponseModel> AddProduct(Products product, IFormFile photo) 
        {
            ResponseModel model = new ResponseModel();
            try
            {
                if(product.Category!=null)
                {
                    var category = await _context.Categories.FindAsync(product.Category.CategoryId);
                    product.Category = category;
                }
                if (product.Photo != null && product.Photo.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.Photo.FileName);
                    var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.Photo.CopyToAsync(stream);
                    }

                    product.PhotoUrl = $"/uploads/{fileName}";
                }

                _context.Products.Add(product);
                _context.SaveChanges();
                model.ResponseId = product.ProductId;
                model.ResponseCode = product.ProductName;
                model.Message = "Product " + product.ProductName + " Added";
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.ResponseId = product.ProductId;
                model.ResponseCode = product.ProductName;
                model.Message = ex.Message;
                model.IsSuccess = false;
            }
            return model;
        }
        public List<ProductsCategoryView> GetProduts()
        {
            List<ProductsCategoryView> productList = _context.ProductsCategoryView.ToList();
            return productList;
        }
        public async Task<Products> GetProductById(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }
        public async Task<ResponseModel> Update(Products product, IFormFile photo)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Products productToUpdate = await GetProductById(product.ProductId);
                if (product.Category != null)
                {
                    var category = await _context.Categories.FindAsync(product.Category.CategoryId);
                    productToUpdate.Category = category;
                }
                if (product.Photo != null && product.Photo.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.Photo.FileName);
                    var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.Photo.CopyToAsync(stream);
                    }

                    productToUpdate.PhotoUrl = $"/uploads/{fileName}";
                }
                productToUpdate.ProductName= product.ProductName;
                productToUpdate.ProductDescription= product.ProductDescription;
                productToUpdate.ProductPrice = product.ProductPrice;
                productToUpdate.Currency = product.Currency;
                productToUpdate.Date = product.Date;
                await _context.SaveChangesAsync();
                model.Message = "Product Updated!!";

            }
            catch (Exception ex)
            {
                model.Message = ex.Message;
            }
            return model;
        }
        public void Delete(Products product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();

        }
        public async Task<List<ProductsInfo>> GetProductsInfo()
        {
            List<ProductsInfo> productsInfo = await (from product in _context.ProductsCategoryView
                                        select new ProductsInfo
                                        {
                                            ProductName = product.ProductName,
                                            ProductDescription = product.ProductDescription,
                                            CategoryName = product.CategoryName
                                        }).ToListAsync();
            return productsInfo;
        }
    }
}

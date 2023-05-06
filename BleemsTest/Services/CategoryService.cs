using BleemsTest.Models;
using BleemsTest.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BleemsTest.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        { 
            _context = context;
        }

        public async Task<ResponseModel> AddCategory(Category category)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                model.ResponseId = category.CategoryId;
                model.ResponseCode = category.Name;
                model.Message = "Category " + category.Name + " Added";
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.ResponseId = category.CategoryId;
                model.ResponseCode = category.Name;
                model.Message = ex.Message;
                model.IsSuccess = false;
            }
            return model;
        }
        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<Category> GetCategory(int id)
        {
            return await _context.Categories.FindAsync(id);
        }
        public async Task<Category> Update(Category category)
        {
            Category item =await GetCategory(category.CategoryId);
            if (item != null)
            {
                item.Name = category.Name;
                item.Description = category.Description;
                await _context.SaveChangesAsync();
            }
            return category;
        }
    }
}

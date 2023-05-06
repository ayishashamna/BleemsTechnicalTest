using BleemsTest.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BleemsTest.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductsCategoryView> ProductsCategoryView { get; set;}
    }
}

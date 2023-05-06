using ProductsAPI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ProductsAPI.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductsCategoryView> ProductsCategoryView { get; set;}
        public DbSet<ProductPrice> ProductPrice { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductPrice>(eb =>
            {
                eb.HasNoKey();
                eb.Property(v => v.ProductName).HasColumnName("ProductName");
                eb.Property(v => v.ProductDescription).HasColumnName("ProductDescription");
                eb.Property(v => v.Price).HasColumnName("Price");

            });

        }
    }
}

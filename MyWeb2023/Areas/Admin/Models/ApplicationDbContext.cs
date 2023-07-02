using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;
using System.Data;

namespace MyWeb2023.Areas.Admin.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Catalog> Catalogs {get; set; }
    }
}

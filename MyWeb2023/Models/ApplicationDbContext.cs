using Microsoft.EntityFrameworkCore;
using Myweb.Domain.Models.Entities;

namespace MyWeb2023.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Cake> Cakes { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

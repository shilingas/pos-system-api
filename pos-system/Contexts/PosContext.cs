using Microsoft.EntityFrameworkCore;
using pos_system.Models;
using pos_system.Order;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace pos_system.Contexts
{
    public class PosContext : DbContext
    {
        public PosContext(DbContextOptions<PosContext> options) : base(options)
        {
        }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<ServiceModel> Services { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderProductModel> OrderProducts { get; set; }
    }
}

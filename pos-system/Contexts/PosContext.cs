using Microsoft.EntityFrameworkCore;
using pos_system.Coupons;
using pos_system.Customers;
using pos_system.Order;
using pos_system.Products;
using pos_system.Reservation;
using pos_system.Services;
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
        public DbSet<OrderServiceModel> OrderServices{ get; set;}
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<ReservationModel> Reservations { get; set; }
        public DbSet<CouponModel> Coupons { get; set; }
    }
}

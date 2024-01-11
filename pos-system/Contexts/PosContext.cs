using Microsoft.EntityFrameworkCore;
using pos_system.Coupons;
using pos_system.Admins;
using pos_system.Customers;
using pos_system.Order;
using pos_system.Products;
using pos_system.Reservation;
using pos_system.Services;
using System.Data.Entity.ModelConfiguration.Conventions;
using pos_system.Discounts;
using pos_system.Payments;

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
        public DbSet<AdminModel> Admins { get; set; }
        public DbSet<DiscountModel> Discounts { get; set; }
        public DbSet<DiscountProductModel> DiscountProducts { get; set; }
        public DbSet<DiscountServiceModel> DiscountServices { get; set; }
        public DbSet<PaymentModel> Payments { get; set; }
    }
}

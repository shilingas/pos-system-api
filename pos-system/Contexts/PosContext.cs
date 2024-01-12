//PosContext

using Microsoft.EntityFrameworkCore;
using pos_system.Customers;
using pos_system.Order;
using System.Data.Entity.ModelConfiguration.Conventions;
using pos_system.Workers;
using pos_system.Workers.Roles;
using pos_system.Customers.Coupons;
using pos_system.Order.Payments;
using pos_system.Management.Admins;
using pos_system.ProductService.Products;
using pos_system.ProductService.Services;
using pos_system.ProductService.Discounts;
using pos_system.ProductService.Reservation;

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
        public DbSet<OrderServiceModel> OrderServices { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<ReservationModel> Reservations { get; set; }
        public DbSet<CouponModel> Coupons { get; set; }
        public DbSet<AdminModel> Admins { get; set; }
        public DbSet<DiscountModel> Discounts { get; set; }
        public DbSet<DiscountProductModel> DiscountProducts { get; set; }
        public DbSet<DiscountServiceModel> DiscountServices { get; set; }
        public DbSet<WorkerModel> Workers { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<WorkerRole> WorkerRoles { get; set; }
        public DbSet<PaymentModel> Payments { get; set; }
    }
}

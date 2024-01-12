using Microsoft.EntityFrameworkCore;
using pos_system.Contexts;
using pos_system.Customers;
using pos_system.Order;
using pos_system.Workers;
using System;
using pos_system.Order.Tips;
using pos_system.Workers.Roles;
using pos_system.Customers.Coupons;
using pos_system.Order.Payments;
using pos_system.Order.Bill;
using pos_system.Management.Admins;
using pos_system.ProductService.Products;
using pos_system.ProductService.Services;
using pos_system.ProductService.Discounts;
using pos_system.ProductService.Reservation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PosContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IWorkerService, WorkerService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IBillService, BillService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ITipService, TipService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IAdminService, AdminService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

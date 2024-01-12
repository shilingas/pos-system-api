﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pos_system.Contexts;

#nullable disable

namespace pos_system.Migrations
{
    [DbContext(typeof(PosContext))]
    [Migration("20240112112017_AdditionalReservations")]
    partial class AdditionalReservations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("pos_system.Customers.Coupons.CouponModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ValidUntilDateTime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Coupons");
                });

            modelBuilder.Entity("pos_system.Customers.CustomerModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("pos_system.Management.Admins.AdminModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("pos_system.Order.OrderModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<decimal?>("Tip")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("pos_system.Order.OrderProductModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("pos_system.Order.OrderServiceModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float?>("Duration")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("ServiceId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrderServices");
                });

            modelBuilder.Entity("pos_system.Order.Payments.PaymentModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("pos_system.ProductService.Discounts.DiscountModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double?>("Percentage")
                        .HasColumnType("float");

                    b.Property<DateTime?>("ValidUntilDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("pos_system.ProductService.Discounts.DiscountProductModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DiscountId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiscountModelId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DiscountModelId");

                    b.ToTable("DiscountProducts");
                });

            modelBuilder.Entity("pos_system.ProductService.Discounts.DiscountServiceModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DiscountId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DiscountServices");
                });

            modelBuilder.Entity("pos_system.ProductService.Products.ProductModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("pos_system.ProductService.Reservation.ReservationModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan?>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("WorkerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkerModelId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("WorkerModelId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("pos_system.ProductService.Services.ServiceModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float?>("Duration")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("pos_system.WorkerRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("WorkerId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("WorkerId");

                    b.ToTable("WorkerRoles");
                });

            modelBuilder.Entity("pos_system.Workers.Roles.RoleModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("pos_system.Workers.WorkerModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("pos_system.ProductService.Discounts.DiscountProductModel", b =>
                {
                    b.HasOne("pos_system.ProductService.Discounts.DiscountModel", null)
                        .WithMany("Products")
                        .HasForeignKey("DiscountModelId");
                });

            modelBuilder.Entity("pos_system.ProductService.Reservation.ReservationModel", b =>
                {
                    b.HasOne("pos_system.ProductService.Services.ServiceModel", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId");

                    b.HasOne("pos_system.Workers.WorkerModel", null)
                        .WithMany("Reservations")
                        .HasForeignKey("WorkerModelId");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("pos_system.WorkerRole", b =>
                {
                    b.HasOne("pos_system.Workers.Roles.RoleModel", "Role")
                        .WithMany("WorkerRoles")
                        .HasForeignKey("RoleId");

                    b.HasOne("pos_system.Workers.WorkerModel", "Worker")
                        .WithMany("WorkerRoles")
                        .HasForeignKey("WorkerId");

                    b.Navigation("Role");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("pos_system.ProductService.Discounts.DiscountModel", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("pos_system.Workers.Roles.RoleModel", b =>
                {
                    b.Navigation("WorkerRoles");
                });

            modelBuilder.Entity("pos_system.Workers.WorkerModel", b =>
                {
                    b.Navigation("Reservations");

                    b.Navigation("WorkerRoles");
                });
#pragma warning restore 612, 618
        }
    }
}

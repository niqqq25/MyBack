﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyBack.Infrastructure.Persistence;

#nullable disable

namespace MyBack.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(MyBackDbContext))]
    partial class MyBackDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyBack.Domain.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("MyBack.Domain.Orders.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ItemPrice")
                        .HasColumnType("decimal(38,0)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("OrderItems", (string)null);
                });

            modelBuilder.Entity("MyBack.Domain.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(38,0)");

                    b.Property<int?>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("MyBack.Domain.Orders.Order", b =>
                {
                    b.OwnsOne("MyBack.Domain.Orders.Order.ShippingAddress#MyBack.Domain.Orders.ValueObjects.Address", "ShippingAddress", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(45)
                                .HasColumnType("nvarchar(45)")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(56)
                                .HasColumnType("nvarchar(56)")
                                .HasColumnName("Country");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(70)
                                .HasColumnType("nvarchar(70)")
                                .HasColumnName("Address");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasMaxLength(11)
                                .HasColumnType("nvarchar(11)")
                                .HasColumnName("ZipCode");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("ShippingAddress")
                        .IsRequired();
                });

            modelBuilder.Entity("MyBack.Domain.Orders.OrderItem", b =>
                {
                    b.HasOne("MyBack.Domain.Orders.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MyBack.Domain.Products.Product", "Product")
                        .WithOne()
                        .HasForeignKey("MyBack.Domain.Orders.OrderItem", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MyBack.Domain.Orders.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}

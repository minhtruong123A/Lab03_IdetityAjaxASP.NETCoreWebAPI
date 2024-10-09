﻿// <auto-generated />
using System;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessObjects.Migrations
{
    [DbContext(typeof(ProductStoreContext))]
    partial class ProductStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessObjects.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryName = "Electronics"
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryName = "Books"
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryName = "Clothing"
                        },
                        new
                        {
                            CategoryId = 4,
                            CategoryName = "Food"
                        },
                        new
                        {
                            CategoryId = 5,
                            CategoryName = "Toys"
                        },
                        new
                        {
                            CategoryId = 6,
                            CategoryName = "Furniture"
                        },
                        new
                        {
                            CategoryId = 7,
                            CategoryName = "Sports"
                        },
                        new
                        {
                            CategoryId = 8,
                            CategoryName = "Music"
                        },
                        new
                        {
                            CategoryId = 9,
                            CategoryName = "Health"
                        },
                        new
                        {
                            CategoryId = 10,
                            CategoryName = "Movies"
                        });
                });

            modelBuilder.Entity("BusinessObjects.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("UnitsInStock")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            ProductName = "Laptop",
                            UnitPrice = 1000m,
                            UnitsInStock = 50
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1,
                            ProductName = "Smartphone",
                            UnitPrice = 800m,
                            UnitsInStock = 100
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 3,
                            ProductName = "T-Shirt",
                            UnitPrice = 20m,
                            UnitsInStock = 200
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 4,
                            ProductName = "Pizza",
                            UnitPrice = 15m,
                            UnitsInStock = 30
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 2,
                            ProductName = "Novel",
                            UnitPrice = 10m,
                            UnitsInStock = 60
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 8,
                            ProductName = "Guitar",
                            UnitPrice = 300m,
                            UnitsInStock = 40
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 7,
                            ProductName = "Dumbbells",
                            UnitPrice = 50m,
                            UnitsInStock = 25
                        },
                        new
                        {
                            ProductId = 8,
                            CategoryId = 9,
                            ProductName = "Shampoo",
                            UnitPrice = 5m,
                            UnitsInStock = 150
                        },
                        new
                        {
                            ProductId = 9,
                            CategoryId = 10,
                            ProductName = "Movie DVD",
                            UnitPrice = 12m,
                            UnitsInStock = 80
                        },
                        new
                        {
                            ProductId = 10,
                            CategoryId = 6,
                            ProductName = "Sofa",
                            UnitPrice = 500m,
                            UnitsInStock = 20
                        });
                });

            modelBuilder.Entity("BusinessObjects.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            RoleName = "User"
                        });
                });

            modelBuilder.Entity("BusinessObjects.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "023482dbf1828b4210ff8d03af8a3dca16d769c4d17a38cb7ec0222905cd44f5",
                            RoleId = 1,
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Password = "023482dbf1828b4210ff8d03af8a3dca16d769c4d17a38cb7ec0222905cd44f5",
                            RoleId = 2,
                            Username = "user1"
                        },
                        new
                        {
                            Id = 3,
                            Password = "023482dbf1828b4210ff8d03af8a3dca16d769c4d17a38cb7ec0222905cd44f5",
                            RoleId = 2,
                            Username = "user2"
                        },
                        new
                        {
                            Id = 4,
                            Password = "023482dbf1828b4210ff8d03af8a3dca16d769c4d17a38cb7ec0222905cd44f5",
                            RoleId = 2,
                            Username = "user3"
                        },
                        new
                        {
                            Id = 5,
                            Password = "023482dbf1828b4210ff8d03af8a3dca16d769c4d17a38cb7ec0222905cd44f5",
                            RoleId = 2,
                            Username = "user4"
                        },
                        new
                        {
                            Id = 6,
                            Password = "023482dbf1828b4210ff8d03af8a3dca16d769c4d17a38cb7ec0222905cd44f5",
                            RoleId = 2,
                            Username = "user5"
                        },
                        new
                        {
                            Id = 7,
                            Password = "023482dbf1828b4210ff8d03af8a3dca16d769c4d17a38cb7ec0222905cd44f5",
                            RoleId = 2,
                            Username = "user6"
                        },
                        new
                        {
                            Id = 8,
                            Password = "023482dbf1828b4210ff8d03af8a3dca16d769c4d17a38cb7ec0222905cd44f5",
                            RoleId = 2,
                            Username = "user7"
                        },
                        new
                        {
                            Id = 9,
                            Password = "023482dbf1828b4210ff8d03af8a3dca16d769c4d17a38cb7ec0222905cd44f5",
                            RoleId = 2,
                            Username = "user8"
                        },
                        new
                        {
                            Id = 10,
                            Password = "023482dbf1828b4210ff8d03af8a3dca16d769c4d17a38cb7ec0222905cd44f5",
                            RoleId = 2,
                            Username = "user9"
                        });
                });

            modelBuilder.Entity("BusinessObjects.Product", b =>
                {
                    b.HasOne("BusinessObjects.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BusinessObjects.User", b =>
                {
                    b.HasOne("BusinessObjects.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BusinessObjects.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}

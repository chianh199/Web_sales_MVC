using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVC2.Data;

public partial class FoodContext : DbContext
{
    public FoodContext()
    {
    }

    public FoodContext(DbContextOptions<FoodContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Catalog> Catalogs { get; set; }

    public virtual DbSet<ChitietBill> ChitietBills { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Catalog>(entity =>
        {
            entity.ToTable("CATALOG");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<ChitietBill>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CHITIET_BILL");

            entity.Property(e => e.Bill).HasColumnName("BILL");
            entity.Property(e => e.Product).HasColumnName("PRODUCT");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
            entity.Property(e => e.Total).HasColumnName("TOTAL");

            entity.HasOne(d => d.BillNavigation).WithMany()
                .HasForeignKey(d => d.Bill)
                .HasConstraintName("FK_CHITIET_BILL_ORDER");

            entity.HasOne(d => d.ProductNavigation).WithMany()
                .HasForeignKey(d => d.Product)
                .HasConstraintName("FK_CHITIET_BILL_PRODUCT");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.ToTable("KHACHHANG");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.Phone).HasColumnName("PHONE");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .HasColumnName("POSITION");
            entity.Property(e => e.Pwd)
                .HasMaxLength(20)
                .HasColumnName("PWD");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.ToTable("NHANVIEN");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.Phone).HasColumnName("PHONE");
            entity.Property(e => e.Pwd)
                .HasMaxLength(20)
                .HasColumnName("PWD");
            entity.Property(e => e.Role).HasColumnName("ROLE");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("ORDER");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DateCreate)
                .HasColumnType("datetime")
                .HasColumnName("DATE_CREATE");
            entity.Property(e => e.Khachhang).HasColumnName("KHACHHANG");
            entity.Property(e => e.NvCreate).HasColumnName("NV_CREATE");
            entity.Property(e => e.NvShip).HasColumnName("NV_SHIP");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasColumnName("STATUS");
            entity.Property(e => e.Total).HasColumnName("TOTAL");

            entity.HasOne(d => d.KhachhangNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Khachhang)
                .HasConstraintName("FK_ORDER_KHACHHANG");

            entity.HasOne(d => d.NvCreateNavigation).WithMany(p => p.OrderNvCreateNavigations)
                .HasForeignKey(d => d.NvCreate)
                .HasConstraintName("FK_ORDER_NHANVIEN");

            entity.HasOne(d => d.NvShipNavigation).WithMany(p => p.OrderNvShipNavigations)
                .HasForeignKey(d => d.NvShip)
                .HasConstraintName("FK_ORDER_NHANVIEN1");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("PRODUCT");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Catalog).HasColumnName("CATALOG");
            entity.Property(e => e.Describe).HasColumnName("DESCRIBE");
            entity.Property(e => e.Image)
                .HasMaxLength(30)
                .HasColumnName("IMAGE");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("NAME");
            entity.Property(e => e.Price).HasColumnName("PRICE");
            entity.Property(e => e.Status).HasColumnName("STATUS");

            entity.HasOne(d => d.CatalogNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Catalog)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT_CATALOG");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

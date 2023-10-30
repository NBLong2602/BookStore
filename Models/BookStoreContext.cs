using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Models;

public partial class BookStoreContext : DbContext
{
    public BookStoreContext()
    {
    }

    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookCategory> BookCategories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerType> CustomerTypes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderInfo> OrderInfos { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:dbBookStore");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Account__536C85E5A43D63E6");

            entity.ToTable("Account");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKAccount437921");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Author__3214EC07B2E7527C");

            entity.ToTable("Author");

            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(15);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Isbn).HasName("PK__Book__9271CED1C2DC2814");

            entity.ToTable("Book");

            entity.Property(e => e.BookName).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Language).HasMaxLength(100);
            entity.Property(e => e.PictureThumb).HasMaxLength(100);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBook666529");

            entity.HasOne(d => d.BookCategory).WithMany(p => p.Books)
                .HasForeignKey(d => d.BookCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBook308999");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBook221707");
        });

        modelBuilder.Entity<BookCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookCate__3214EC07378BCDB8");

            entity.ToTable("BookCategory");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC075AE32E90");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.Email, "UQ__Customer__A9D105343946FD8C").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(15);

            entity.HasOne(d => d.CustomerType).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CustomerTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKCustomer670752");
        });

        modelBuilder.Entity<CustomerType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC0783388A94");

            entity.ToTable("CustomerType");

            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07141FC525");

            entity.ToTable("Employee");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.BookIsbn, e.OrderId }).HasName("PK__OrderDet__EB20C2CD85C61E59");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.TransactionDate)
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.HasOne(d => d.BookIsbnNavigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.BookIsbn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKOrderDetai572182");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKOrderDetai762072");
        });

        modelBuilder.Entity<OrderInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderInf__3214EC07FCF33084");

            entity.ToTable("OrderInfo");

            entity.Property(e => e.OrderDate)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.OrderInfos)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKOrder556775");

            entity.HasOne(d => d.Employee).WithMany(p => p.OrderInfos)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FKOrder900062");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Publishe__3214EC07D757B1D8");

            entity.ToTable("Publisher");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using Microsoft.EntityFrameworkCore;
using OtelOtomasyon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(r => r.RoomNumber)
                .IsRequired()
                .HasMaxLength(10);

                entity.Property(r => r.Status)
                   .HasConversion<string>();
            });

            //RoomType entity configuration
            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);

                entity.Property(r => r.PricePerNight)
                .HasColumnType("decimal(18,2)");
            });

            //Guest entity configuration
            modelBuilder.Entity<Guest>(entity =>
            {
                entity.Property(g => g.Email)
                .IsRequired()
                .HasMaxLength(100);

                entity.HasIndex(g => g.Email)
                .IsUnique();
            });

            //Reservation entity configuration
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(r => r.Status)
                .HasConversion<string>();
            });

            //Invoice entity configuration
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(i => i.TotalAmount)
                .HasColumnType("decimal(18,2)");

                entity.Property(i => i.PaymentStatus)
                .HasConversion<string>();
            });

            //InvoiceItem entity configuration
            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.Property(i => i.UnitPrice)
                .HasColumnType("decimal(18,2)");

                entity.Property(i => i.TotalPrice)
                .HasColumnType("decimal(18,2)");
            });

            //Order entity configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

                entity.Property(o => o.Status)
                .HasConversion<string>();
            });

            //OrderItem entity configuration
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(o => o.UnitPrice)
                .HasColumnType("decimal(18,2)");

                entity.Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)");
            });

        }
    }
}

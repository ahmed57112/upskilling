using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using upskilling.Models;

namespace MyProject.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderProduct> OrderProducts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = DESKTOP-2V10NBB; Database = UPSKILLIING; Trusted_Connection = True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductId);
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.Description).IsRequired();
                entity.Property(p => p.Price).HasColumnType("decimal(10, 2)").IsRequired();
                entity.Property(p => p.Stock).IsRequired();
            });

            // Configuring Order entity
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.OrderId);
                entity.Property(o => o.OrderDate).HasDefaultValueSql("GETDATE()");
                entity.Property(o => o.TotalAmount).HasColumnType("decimal(10, 2)").IsRequired();
                entity
                        .HasMany<Product>(g => g.products)
                        .WithOne(s => s.Order)
                        .HasForeignKey(s => s.OrderId);
            });

            //// Configuring OrderProduct entity
            //modelBuilder.Entity<OrderProduct>(entity =>
            //{
            //    entity.HasKey(op => op.OrderProductId);
            //    entity.HasOne(op => op.Order)
            //          .WithMany(o => o.OrderProducts)
            //          .HasForeignKey(op => op.OrderId);
            //    entity.HasOne(op => op.Product)
            //          .WithMany(p => p.OrderProducts)
            //          .HasForeignKey(op => op.ProductId);
            //});
        }
    }
}

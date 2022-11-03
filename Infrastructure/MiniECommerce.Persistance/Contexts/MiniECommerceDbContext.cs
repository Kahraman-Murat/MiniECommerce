using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MiniECommerce.Domain.Entities;
using MiniECommerce.Domain.Entities.Common;
using MiniECommerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Persistence.Contexts
{
    public class MiniECommerceDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public MiniECommerceDbContext(DbContextOptions options) : base(options)
        {
        }

        //
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<CompletedOrder> CompletedOrders { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .HasKey(b => b.Id);

            builder.Entity<Order>()
                .HasIndex(o => o.OrderCode)
                .IsUnique();

            builder.Entity<Basket>()
                .HasOne(b => b.Order)
                .WithOne(o => o.Basket)
                .HasForeignKey<Order>(b => b.Id);

            builder.Entity<Order>()
                .HasOne(o => o.CompletedOrder)
                .WithOne(c => c.Order)
                .HasForeignKey<CompletedOrder>(c => c.OrderId);


            base.OnModelCreating(builder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // ChangeTracker : Entityler üzerinden yapilan degisikliklerin yada yeni eklenen verinin yakalanmasini saglayan propertydir. Update operasyonlarinda Track edilen verileri yakalayip elde etmemizi saglar.

            var datas = ChangeTracker
                 .Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        /// </summary>

        //public DbSet<Product> Products { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<Customer> Customers { get; set; }

        //public DbSet<Domain.Entities.File> Files { get; set; }
        //public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        //public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        //public DbSet<Basket> Baskets { get; set; }
        //public DbSet<BasketItem> BasketItems { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<Order>()
        //        .HasKey(b => b.Id);

        //    builder.Entity<Basket>()
        //        .HasOne(b => b.Order)
        //        .WithOne(o => o.Basket)
        //        .HasForeignKey<Order>(b => b.Id);

        //    base.OnModelCreating(builder);
        //}
        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    // ChangeTracker : Entityler üzerinden yapilan degisikliklerin yada yeni eklenen verinin yakalanmasinisaglayan propertydir. Update operasyonlarinda Track edilen verileri yakalayip elde etmemizi saglar.

        //    var datas = ChangeTracker
        //        .Entries<BaseEntity>();

        //    foreach (var data in datas)
        //    {
        //        _ = data.State switch
        //        {
        //            EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
        //            EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
        //            _ => DateTime.UtcNow
        //        };
        //    }

        //    return await base.SaveChangesAsync(cancellationToken);
        //}

        
    }
}

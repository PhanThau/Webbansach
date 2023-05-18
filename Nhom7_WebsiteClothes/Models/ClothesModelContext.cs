using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Nhom7_WebsiteClothes.Models
{
    public partial class ClothesModelContext : DbContext
    {
        public ClothesModelContext()
            : base("name=ClothesModelContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Cloth> Clothes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cloth>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Cloth>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Cloth)
                .HasForeignKey(e => e.ClothesId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);
        }
    }
}

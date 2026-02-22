using Sales.Data.Entities;
using System.Data.Entity;

namespace Sales.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext() : base("name=SalesDB")
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>()
                .HasMany(s => s.SaleItems)
                .WithRequired(i => i.Sale)
                .HasForeignKey(i => i.SaleId);

            modelBuilder.Entity<SaleItem>()
                .HasRequired(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId);

            modelBuilder.Entity<Sale>()
                .HasRequired(s => s.Customer)
                .WithMany()
                .HasForeignKey(s => s.CustomerId);

            modelBuilder.Entity<SaleItem>()
                .Property(i => i.Subtotal)
                .HasDatabaseGeneratedOption(
                    System.ComponentModel.DataAnnotations.Schema
                    .DatabaseGeneratedOption.Computed);

            base.OnModelCreating(modelBuilder);
        }
    }
}

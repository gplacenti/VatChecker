using Microsoft.EntityFrameworkCore;
using VatChecker.Models.Entities;

namespace VatChecker.DataAccess;

public class VatCheckerDbContext(DbContextOptions<VatCheckerDbContext> options) : DbContext(options)
{
    public DbSet<CustomerInfo> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Fluent API configuration if needed
        modelBuilder.Entity<CustomerInfo>()
                    .HasIndex(c => new { c.CountryCode, c.VatNumber })
                    .IsUnique();
    }
}
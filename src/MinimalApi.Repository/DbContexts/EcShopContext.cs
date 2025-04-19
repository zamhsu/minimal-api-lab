using Microsoft.EntityFrameworkCore;
using MinimalApi.Repository.Entities;

namespace MinimalApi.Repository.DbContexts;

public class EcShopContext : DbContext
{
    public EcShopContext(DbContextOptions<EcShopContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Product { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(e =>
        {
            e.HasIndex(b => b.Id).IsUnique();
        });
    }
}
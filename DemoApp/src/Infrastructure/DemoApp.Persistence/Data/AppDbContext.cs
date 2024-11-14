using DemoApp.Application.Interfaces.Identity;
using DemoApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Persistence.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options, ILoggedInUserService loggedInUserService)
    : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasIndex(p => p.Id).IsUnique();
        modelBuilder.Entity<Category>().HasIndex(c => c.Id).IsUnique();

        modelBuilder.ApplyConfiguration(new SeedCategory());

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = string.IsNullOrWhiteSpace(entry.Entity.CreatedBy) ? loggedInUserService.Username! : entry.Entity.CreatedBy;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.IsDeleted = false;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.IsDeleted = false;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
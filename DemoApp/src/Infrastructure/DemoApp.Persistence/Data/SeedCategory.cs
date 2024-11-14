using DemoApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApp.Persistence.Data;

public class SeedCategory : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData([
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Bags",
                Description = "Bags for men and women",
                CreatedBy = "System",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Shoes",
                Description = "Unisex shoes",
                CreatedBy = "System",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Belts",
                Description = "Belts for men",
                CreatedBy = "System",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
        ]);
    }
}
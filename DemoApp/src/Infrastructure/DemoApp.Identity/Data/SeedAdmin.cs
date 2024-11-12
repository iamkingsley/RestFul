using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApp.Identity.Data;

public class SeedAdmin : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // seed first administrator

        var admin = new ApplicationUser
        {
            Name = "Bernard",
            Surname = "Codjoe",
            DateOfBirth = DateTime.UtcNow,
            EmailConfirmed = true,
            Email = "iamkingsley@admin.com",
            NormalizedEmail = "IAMKINGSLEY@ADMIN.COM",
            UserName = "iamkingsley",
            NormalizedUserName = "IAMKINGSLEY",
            LockoutEnabled = true,
        };

        PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
        admin.PasswordHash = ph.HashPassword(admin, "Password@1");

        builder.HasData(admin);
    }
}

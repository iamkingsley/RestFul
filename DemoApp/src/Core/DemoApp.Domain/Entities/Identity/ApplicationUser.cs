using Microsoft.AspNetCore.Identity;
namespace DemoApp.Domain.Entities.Identity;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string? MiddleNames { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? ProfileImage { get; set; }
    public string? Gender { get; set; }
}

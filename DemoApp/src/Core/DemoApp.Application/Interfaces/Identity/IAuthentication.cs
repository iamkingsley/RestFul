using DemoApp.Contracts.Dtos.Identity;
using DemoApp.Domain.Entities.Identity;

namespace DemoApp.Application.Interfaces.Identity;

public interface IAuthentication
{
    Task<LoginResponse?> AuthenticateAsync(string username, string password);
    Task<string?> RegistrationAsync(ApplicationUser applicationUser, string password, List<string>? roles = null);
    Task<bool> ChangePasswordAsync(string oldPassword, string newPassword, string username);
    Task UpdateUserAsync(ApplicationUser user);
    Task AssignUserRoleAsync(ApplicationUser user, IEnumerable<string> roles, CancellationToken cancellationToken = default);
    Task<ApplicationUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<List<string>?> GetUserRoleByUserNameAsync(string username, CancellationToken cancellationToken = default);
    Task<ApplicationUser?> GetUserByUserNameAsync(string email, CancellationToken cancellationToken = default);
    Task<(long, IReadOnlyList<ApplicationUser>)> GetUsersAsync(string role, int page, int size, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ApplicationUser>> GetUsersWithUsernamesAsync(IEnumerable<string> usernames,
        CancellationToken cancellationToken = default);
    Task<(long, IReadOnlyList<ApplicationUser>)> FilterUsersAsync(string keyword, int page, int size,
        CancellationToken cancellationToken = default);
    Task<string?> RequestPasswordResetAsync(string username, string password);
    Task<string?> RequestAdminPasswordResetAsync(string email);
    Task<bool> RemoveFromRoleAsync(ApplicationUser user, string role);
    Task DeleteAsync(string email, bool archive);
}

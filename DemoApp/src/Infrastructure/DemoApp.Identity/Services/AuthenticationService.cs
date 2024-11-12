using DemoApp.Application.Interfaces.Identity;
using DemoApp.Application.Utility;
using DemoApp.Contracts.Dtos.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoApp.Identity.Services;

public class AuthenticationServices(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IOptions<JwtOptions> jwtSettings)
    : IAuthentication
{
    public async Task<LoginResponse?> AuthenticateAsync(string username, string password)
    {
        var user = await userManager.FindByNameAsync(username);

        if (user is null)
        {
            return null;
        }

        var result = await signInManager.PasswordSignInAsync(user.UserName!, password, false, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return null;
        }

        var jwtToken = await GenerateToken(user);

        return new LoginResponse() { UserId = user.Id, Username = user.UserName!, Token = jwtToken};
    }

    public async Task<bool> ChangePasswordAsync(string oldPassword, string newPassword, string username)
    {
        var user = await userManager.FindByNameAsync(username);

        if (user is null)
        {
            return false;
        }

        var result = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);

        return result.Succeeded;
    }

    public async Task<string?> RequestAdminPasswordResetAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null) return null;

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var newPassword = GenerateNewPassword() + "@1";
        var changeResult = await userManager.ResetPasswordAsync(user, token, newPassword);

        return !changeResult.Succeeded ? null : newPassword;
    }

    public async Task<bool> RemoveFromRoleAsync(ApplicationUser user, string role)
    {
        var response = await userManager.RemoveFromRoleAsync(user, role);
        return response.Succeeded;
    }

    public Task DeleteAsync(string email, bool archive)
    {
        throw new NotImplementedException();
    }

    public async Task AssignUserRoleAsync(ApplicationUser user, IEnumerable<string> roles, CancellationToken cancellationToken = default)
    {
        await userManager.AddToRolesAsync(user, roles);
    }

    public Task<ApplicationUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<ApplicationUser?> GetUserByUserNameAsync(string username, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByNameAsync(username);

        return user ?? null;
    }

    public async Task<(long, IReadOnlyList<ApplicationUser>)> GetUsersAsync(string role, int page, int size, CancellationToken cancellationToken = default)
    {
        IList<ApplicationUser> users;

        if (!string.IsNullOrWhiteSpace(role))
        {
            users = await userManager.GetUsersInRoleAsync(role);
        }
        else
        {
            users = userManager.Users.ToList();
        }

        var pagedList = users.Count() > size ? users : users.Skip((page - 1) * size).Take(size);

        return (users.LongCount(), pagedList.ToList());
    }

    public async Task<IReadOnlyList<ApplicationUser>> GetUsersWithUsernamesAsync(IEnumerable<string> usernames, CancellationToken cancellationToken = default)
    {
        return await userManager.Users.Where(u => usernames.Contains(u.UserName!)).ToListAsync(cancellationToken);
    }

    public async Task<(long, IReadOnlyList<ApplicationUser>)> FilterUsersAsync(string keyword, int page, int size, CancellationToken cancellationToken = default)
    {
        IList<ApplicationUser> users;

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            users = await userManager
                .Users
                .Where(u => u.UserName!.ToLower().Contains(keyword.ToLower()) ||
                                                       u.Name.ToLower().Contains(keyword.ToLower()) ||
                                                       u.Surname.ToLower().Contains(keyword.ToLower())).ToListAsync(cancellationToken);
        }
        else
        {
            users = await userManager.Users.ToListAsync(cancellationToken);
        }

        var count = users.LongCount();
        var pagedList = users.Count() > size ? users : users.OrderBy(u => u.Surname).Skip((page - 1) * size).Take(size);

        return (count, pagedList.ToList());
    }

    public async Task<List<string>?> GetUserRoleByUserNameAsync(string username, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByNameAsync(username);

        if (user is null)
            return null;

        var roles = await userManager.GetRolesAsync(user);

        return roles.ToList();
    }

    public async Task<string?> RegistrationAsync(ApplicationUser applicationUser, string password, List<string>? roles = null)
    {
        var result = await userManager.CreateAsync(applicationUser, password);

        if (!result.Succeeded) return null;

        if (roles is null) return applicationUser.UserName;

        var newUser = await userManager.FindByNameAsync(applicationUser.UserName!);
        var roleResult = await userManager.AddToRolesAsync(newUser!, roles);

        if (roleResult.Succeeded) return applicationUser.UserName;

        await userManager.DeleteAsync(newUser!);
        return null;
    }

    public async Task<string?> RequestPasswordResetAsync(string username, string password)
    {
        var result = await userManager.FindByNameAsync(username);

        if (result is null)
            return null;

        var token = await userManager.GeneratePasswordResetTokenAsync(result);
        var changeResult = await userManager.ResetPasswordAsync(result, token, password);

        return !changeResult.Succeeded ? null : username;
    }

    public async Task UpdateUserAsync(ApplicationUser user)
    {
        await userManager.UpdateAsync(user);
    }

    private async Task<string> GenerateToken(ApplicationUser user)
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);

        var roleClaims = roles.Select(t => new Claim("roles", t)).ToList();

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: jwtSettings.Value.Issuer,
            audience: jwtSettings.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.Value.DurationInMinutes),
            signingCredentials: signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(jwtSecurityToken);

        return tokenValue;
    }

    private string GenerateNewPassword()
    {
        var rand = new Random();
        var stringLength = rand.Next(6, 10);
        var str = "pass";
        for (var i = 0; i < stringLength; i++)
        {
            var randomValue = rand.Next(0, 26);
            var letter = Convert.ToChar(randomValue + 65);
            str = str + letter;
        }
        return str;
    }
}


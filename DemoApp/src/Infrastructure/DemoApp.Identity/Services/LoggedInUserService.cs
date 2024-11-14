using System.Security.Claims;
using DemoApp.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Http;

namespace DemoApp.Identity.Services;

public class LoggedInUserService(IHttpContextAccessor contextAccessor) : ILoggedInUserService
{
    public string? Username => contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}
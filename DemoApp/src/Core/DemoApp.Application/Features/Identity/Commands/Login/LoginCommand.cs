using DemoApp.Contracts.Dtos.Identity;

namespace DemoApp.Application.Features.Identity.Commands.Login;

public record LoginCommand(string username, string password) : IRequest<Result<LoginResponse>>;

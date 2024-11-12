using DemoApp.Application.Interfaces.Identity;
using DemoApp.Contracts.Dtos.Identity;

namespace DemoApp.Application.Features.Identity.Commands.Login;

public class LoginCommandHandler(
    IAuthentication authentication
    ) : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var loginResponse = await authentication.AuthenticateAsync(request.username, request.password);
        if (loginResponse is null)
            return Result.Fail("Login failed");
        return Result.Ok(loginResponse);
    }
}

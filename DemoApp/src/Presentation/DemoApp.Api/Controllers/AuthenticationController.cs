using DemoApp.Application.Features.Identity.Commands.Login;
using DemoApp.Contracts.Dtos;
using DemoApp.Contracts.Dtos.Identity;

namespace DemoApp.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthenticationController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await sender.Send(command);
        var response = new BaseResponse<LoginResponse>()
        {
            Success = true,
            StatusCode = StatusCodes.Status200OK,
        };
        if (result.IsFailed)
        {
            response.StatusCode = StatusCodes.Status400BadRequest;
            response.Success = false;
            response.Errors = result.Errors.Select(e => e.Message).ToList();
            return BadRequest(response);
        }
        response.Data = result.ValueOrDefault;
        return Ok(response);
    }
}

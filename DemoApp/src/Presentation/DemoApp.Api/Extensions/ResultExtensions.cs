using DemoApp.Contracts.Dtos;

namespace DemoApp.Api.Extensions;

public static class ResultExtensions
{
    public static ActionResult ToHttpResult<T>(this FluentResults.IResult<T> result, string routeName = "", object? routeValues = null, int statusCode = 200)
    {
        if (result.IsFailed)
        {
            return new
                NotFoundObjectResult(
                    new BaseResponse<string>()
                    {
                        Success = false,
                        Message = "Failed",
                        Errors = result.Errors.Select(x => x.ToString()).ToList()
                    });
        }

        return statusCode switch
        {
            201 => new CreatedAtRouteResult(routeName, routeValues,
              new BaseResponse<T> { StatusCode = 201, Success = true, Message = "Success", Data = result.Value }),
             204 => new NoContentResult(),
            _ => new OkObjectResult(new BaseResponse<T>
                {
                    StatusCode = 200, Success = true, Message = "Success", Data = result.Value
                })
        };
    }
}
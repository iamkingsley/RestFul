
using DemoApp.Application.Features.Products.Queries.GetAllProducts;
using DemoApp.Contracts.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace DemoApp.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class ProductsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(BaseResponse<IEnumerable<ProductResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await sender.Send(new GetAllProductsQuery());
        return Ok(new BaseResponse<IEnumerable<ProductResponse>>()
        {
            Data = result.ValueOrDefault,
            Success = true,
            StatusCode = StatusCodes.Status200OK,
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResponse<int>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddProduct([FromBody] AddProductCommand command)
    {
        var result = await sender.Send(command);
        if (result.IsFailed)
            return BadRequest(new BaseResponse<int>()
            {
                Success = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Errors = result.Errors.Select(e => e.Message).ToList()
            });

        return Ok(new BaseResponse<int>()
        {
            Success = true,
            StatusCode = StatusCodes.Status201Created,
            Data = result.ValueOrDefault,
        });
    }
}

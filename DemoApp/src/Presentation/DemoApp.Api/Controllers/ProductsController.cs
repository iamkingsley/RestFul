using DemoApp.Api.Extensions;
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
        return result.ToHttpResult();
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResponse<int>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddProduct([FromBody] AddProductCommand command)
    {
        var result = await sender.Send(command);
        return result.ToHttpResult(statusCode: 201);
    }
}

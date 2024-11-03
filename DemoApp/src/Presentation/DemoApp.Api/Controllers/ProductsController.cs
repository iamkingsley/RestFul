
using DemoApp.Application.Features.Products.Queries.GetAllProducts;
using DemoApp.Contracts.Dtos;

namespace DemoApp.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductsController : ControllerBase
{
    private readonly ISender _sender;

    public ProductsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await this._sender.Send(new GetAllProductsQuery());
        return Ok(new BaseResponse<IEnumerable<ProductResponse>>()
        {
            Data = result.ValueOrDefault,
            Success = true,
            StatusCode = StatusCodes.Status200OK,
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddProduct([FromBody] AddProductCommand command)
    {
        var result = await this._sender.Send(command);
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


using DemoApp.Application.Features.Products.Queries.GetAllProducts;
using System.Net;

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
    [ProducesResponseType(typeof(IEnumerable<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await this._sender.Send(new GetAllProductsQuery());
        return Ok(result.Value);
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> AddProduct([FromBody] AddProductCommand command)
    {
        var result = await this._sender.Send(command);
        if (result.IsFailed)
            return BadRequest(result.Errors.Select(e => e.Message));
        return Ok(result.Value);
    }
}

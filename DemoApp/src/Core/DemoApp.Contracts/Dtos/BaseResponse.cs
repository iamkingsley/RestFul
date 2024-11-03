
namespace DemoApp.Contracts.Dtos;

public class BaseResponse<TResponse>
{
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; }
    public bool Success { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
    public TResponse Data { get; set; } = default!;
}

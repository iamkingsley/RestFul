namespace DemoApp.Contracts.Dtos.Identity;

public record LoginResponse
{
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = [];
    public string Token { get; set; } = string.Empty;
}

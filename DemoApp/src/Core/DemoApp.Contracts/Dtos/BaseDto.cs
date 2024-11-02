namespace DemoApp.Contracts.Dtos;

public record BaseDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
}


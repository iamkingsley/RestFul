﻿namespace DemoApp.Contracts.Dtos;

public record BaseDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string CreatedBy { get; set; }
    public bool IsDeleted { get; set; }
}


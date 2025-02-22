﻿
namespace DemoApp.Domain.Entities;
public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<Product> products { get; set; } = new List<Product>();
}

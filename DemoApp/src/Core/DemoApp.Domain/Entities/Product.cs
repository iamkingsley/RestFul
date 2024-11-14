﻿
namespace DemoApp.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
        public double Price { get; set; }
    }
}

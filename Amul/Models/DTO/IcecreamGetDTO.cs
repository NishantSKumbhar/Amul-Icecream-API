﻿namespace Amul.Models.DTO
{
    public class IcecreamGetDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }        // Foreign Key
        public string Quantity { get; set; }
        public bool IsAvailable { get; set; }
        public int Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}

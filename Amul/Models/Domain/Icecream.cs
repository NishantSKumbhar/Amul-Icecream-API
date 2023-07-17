namespace Amul.Models.Domain
{
    public class Icecream
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }        // Foreign Key
        public string Quantity { get; set; }
        public bool IsAvailable { get; set; }
        public int Price { get; set; }
        public string? ImageUrl { get; set; }

        // Navigation Property
        public Categories Category { get; set; }


    }
}

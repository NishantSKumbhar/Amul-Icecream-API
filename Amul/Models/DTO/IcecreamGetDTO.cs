using System.ComponentModel.DataAnnotations;

namespace Amul.Models.DTO
{
    public class IcecreamGetDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name max length must be 100")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Description max length must be 1000")]
        public string Description { get; set; }

        [Required]
        public Guid CategoryId { get; set; }        // Foreign Key

        [Required]
        [MaxLength(20, ErrorMessage = "Quantity max length must be 20")]
        public string Quantity { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Price Range should be 0, 100")]
        public int Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}

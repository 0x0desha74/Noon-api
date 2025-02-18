using System.ComponentModel.DataAnnotations;

namespace Noon.API.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Range(.1,double.MaxValue,ErrorMessage = "Price must be greater than zero")]
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Range(1, int.MaxValue, ErrorMessage ="Quantity must be one item at least!!")]
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string brand { get; set; }
        [Required]
        public string type { get; set; }
    }
}
using Noon.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Noon.API.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}

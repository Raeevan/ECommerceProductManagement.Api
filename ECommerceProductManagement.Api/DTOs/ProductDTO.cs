using System.ComponentModel.DataAnnotations;

namespace ECommerceProductManagement.Api.DTos
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100)]
        public string Name { get; set; } = default!;

        [StringLength(250)]
        public string? Description { get; set; }

        [Range(0.01, 10000)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        public List<int> CategoryIds { get; set; } = new();
    }
}

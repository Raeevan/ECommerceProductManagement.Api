using System.ComponentModel.DataAnnotations;

namespace ECommerceProductManagement.Api.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer name is required")]
        [StringLength(100)]
        public string CustomerName { get; set; } = default!;

        [Required]
        public DateTime OrderDate { get; set; }

        [MinLength(1, ErrorMessage = "At least one order item is required.")]
        public List<OrderItemDTO> OrderItems { get; set; } = new();
    }
}

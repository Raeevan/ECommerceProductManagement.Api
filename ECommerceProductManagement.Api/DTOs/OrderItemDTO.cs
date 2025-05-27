using System.ComponentModel.DataAnnotations;

namespace ECommerceProductManagement.Api.DTOs
{
    public class OrderItemDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Id is required")]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal UnitPrice { get; set; }
    }
}

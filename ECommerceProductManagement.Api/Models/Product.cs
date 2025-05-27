using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerceProductManagement.Api.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }


}

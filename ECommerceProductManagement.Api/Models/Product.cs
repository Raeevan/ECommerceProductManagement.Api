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

        public ICollection<CategoryProduct> CategoryProducts { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
    public class CategoryProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }

}

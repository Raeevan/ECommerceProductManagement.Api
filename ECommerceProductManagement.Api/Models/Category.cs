using System.ComponentModel.DataAnnotations;

namespace ECommerceProductManagement.Api.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        // Navigation property for many-to-many relationship with Product
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

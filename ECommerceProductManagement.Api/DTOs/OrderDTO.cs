namespace ECommerceProductManagement.Api.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}

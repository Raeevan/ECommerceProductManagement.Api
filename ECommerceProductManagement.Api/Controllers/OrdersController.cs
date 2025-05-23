using ECommerceProductManagement.Api.Data;
using ECommerceProductManagement.Api.DTOs;
using ECommerceProductManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProductManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public OrdersController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orders = await _db.Orders
                .Include(o => o.OrderItems)
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    CustomerName = o.CustomerName,
                    OrderDate = o.OrderDate,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                    {
                        Id = oi.Id,
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice
                    }).ToList()
                }).ToListAsync();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            var order = await _db.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            var dto = new OrderDTO
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreateOrder(OrderDTO dto)
        {
            var order = new Order
            {
                CustomerName = dto.CustomerName,
                OrderDate = dto.OrderDate,
                OrderItems = dto.OrderItems.Select(oi => new OrderItem
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            dto.Id = order.Id;
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderDTO dto)
        {
            var order = await _db.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return NotFound();

            order.CustomerName = dto.CustomerName;
            order.OrderDate = dto.OrderDate;

            order.OrderItems = dto.OrderItems.Select(oi => new OrderItem
            {
                Id = oi.Id,
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice,
                OrderId = id
            }).ToList();

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order == null) return NotFound();

            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();

            return NoContent();
        }
        
    }
}

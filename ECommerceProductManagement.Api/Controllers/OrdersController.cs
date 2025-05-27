using ECommerceProductManagement.Api.Data;
using ECommerceProductManagement.Api.DTOs;
using ECommerceProductManagement.Api.Interfaces;
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
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IGenericRepository<Product> _productRepo;

        public OrdersController(
            ApplicationDbContext db,
            IGenericRepository<Order> orderRepo,
            IGenericRepository<Product> productRepo)
        {
            _db = db;
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orders = await _db.Orders
                .Include(o => o.OrderItems)
                .ToListAsync();

            var orderDtos = orders.Select(order => new OrderDTO
            {
                        Id = order.Id,
                        CustomerName = order.CustomerName,
                        OrderDate = order.OrderDate,
                        OrderItems = order.OrderItems != null
                    ? order.OrderItems.Select(item => new OrderItemDTO
                    {
                        Id = item.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    }).ToList()
                    : new List<OrderItemDTO>()
             });

            return Ok(orderDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null) return NotFound();

            return new OrderDTO
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems?.Select(oi => new OrderItemDTO
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList() ?? new List<OrderItemDTO>()
            };
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreateOrder(OrderDTO dto)
        {
            var orderItems = new List<OrderItem>();
            foreach (var item in dto.OrderItems)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId);
                if (product == null)
                    return BadRequest($"Product ID {item.ProductId} not found.");

                orderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            var order = new Order
            {
                CustomerName = dto.CustomerName,
                OrderDate = dto.OrderDate,
                OrderItems = orderItems
            };

            await _orderRepo.AddAsync(order);

            dto.Id = order.Id;
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderDTO dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch.");

            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null) return NotFound();

            var orderItems = new List<OrderItem>();
            foreach (var item in dto.OrderItems)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId);
                if (product == null)
                    return BadRequest($"Product ID {item.ProductId} not found.");

                orderItems.Add(new OrderItem
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    OrderId = id
                });
            }

            order.CustomerName = dto.CustomerName;
            order.OrderDate = dto.OrderDate;
            order.OrderItems = orderItems;

            await _orderRepo.UpdateAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null) return NotFound();

            await _orderRepo.DeleteAsync(id);
            return NoContent();
        }
    }

}

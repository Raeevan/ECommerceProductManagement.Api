using ECommerceProductManagement.Api.DTOs;
using ECommerceProductManagement.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceProductManagement.Api.DTos;

namespace ECommerceProductManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ReportsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // 1 Get products by category
        [HttpGet("products-by-category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByCategory(int categoryId)
        {
            var products = await _db.Products
                .Where(p => p.Categories.Any(c => c.Id == categoryId))
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryIds = p.Categories.Select(c => c.Id).ToList()
                })
                .ToListAsync();

            return Ok(products);
        }

        // 2. Orders in the last month
        [HttpGet("recent-orders")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetRecentOrders()
        {
            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);

            var orders = await _db.Orders
                .Where(o => o.OrderDate >= oneMonthAgo)
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

        // 3. Total sales per product
        [HttpGet("sales-by-product")]
        public async Task<ActionResult<IEnumerable<object>>> GetTotalSalesByProduct()
        {
            var sales = await _db.OrderItems
                .GroupBy(oi => oi.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalSales = g.Sum(oi => oi.Quantity * oi.UnitPrice)
                }).ToListAsync();

            return Ok(sales);
        }

        // 4. Top 5 products by sales
        [HttpGet("top-products")]
        public async Task<ActionResult<IEnumerable<object>>> GetTopProducts()
        {
            var topProducts = await _db.OrderItems
                .GroupBy(oi => oi.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalSales = g.Sum(oi => oi.Quantity * oi.UnitPrice)
                })
                .OrderByDescending(p => p.TotalSales)
                .Take(5)
                .ToListAsync();

            return Ok(topProducts);
        }
    }
}

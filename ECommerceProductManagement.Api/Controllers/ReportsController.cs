using ECommerceProductManagement.Api.DTos;
using ECommerceProductManagement.Api.DTOs;
using System;
using Microsoft.AspNetCore.Mvc;
using ECommerceProductManagement.Api.Data;
using Microsoft.EntityFrameworkCore;

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

        // 1. Products in a specific category
        [HttpGet("products-by-category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByCategory(int categoryId)
        {
            var products = await _db.CategoryProducts
                .Where(cp => cp.CategoryId == categoryId)
                .Select(cp => new ProductDTO
                {
                    Id = cp.Product.Id,
                    Name = cp.Product.Name,
                    Description = cp.Product.Description,
                    Price = cp.Product.Price,
                    StockQuantity = cp.Product.StockQuantity,
                    CategoryIds = cp.Product.CategoryProducts.Select(x => x.CategoryId).ToList()
                }).ToListAsync();

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
        public async Task<ActionResult> GetTotalSalesByProduct()
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
        public async Task<ActionResult> GetTopProducts()
        {
            var topProducts = await _db.OrderItems
                .GroupBy(oi => oi.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalSales = g.Sum(oi => oi.Quantity * oi.UnitPrice)
                })
                .OrderByDescending(x => x.TotalSales)
                .Take(5)
                .ToListAsync();

            return Ok(topProducts);
        }
    }

}

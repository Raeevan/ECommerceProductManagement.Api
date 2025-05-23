using ECommerceProductManagement.Api.DTos;
using ECommerceProductManagement.Api.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using ECommerceProductManagement.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProductManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _db.Products
                .Include(p => p.CategoryProducts)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryIds = p.CategoryProducts.Select(cp => cp.CategoryId).ToList()
                }).ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _db.Products
                .Include(p => p.CategoryProducts)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            var dto = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryIds = product.CategoryProducts.Select(cp => cp.CategoryId).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                CategoryProducts = dto.CategoryIds.Select(cid => new CategoryProduct { CategoryId = cid }).ToList()
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            dto.Id = product.Id;
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDTO dto)
        {
            var product = await _db.Products.Include(p => p.CategoryProducts).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;

            product.CategoryProducts = dto.CategoryIds.Select(cid => new CategoryProduct { ProductId = id, CategoryId = cid }).ToList();

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }

}

using ECommerceProductManagement.Api.DTos;
using ECommerceProductManagement.Api.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using ECommerceProductManagement.Api.Data;
using Microsoft.EntityFrameworkCore;
using ECommerceProductManagement.Api.Interfaces;

namespace ECommerceProductManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<Category> _categoryRepo;

        public ProductsController(
            IGenericRepository<Product> productRepo,
            IGenericRepository<Category> categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _productRepo.GetAllAsync();
            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CategoryIds = p.Categories.Select(c => c.Id).ToList()
            }).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO dto)
        {
            var categories = new List<Category>();

            foreach (var id in dto.CategoryIds)
            {
                var category = await _categoryRepo.GetByIdAsync(id);
                if (category == null)
                    return BadRequest($"Category ID {id} not found.");
                categories.Add(category);
            }

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                Categories = categories
            };

            await _productRepo.AddAsync(product);

            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryIds = categories.Select(c => c.Id).ToList()
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch.");

            var product = await _productRepo.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            // Validate and load category references
            var categories = new List<Category>();
            foreach (var catId in dto.CategoryIds)
            {
                var category = await _categoryRepo.GetByIdAsync(catId);
                if (category == null)
                    return BadRequest($"Category ID {catId} not found.");
                categories.Add(category);
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
            product.Categories = categories;

            await _productRepo.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            await _productRepo.DeleteAsync(id);
            return NoContent();
        }
    }


}

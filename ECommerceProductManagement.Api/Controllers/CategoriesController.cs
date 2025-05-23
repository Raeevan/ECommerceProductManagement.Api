using ECommerceProductManagement.Api.DTOs;
using ECommerceProductManagement.Api.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using ECommerceProductManagement.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProductManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CategoriesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _db.Categories
                .Select(c => new CategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                }).ToListAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null) return NotFound();

            var dto = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryDTO dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _db.Categories.Add(category);
            await _db.SaveChangesAsync();

            dto.Id = category.Id;
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDTO dto)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null) return NotFound();

            category.Name = dto.Name;
            category.Description = dto.Description;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null) return NotFound();

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}

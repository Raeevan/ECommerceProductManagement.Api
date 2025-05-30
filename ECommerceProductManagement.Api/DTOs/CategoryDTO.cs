﻿using System.ComponentModel.DataAnnotations;

namespace ECommerceProductManagement.Api.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Category name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

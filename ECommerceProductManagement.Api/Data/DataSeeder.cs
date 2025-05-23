using ECommerceProductManagement.Api.Models;
using System;

namespace ECommerceProductManagement.Api.Data
{
    public static class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Electronics", Description = "Electronic items" },
                    new Category { Name = "Books", Description = "Various books" },
                    new Category { Name = "Clothing", Description = "Apparel and garments" }
                );
            }

            if (!context.Products.Any())
            {
                var product1 = new Product { Name = "Laptop", Description = "Gaming Laptop", Price = 1500, StockQuantity = 10 };
                var product2 = new Product { Name = "Novel", Description = "Fiction Book", Price = 20, StockQuantity = 50 };

                context.Products.AddRange(product1, product2);
                context.SaveChanges();

                var electronics = context.Categories.First(c => c.Name == "Electronics");
                var books = context.Categories.First(c => c.Name == "Books");

                context.CategoryProducts.AddRange(
                    new CategoryProduct { ProductId = product1.Id, CategoryId = electronics.Id },
                    new CategoryProduct { ProductId = product2.Id, CategoryId = books.Id }
                );
            }

            if (!context.Orders.Any())
            {
                var order = new Order
                {
                    CustomerName = "John Doe",
                    OrderDate = DateTime.UtcNow,
                    OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = context.Products.First().Id, Quantity = 2, UnitPrice = 1500 }
                }
                };
                context.Orders.Add(order);
            }

            context.SaveChanges();
        }
    }

}

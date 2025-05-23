using ECommerceProductManagement.Api.Models;
using System;

namespace ECommerceProductManagement.Api.Data
{
    public static class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // --- Categories ---
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
            {
                new Category { Name = "Electronics", Description = "Electronic items" },
                new Category { Name = "Books", Description = "Various books" },
                new Category { Name = "Clothing", Description = "Apparel and garments" },
                new Category { Name = "Furniture", Description = "Home and office furniture" },
                new Category { Name = "Sports", Description = "Sporting goods" },
                new Category { Name = "Music", Description = "Instruments and accessories" },
                new Category { Name = "Toys", Description = "Toys and games" },
                new Category { Name = "Health", Description = "Health products" },
                new Category { Name = "Beauty", Description = "Beauty products" },
                new Category { Name = "Groceries", Description = "Daily grocery items" },
                new Category { Name = "Pet Supplies", Description = "Items for pets" },
                new Category { Name = "Automotive", Description = "Car accessories" },
                new Category { Name = "Tools", Description = "Hand and power tools" },
                new Category { Name = "Garden", Description = "Gardening items" },
                new Category { Name = "Office Supplies", Description = "Workplace supplies" },
                new Category { Name = "Travel", Description = "Travel accessories" },
                new Category { Name = "Baby", Description = "Products for babies" },
                new Category { Name = "Home Decor", Description = "Decorative items" },
                new Category { Name = "Kitchen", Description = "Kitchen appliances" },
                new Category { Name = "Photography", Description = "Cameras and lenses" }
            };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            // --- Products ---
            if (!context.Products.Any())
            {
                var products = new List<Product>
            {
                new Product { Name = "Laptop", Description = "Gaming Laptop", Price = 1500, StockQuantity = 10 },
                new Product { Name = "Novel", Description = "Fiction Book", Price = 20, StockQuantity = 50 },
                new Product { Name = "Desk Chair", Description = "Ergonomic chair", Price = 120, StockQuantity = 15 },
                new Product { Name = "Soccer Ball", Description = "FIFA certified", Price = 30, StockQuantity = 40 },
                new Product { Name = "Guitar", Description = "Acoustic guitar", Price = 200, StockQuantity = 10 },
                new Product { Name = "Toy Train", Description = "Electric toy train", Price = 45, StockQuantity = 25 },
                new Product { Name = "Vitamins", Description = "Multivitamin pack", Price = 25, StockQuantity = 60 },
                new Product { Name = "Lipstick", Description = "Matte lipstick", Price = 18, StockQuantity = 70 },
                new Product { Name = "Cereal", Description = "Whole grain cereal", Price = 6, StockQuantity = 100 },
                new Product { Name = "Dog Food", Description = "Dry dog food", Price = 45, StockQuantity = 30 },
                new Product { Name = "Car Charger", Description = "USB fast charger", Price = 12, StockQuantity = 35 },
                new Product { Name = "Hammer", Description = "Steel hammer", Price = 15, StockQuantity = 45 },
                new Product { Name = "Hose", Description = "50ft garden hose", Price = 28, StockQuantity = 20 },
                new Product { Name = "Notebook", Description = "Spiral notebook", Price = 3, StockQuantity = 80 },
                new Product { Name = "Luggage", Description = "Carry-on suitcase", Price = 90, StockQuantity = 12 },
                new Product { Name = "Diapers", Description = "Size 3 baby diapers", Price = 40, StockQuantity = 50 },
                new Product { Name = "Wall Art", Description = "Abstract canvas art", Price = 60, StockQuantity = 10 },
                new Product { Name = "Blender", Description = "Kitchen blender", Price = 70, StockQuantity = 18 },
                new Product { Name = "Camera", Description = "DSLR camera", Price = 800, StockQuantity = 8 },
                new Product { Name = "Keyboard", Description = "Mechanical keyboard", Price = 85, StockQuantity = 22 }
            };

                context.Products.AddRange(products);
                context.SaveChanges();

                // Assign each product to 1 matching category manually (simple logic)
                var categories = context.Categories.ToList();
                for (int i = 0; i < 20; i++)
                {
                    context.CategoryProducts.Add(new CategoryProduct
                    {
                        ProductId = products[i].Id,
                        CategoryId = categories[i].Id
                    });
                }

                context.SaveChanges();
            }

            // --- Orders ---
            if (!context.Orders.Any())
            {
                var products = context.Products.ToList();

                for (int i = 1; i <= 20; i++)
                {
                    var product = products[i - 1];
                    var order = new Order
                    {
                        CustomerName = $"Customer {i}",
                        OrderDate = DateTime.UtcNow.AddDays(-i),
                        OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = product.Id,
                            Quantity = 1 + (i % 3),
                            UnitPrice = product.Price
                        }
                    }
                    };

                    context.Orders.Add(order);
                }

                context.SaveChanges();
            }
        }
    }

}

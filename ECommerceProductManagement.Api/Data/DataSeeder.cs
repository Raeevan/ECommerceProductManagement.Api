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
                new Category { Name = "Furniture", Description = "Home and office furniture" },
                new Category { Name = "Sports", Description = "Sporting goods" },
                new Category { Name = "Music", Description = "Instruments and accessories" },
                new Category { Name = "Toys", Description = "Toys and games" },
                new Category { Name = "Health", Description = "Health products" },
                new Category { Name = "Beauty", Description = "Beauty products" },
                new Category { Name = "Groceries", Description = "Daily grocery items" },
                new Category { Name = "Pet Supplies", Description = "Items for pets" }
            };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            // --- Products ---
            if (!context.Products.Any())
            {
                var categories = context.Categories.ToList();
                var products = new List<Product>
            {
                new Product { Name = "Laptop", Description = "Gaming Laptop", Price = 1500, StockQuantity = 10, Categories = { categories[0] } },
                new Product { Name = "Novel", Description = "Fiction Book", Price = 20, StockQuantity = 50, Categories = { categories[1] } },
                new Product { Name = "Desk Chair", Description = "Ergonomic chair", Price = 120, StockQuantity = 15, Categories = { categories[2] } },
                new Product { Name = "Soccer Ball", Description = "FIFA certified", Price = 30, StockQuantity = 40, Categories = { categories[3] } },
                new Product { Name = "Guitar", Description = "Acoustic guitar", Price = 200, StockQuantity = 10, Categories = { categories[4] } },
                new Product { Name = "Toy Train", Description = "Electric toy train", Price = 45, StockQuantity = 25, Categories = { categories[5] } },
                new Product { Name = "Vitamins", Description = "Multivitamin pack", Price = 25, StockQuantity = 60, Categories = { categories[6] } },
                new Product { Name = "Lipstick", Description = "Matte lipstick", Price = 18, StockQuantity = 70, Categories = { categories[7] } },
                new Product { Name = "Cereal", Description = "Whole grain cereal", Price = 6, StockQuantity = 100, Categories = { categories[8] } },
                new Product { Name = "Dog Food", Description = "Dry dog food", Price = 45, StockQuantity = 30, Categories = { categories[9] } },
                new Product { Name = "Car Charger", Description = "USB fast charger", Price = 12, StockQuantity = 35, Categories = { categories[0], categories[9] } },
                new Product { Name = "Hammer", Description = "Steel hammer", Price = 15, StockQuantity = 45, Categories = { categories[2] } },
                new Product { Name = "Hose", Description = "50ft garden hose", Price = 28, StockQuantity = 20, Categories = { categories[2] } },
                new Product { Name = "Notebook", Description = "Spiral notebook", Price = 3, StockQuantity = 80, Categories = { categories[1] } },
                new Product { Name = "Luggage", Description = "Carry-on suitcase", Price = 90, StockQuantity = 12, Categories = { categories[2] } },
                new Product { Name = "Diapers", Description = "Size 3 baby diapers", Price = 40, StockQuantity = 50, Categories = { categories[8] } },
                new Product { Name = "Wall Art", Description = "Abstract canvas art", Price = 60, StockQuantity = 10, Categories = { categories[2] } },
                new Product { Name = "Blender", Description = "Kitchen blender", Price = 70, StockQuantity = 18, Categories = { categories[8] } },
                new Product { Name = "Camera", Description = "DSLR camera", Price = 800, StockQuantity = 8, Categories = { categories[0], categories[4] } },
                new Product { Name = "Keyboard", Description = "Mechanical keyboard", Price = 85, StockQuantity = 22, Categories = { categories[0] } }
            };

                context.Products.AddRange(products);
                context.SaveChanges();

                // Re-fetch categories and products with tracking enabled
                var allCategories = context.Categories.ToList();

                for (int i = 0; i < products.Count; i++)
                {
                    // Assign 1 category per product based on index (rotate if more products than categories)
                    var category = allCategories[i % allCategories.Count];

                    // Assign the category directly to the product's navigation property
                    products[i].Categories = new List<Category> { category };
                }

                context.SaveChanges();
            }

            // --- Orders ---
            if (!context.Orders.Any())
            {
                var products = context.Products.ToList();

                if (products.Count >= 20)
                {
                    context.Orders.AddRange(
                        new Order
                        {
                            CustomerName = "Customer 1",
                            OrderDate = DateTime.UtcNow.AddDays(-1),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[0].Id, Quantity = 2, UnitPrice = products[0].Price },
                    new OrderItem { ProductId = products[1].Id, Quantity = 1, UnitPrice = products[1].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 2",
                            OrderDate = DateTime.UtcNow.AddDays(-2),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[2].Id, Quantity = 1, UnitPrice = products[2].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 3",
                            OrderDate = DateTime.UtcNow.AddDays(-3),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[3].Id, Quantity = 3, UnitPrice = products[3].Price },
                    new OrderItem { ProductId = products[4].Id, Quantity = 2, UnitPrice = products[4].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 4",
                            OrderDate = DateTime.UtcNow.AddDays(-4),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[5].Id, Quantity = 1, UnitPrice = products[5].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 5",
                            OrderDate = DateTime.UtcNow.AddDays(-5),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[6].Id, Quantity = 2, UnitPrice = products[6].Price },
                    new OrderItem { ProductId = products[7].Id, Quantity = 1, UnitPrice = products[7].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 6",
                            OrderDate = DateTime.UtcNow.AddDays(-6),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[8].Id, Quantity = 1, UnitPrice = products[8].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 7",
                            OrderDate = DateTime.UtcNow.AddDays(-7),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[9].Id, Quantity = 2, UnitPrice = products[9].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 8",
                            OrderDate = DateTime.UtcNow.AddDays(-8),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[10].Id, Quantity = 1, UnitPrice = products[10].Price },
                    new OrderItem { ProductId = products[11].Id, Quantity = 2, UnitPrice = products[11].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 9",
                            OrderDate = DateTime.UtcNow.AddDays(-9),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[12].Id, Quantity = 2, UnitPrice = products[12].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 10",
                            OrderDate = DateTime.UtcNow.AddDays(-10),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[13].Id, Quantity = 3, UnitPrice = products[13].Price },
                    new OrderItem { ProductId = products[14].Id, Quantity = 1, UnitPrice = products[14].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 11",
                            OrderDate = DateTime.UtcNow.AddDays(-11),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[15].Id, Quantity = 1, UnitPrice = products[15].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 12",
                            OrderDate = DateTime.UtcNow.AddDays(-12),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[16].Id, Quantity = 2, UnitPrice = products[16].Price },
                    new OrderItem { ProductId = products[17].Id, Quantity = 1, UnitPrice = products[17].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 13",
                            OrderDate = DateTime.UtcNow.AddDays(-3),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[18].Id, Quantity = 1, UnitPrice = products[18].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 14",
                            OrderDate = DateTime.UtcNow.AddDays(-14),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[19].Id, Quantity = 2, UnitPrice = products[19].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 15",
                            OrderDate = DateTime.UtcNow.AddDays(-15),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[0].Id, Quantity = 1, UnitPrice = products[0].Price },
                    new OrderItem { ProductId = products[1].Id, Quantity = 2, UnitPrice = products[1].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 16",
                            OrderDate = DateTime.UtcNow.AddDays(-9),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[2].Id, Quantity = 3, UnitPrice = products[2].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 17",
                            OrderDate = DateTime.UtcNow.AddDays(-17),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[3].Id, Quantity = 2, UnitPrice = products[3].Price },
                    new OrderItem { ProductId = products[4].Id, Quantity = 1, UnitPrice = products[4].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 18",
                            OrderDate = DateTime.UtcNow.AddDays(-18),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[5].Id, Quantity = 1, UnitPrice = products[5].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 19",
                            OrderDate = DateTime.UtcNow.AddDays(-19),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[6].Id, Quantity = 2, UnitPrice = products[6].Price }
                            }
                        },
                        new Order
                        {
                            CustomerName = "Customer 20",
                            OrderDate = DateTime.UtcNow.AddDays(-20),
                            OrderItems = new List<OrderItem>
                            {
                    new OrderItem { ProductId = products[7].Id, Quantity = 2, UnitPrice = products[7].Price }
                            }
                        }
                    );

                    context.SaveChanges();
                }
            }

        }
    }

}

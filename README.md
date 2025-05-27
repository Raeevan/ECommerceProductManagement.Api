# E-commerce Product Management API

A basic E-commerce Product Management API built with ASP.NET Core Web API and Entity Framework Core. Includes full CRUD operations for Products, Categories, and Orders, along with LINQ-based sales reports.

## 🛠 Technologies
- ASP.NET Core
- Entity Framework Core (Code-First)
- SQL Server 
- Swagger UI (for API testing)

## 🚀 Getting Started


### 📦 Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/Raeevan/ECommerceProductManagement.Api.git
   cd ECommerceProductManagement.Api
   ```
2. Update `appsettings.json` with your local DB connection:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=EcommerceDb;Trusted_Connection=True;TrustServerCertificate=True"
   }
   ```
3. Open Package Manager Console and run:
   ```powershell
   Drop-Database         # optional, for a fresh start
   Update-Database       # applies migrations
   ```
4. Run the application:
   ```powershell
   dotnet run
   ```

5. Open your browser to:
   ```
   https://localhost:7200/swagger
   ```
   to access the Swagger UI and test endpoints.

## 🔌 API Usage Guide

### 📁 Product Endpoints
- `GET /api/products` – List all products
- `GET /api/products/{id}` – Get product by ID
- `POST /api/products` – Add a new product
- `PUT /api/products/{id}` – Update existing product
- `DELETE /api/products/{id}` – Delete product

Example JSON for creating a product:
```json
{
  "name": "New Product",
  "description": "Test description",
  "price": 99.99,
  "stockQuantity": 10,
  "categoryIds": [1, 2]
}
```

### 🗂 Category Endpoints
- `GET /api/categories`
- `POST /api/categories`
- ... (same structure as products)

### 🧾 Order Endpoints
- `GET /api/orders`
- `POST /api/orders`
- ... (same structure)

### 📊 Reports Endpoints
- `GET /api/reports/products-by-category/{categoryId}`
- `GET /api/reports/recent-orders`
- `GET /api/reports/sales-by-product`
- `GET /api/reports/top-products`

## 📌 Notes
- Database seeding will insert 20 manual records for each entity if tables are empty.
- To reseed, drop the DB and run the app again.
- For testing in Swagger, use seeded category and product IDs.


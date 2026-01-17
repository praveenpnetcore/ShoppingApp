# ShoppingApp

## Project Description

ShoppingApp is a web-based e-commerce platform built with ASP.NET Core MVC. It allows users to browse products, manage shopping carts, place orders.

## Table of Contents

- [Project Description](#project-description)
- [Architecture & Flow](#architecture--flow)
- [Features](#features)
- [Setup & Installation](#setup--installation)
- [Project Structure](#project-structure)
- [Tools & Versions](#tools--versions)
- [Demo]

## Architecture & Flow

1. **User Browses Products**
   - User visits the home page ([HomeController](Controllers/HomeController.cs)).
   - Products are fetched from the database ([ProductController](Controllers/ProductController.cs)).

2. **Cart Management**
   - User adds/removes products to/from cart ([CartController](Controllers/CartController.cs)).
   - Cart items are managed using the [`CartItem`](Models/CartItem.cs) model.

3. **Data Access**
   - All data operations are handled via [`ApplicationDbContext`](Data/ApplicationDbContext.cs).

## Features

- Product catalog and categories
- Shopping cart
- Discount settings

## Setup & Installation

1. **Clone the repository**
   ```sh
   git clone <repository-url>
   cd ShoppingApp
   ```

2. **Restore dependencies**
   ```sh
   dotnet restore
   ```

3. **Update database**
   ```sh
   dotnet ef database update
   ```

4. **Run the application**
   ```sh
   dotnet run
   ```

## Project Structure

- `Controllers/` - MVC controllers for handling requests
- `Models/` - Entity models
- `Data/` - Database context
- `Repositories/` - Data access logic
- `Services/` - Business logic
- `ViewModels/` - Data transfer objects for views
- `Views/` - Razor views
- `wwwroot/` - Static files

## Tools & Versions

- **.NET SDK:** 8.0 (update as per your project)
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **IDE:** Visual Studio 2022 / Visual Studio Code
- **Database:** SQL Server (or your choice)
- **Other Tools:** Git, GitHub Actions (CI/CD in [.github/workflows/](.github/workflows/))

## Demo
<img width="952" height="524" alt="image" src="https://github.com/user-attachments/assets/108316c6-f3a3-49c5-8315-b399ecd5a9c9" />
<img width="956" height="530" alt="image" src="https://github.com/user-attachments/assets/fd268546-b1a3-4f68-8c81-c4da036bc11e" />
<img width="952" height="530" alt="image" src="https://github.com/user-attachments/assets/8e38b2dc-15f8-4a20-ad81-5d98cc16a4e3" />
<img width="952" height="526" alt="image" src="https://github.com/user-attachments/assets/eb4c9435-74d2-471e-8bfa-8a9e2dbf2e18" />



















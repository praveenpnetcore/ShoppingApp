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
<img width="947" height="494" alt="image" src="https://github.com/user-attachments/assets/55a2fa0e-5669-41bb-bb5a-41328e0a9993" />
<img width="950" height="524" alt="image" src="https://github.com/user-attachments/assets/2899b8a4-2804-4f23-8dca-650b24a87843" />
<img width="953" height="525" alt="image" src="https://github.com/user-attachments/assets/42113d8e-f277-4531-b79e-4715bc010db1" />
<img width="940" height="531" alt="image" src="https://github.com/user-attachments/assets/2ae3a106-6e85-465e-88ba-89656ea47c33" />
<img width="954" height="472" alt="image" src="https://github.com/user-attachments/assets/528ec393-0292-498c-b51b-a18c56868bb5" />















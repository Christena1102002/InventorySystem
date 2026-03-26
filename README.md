# Inventory System - Prevent Overselling

# Overview
This project is a simple Inventory and Sales System for the .NET Junior Backend Developer assessment.
The system ensures users **cannot sell more items than available stock**.

---

## Technologies
**Backend:** ASP.NET Core Web API, Entity Framework Core, SQL Server  
**Frontend:** Html,css,js

---

## Features
- Create and manage products
- Perform sales operations
- Prevent overselling
- Real-time stock validation
- Optional: Pagination, DTOs, Error handling,Search



## API Endpoints

**Products**
- POST /api/products → Create product
- GET /api/products → List all products

**Sales**
- POST /api/sales → Create sale

**Auth**
-POST /api/login
-POST /api/register


**Concurrency Handling:**  
- Ensures two simultaneous sales cannot reduce stock below zero.
- Implemented using EF Core transactions or row versioning (if applicable)


## Running the Project

### Backend
1. Open solution in Visual Studio
2. Update connection string in appsettings.json
3. Run migrations:
4. Run the backend project

This is a simple frontend built with HTML, CSS, and JavaScript, consisting of **3 main pages**:

1. **Login** – allows existing users to log in.
2. **Register** – allows new users to create an account.
3. **Main Page / Sales** – allows users to select products and perform sales operations.

### How to run
1. Open the project folder.
2. Open the `login .html` file in any web browser (Chrome, Edge, Firefox).  
3. Make sure the backend API is running so the frontend can fetch and update data.

### Features
- User authentication (Login / Register)
- Display products
- Perform sales operations
- Show error message if stock is insufficient
- Navigate between Login, Register, and Main/Sales pages
Organization Hierarchy Management API
This is a .NET Core 7 Web API project designed to manage an organization's employee hierarchy or structure. 
The API supports a hierarchical structure where positions have a parent-child relationship, 
allowing users to add, update, delete, and retrieve positions in a structured manner. 
The application also provides an endpoint to view the entire position tree.

Features
Create, update, and delete positions.
Retrieve a position by its ID.
Get a list of all positions.
View the entire organizational hierarchy in a tree structure.
Prevent deletion of positions that have child positions.
Implemented repository and service patterns to separate business logic and data access layers.
Uses AutoMapper to transform entity models to DTOs.
Technologies Used
.NET Core 7
Entity Framework Core
SQL Server for data storage
AutoMapper for model mapping
Repository Pattern for data access
Dependency Injection for better code organization
Swagger for API documentation (optional)
Getting Started
Prerequisites
.NET Core SDK 7.0
SQL Server
Visual Studio 2022 or Visual Studio Code
Postman (optional for testing API endpoints)
Setup
Clone the repository:

bash
Copy code
git clone https://github.com/yourusername/OrgHierarchyAPI.git
cd OrgHierarchyAPI
Configure the database connection string:

Open the appsettings.json file and update the connection string to match your local SQL Server configuration:

json
Copy code
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=DB;Trusted_Connection=True;"
  }
}
Apply database migrations:

Use the following command to create the database and apply migrations:

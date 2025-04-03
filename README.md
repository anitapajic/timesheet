# Timesheet

Timesheet is a .NET Core application designed for managing employee work hours using the Mediator and CQRS pattern. It ensures a clean architecture by separating commands, queries, and business logic.

## Features
- Employee work hour tracking
- Role-based access control
- Command and Query Responsibility Segregation (CQRS)
- Mediator pattern for decoupling dependencies
- RESTful API for frontend integration

## Technologies Used
- .NET Core
- MediatR (for the Mediator pattern)
- CQRS (Command and Query Responsibility Segregation)
- Entity Framework Core
- PostgreSQL
- ASP.NET Web API
- Dependency Injection

## Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/anitapajic/timesheet.git
   cd timesheet
   ```
2. Restore dependencies:
   ```sh
   dotnet restore
   ```
3. Apply database migrations:
   ```sh
   dotnet ef database update
   ```
4. Run the application:
   ```sh
   dotnet run
   ```

---
Made with ❤️ by Anita Pajić.


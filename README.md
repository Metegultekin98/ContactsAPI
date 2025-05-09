# ContactsAPI Project

## Overview
The ContactsAPI project is a .NET-based application designed to manage user data and generate reports. It uses a modular architecture with multiple projects for domain, persistence, application logic, and web API. The project also integrates RabbitMQ for background processing and message handling.

---

## Prerequisites
To run the project, ensure you have the following installed:

- .NET 6.0 or later
- PostgreSQL
- RabbitMQ
- Visual Studio or JetBrains Rider

---

## Project Structure

- **Core.Application**: Contains application-level logic, such as commands, queries, and DTOs.
- **Core.CrossCuttingConcerns**: Implements cross-cutting concerns like validation.
- **Core.Domain**: Contains domain entities and business rules.
- **Core.Helpers**: Provides helper classes for various functionalities.
- **Core.Persistence**: Handles database context and migrations.
- **Core.Utilities**: Provides utility classes, such as RabbitMQ integration.
- **webAPI**: The main entry point for the application, exposing RESTful endpoints.
- **webAPI.Application**: Implements application-specific features.
- **webAPI.Persistence**: Manages persistence logic specific to the webAPI.

---

## Configuration

### Database Configuration
Update the connection string in `appsettings.json` under the `webAPI` project:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YourConnectionStringHere"
  }
}
```

---

### Database Setup

Run migrations to set up the database:
```bash
dotnet ef database update --project src/projects/contactsapi/webAPI.Persistence --startup-project src/projects/contactsapi/webAPI
```

---

### RabbitMQ Configuration

- Ensure RabbitMQ is running locally or in a Docker container.
- Update the RabbitMQ connection settings in `appsettings.json`:
```json
{
  "MessageBrokerOptions": {
    "HostName": "localhost",
    "UserName": "guest",
    "Password": "guest",
    "Queues": {
      "ReportProcessingQueue": "report-processing-queue"
    }
  }
}
```

You can run RabbitMQ in a Docker container using the following command:
```bash
docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

This command will start RabbitMQ with the management plugin enabled, allowing you to access the management interface at `http://localhost:15672` using the default credentials (guest/guest).
Hostname and container name can be changed as per your requirements.

---

## Running the Application

To run the application, navigate to the `webAPI` project directory and execute:
```bash
dotnet run
```

OR

You can run the application directly from Visual Studio or JetBrains Rider by setting the `webAPI` project as the startup project and pressing F5.

---

## Features

### Users API

- **Get User by ID**  
  `GET /api/Users/{id}`

- **Get User List**  
  `GET /api/Users`

- **Create User**  
  `POST /api/Users`

- **Update User**  
  `PUT /api/Users`

- **Delete User**  
  `DELETE /api/Users`

- **Create Report by Location**  
  `POST /api/Users/CreateUserUserByLocation`
    - This endpoint generates a report based on the user's location and sends it to the RabbitMQ queue for processing.
    - It searches users with contact information type `Konum` then looks in to value and creates a report for it

### Report API
- **Get Report by ID**  
  `GET /api/Report/{id}`
- **Get Report List**  
  `GET /api/Report`

### Company API
- **Get Company by ID**  
  `GET /api/Company/{id}`
- **Get Company List**  
  `GET /api/Company`
- **Create Company**  
  `POST /api/Company`
- **Update Company**  
  `PUT /api/Company`
- **Delete Company**  
  `DELETE /api/Company`

### Contact Information API
- **Get Contact Information by ID**  
  `GET /api/ContactInfos/{id}`
- **Get Contact Information List**  
  `GET /api/ContactInfos`
- **Get Contact Information by User ID**  
  `GET /api/ContactInfos/GetByUserId/{userId}`
- **Get Contact Information Dynamically**  
  `POST /api/ContactInfos/GetList/ByDynamic`
- **Create Contact Information**  
  `POST /api/ContactInfos`
- **Update Contact Information**  
  `PUT /api/ContactInfos`
- **Delete Contact Information**  
  `DELETE /api/ContactInfos`

---

### Background Services

- **RabbitMQ Consumer**:  
  Processes messages from the `report-processing-queue` to handle report generation.

---

## Testing

### Requirements
- **Coverlet**: Ensure you have Coverlet installed for code coverage.
- **ReportGenerator**: Use ReportGenerator to generate reports from the coverage data.

### Installation
```bash
dotnet tool install -g coverlet.console
dotnet tool install -g dotnet-reportgenerator-globaltool
```

### Running Tests
```bash
coverlet /path/to/project/ContactsAPI/tests/Application.Tests/bin/Debug/net8.0/Application.Tests.dll --target "dotnet" --targetargs "test /path/to/project/ContactsAPI/tests/Application.Tests --no-build" --format "opencover"    
```

### Generating Reports
```bash
reportgenerator -reports:coverage.opencover.xml -targetdir:coverage-report
```

---

## Troubleshooting

- **Database Issues**: Ensure the connection string is correct and migrations are applied.
- **RabbitMQ Issues**: Verify RabbitMQ is running and accessible.
- **Dependency Injection Errors**: Ensure all required services are registered in `Startup.cs` or `Program.cs`.
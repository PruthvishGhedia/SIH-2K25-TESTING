# SIH ERP SOAP API - ASP.NET Core 8 Implementation

This is the ASP.NET Core 8 implementation of the SIH ERP system, providing SOAP-based APIs for enterprise resource planning functionality.

## Project Structure

```
SIH.ERP.Soap/
├── Models/                 # POCO entities
├── Contracts/              # WCF service contracts
├── Repositories/           # Data access layer using Dapper
├── Services/               # Service implementations
├── Program.cs              # Application entry point
├── appsettings.json        # Configuration file
└── SIH.ERP.Soap.csproj     # Project file

SIH.ERP.Soap.Tests/
├── *Tests.cs               # Unit tests for each service
└── SIH.ERP.Soap.Tests.csproj  # Test project file
```

## Features

- SOAP endpoints for all ERP entities
- RESTful health check endpoint
- Swagger UI for API documentation
- PostgreSQL database integration with Dapper
- Clean architecture with separation of concerns
- Dependency injection for testability

## Prerequisites

- .NET 8 SDK
- PostgreSQL database
- Environment variables configured

## Environment Variables

Create a `.env` file in the project root with the following:

```env
DATABASE_URL=your_postgresql_connection_string
```

## Endpoints

### SOAP Services

- Generic CRUD: `/soap` 
- Department: `/soap/department`
- Role: `/soap/role`
- Course: `/soap/course`
- Subject: `/soap/subject`
- Student: `/soap/student`
- Guardian: `/soap/guardian`
- Admission: `/soap/admission`
- Hostel: `/soap/hostel`
- Room: `/soap/room`
- Hostel Allocation: `/soap/hostelallocation`
- Fees: `/soap/fees`
- Library: `/soap/library`
- Book Issue: `/soap/bookissue`
- Exam: `/soap/exam`
- Result: `/soap/result`
- User: `/soap/user`
- User Role: `/soap/userrole`
- Contact Details: `/soap/contactdetails`

### REST Services

- Health Check: `GET /health`
- Swagger UI: `/swagger`

## Building and Running

```bash
# Navigate to the project directory
cd SIH.ERP.Soap

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run
```

## Running Tests

```bash
# Navigate to the test directory
cd SIH.ERP.Soap.Tests

# Run tests
dotnet test
```

## Technology Stack

- ASP.NET Core 8
- CoreWCF for SOAP services
- Dapper for data access
- PostgreSQL database
- Swagger for API documentation
- xUnit for testing
- DotNetEnv for environment variables

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a pull request

## License

This project is licensed under the MIT License.
# SIH ERP Backend

## Overview
The SIH ERP Backend is a comprehensive REST-based Web API system built with ASP.NET Core. It provides complete CRUD operations for managing educational institutions, including students, courses, departments, faculty, admissions, hostel management, library services, and more.

## Key Features
- Production-ready REST APIs using ASP.NET Core
- Complete CRUD operations for 23 educational entities
- Health checks with database connectivity verification
- Secure database access with parameterized queries
- JSON-based REST communication
- Support for students, courses, departments, fees, exams, guardians, admissions, hostels, rooms, hostel allocations, library, book issues, results, user roles, contact details, faculty, enrollments, attendance, and payments

## Technology Stack
- ASP.NET Core 8
- PostgreSQL with Dapper ORM
- REST over HTTP with JSON serialization
- Swagger/OpenAPI-based API documentation

## API Endpoints
The backend provides the following endpoints:

### Core Endpoints
- Health Check: `GET /health`
- API Version: `GET /api/version`
- Swagger UI: `GET /`
- Swagger JSON: `GET /swagger/v1/swagger.json`

### REST API Endpoints
- Students: `/api/student`
- Courses: `/api/course`
- Departments: `/api/department`
- Users: `/api/user`
- Fees: `/api/fees`
- Exams: `/api/exam`
- Guardians: `/api/guardian`
- Admissions: `/api/admission`
- Hostels: `/api/hostel`
- Rooms: `/api/room`
- Hostel Allocations: `/api/hostelallocation`
- Libraries: `/api/library`
- Book Issues: `/api/bookissue`
- Results: `/api/result`
- Roles: `/api/role`
- Subjects: `/api/subject`
- User Roles: `/api/userrole`
- Contact Details: `/api/contactdetails`
- Faculty: `/api/faculty`
- Enrollments: `/api/enrollment`

## Getting Started
1. Clone the repository
2. Set up the PostgreSQL database using the setup script
3. Configure the database connection string in environment variables
4. Run the application with `dotnet run`

## Testing
The backend includes comprehensive unit tests and integration tests. Run tests with `dotnet test`.

# SIH ERP REST API - ASP.NET Core 8 Implementation

This is the ASP.NET Core 8 implementation of the SIH ERP system, providing REST-based APIs for enterprise resource planning functionality.

## Project Structure

```
SIH.ERP.Soap/
├── Models/                 # POCO entities
├── Controllers/            # REST API controllers
├── Repositories/           # Data access layer using Dapper
├── Services/               # Service implementations
├── Program.cs              # Application entry point
├── appsettings.json        # Configuration file
└── SIH.ERP.Soap.csproj     # Project file
```

## Features

- REST endpoints for all ERP entities
- RESTful health check endpoint
- JSON-based API documentation with Swagger
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

### REST Services

- Students: `/api/student`
- Courses: `/api/course`
- Departments: `/api/department`
- Users: `/api/user`
- Fees: `/api/fees`
- Exams: `/api/exam`
- Guardians: `/api/guardian`
- Admissions: `/api/admission`
- Hostels: `/api/hostel`
- Rooms: `/api/room`
- Hostel Allocations: `/api/hostelallocation`
- Libraries: `/api/library`
- Book Issues: `/api/bookissue`
- Results: `/api/result`
- Roles: `/api/role`
- Subjects: `/api/subject`
- User Roles: `/api/userrole`
- Contact Details: `/api/contactdetails`
- Faculty: `/api/faculty`
- Enrollments: `/api/enrollment`

### Additional Endpoints

- Health Check: `GET /health`
- API Information: `GET /api/version`
- Swagger UI: `GET /`
- Swagger JSON: `GET /swagger/v1/swagger.json`

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

## Technology Stack

- ASP.NET Core 8
- Dapper for data access
- PostgreSQL database
- Swagger/OpenAPI for API documentation
- DotNetEnv for environment variables

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a pull request

## License

This project is licensed under the MIT License.
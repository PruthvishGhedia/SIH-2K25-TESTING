# SIH ERP SOAP API - ASP.NET Core 8 Implementation

This is the ASP.NET Core 8 implementation of the SIH ERP system, providing SOAP-based APIs for enterprise resource planning functionality.

## Recent Improvements

### Git Ignore Configuration
A comprehensive `.gitignore` file has been added to the root of the project to prevent accidental commits of:
- Build artifacts and binaries
- IDE-specific files
- Environment-specific configurations
- Temporary and log files

### Enhanced Swagger API Documentation
The Swagger implementation has been significantly enhanced with:
- Detailed API information including title, description, contact, and license
- XML comments support for better endpoint documentation
- Security definitions for authentication documentation
- Improved Swagger UI presentation

### Comprehensive XML Documentation
All service contracts and key models have been enhanced with comprehensive XML documentation:
- Service contracts with operation-level documentation
- Models with property-level documentation
- Clear descriptions of parameters and return values

## Project Structure

```
SIH.ERP.Soap/
├── Models/                 # POCO entities with XML documentation
├── Contracts/              # WCF service contracts with XML documentation
├── Repositories/           # Data access layer using Dapper
├── Services/               # Service implementations
├── Program.cs              # Application entry point with enhanced Swagger
├── appsettings.json        # Configuration file
└── SIH.ERP.Soap.csproj     # Project file with XML documentation generation
```

## Features

- SOAP endpoints for all ERP entities
- RESTful health check endpoint
- Enhanced Swagger UI for comprehensive API documentation
- PostgreSQL database integration with Dapper
- Clean architecture with separation of concerns
- Dependency injection for testability
- Comprehensive XML documentation for all public APIs

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
- Faculty: `/soap/faculty`
- Enrollment: `/soap/enrollment`
- Attendance: `/soap/attendance`
- Payment: `/soap/payment`

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

## Accessing API Documentation

After running the application, navigate to:
```
http://localhost:<port>/swagger
```

The enhanced Swagger UI provides:
- Detailed operation descriptions for all endpoints
- Comprehensive parameter documentation
- Clear return value schemas
- Interactive testing capabilities
- Authentication requirement documentation
- Model documentation with field descriptions

## Comprehensive API Documentation

For a complete reference of all available services and operations, see:
- [COMPREHENSIVE_API_DOCUMENTATION.md](file://d:\A%20code\Neon\SIH-2K25-TESTING\Backend\SIH.ERP.Soap\COMPREHENSIVE_API_DOCUMENTATION.md)

## Technology Stack

- ASP.NET Core 8
- CoreWCF for SOAP services
- Dapper for data access
- PostgreSQL database
- Swagger for API documentation
- DotNetEnv for environment variables

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a pull request

## License

This project is licensed under the MIT License.
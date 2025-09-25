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
- Enhanced data flow and source information
- Detailed operation descriptions with examples
- Data source information for all APIs
- Comprehensive field-level documentation

### Comprehensive XML Documentation
All service contracts and key models have been enhanced with comprehensive XML documentation:
- Service contracts with operation-level documentation
- Models with property-level documentation
- Clear descriptions of parameters and return values
- Data source information for better understanding
- Code examples for common operations
- Detailed field descriptions with examples

### Complete Swagger Implementation
A full Swagger/OpenAPI documentation implementation has been completed:
- Interactive Swagger UI with enhanced features
- Additional REST endpoints for API information
- Comprehensive documentation files
- [SWAGGER_IMPLEMENTATION_SUMMARY.md](SWAGGER_IMPLEMENTATION_SUMMARY.md) with implementation details

### Enhanced Data Documentation
Detailed documentation of data requirements and sources:
- [DETAILED_API_DOCUMENTATION.md](DETAILED_API_DOCUMENTATION.md) with comprehensive information about all services
- Enhanced service contracts with data source information
- Enhanced data models with field-level data source information
- Complete data flow documentation for all entities

### Dashboard API with Real-time Updates
A new Dashboard REST API has been implemented with real-time updates using SignalR:
- Aggregates data from all underlying services
- Provides real-time updates when any data changes
- Includes endpoints for summary, statistics, and recent activity
- Fully integrated with Swagger documentation

## Project Structure

```
SIH.ERP.Soap/
├── Models/                 # POCO entities with XML documentation
├── Contracts/              # WCF service contracts with XML documentation
├── Repositories/           # Data access layer using Dapper
├── Services/               # Service implementations with real-time updates
├── Controllers/            # REST API controllers including Dashboard
├── Hubs/                   # SignalR hubs for real-time communication
├── Program.cs              # Application entry point with enhanced Swagger
├── appsettings.json        # Configuration file
└── SIH.ERP.Soap.csproj     # Project file with XML documentation generation
```

## Features

- SOAP endpoints for all ERP entities with real-time updates
- RESTful Dashboard API with real-time updates via SignalR
- Enhanced Swagger UI for comprehensive API documentation
- PostgreSQL database integration with Dapper
- Clean architecture with separation of concerns
- Dependency injection for testability
- Comprehensive XML documentation for all public APIs
- Detailed data flow and source information
- Field-level data source documentation
- Complete API operation examples

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

- Health Check: `GET /api/health`
- API Version Info: `GET /api/version`
- API Documentation Info: `GET /api/documentation`
- Dashboard Summary: `GET /api/dashboard/summary`
- Dashboard Statistics: `GET /api/dashboard/stats`
- Dashboard Recent Activity: `GET /api/dashboard/recent-activity`
- Dashboard Real-time Updates: `/api/dashboard/hub` (SignalR)
- Swagger UI: `/swagger`
- Swagger JSON: `/swagger/v1/swagger.json`

## Real-time Updates

The SIH ERP system now includes real-time updates for all SOAP services using SignalR:

- When any data is created, updated, or deleted via SOAP services, real-time updates are sent to all connected clients
- The Dashboard API receives these updates automatically and can display live data
- Clients can connect to the SignalR hub at `/api/dashboard/hub` to receive real-time updates

### Using Real-time Updates

1. **Connect to SignalR Hub**: Connect to `/api/dashboard/hub` using a SignalR client
2. **Listen for Updates**: Subscribe to events like `ReceiveStudentUpdate`, `ReceiveCourseUpdate`, etc.
3. **Receive Live Data**: Get real-time updates whenever data changes in any service

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
- Data source information for better understanding
- Example values for all fields
- Complete data flow documentation

### Using Swagger UI

1. **Explore Endpoints**: All available SOAP endpoints are listed and grouped by service
2. **Test Operations**: Click any endpoint, then "Try it out" to test with sample data
3. **View Models**: See detailed schema definitions for all data models
4. **Authentication**: Some endpoints may require authentication credentials

## API Documentation

The SIH ERP system provides comprehensive API documentation through Swagger UI:

- **Swagger UI**: http://localhost:5000/swagger
- **Swagger JSON**: http://localhost:5000/swagger/v1/swagger.json

### REST API Endpoints

The system provides REST endpoints for API information, health checks, and dashboard functionality:

- **API Version**: `GET /api/version`
- **API Documentation**: `GET /api/documentation`
- **Health Check**: `GET /api/health`
- **Services List**: `GET /api/services`
- **Services Details**: `GET /api/services/details`
- **Dashboard Summary**: `GET /api/dashboard/summary`
- **Dashboard Statistics**: `GET /api/dashboard/stats`
- **Dashboard Recent Activity**: `GET /api/dashboard/recent-activity`

### Dashboard API Endpoints

The Dashboard API provides aggregated data from all underlying services:

- **Dashboard Summary**: `GET /api/dashboard/summary` - Key metrics overview
- **Dashboard Statistics**: `GET /api/dashboard/stats` - Detailed statistics
- **Dashboard Recent Activity**: `GET /api/dashboard/recent-activity` - Recent system activity

### SOAP API Documentation

All SOAP services are documented in detail:

- [SOAP API Documentation](SOAP_API_DOCUMENTATION.md) - Complete documentation for all 23 SOAP services
- Each service supports standard CRUD operations (List, Get, Create, Update, Remove)
- All services are available at their respective endpoints (e.g., `/soap/student`)
- All services now include real-time updates via SignalR

### SOAP Service Endpoints

The system provides 23 SOAP services for different aspects of educational institution management:

1. **Admission Service**: `/soap/admission` - Manages student admission processes
2. **Student Service**: `/soap/student` - Manages student records and information
3. **Department Service**: `/soap/department` - Manages academic departments
4. **Course Service**: `/soap/course` - Manages academic courses
5. **Subject Service**: `/soap/subject` - Manages academic subjects
6. **Role Service**: `/soap/role` - Manages user roles and permissions
7. **User Service**: `/soap/user` - Manages user accounts
8. **Guardian Service**: `/soap/guardian` - Manages student guardian information
9. **Hostel Service**: `/soap/hostel` - Manages hostel facilities
10. **Room Service**: `/soap/room` - Manages hostel rooms
11. **Hostel Allocation Service**: `/soap/hostelallocation` - Manages hostel allocations for students
12. **Fees Service**: `/soap/fees` - Manages fee structures and calculations
13. **Library Service**: `/soap/library` - Manages library books and resources
14. **Book Issue Service**: `/soap/bookissue` - Manages library book issues and returns
15. **Exam Service**: `/soap/exam` - Manages examination schedules and information
16. **Result Service**: `/soap/result` - Manages examination results
17. **User Role Service**: `/soap/userrole` - Manages user-role associations
18. **Contact Details Service**: `/soap/contactdetails` - Manages contact information
19. **Faculty Service**: `/soap/faculty` - Manages faculty members
20. **Enrollment Service**: `/soap/enrollment` - Manages student course enrollments
21. **Attendance Service**: `/soap/attendance` - Manages student attendance records
22. **Payment Service**: `/soap/payment` - Manages payment transactions
23. **Generic CRUD Service**: `/soap` - Provides generic CRUD operations for all tables (restricted access)

Each service supports the following standard operations:
- `ListAsync` - Retrieve a list of items with pagination
- `GetAsync` - Retrieve a specific item by ID
- `CreateAsync` - Create a new item
- `UpdateAsync` - Update an existing item
- `RemoveAsync` - Delete an item by ID

All services now include real-time updates via SignalR - when any operation is performed, real-time updates are sent to all connected clients.

## Comprehensive API Documentation

For a complete reference of all available services and operations, see:
- [COMPREHENSIVE_API_DOCUMENTATION.md](COMPREHENSIVE_API_DOCUMENTATION.md)
- [SWAGGER_DOCUMENTATION.md](SWAGGER_DOCUMENTATION.md)
- [README_API.md](README_API.md)
- [SWAGGER_IMPLEMENTATION_SUMMARY.md](SWAGGER_IMPLEMENTATION_SUMMARY.md)
- [DETAILED_API_DOCUMENTATION.md](DETAILED_API_DOCUMENTATION.md)
- [ENHANCED_SWAGGER_DOCUMENTATION.md](ENHANCED_SWAGGER_DOCUMENTATION.md)

## Technology Stack

- ASP.NET Core 8
- CoreWCF for SOAP services
- SignalR for real-time updates
- Dapper for data access
- PostgreSQL database
- Swagger/OpenAPI for API documentation
- DotNetEnv for environment variables

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
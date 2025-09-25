# SIH ERP System

A comprehensive Educational Resource Planning (ERP) system built with ASP.NET Core 8, CoreWCF SOAP services, PostgreSQL, and React frontend.

## Architecture

- **Backend**: ASP.NET Core 8 with CoreWCF SOAP services
- **Database**: PostgreSQL with Dapper ORM
- **Frontend**: React 18 + Vite + TypeScript + Tailwind CSS
- **Communication**: SOAP over HTTP with XML serialization

## Features

### Backend Features
- ✅ Production-ready SOAP services with CoreWCF
- ✅ CORS configuration for frontend integration
- ✅ Global error handling with SOAP fault support
- ✅ Health checks with database connectivity
- ✅ Scoped dependency injection for better resource management
- ✅ Secure GenericCrudService with allowlisted tables
- ✅ Parameterized queries to prevent SQL injection
- ✅ Comprehensive seed data for development

### Frontend Features
- ✅ Modern React 18 with TypeScript
- ✅ SOAP client with XML parsing
- ✅ Responsive design with Tailwind CSS
- ✅ Complete CRUD operations for all entities
- ✅ Form validation and error handling
- ✅ Search, pagination, and sorting
- ✅ Dashboard with statistics

### Supported Entities
- Students, Courses, Departments, Users
- Fees, Exams, Guardians, Admissions
- Hostels, Rooms, Hostel Allocations
- Library, Book Issues, Results
- User Roles, Contact Details

## API Documentation

Comprehensive API documentation is available through Swagger UI and additional documentation files:

### Swagger UI
- **URL**: http://localhost:5000/swagger
- **Description**: Interactive API documentation for all REST endpoints
- **Features**: 
  - Detailed operation descriptions
  - Parameter and response documentation
  - Example requests and responses
  - Authentication support

### REST API Endpoints
The system provides REST endpoints for API information and health checks:
- **API Version**: `GET /api/version`
- **API Documentation**: `GET /api/documentation`
- **Health Check**: `GET /api/health`
- **Services List**: `GET /api/services`
- **Services Details**: `GET /api/services/details`

### SOAP API Documentation
All 23 SOAP services are thoroughly documented:
- [Backend/SIH.ERP.Soap/SOAP_API_DOCUMENTATION.md](Backend/SIH.ERP.Soap/SOAP_API_DOCUMENTATION.md) - Complete documentation for all SOAP services
- Each service endpoint is clearly listed with available operations
- Standard CRUD operations are consistent across all services

### SOAP Service Endpoints
The system provides 23 SOAP services accessible at their respective endpoints:
1. **Admission Service**: `/soap/admission`
2. **Student Service**: `/soap/student`
3. **Department Service**: `/soap/department`
4. **Course Service**: `/soap/course`
5. **Subject Service**: `/soap/subject`
6. **Role Service**: `/soap/role`
7. **User Service**: `/soap/user`
8. **Guardian Service**: `/soap/guardian`
9. **Hostel Service**: `/soap/hostel`
10. **Room Service**: `/soap/room`
11. **Hostel Allocation Service**: `/soap/hostelallocation`
12. **Fees Service**: `/soap/fees`
13. **Library Service**: `/soap/library`
14. **Book Issue Service**: `/soap/bookissue`
15. **Exam Service**: `/soap/exam`
16. **Result Service**: `/soap/result`
17. **User Role Service**: `/soap/userrole`
18. **Contact Details Service**: `/soap/contactdetails`
19. **Faculty Service**: `/soap/faculty`
20. **Enrollment Service**: `/soap/enrollment`
21. **Attendance Service**: `/soap/attendance`
22. **Payment Service**: `/soap/payment`
23. **Generic CRUD Service**: `/soap` (restricted access)

Each service supports the following standard operations:
- `ListAsync` - Retrieve a list of items with pagination
- `GetAsync` - Retrieve a specific item by ID
- `CreateAsync` - Create a new item
- `UpdateAsync` - Update an existing item
- `RemoveAsync` - Delete an item by ID

## Quick Start

### Prerequisites
- .NET 8 SDK
- Node.js 18+
- PostgreSQL 12+
- Git

### Backend Setup

1. **Clone and navigate to backend**
   ```bash
   git clone <repository-url>
   cd SIH-2k25/Backend/SIH.ERP.Soap
   ```

2. **Set up environment variables**
   ```bash
   # Create .env file
   echo "DATABASE_URL=Host=localhost;Database=sih_erp;Username=postgres;Password=yourpassword" > .env
   ```

3. **Install dependencies and run**
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

4. **Seed the database** (optional)
   ```bash
   # Run the seed script in your PostgreSQL client
   psql -d sih_erp -f Seed/seed.sql
   ```

5. **Verify backend is running**
   ```bash
   curl http://localhost:5000/health
   ```

### Frontend Setup

1. **Navigate to frontend**
   ```bash
   cd SIH-2k25/Frontend
   ```

2. **Install dependencies**
   ```bash
   npm install
   ```

3. **Start development server**
   ```bash
   npm run dev
   ```

4. **Verify frontend is running**
   - Open http://localhost:5173
   - Check browser console for any SOAP connection errors

## API Testing

### Health Check
```bash
curl http://localhost:5000/health
```

### SOAP Student List
```bash
curl -X POST http://localhost:5000/soap/student \
  -H "Content-Type: text/xml; charset=utf-8" \
  -H "SOAPAction: \"http://tempuri.org/IStudentService/ListAsync\"" \
  -d '<?xml version="1.0"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <ListAsync xmlns="http://tempuri.org/">
      <limit>10</limit>
      <offset>0</offset>
    </ListAsync>
  </soap:Body>
</soap:Envelope>'
```

### SOAP Student Get
```bash
curl -X POST http://localhost:5000/soap/student \
  -H "Content-Type: text/xml; charset=utf-8" \
  -H "SOAPAction: \"http://tempuri.org/IStudentService/GetAsync\"" \
  -d '<?xml version="1.0"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <GetAsync xmlns="http://tempuri.org/">
      <student_id>1</student_id>
    </GetAsync>
  </soap:Body>
</soap:Envelope>'
```

## Frontend SOAP Client Usage

```javascript
import soapClient from './services/soapClient';

// List students
const students = await soapClient.list('student', { limit: 20, offset: 0 });

// Get a specific student
const student = await soapClient.get('student', 1);

// Create a new student
const newStudent = await soapClient.create('student', {
  first_name: 'John',
  last_name: 'Doe',
  email: 'john@example.com',
  dob: '2000-01-01',
  department_id: 1,
  course_id: 1
});

// Update a student
const updatedStudent = await soapClient.update('student', 1, {
  first_name: 'Jane',
  last_name: 'Smith'
});

// Delete a student
await soapClient.remove('student', 1);
```

## Project Structure

```
SIH-2k25/
├── Backend/
│   ├── SIH.ERP.Soap/
│   │   ├── Contracts/          # SOAP service contracts
│   │   ├── Models/             # Data models
│   │   ├── Repositories/       # Data access layer
│   │   ├── Services/           # Business logic
│   │   ├── Middleware/         # Custom middleware
│   │   ├── Data/              # Connection factories
│   │   ├── Exceptions/        # Custom exceptions
│   │   ├── Seed/              # Database seed data
│   │   └── Program.cs         # Application entry point
├── Frontend/
│   ├── src/
│   │   ├── services/          # SOAP client
│   │   ├── components/        # React components
│   │   ├── pages/            # Page components
│   │   ├── types/            # TypeScript definitions
│   │   └── context/          # State management
│   └── package.json
└── README.md
```

## Configuration

### Backend Configuration
- **DATABASE_URL**: PostgreSQL connection string
- **CORS_POLICY**: Configured for localhost:5173
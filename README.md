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
- **Health Checks**: Enabled with database connectivity check

### Frontend Configuration
- **VITE_BACKEND_BASE**: Backend URL (default: http://localhost:5000)
- **CORS**: Configured to allow credentials

## Security Features

- ✅ CORS policy enforcement
- ✅ SQL injection prevention with parameterized queries
- ✅ Table allowlisting in GenericCrudService
- ✅ Input validation in services
- ✅ Error sanitization in production
- ✅ Scoped database connections

## Development Workflow

1. **Backend Development**
   - Make changes to services/repositories
   - Test SOAP endpoints with curl
   - Verify health checks

2. **Frontend Development**
   - Use SOAP client for API calls
   - Test with browser dev tools
   - Verify CORS configuration
   - Check error handling

3. **Integration Testing**
   - Start both backend and frontend
   - Test CRUD operations end-to-end
   - Verify data persistence
   - Check error scenarios

## Troubleshooting

### Common Issues

1. **CORS Errors**
   - Ensure backend CORS policy includes frontend URL
   - Check that credentials are included in requests

2. **SOAP Parsing Errors**
   - Verify XML envelope format
   - Check SOAPAction headers
   - Ensure proper namespace declarations

3. **Database Connection Issues**
   - Verify DATABASE_URL environment variable
   - Check PostgreSQL is running
   - Ensure database exists and is accessible

4. **Health Check Failures**
   - Check database connectivity
   - Verify connection string format
   - Check PostgreSQL service status

## Production Deployment

### Backend
- Use environment variables for configuration
- Enable HTTPS in production
- Configure proper CORS origins
- Use connection pooling
- Set up logging and monitoring

### Frontend
- Build for production: `npm run build`
- Serve static files from web server
- Configure proper backend URL
- Enable HTTPS
- Set up CDN if needed

## Contributing

1. Create feature branch
2. Make changes with tests
3. Run full test suite
4. Update documentation
5. Submit pull request

## License

This project is part of the SIH (Smart India Hackathon) initiative.
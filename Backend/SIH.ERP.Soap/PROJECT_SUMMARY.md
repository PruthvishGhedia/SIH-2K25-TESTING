# SIH ERP SOAP API Project Summary

## Overview

This project is a SOAP-based API for an ERP system with CRUD operations for various entities. The Backend folder contains an enhanced version of the dotnet folder with additional features and improvements.

## Key Differences Between Backend and dotnet Folders

### Enhancements in Backend Folder

1. **Enhanced Error Handling**
   - Added custom middleware for error handling
   - Added RepositoryException for better error management
   - Improved SOAP fault handling

2. **Security Improvements**
   - Added table allowlist in GenericCrudService for security
   - Input validation in service layers

3. **Dependency Injection**
   - Using Scrutor for automatic dependency injection
   - Using scoped lifetime instead of singleton for better resource management

4. **Database Connection Management**
   - Added IDbConnectionFactory for better connection management
   - Added RepositoryBase for common repository functionality

5. **Additional Features**
   - Added CORS support
   - Added comprehensive seed data
   - Added more entity models, repositories, and services

6. **Testing**
   - Added integration tests
   - Added repository tests

### Missing Components Added

The Backend folder contains additional components not present in the dotnet folder:
- AttendanceService and related components
- EnrollmentService and related components
- FacultyService and related components
- PaymentService and related components

## Changes Made to Make Backend Workable

1. **Fixed Package Dependencies**
   - Removed problematic health check package reference
   - Updated test project dependencies

2. **Fixed Test Issues**
   - Fixed FacultyIntegrationTests.cs formatting issues
   - Added missing using statements to test files
   - Made Program class public for testing

3. **Removed Health Check References**
   - Removed health check endpoints from Program.cs
   - Removed health check package reference

4. **Added Documentation**
   - Created README.md with setup and usage instructions
   - Created Postman collection for API testing
   - Created database setup guide

## API Endpoints

### SOAP Endpoints

- Generic CRUD: `http://localhost:5000/soap`
- Department: `http://localhost:5000/soap/department`
- Role: `http://localhost:5000/soap/role`
- Course: `http://localhost:5000/soap/course`
- Subject: `http://localhost:5000/soap/subject`
- Student: `http://localhost:5000/soap/student`
- Guardian: `http://localhost:5000/soap/guardian`
- Admission: `http://localhost:5000/soap/admission`
- Hostel: `http://localhost:5000/soap/hostel`
- Room: `http://localhost:5000/soap/room`
- Hostel Allocation: `http://localhost:5000/soap/hostelallocation`
- Fees: `http://localhost:5000/soap/fees`
- Library: `http://localhost:5000/soap/library`
- Book Issue: `http://localhost:5000/soap/bookissue`
- Exam: `http://localhost:5000/soap/exam`
- Result: `http://localhost:5000/soap/result`
- User: `http://localhost:5000/soap/user`
- User Role: `http://localhost:5000/soap/userrole`
- Contact Details: `http://localhost:5000/soap/contactdetails`
- Faculty: `http://localhost:5000/soap/faculty`
- Enrollment: `http://localhost:5000/soap/enrollment`
- Attendance: `http://localhost:5000/soap/attendance`
- Payment: `http://localhost:5000/soap/payment`

### REST Endpoints

- Swagger UI: `http://localhost:5000/swagger`

## Testing

The project includes both unit tests and integration tests. To run the tests:

```bash
dotnet test
```

Note: Some tests require a working database connection to pass.

## Running the Application

To run the application:

```bash
dotnet run
```

The application will start on `http://localhost:5000` by default.

## Testing with Postman

Import the `SIH-ERP-SOAP-API.postman_collection.json` file into Postman to get a collection of pre-configured requests for testing the API.
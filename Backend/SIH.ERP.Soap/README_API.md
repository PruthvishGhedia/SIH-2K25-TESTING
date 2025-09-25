# SIH ERP SOAP API Documentation

## Overview

This project provides a comprehensive SOAP-based Web Services API for the SIH ERP System. The API includes endpoints for managing all aspects of an educational institution including students, courses, departments, faculty, admissions, hostel management, library services, and more.

## API Documentation Access

Interactive API documentation is available through Swagger UI:

```
http://localhost:<port>/swagger
```

## Available Services

The API provides the following services:

1. **Department Service** - `/soap/department`
2. **Role Service** - `/soap/role`
3. **Course Service** - `/soap/course`
4. **Subject Service** - `/soap/subject`
5. **Student Service** - `/soap/student`
6. **Guardian Service** - `/soap/guardian`
7. **Admission Service** - `/soap/admission`
8. **Hostel Service** - `/soap/hostel`
9. **Room Service** - `/soap/room`
10. **Hostel Allocation Service** - `/soap/hostelallocation`
11. **Fees Service** - `/soap/fees`
12. **Library Service** - `/soap/library`
13. **Book Issue Service** - `/soap/bookissue`
14. **Exam Service** - `/soap/exam`
15. **Result Service** - `/soap/result`
16. **User Service** - `/soap/user`
17. **User Role Service** - `/soap/userrole`
18. **Contact Details Service** - `/soap/contactdetails`
19. **Faculty Service** - `/soap/faculty`
20. **Enrollment Service** - `/soap/enrollment`
21. **Attendance Service** - `/soap/attendance`
22. **Payment Service** - `/soap/payment`

## Getting Started

1. Start the application
2. Navigate to `http://localhost:<port>/swagger` in your browser
3. Explore the available endpoints
4. Use the "Try it out" feature to test API calls

## Authentication

The API uses Basic Authentication for secured endpoints. Credentials must be provided in the Authorization header.

## Common Operations

All services support these common CRUD operations:

- **ListAsync**: Retrieve a list of items with pagination
- **GetAsync**: Retrieve a specific item by ID
- **CreateAsync**: Create a new item
- **UpdateAsync**: Update an existing item
- **RemoveAsync**: Delete an item by ID

## Data Models

The API uses strongly-typed data models for all requests and responses. Detailed information about each model can be found in the Swagger UI documentation.

## Error Handling

The API follows standard SOAP fault patterns for error reporting. Detailed error information is included in fault responses.

## Support

For support, please contact the SIH ERP Team.
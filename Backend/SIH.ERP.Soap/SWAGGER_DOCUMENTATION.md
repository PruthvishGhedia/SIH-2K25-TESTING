# SIH ERP SOAP API Documentation

## Overview

This document provides comprehensive documentation for the SIH ERP SOAP API. The API follows SOAP standards and provides endpoints for managing various aspects of an educational institution including students, courses, departments, faculty, admissions, hostel management, library services, and more.

## Accessing the API Documentation

The interactive API documentation is available through Swagger UI. After starting the application, you can access the documentation at:

```
http://localhost:<port>/swagger
```

Replace `<port>` with the actual port number your application is running on (typically 5000 for development).

## Available Services

The API provides the following services:

1. **Department Service** - Manage academic departments
2. **Role Service** - Manage user roles and permissions
3. **Course Service** - Manage academic courses
4. **Subject Service** - Manage course subjects
5. **Student Service** - Manage student records
6. **Guardian Service** - Manage student guardians
7. **Admission Service** - Handle student admissions
8. **Hostel Service** - Manage hostel facilities
9. **Room Service** - Manage hostel rooms
10. **Hostel Allocation Service** - Manage room allocations
11. **Fees Service** - Manage fee structures
12. **Library Service** - Manage library resources
13. **Book Issue Service** - Manage book lending
14. **Exam Service** - Manage examination schedules
15. **Result Service** - Manage student results
16. **User Service** - Manage user accounts
17. **User Role Service** - Manage user-role assignments
18. **Contact Details Service** - Manage contact information
19. **Faculty Service** - Manage faculty members
20. **Enrollment Service** - Manage course enrollments
21. **Attendance Service** - Track attendance
22. **Payment Service** - Process payments

## Using the Swagger UI

The Swagger UI provides an interactive way to explore and test the API endpoints:

1. **Viewing Endpoints**: All available SOAP endpoints are listed on the main page, grouped by service.

2. **Testing Endpoints**: 
   - Click on any endpoint to expand its details
   - Click "Try it out" to enable testing
   - Fill in required parameters
   - Click "Execute" to send a request
   - View the response in the "Responses" section

3. **Authentication**: Some endpoints may require authentication. If so, you'll see a lock icon next to the endpoint. Click it to enter your credentials.

4. **Models**: The "Schemas" section at the bottom shows the data models used by the API, including their structure and field descriptions.

## Common Operations

All services typically support these common CRUD operations:

- **ListAsync**: Retrieve a list of items with pagination support
- **GetAsync**: Retrieve a specific item by ID
- **CreateAsync**: Create a new item
- **UpdateAsync**: Update an existing item
- **RemoveAsync**: Delete an item by ID

## Error Handling

The API uses standard HTTP status codes to indicate the success or failure of requests:

- **200 OK**: The request was successful
- **400 Bad Request**: The request was invalid or malformed
- **401 Unauthorized**: Authentication is required or has failed
- **403 Forbidden**: The authenticated user does not have permission to perform the operation
- **404 Not Found**: The requested resource could not be found
- **500 Internal Server Error**: An unexpected error occurred on the server

Detailed error information is provided in the response body when applicable.

## Data Formats

The API uses XML for data exchange, as per SOAP standards. All requests and responses are formatted as SOAP envelopes.

## Rate Limiting

Currently, there are no rate limits imposed on the API. However, please use the API responsibly to avoid overloading the server.

## Versioning

The current version of the API is v1. Version information is included in the URL path for endpoints.

## Support

For support or questions about the API, please contact the SIH ERP Team at support@sih-erp.com.
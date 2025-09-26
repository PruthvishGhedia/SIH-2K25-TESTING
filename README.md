# SIH-2K25-TESTING

Educational Resource Planning (ERP) system developed as part of the Smart India Hackathon (SIH) initiative.

## Project Overview
This repository contains a comprehensive Educational Resource Planning (ERP) system developed as part of the Smart India Hackathon (SIH) initiative. The system provides a unified platform for managing all aspects of an educational institution, including student information, course management, department administration, fee collection, examination scheduling, hostel management, library services, and more.

## System Features
The SIH ERP system offers a complete solution for educational institutions with the following key features:

### Core Functionality
- Centralized management of educational resources
- Streamlined administrative processes for educational institutions
- Unified system for student, course, department, and financial management
- Comprehensive tracking of academic performance and activities

### Technical Features
- Production-ready REST APIs using ASP.NET Core
- Complete CRUD operations for 23 educational entities
- Health checks with database connectivity verification
- Secure database access with parameterized queries
- Responsive frontend with modern React and Tailwind CSS
- JSON-based REST communication between frontend and backend

## Technology Stack
### Backend
- ASP.NET Core 8
- PostgreSQL with Dapper ORM
- REST over HTTP with JSON serialization

### Frontend
- React 18 with TypeScript
- Vite as the build tool
- Tailwind CSS for styling

## API Documentation
Comprehensive API documentation is available through Swagger UI:
- **Swagger UI**: `http://localhost:5000/`
- **API Version**: `http://localhost:5000/api/version`
- **Health Check**: `http://localhost:5000/api/health`

## Supported Entities
The system provides complete CRUD operations for the following educational entities:
1. Students
2. Courses
3. Departments
4. Users
5. Fees
6. Exams
7. Guardians
8. Admissions
9. Hostels
10. Rooms
11. Hostel Allocations
12. Library
13. Book Issues
14. Results
15. User Roles
16. Contact Details
17. Faculty
18. Enrollments
19. Attendance
20. Payments
21. Subjects
22. Roles
23. User Roles

## Getting Started
### Backend Setup
1. Navigate to the Backend directory
2. Set up the PostgreSQL database using the setup script
3. Configure the database connection string in environment variables
4. Run the application with `dotnet run`

### Frontend Setup
1. Navigate to the Frontend directory
2. Install dependencies with `npm install`
3. Start the development server with `npm run dev`

## Deployment
### Backend
- Use environment variables for configuration
- Enable HTTPS in production
- Configure proper CORS origins
- Use connection pooling
- Set up logging and monitoring

### Frontend
- Build with `npm run build`
- Serve static files from web server
- Configure proper backend URL
- Enable HTTPS
- Set up CDN if needed
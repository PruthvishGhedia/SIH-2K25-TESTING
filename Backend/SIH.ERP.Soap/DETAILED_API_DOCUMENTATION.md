# SIH ERP SOAP API - Complete Documentation

## Overview

This document provides comprehensive documentation for all SOAP services available in the SIH ERP system. Each service follows a consistent CRUD pattern with List, Get, Create, Update, and Remove operations.

## Data Flow and Sources

All data in this system flows from various sources:
- **Student Data**: Admission forms, enrollment records, and student self-service portals
- **Academic Data**: Course catalogs, faculty assignments, and examination results
- **Financial Data**: Fee structures, payment processing, and financial reports
- **Facility Data**: Hostel management, room allocations, and library resources
- **Administrative Data**: User accounts, roles, and system configurations
- **Human Resources Data**: Faculty hiring records and staff management
- **Library Data**: Book acquisitions, cataloging, and circulation records
- **Communication Data**: Contact information and messaging systems

## Authentication

The API uses Basic Authentication for secured endpoints. Credentials must be provided in the Authorization header.

## Common Operations

All services support these common CRUD operations:
- **ListAsync**: Retrieve a list of items with pagination
- **GetAsync**: Retrieve a specific item by ID
- **CreateAsync**: Create a new item
- **UpdateAsync**: Update an existing item
- **RemoveAsync**: Delete an item by ID

## Available Services

### 1. Student Service
**Endpoint**: `/soap/student`
**Interface**: `IStudentService`
**Data Source**: Student application forms, admission records, and enrollment data
**Detailed Data Flow**:
- Personal information from student applications
- Academic details from enrollment processes
- Contact information from self-service updates
- Verification status from administrative processes

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve students with pagination
- `GetAsync(int student_id)` - Retrieve a specific student by ID
- `CreateAsync(Student item)` - Create a new student record
- `UpdateAsync(int student_id, Student item)` - Update an existing student record
- `RemoveAsync(int student_id)` - Remove a student record by ID

### 2. Department Service
**Endpoint**: `/soap/department`
**Interface**: `IDepartmentService`
**Data Source**: Institutional organizational structure and administrative planning system
**Detailed Data Flow**:
- Department names from institutional structure
- Descriptions from administrative documentation
- Leadership assignments from HR processes
- Organizational changes from administrative updates

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve departments with pagination
- `GetAsync(int dept_id)` - Retrieve a specific department by ID
- `CreateAsync(Department item)` - Create a new department record
- `UpdateAsync(int dept_id, Department item)` - Update an existing department record
- `RemoveAsync(int dept_id)` - Remove a department record by ID

### 3. Course Service
**Endpoint**: `/soap/course`
**Interface**: `ICourseService`
**Data Source**: Academic planning and curriculum development system
**Detailed Data Flow**:
- Course names and codes from curriculum planning
- Descriptions from academic program documentation
- Department associations from curriculum mapping
- Duration and fee information from academic planning

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve courses with pagination
- `GetAsync(int course_id)` - Retrieve a specific course by ID
- `CreateAsync(Course item)` - Create a new course record
- `UpdateAsync(int course_id, Course item)` - Update an existing course record
- `RemoveAsync(int course_id)` - Remove a course record by ID

### 4. Role Service
**Endpoint**: `/soap/role`
**Interface**: `IRoleService`
**Data Source**: System administration and security configuration
**Detailed Data Flow**:
- Role names from system design
- Descriptions from security policy documentation
- Permission sets from access control planning
- Role assignments from user management processes

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve roles with pagination
- `GetAsync(int role_id)` - Retrieve a specific role by ID
- `CreateAsync(Role item)` - Create a new role record
- `UpdateAsync(int role_id, Role item)` - Update an existing role record
- `RemoveAsync(int role_id)` - Remove a role record by ID

### 5. User Service
**Endpoint**: `/soap/user`
**Interface**: `IUserService`
**Data Source**: User registration forms and account management system
**Detailed Data Flow**:
- Personal information from user registration
- Authentication credentials from account creation
- Account status from administrative management
- Profile updates from user self-service

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve users with pagination
- `GetAsync(int user_id)` - Retrieve a specific user by ID
- `CreateAsync(User item)` - Create a new user record
- `UpdateAsync(int user_id, User item)` - Update an existing user record
- `RemoveAsync(int user_id)` - Remove a user record by ID

### 6. Subject Service
**Endpoint**: `/soap/subject`
**Interface**: `ISubjectService`
**Data Source**: Academic curriculum and course catalog system
**Detailed Data Flow**:
- Subject names from curriculum development
- Course associations from academic mapping
- Credit hours from academic planning
- Subject codes from cataloging system

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve subjects with pagination
- `GetAsync(int subject_code)` - Retrieve a specific subject by ID
- `CreateAsync(Subject item)` - Create a new subject record
- `UpdateAsync(int subject_code, Subject item)` - Update an existing subject record
- `RemoveAsync(int subject_code)` - Remove a subject record by ID

### 7. Guardian Service
**Endpoint**: `/soap/guardian`
**Interface**: `IGuardianService`
**Data Source**: Student application forms and family information system
**Detailed Data Flow**:
- Guardian names from student applications
- Relationship information from family data
- Contact details from application forms
- Address information from residential data

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve guardians with pagination
- `GetAsync(int guardian_id)` - Retrieve a specific guardian by ID
- `CreateAsync(Guardian item)` - Create a new guardian record
- `UpdateAsync(int guardian_id, Guardian item)` - Update an existing guardian record
- `RemoveAsync(int guardian_id)` - Remove a guardian record by ID

### 8. Admission Service
**Endpoint**: `/soap/admission`
**Interface**: `IAdmissionService`
**Data Source**: Admission form data submitted by prospective students
**Detailed Data Flow**:
- Personal information from admission applications
- Academic interests from course selections
- Contact information from application forms
- Application status from administrative processing

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve admissions with pagination
- `GetAsync(int admission_id)` - Retrieve a specific admission by ID
- `CreateAsync(Admission item)` - Create a new admission record
- `UpdateAsync(int admission_id, Admission item)` - Update an existing admission record
- `RemoveAsync(int admission_id)` - Remove an admission record by ID

### 9. Hostel Service
**Endpoint**: `/soap/hostel`
**Interface**: `IHostelService`
**Data Source**: Administrative data about hostel facilities managed by the institution
**Detailed Data Flow**:
- Hostel names from facility management
- Facility types from administrative classification
- Capacity information from infrastructure data
- Location details from campus planning

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve hostels with pagination
- `GetAsync(int hostel_id)` - Retrieve a specific hostel by ID
- `CreateAsync(Hostel item)` - Create a new hostel record
- `UpdateAsync(int hostel_id, Hostel item)` - Update an existing hostel record
- `RemoveAsync(int hostel_id)` - Remove a hostel record by ID

### 10. Room Service
**Endpoint**: `/soap/room`
**Interface**: `IRoomService`
**Data Source**: Administrative data about hostel room facilities
**Detailed Data Flow**:
- Room numbers from facility management
- Hostel associations from room assignments
- Capacity information from room specifications
- Occupancy status from allocation tracking

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve rooms with pagination
- `GetAsync(int room_id)` - Retrieve a specific room by ID
- `CreateAsync(Room item)` - Create a new room record
- `UpdateAsync(int room_id, Room item)` - Update an existing room record
- `RemoveAsync(int room_id)` - Remove a room record by ID

### 11. Hostel Allocation Service
**Endpoint**: `/soap/hostelallocation`
**Interface**: `IHostelAllocationService`
**Data Source**: Hostel management and student accommodation system
**Detailed Data Flow**:
- Student assignments from accommodation requests
- Room allocations from housing assignments
- Time periods from academic calendar
- Status updates from administrative processes

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve hostel allocations with pagination
- `GetAsync(int allocation_id)` - Retrieve a specific hostel allocation by ID
- `CreateAsync(HostelAllocation item)` - Create a new hostel allocation record
- `UpdateAsync(int allocation_id, HostelAllocation item)` - Update an existing hostel allocation record
- `RemoveAsync(int allocation_id)` - Remove a hostel allocation record by ID

### 12. Fees Service
**Endpoint**: `/soap/fees`
**Interface**: `IFeesService`
**Data Source**: Fee structure defined by the institution and payment processing system
**Detailed Data Flow**:
- Fee types from institutional policies
- Amounts from fee structures
- Due dates from payment schedules
- Payment status from transaction processing
- Payment modes from student selections

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve fees records with pagination
- `GetAsync(int fee_id)` - Retrieve a specific fees record by ID
- `CreateAsync(Fees item)` - Create a new fees record
- `UpdateAsync(int fee_id, Fees item)` - Update an existing fees record
- `RemoveAsync(int fee_id)` - Remove a fees record by ID

### 13. Library Service
**Endpoint**: `/soap/library`
**Interface**: `ILibraryService`
**Data Source**: Library acquisition and cataloging system
**Detailed Data Flow**:
- Book titles from acquisitions
- Author information from cataloging
- Shelf locations from library organization
- ISBN numbers from publisher data
- Copy counts from inventory management

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve library items with pagination
- `GetAsync(int book_id)` - Retrieve a specific library item by ID
- `CreateAsync(Library item)` - Create a new library item record
- `UpdateAsync(int book_id, Library item)` - Update an existing library item record
- `RemoveAsync(int book_id)` - Remove a library item record by ID

### 14. Book Issue Service
**Endpoint**: `/soap/bookissue`
**Interface**: `IBookIssueService`
**Data Source**: Library circulation system and student borrowing records
**Detailed Data Flow**:
- Book assignments from lending transactions
- Student borrowers from library cards
- Issue dates from transaction timestamps
- Return dates from lending policies
- Status updates from circulation tracking

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve book issues with pagination
- `GetAsync(int issue_id)` - Retrieve a specific book issue by ID
- `CreateAsync(BookIssue item)` - Create a new book issue record
- `UpdateAsync(int issue_id, BookIssue item)` - Update an existing book issue record
- `RemoveAsync(int issue_id)` - Remove a book issue record by ID

### 15. Exam Service
**Endpoint**: `/soap/exam`
**Interface**: `IExamService`
**Data Source**: Academic examination scheduling system
**Detailed Data Flow**:
- Exam dates from academic calendar
- Subject associations from curriculum mapping
- Assessment types from examination policies
- Maximum marks from grading schemes
- Creator information from faculty assignments

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve exams with pagination
- `GetAsync(int exam_id)` - Retrieve a specific exam by ID
- `CreateAsync(Exam item)` - Create a new exam record
- `UpdateAsync(int exam_id, Exam item)` - Update an existing exam record
- `RemoveAsync(int exam_id)` - Remove an exam record by ID

### 16. Result Service
**Endpoint**: `/soap/result`
**Interface**: `IResultService`
**Data Source**: Examination results processing and academic records
**Detailed Data Flow**:
- Exam associations from result entry
- Student identifiers from enrollment data
- Marks from evaluation processes
- Grades from grading algorithms
- Result status from administrative review

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve results with pagination
- `GetAsync(int result_id)` - Retrieve a specific result by ID
- `CreateAsync(Result item)` - Create a new result record
- `UpdateAsync(int result_id, Result item)` - Update an existing result record
- `RemoveAsync(int result_id)` - Remove a result record by ID

### 17. User Role Service
**Endpoint**: `/soap/userrole`
**Interface**: `IUserRoleService`
**Data Source**: System administration and user management system
**Detailed Data Flow**:
- User assignments from account management
- Role associations from access control
- Assignment dates from administrative actions
- Status information from permission management

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve user roles with pagination
- `GetAsync(int user_role_id)` - Retrieve a specific user role by ID
- `CreateAsync(UserRole item)` - Create a new user role record
- `UpdateAsync(int user_role_id, UserRole item)` - Update an existing user role record
- `RemoveAsync(int user_role_id)` - Remove a user role record by ID

### 18. Contact Details Service
**Endpoint**: `/soap/contactdetails`
**Interface**: `IContactDetailsService`
**Data Source**: User profile management and contact information system
**Detailed Data Flow**:
- Contact types from communication preferences
- Contact values from user input
- Primary indicators from user preferences
- User associations from profile management

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve contact details with pagination
- `GetAsync(int contact_id)` - Retrieve specific contact details by ID
- `CreateAsync(ContactDetails item)` - Create new contact details record
- `UpdateAsync(int contact_id, ContactDetails item)` - Update existing contact details record
- `RemoveAsync(int contact_id)` - Remove contact details record by ID

### 19. Faculty Service
**Endpoint**: `/soap/faculty`
**Interface**: `IFacultyService`
**Data Source**: Human resources and faculty hiring system
**Detailed Data Flow**:
- Personal information from hiring records
- Contact details from employment forms
- Department assignments from organizational structure
- Employment status from HR management

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve faculty members with pagination
- `GetAsync(int faculty_id)` - Retrieve a specific faculty member by ID
- `CreateAsync(Faculty item)` - Create a new faculty member record
- `UpdateAsync(int faculty_id, Faculty item)` - Update an existing faculty member record
- `RemoveAsync(int faculty_id)` - Remove a faculty member record by ID

### 20. Enrollment Service
**Endpoint**: `/soap/enrollment`
**Interface**: `IEnrollmentService`
**Data Source**: Student enrollment and academic registration system
**Detailed Data Flow**:
- Student associations from registration
- Course assignments from academic selection
- Enrollment dates from registration periods
- Status information from academic progress

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve enrollments with pagination
- `GetAsync(int enrollment_id)` - Retrieve a specific enrollment by ID
- `CreateAsync(Enrollment item)` - Create a new enrollment record
- `UpdateAsync(int enrollment_id, Enrollment item)` - Update an existing enrollment record
- `RemoveAsync(int enrollment_id)` - Remove an enrollment record by ID

### 21. Attendance Service
**Endpoint**: `/soap/attendance`
**Interface**: `IAttendanceService`
**Data Source**: Academic attendance tracking system
**Detailed Data Flow**:
- Student identifiers from class rosters
- Course associations from academic schedules
- Attendance dates from class sessions
- Presence indicators from tracking systems

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve attendance records with pagination
- `GetAsync(int attendance_id)` - Retrieve a specific attendance record by ID
- `CreateAsync(Attendance item)` - Create a new attendance record
- `UpdateAsync(int attendance_id, Attendance item)` - Update an existing attendance record
- `RemoveAsync(int attendance_id)` - Remove an attendance record by ID

### 22. Payment Service
**Endpoint**: `/soap/payment`
**Interface**: `IPaymentService`
**Data Source**: Financial transaction processing system
**Detailed Data Flow**:
- Student associations from account linking
- Payment amounts from transaction data
- Payment dates from processing timestamps
- Status information from transaction completion
- Payment modes from user selections

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve payments with pagination
- `GetAsync(int payment_id)` - Retrieve a specific payment by ID
- `CreateAsync(Payment item)` - Create a new payment record
- `UpdateAsync(int payment_id, Payment item)` - Update an existing payment record
- `RemoveAsync(int payment_id)` - Remove a payment record by ID

## Generic CRUD Service
**Endpoint**: `/soap`
**Interface**: `IGenericCrud`
**Data Source**: Various database tables with restricted access for security
**Detailed Data Flow**:
- Table-specific data from various sources
- Access controlled through security permissions
- Operations logged for audit purposes
- Data integrity maintained through constraints

A generic service that provides CRUD operations for any table in the database. This service is restricted to a predefined list of allowed tables for security purposes.

## Model Documentation

All models in the system are documented with XML comments that appear in the Swagger documentation, including:
- Field descriptions
- Data types
- Required fields
- Example values
- Data source information

This comprehensive documentation allows developers to understand and use the API effectively without needing to examine the source code.

## Accessing Documentation

After running the application, navigate to:
```
http://localhost:<port>/swagger
```

The Swagger UI will display all available SOAP endpoints with:
- Detailed operation descriptions
- Request/response schemas
- Authentication requirements
- Interactive testing capabilities
- Data source information
- Example values
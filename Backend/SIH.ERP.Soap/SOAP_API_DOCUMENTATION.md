# SIH ERP SOAP API Documentation

## Overview

This document provides comprehensive documentation for all SOAP services available in the SIH ERP system. Each service supports standard CRUD operations (List, Get, Create, Update, Remove) with consistent method signatures.

## Available SOAP Services

### 1. Student Service
**Endpoint**: `/soap/student`
**Interface**: `IStudentService`
**Description**: Manages student records and information

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve students with pagination
- `GetAsync(int student_id)` - Retrieve a specific student by ID
- `CreateAsync(Student item)` - Create a new student record
- `UpdateAsync(int student_id, Student item)` - Update an existing student record
- `RemoveAsync(int student_id)` - Remove a student record by ID

### 2. Department Service
**Endpoint**: `/soap/department`
**Interface**: `IDepartmentService`
**Description**: Manages academic departments

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve departments with pagination
- `GetAsync(int dept_id)` - Retrieve a specific department by ID
- `CreateAsync(Department item)` - Create a new department record
- `UpdateAsync(int dept_id, Department item)` - Update an existing department record
- `RemoveAsync(int dept_id)` - Remove a department record by ID

### 3. Course Service
**Endpoint**: `/soap/course`
**Interface**: `ICourseService`
**Description**: Manages academic courses

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve courses with pagination
- `GetAsync(int course_id)` - Retrieve a specific course by ID
- `CreateAsync(Course item)` - Create a new course record
- `UpdateAsync(int course_id, Course item)` - Update an existing course record
- `RemoveAsync(int course_id)` - Remove a course record by ID

### 4. Subject Service
**Endpoint**: `/soap/subject`
**Interface**: `ISubjectService`
**Description**: Manages academic subjects

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve subjects with pagination
- `GetAsync(int subject_id)` - Retrieve a specific subject by ID
- `CreateAsync(Subject item)` - Create a new subject record
- `UpdateAsync(int subject_id, Subject item)` - Update an existing subject record
- `RemoveAsync(int subject_id)` - Remove a subject record by ID

### 5. Role Service
**Endpoint**: `/soap/role`
**Interface**: `IRoleService`
**Description**: Manages user roles and permissions

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve roles with pagination
- `GetAsync(int role_id)` - Retrieve a specific role by ID
- `CreateAsync(Role item)` - Create a new role record
- `UpdateAsync(int role_id, Role item)` - Update an existing role record
- `RemoveAsync(int role_id)` - Remove a role record by ID

### 6. User Service
**Endpoint**: `/soap/user`
**Interface**: `IUserService`
**Description**: Manages user accounts

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve users with pagination
- `GetAsync(int user_id)` - Retrieve a specific user by ID
- `CreateAsync(User item)` - Create a new user record
- `UpdateAsync(int user_id, User item)` - Update an existing user record
- `RemoveAsync(int user_id)` - Remove a user record by ID

### 7. Guardian Service
**Endpoint**: `/soap/guardian`
**Interface**: `IGuardianService`
**Description**: Manages student guardian information

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve guardians with pagination
- `GetAsync(int guardian_id)` - Retrieve a specific guardian by ID
- `CreateAsync(Guardian item)` - Create a new guardian record
- `UpdateAsync(int guardian_id, Guardian item)` - Update an existing guardian record
- `RemoveAsync(int guardian_id)` - Remove a guardian record by ID

### 8. Admission Service
**Endpoint**: `/soap/admission`
**Interface**: `IAdmissionService`
**Description**: Manages student admission processes

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve admissions with pagination
- `GetAsync(int admission_id)` - Retrieve a specific admission by ID
- `CreateAsync(Admission item)` - Create a new admission record
- `UpdateAsync(int admission_id, Admission item)` - Update an existing admission record
- `RemoveAsync(int admission_id)` - Remove an admission record by ID

### 9. Hostel Service
**Endpoint**: `/soap/hostel`
**Interface**: `IHostelService`
**Description**: Manages hostel facilities

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve hostels with pagination
- `GetAsync(int hostel_id)` - Retrieve a specific hostel by ID
- `CreateAsync(Hostel item)` - Create a new hostel record
- `UpdateAsync(int hostel_id, Hostel item)` - Update an existing hostel record
- `RemoveAsync(int hostel_id)` - Remove a hostel record by ID

### 10. Room Service
**Endpoint**: `/soap/room`
**Interface**: `IRoomService`
**Description**: Manages hostel rooms

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve rooms with pagination
- `GetAsync(int room_id)` - Retrieve a specific room by ID
- `CreateAsync(Room item)` - Create a new room record
- `UpdateAsync(int room_id, Room item)` - Update an existing room record
- `RemoveAsync(int room_id)` - Remove a room record by ID

### 11. Hostel Allocation Service
**Endpoint**: `/soap/hostelallocation`
**Interface**: `IHostelAllocationService`
**Description**: Manages hostel allocations for students

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve hostel allocations with pagination
- `GetAsync(int allocation_id)` - Retrieve a specific hostel allocation by ID
- `CreateAsync(HostelAllocation item)` - Create a new hostel allocation record
- `UpdateAsync(int allocation_id, HostelAllocation item)` - Update an existing hostel allocation record
- `RemoveAsync(int allocation_id)` - Remove a hostel allocation record by ID

### 12. Fees Service
**Endpoint**: `/soap/fees`
**Interface**: `IFeesService`
**Description**: Manages fee structures and calculations

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve fees records with pagination
- `GetAsync(int fee_id)` - Retrieve a specific fees record by ID
- `CreateAsync(Fees item)` - Create a new fees record
- `UpdateAsync(int fee_id, Fees item)` - Update an existing fees record
- `RemoveAsync(int fee_id)` - Remove a fees record by ID

### 13. Library Service
**Endpoint**: `/soap/library`
**Interface**: `ILibraryService`
**Description**: Manages library books and resources

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve library items with pagination
- `GetAsync(int book_id)` - Retrieve a specific library item by ID
- `CreateAsync(Library item)` - Create a new library item record
- `UpdateAsync(int book_id, Library item)` - Update an existing library item record
- `RemoveAsync(int book_id)` - Remove a library item record by ID

### 14. Book Issue Service
**Endpoint**: `/soap/bookissue`
**Interface**: `IBookIssueService`
**Description**: Manages library book issues and returns

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve book issues with pagination
- `GetAsync(int issue_id)` - Retrieve a specific book issue by ID
- `CreateAsync(BookIssue item)` - Create a new book issue record
- `UpdateAsync(int issue_id, BookIssue item)` - Update an existing book issue record
- `RemoveAsync(int issue_id)` - Remove a book issue record by ID

### 15. Exam Service
**Endpoint**: `/soap/exam`
**Interface**: `IExamService`
**Description**: Manages examination schedules and information

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve exams with pagination
- `GetAsync(int exam_id)` - Retrieve a specific exam by ID
- `CreateAsync(Exam item)` - Create a new exam record
- `UpdateAsync(int exam_id, Exam item)` - Update an existing exam record
- `RemoveAsync(int exam_id)` - Remove an exam record by ID

### 16. Result Service
**Endpoint**: `/soap/result`
**Interface**: `IResultService`
**Description**: Manages examination results

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve results with pagination
- `GetAsync(int result_id)` - Retrieve a specific result by ID
- `CreateAsync(Result item)` - Create a new result record
- `UpdateAsync(int result_id, Result item)` - Update an existing result record
- `RemoveAsync(int result_id)` - Remove a result record by ID

### 17. User Role Service
**Endpoint**: `/soap/userrole`
**Interface**: `IUserRoleService`
**Description**: Manages user-role associations

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve user roles with pagination
- `GetAsync(int user_role_id)` - Retrieve a specific user role by ID
- `CreateAsync(UserRole item)` - Create a new user role record
- `UpdateAsync(int user_role_id, UserRole item)` - Update an existing user role record
- `RemoveAsync(int user_role_id)` - Remove a user role record by ID

### 18. Contact Details Service
**Endpoint**: `/soap/contactdetails`
**Interface**: `IContactDetailsService`
**Description**: Manages contact information

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve contact details with pagination
- `GetAsync(int contact_id)` - Retrieve a specific contact detail by ID
- `CreateAsync(ContactDetails item)` - Create a new contact detail record
- `UpdateAsync(int contact_id, ContactDetails item)` - Update an existing contact detail record
- `RemoveAsync(int contact_id)` - Remove a contact detail record by ID

### 19. Faculty Service
**Endpoint**: `/soap/faculty`
**Interface**: `IFacultyService`
**Description**: Manages faculty members

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve faculty members with pagination
- `GetAsync(int faculty_id)` - Retrieve a specific faculty member by ID
- `CreateAsync(Faculty item)` - Create a new faculty member record
- `UpdateAsync(int faculty_id, Faculty item)` - Update an existing faculty member record
- `RemoveAsync(int faculty_id)` - Remove a faculty member record by ID

### 20. Enrollment Service
**Endpoint**: `/soap/enrollment`
**Interface**: `IEnrollmentService`
**Description**: Manages student course enrollments

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve enrollments with pagination
- `GetAsync(int enrollment_id)` - Retrieve a specific enrollment by ID
- `CreateAsync(Enrollment item)` - Create a new enrollment record
- `UpdateAsync(int enrollment_id, Enrollment item)` - Update an existing enrollment record
- `RemoveAsync(int enrollment_id)` - Remove an enrollment record by ID

### 21. Attendance Service
**Endpoint**: `/soap/attendance`
**Interface**: `IAttendanceService`
**Description**: Manages student attendance records

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve attendance records with pagination
- `GetAsync(int attendance_id)` - Retrieve a specific attendance record by ID
- `CreateAsync(Attendance item)` - Create a new attendance record
- `UpdateAsync(int attendance_id, Attendance item)` - Update an existing attendance record
- `RemoveAsync(int attendance_id)` - Remove an attendance record by ID

### 22. Payment Service
**Endpoint**: `/soap/payment`
**Interface**: `IPaymentService`
**Description**: Manages payment transactions

Operations:
- `ListAsync(int limit = 100, int offset = 0)` - Retrieve payments with pagination
- `GetAsync(int payment_id)` - Retrieve a specific payment by ID
- `CreateAsync(Payment item)` - Create a new payment record
- `UpdateAsync(int payment_id, Payment item)` - Update an existing payment record
- `RemoveAsync(int payment_id)` - Remove a payment record by ID

### 23. Generic CRUD Service
**Endpoint**: `/soap`
**Interface**: `IGenericCrud`
**Description**: Provides generic CRUD operations for all tables (restricted access)

Operations:
- `ListAsync(string table, int limit = 100, int offset = 0)` - Retrieve records from a specified table
- `GetAsync(string table, string primaryKey, string id)` - Retrieve a specific record by ID from a specified table
- `CreateAsync(string table, Dictionary<string, object?> item)` - Create a new record in a specified table
- `UpdateAsync(string table, string primaryKey, string id, Dictionary<string, object?> item)` - Update an existing record in a specified table
- `RemoveAsync(string table, string primaryKey, string id)` - Remove a record by ID from a specified table

## Using the SOAP Services

To interact with these SOAP services, you'll need to:

1. Create a SOAP client that points to the appropriate endpoint
2. Use the service contract interface to call the desired operations
3. Provide proper authentication credentials where required

## Authentication

All services require Basic Authentication. Provide credentials in the Authorization header of your SOAP requests.

## Data Models

Each service works with specific data models that are documented in the XML comments of the service contracts and model classes. Refer to the source code for detailed information about each model's properties and validation rules.

## Error Handling

All services follow consistent error handling patterns using SOAP Faults. Check the service contract definitions for specific fault types that may be thrown by each operation.
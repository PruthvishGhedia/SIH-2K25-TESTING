export const ENTITY_IDS = {
  student: 'student_id',
  course: 'course_id',
  department: 'dept_id',
  user: 'user_id',
  role: 'role_id',
  userrole: 'userrole_id',
  fees: 'fee_id',
  payment: 'payment_id',
  exam: 'exam_id',
  result: 'result_id',
  guardian: 'guardian_id',
  admission: 'admission_id',
  hostel: 'hostel_id',
  room: 'room_id',
  hostelallocation: 'allocation_id',
  library: 'book_id',
  book: 'book_id',
  bookissue: 'issue_id',
  faculty: 'faculty_id',
  enrollment: 'enrollment_id',
  attendance: 'attendance_id',
  contactdetails: 'contact_id',
};

export const ENTITY_LABELS = {
  student: 'Student',
  course: 'Course',
  department: 'Department',
  user: 'User',
  role: 'Role',
  userrole: 'User Role',
  fees: 'Fees',
  payment: 'Payment',
  exam: 'Exam',
  result: 'Result',
  guardian: 'Guardian',
  admission: 'Admission',
  hostel: 'Hostel',
  room: 'Room',
  hostelallocation: 'Hostel Allocation',
  library: 'Library Book',
  book: 'Book',
  bookissue: 'Book Issue',
  faculty: 'Faculty',
  enrollment: 'Enrollment',
  attendance: 'Attendance',
  contactdetails: 'Contact Details',
};

export const PAGE_SIZES = [10, 25, 50];

export const DEFAULT_SORT = { key: 'created_at', order: 'desc' };

// API Configuration
export const API_BASE_URL = 'http://localhost:5000'; // Update with your backend URL

// SOAP Endpoints
export const SOAP_ENDPOINTS = {
  STUDENT: '/soap/student',
  COURSE: '/soap/course',
  DEPARTMENT: '/soap/department',
  USER: '/soap/user',
  FEES: '/soap/fees',
  EXAM: '/soap/exam',
  GUARDIAN: '/soap/guardian',
  ADMISSION: '/soap/admission',
  HOSTEL: '/soap/hostel',
  ROOM: '/soap/room',
  HOSTEL_ALLOCATION: '/soap/hostelallocation',
  LIBRARY: '/soap/library',
  BOOK_ISSUE: '/soap/bookissue',
  RESULT: '/soap/result',
  USER_ROLE: '/soap/userrole',
  CONTACT_DETAILS: '/soap/contactdetails'
};

// Entity Models
export const ENTITY_MODELS = {
  STUDENT: ['student_id', 'first_name', 'last_name', 'dob', 'email', 'department_id', 'course_id', 'guardian_id'],
  COURSE: ['course_id', 'course_name', 'department_id'],
  DEPARTMENT: ['dept_id', 'dept_name'],
  USER: ['user_id', 'username', 'email', 'role_id', 'password'],
  FEES: ['fees_id', 'student_id', 'amount', 'status', 'due_date'],
  EXAM: ['exam_id', 'course_id', 'exam_date', 'max_marks'],
  GUARDIAN: ['guardian_id', 'first_name', 'last_name', 'email', 'phone', 'student_id'],
  ADMISSION: ['admission_id', 'student_id', 'admission_date', 'status'],
  HOSTEL: ['hostel_id', 'hostel_name', 'capacity'],
  ROOM: ['room_id', 'hostel_id', 'room_number', 'capacity'],
  HOSTEL_ALLOCATION: ['allocation_id', 'student_id', 'room_id', 'start_date', 'end_date'],
  LIBRARY: ['book_id', 'title', 'author', 'publisher', 'isbn', 'available_copies'],
  BOOK_ISSUE: ['issue_id', 'book_id', 'student_id', 'issue_date', 'return_date', 'status'],
  RESULT: ['result_id', 'student_id', 'exam_id', 'marks_obtained', 'grade'],
  USER_ROLE: ['userrole_id', 'user_id', 'role_id'],
  CONTACT_DETAILS: ['contact_id', 'user_id', 'phone', 'address', 'city', 'state', 'zip']
};

// Navigation Menu Items
export const MENU_ITEMS = [
  { path: '/dashboard', label: 'Dashboard', icon: 'LayoutDashboard' },
  { path: '/students', label: 'Students', icon: 'Users' },
  { path: '/courses', label: 'Courses', icon: 'BookOpen' },
  { path: '/departments', label: 'Departments', icon: 'Building' },
  { path: '/users', label: 'Users', icon: 'User' },
  { path: '/fees', label: 'Fees', icon: 'CreditCard' },
  { path: '/exams', label: 'Exams', icon: 'FileText' },
  { path: '/guardians', label: 'Guardians', icon: 'UserCheck' },
  { path: '/admissions', label: 'Admissions', icon: 'UserPlus' },
  { path: '/hostels', label: 'Hostels', icon: 'Home' },
  { path: '/rooms', label: 'Rooms', icon: 'Bed' },
  { path: '/hostel-allocations', label: 'Hostel Allocations', icon: 'MapPin' },
  { path: '/library', label: 'Library', icon: 'Library' },
  { path: '/book-issues', label: 'Book Issues', icon: 'BookMarked' },
  { path: '/results', label: 'Results', icon: 'Award' },
  { path: '/user-roles', label: 'User Roles', icon: 'Shield' },
  { path: '/contact-details', label: 'Contact Details', icon: 'Phone' }
];

// Status Options
export const STATUS_OPTIONS = {
  FEES: ['Pending', 'Paid', 'Overdue'],
  ADMISSION: ['Applied', 'Approved', 'Rejected', 'Enrolled'],
  BOOK_ISSUE: ['Issued', 'Returned', 'Overdue']
};

// Pagination
export const PAGINATION = {
  DEFAULT_PAGE_SIZE: 10,
  PAGE_SIZE_OPTIONS: [10, 25, 50, 100]
};
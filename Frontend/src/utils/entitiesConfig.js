// Entity configuration for SOAP endpoints
export const entitiesConfig = {
  student: {
    name: 'Student',
    endpoint: '/student',
    idField: 'student_id',
    fields: [
      { key: 'student_id', label: 'ID', type: 'number', required: true },
      { key: 'first_name', label: 'First Name', type: 'text', required: true },
      { key: 'last_name', label: 'Last Name', type: 'text', required: true },
      { key: 'email', label: 'Email', type: 'email', required: true },
      { key: 'phone', label: 'Phone', type: 'text' },
      { key: 'date_of_birth', label: 'Date of Birth', type: 'date' },
      { key: 'address', label: 'Address', type: 'text' },
      { key: 'enrollment_date', label: 'Enrollment Date', type: 'date' },
    ],
    listFields: ['student_id', 'first_name', 'last_name', 'email', 'phone']
  },
  course: {
    name: 'Course',
    endpoint: '/course',
    idField: 'course_id',
    fields: [
      { key: 'course_id', label: 'ID', type: 'number', required: true },
      { key: 'course_name', label: 'Course Name', type: 'text', required: true },
      { key: 'course_code', label: 'Course Code', type: 'text', required: true },
      { key: 'credits', label: 'Credits', type: 'number' },
      { key: 'description', label: 'Description', type: 'textarea' },
    ],
    listFields: ['course_id', 'course_name', 'course_code', 'credits']
  },
  department: {
    name: 'Department',
    endpoint: '/department',
    idField: 'department_id',
    fields: [
      { key: 'department_id', label: 'ID', type: 'number', required: true },
      { key: 'department_name', label: 'Department Name', type: 'text', required: true },
      { key: 'department_code', label: 'Department Code', type: 'text', required: true },
      { key: 'head_of_department', label: 'Head of Department', type: 'text' },
    ],
    listFields: ['department_id', 'department_name', 'department_code']
  },
  user: {
    name: 'User',
    endpoint: '/user',
    idField: 'user_id',
    fields: [
      { key: 'user_id', label: 'ID', type: 'number', required: true },
      { key: 'username', label: 'Username', type: 'text', required: true },
      { key: 'email', label: 'Email', type: 'email', required: true },
      { key: 'password', label: 'Password', type: 'password', required: true },
      { key: 'role_id', label: 'Role ID', type: 'number', required: true },
    ],
    listFields: ['user_id', 'username', 'email', 'role_id']
  },
  fees: {
    name: 'Fees',
    endpoint: '/fees',
    idField: 'fee_id',
    fields: [
      { key: 'fee_id', label: 'ID', type: 'number', required: true },
      { key: 'student_id', label: 'Student ID', type: 'number', required: true },
      { key: 'amount', label: 'Amount', type: 'number', required: true },
      { key: 'due_date', label: 'Due Date', type: 'date' },
      { key: 'paid_date', label: 'Paid Date', type: 'date' },
      { key: 'status', label: 'Status', type: 'select', options: ['Pending', 'Paid', 'Overdue'] },
    ],
    listFields: ['fee_id', 'student_id', 'amount', 'status']
  },
  exam: {
    name: 'Exam',
    endpoint: '/exam',
    idField: 'exam_id',
    fields: [
      { key: 'exam_id', label: 'ID', type: 'number', required: true },
      { key: 'exam_name', label: 'Exam Name', type: 'text', required: true },
      { key: 'course_id', label: 'Course ID', type: 'number', required: true },
      { key: 'exam_date', label: 'Exam Date', type: 'date' },
      { key: 'duration', label: 'Duration (minutes)', type: 'number' },
      { key: 'max_marks', label: 'Maximum Marks', type: 'number' },
    ],
    listFields: ['exam_id', 'exam_name', 'course_id', 'exam_date']
  },
  guardian: {
    name: 'Guardian',
    endpoint: '/guardian',
    idField: 'guardian_id',
    fields: [
      { key: 'guardian_id', label: 'ID', type: 'number', required: true },
      { key: 'student_id', label: 'Student ID', type: 'number', required: true },
      { key: 'first_name', label: 'First Name', type: 'text', required: true },
      { key: 'last_name', label: 'Last Name', type: 'text', required: true },
      { key: 'relationship', label: 'Relationship', type: 'text' },
      { key: 'phone', label: 'Phone', type: 'text' },
      { key: 'email', label: 'Email', type: 'email' },
    ],
    listFields: ['guardian_id', 'student_id', 'first_name', 'last_name', 'relationship']
  },
  admission: {
    name: 'Admission',
    endpoint: '/admission',
    idField: 'admission_id',
    fields: [
      { key: 'admission_id', label: 'ID', type: 'number', required: true },
      { key: 'student_id', label: 'Student ID', type: 'number', required: true },
      { key: 'course_id', label: 'Course ID', type: 'number', required: true },
      { key: 'admission_date', label: 'Admission Date', type: 'date' },
      { key: 'status', label: 'Status', type: 'select', options: ['Pending', 'Accepted', 'Rejected'] },
    ],
    listFields: ['admission_id', 'student_id', 'course_id', 'admission_date', 'status']
  },
  hostel: {
    name: 'Hostel',
    endpoint: '/hostel',
    idField: 'hostel_id',
    fields: [
      { key: 'hostel_id', label: 'ID', type: 'number', required: true },
      { key: 'hostel_name', label: 'Hostel Name', type: 'text', required: true },
      { key: 'capacity', label: 'Capacity', type: 'number' },
      { key: 'warden', label: 'Warden', type: 'text' },
    ],
    listFields: ['hostel_id', 'hostel_name', 'capacity', 'warden']
  },
  library: {
    name: 'Library',
    endpoint: '/library',
    idField: 'book_id',
    fields: [
      { key: 'book_id', label: 'ID', type: 'number', required: true },
      { key: 'title', label: 'Title', type: 'text', required: true },
      { key: 'author', label: 'Author', type: 'text', required: true },
      { key: 'isbn', label: 'ISBN', type: 'text' },
      { key: 'publisher', label: 'Publisher', type: 'text' },
      { key: 'available_copies', label: 'Available Copies', type: 'number' },
    ],
    listFields: ['book_id', 'title', 'author', 'isbn', 'available_copies']
  },
  faculty: {
    name: 'Faculty',
    endpoint: '/faculty',
    idField: 'faculty_id',
    fields: [
      { key: 'faculty_id', label: 'ID', type: 'number', required: true },
      { key: 'first_name', label: 'First Name', type: 'text', required: true },
      { key: 'last_name', label: 'Last Name', type: 'text', required: true },
      { key: 'email', label: 'Email', type: 'email' },
      { key: 'department_id', label: 'Department ID', type: 'number' },
      { key: 'hire_date', label: 'Hire Date', type: 'date' },
    ],
    listFields: ['faculty_id', 'first_name', 'last_name', 'email', 'department_id']
  },
  enrollment: {
    name: 'Enrollment',
    endpoint: '/enrollment',
    idField: 'enrollment_id',
    fields: [
      { key: 'enrollment_id', label: 'ID', type: 'number', required: true },
      { key: 'student_id', label: 'Student ID', type: 'number', required: true },
      { key: 'course_id', label: 'Course ID', type: 'number', required: true },
      { key: 'enrollment_date', label: 'Enrollment Date', type: 'date' },
      { key: 'status', label: 'Status', type: 'select', options: ['Active', 'Completed', 'Dropped'] },
    ],
    listFields: ['enrollment_id', 'student_id', 'course_id', 'enrollment_date', 'status']
  },
  attendance: {
    name: 'Attendance',
    endpoint: '/attendance',
    idField: 'attendance_id',
    fields: [
      { key: 'attendance_id', label: 'ID', type: 'number', required: true },
      { key: 'student_id', label: 'Student ID', type: 'number', required: true },
      { key: 'course_id', label: 'Course ID', type: 'number', required: true },
      { key: 'date', label: 'Date', type: 'date', required: true },
      { key: 'status', label: 'Status', type: 'select', options: ['Present', 'Absent', 'Late'] },
    ],
    listFields: ['attendance_id', 'student_id', 'course_id', 'date', 'status']
  },
  payment: {
    name: 'Payment',
    endpoint: '/payment',
    idField: 'payment_id',
    fields: [
      { key: 'payment_id', label: 'ID', type: 'number', required: true },
      { key: 'student_id', label: 'Student ID', type: 'number', required: true },
      { key: 'amount', label: 'Amount', type: 'number', required: true },
      { key: 'payment_date', label: 'Payment Date', type: 'date' },
      { key: 'payment_method', label: 'Payment Method', type: 'select', options: ['Cash', 'Card', 'Bank Transfer'] },
      { key: 'description', label: 'Description', type: 'textarea' },
    ],
    listFields: ['payment_id', 'student_id', 'amount', 'payment_date', 'payment_method']
  }
};

export default entitiesConfig;
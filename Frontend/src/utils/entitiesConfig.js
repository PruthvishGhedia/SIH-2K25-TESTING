// Configuration describing columns and form fields for each entity
// This powers the generic EntityList and EntityForm components

export const entities = {
  student: {
    label: 'Student',
    id: 'student_id',
    listColumns: [
      { key: 'student_id', label: 'ID', sortable: true },
      { key: 'first_name', label: 'First Name', sortable: true },
      { key: 'last_name', label: 'Last Name', sortable: true },
      { key: 'email', label: 'Email', sortable: true },
      { key: 'department_id', label: 'Dept', sortable: true },
      { key: 'course_id', label: 'Course', sortable: true },
    ],
    formFields: [
      { name: 'first_name', label: 'First Name', type: 'text', required: true },
      { name: 'last_name', label: 'Last Name', type: 'text', required: true },
      { name: 'email', label: 'Email', type: 'email', required: true },
      { name: 'dob', label: 'Date of Birth', type: 'date' },
      { name: 'department_id', label: 'Department', type: 'number' },
      { name: 'course_id', label: 'Course', type: 'number' },
      { name: 'verified', label: 'Verified', type: 'checkbox' },
    ],
  },
  faculty: {
    label: 'Faculty',
    id: 'faculty_id',
    listColumns: [
      { key: 'faculty_id', label: 'ID', sortable: true },
      { key: 'first_name', label: 'First Name', sortable: true },
      { key: 'last_name', label: 'Last Name', sortable: true },
      { key: 'email', label: 'Email', sortable: true },
      { key: 'department_id', label: 'Dept', sortable: true },
      { key: 'is_active', label: 'Active', sortable: true },
    ],
    formFields: [
      { name: 'first_name', label: 'First Name', type: 'text', required: true },
      { name: 'last_name', label: 'Last Name', type: 'text', required: true },
      { name: 'email', label: 'Email', type: 'email', required: true },
      { name: 'phone', label: 'Phone', type: 'text' },
      { name: 'department_id', label: 'Department', type: 'number' },
      { name: 'is_active', label: 'Active', type: 'checkbox' },
    ],
  },
  course: {
    label: 'Course', id: 'course_id',
    listColumns: [
      { key: 'course_id', label: 'ID', sortable: true },
      { key: 'course_name', label: 'Name', sortable: true },
      { key: 'dept_id', label: 'Dept', sortable: true },
    ],
    formFields: [
      { name: 'course_name', label: 'Course Name', type: 'text', required: true },
      { name: 'dept_id', label: 'Department', type: 'number' },
      { name: 'course_code', label: 'Course Code', type: 'text' },
    ],
  },
  department: {
    label: 'Department', id: 'dept_id',
    listColumns: [ { key: 'dept_id', label: 'ID', sortable: true }, { key: 'dept_name', label: 'Name', sortable: true } ],
    formFields: [ { name: 'dept_name', label: 'Department Name', type: 'text', required: true } ],
  },
  payment: {
    label: 'Payment', id: 'payment_id',
    listColumns: [ { key: 'payment_id', label: 'ID', sortable: true }, { key: 'student_id', label: 'Student', sortable: true }, { key: 'amount', label: 'Amount', sortable: true }, { key: 'status', label: 'Status', sortable: true } ],
    formFields: [
      { name: 'student_id', label: 'Student', type: 'number', required: true },
      { name: 'amount', label: 'Amount', type: 'number', required: true },
      { name: 'payment_date', label: 'Payment Date', type: 'date', required: true },
      { name: 'status', label: 'Status', type: 'select', options: ['paid','pending','failed'] },
      { name: 'mode', label: 'Mode', type: 'select', options: ['online','cash','card'] },
    ],
  },
  exam: {
    label: 'Exam', id: 'exam_id',
    listColumns: [ { key: 'exam_id', label: 'ID', sortable: true }, { key: 'dept_id', label: 'Dept', sortable: true }, { key: 'subject_code', label: 'Subject', sortable: true }, { key: 'exam_date', label: 'Date', sortable: true } ],
    formFields: [
      { name: 'dept_id', label: 'Department', type: 'number' },
      { name: 'subject_code', label: 'Subject Code', type: 'number' },
      { name: 'exam_date', label: 'Exam Date', type: 'date' },
      { name: 'assessment_type', label: 'Type', type: 'text' },
      { name: 'max_marks', label: 'Max Marks', type: 'number' },
    ],
  },
  enrollment: {
    label: 'Enrollment', id: 'enrollment_id',
    listColumns: [ { key: 'enrollment_id', label: 'ID', sortable: true }, { key: 'student_id', label: 'Student', sortable: true }, { key: 'course_id', label: 'Course', sortable: true }, { key: 'status', label: 'Status', sortable: true } ],
    formFields: [
      { name: 'student_id', label: 'Student', type: 'number', required: true },
      { name: 'course_id', label: 'Course', type: 'number', required: true },
      { name: 'enrollment_date', label: 'Enrollment Date', type: 'date', required: true },
      { name: 'status', label: 'Status', type: 'select', options: ['active','completed','dropped'] },
    ],
  },
  attendance: {
    label: 'Attendance', id: 'attendance_id',
    listColumns: [ { key: 'attendance_id', label: 'ID', sortable: true }, { key: 'student_id', label: 'Student', sortable: true }, { key: 'course_id', label: 'Course', sortable: true }, { key: 'date', label: 'Date', sortable: true }, { key: 'present', label: 'Present', sortable: true } ],
    formFields: [
      { name: 'student_id', label: 'Student', type: 'number', required: true },
      { name: 'course_id', label: 'Course', type: 'number', required: true },
      { name: 'date', label: 'Date', type: 'date', required: true },
      { name: 'present', label: 'Present', type: 'checkbox' },
    ],
  },
};


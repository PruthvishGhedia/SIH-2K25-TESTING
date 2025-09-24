// Backend model interfaces
export interface Student {
  student_id: number;
  user_id?: number;
  dept_id?: number;
  course_id?: number;
  admission_date?: string;
  verified?: boolean;
}

export interface Course {
  course_id: number;
  dept_id?: number;
  course_name: string;
  course_code?: string;
}

export interface Department {
  dept_id: number;
  dept_name: string;
}

export interface User {
  user_id: number;
  full_name?: string;
  email?: string;
  dob?: string;
  password_hash?: string;
  is_active?: boolean;
  created_at?: string;
  updated_at?: string;
}

export interface Fees {
  fee_id: number;
  student_id?: number;
  fee_type?: string;
  amount?: number;
  due_date?: string;
  paid_on?: string;
  payment_status?: string;
  payment_mode?: string;
  created_at?: string;
  updated_at?: string;
}

export interface Exam {
  exam_id: number;
  dept_id?: number;
  subject_code?: number;
  exam_date?: string;
  assessment_type?: string;
  max_marks?: number;
  created_by?: number;
  created_at?: string;
  updated_at?: string;
}

// Form interfaces for creating/updating entities
export interface CreateStudentRequest {
  user_id?: number;
  dept_id?: number;
  course_id?: number;
  admission_date?: string;
  verified?: boolean;
}

export interface CreateCourseRequest {
  dept_id?: number;
  course_name: string;
  course_code?: string;
}

export interface CreateDepartmentRequest {
  dept_name: string;
}

export interface CreateUserRequest {
  full_name?: string;
  email?: string;
  dob?: string;
  password_hash?: string;
  is_active?: boolean;
}

export interface CreateFeesRequest {
  student_id?: number;
  fee_type?: string;
  amount?: number;
  due_date?: string;
  paid_on?: string;
  payment_status?: string;
  payment_mode?: string;
}

export interface CreateExamRequest {
  dept_id?: number;
  subject_code?: number;
  exam_date?: string;
  assessment_type?: string;
  max_marks?: number;
  created_by?: number;
}

// API Response types
export interface ApiResponse<T> {
  success: boolean;
  data?: T;
  error?: string;
  message?: string;
}

export interface PaginatedResponse<T> {
  items: T[];
  total: number;
  page: number;
  pageSize: number;
  hasNext: boolean;
  hasPrevious: boolean;
}

// Table and form states
export interface TableState {
  page: number;
  pageSize: number;
  sortBy?: string;
  sortOrder?: 'asc' | 'desc';
  searchTerm?: string;
}

export interface FormState<T> {
  data: T;
  errors: Record<string, string>;
  isSubmitting: boolean;
  isDirty: boolean;
}

// Navigation
export interface NavigationItem {
  label: string;
  path: string;
  icon?: string;
  children?: NavigationItem[];
}
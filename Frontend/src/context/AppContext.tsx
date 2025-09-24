import React, { createContext, useContext, useReducer, ReactNode } from 'react';
import { Student, Course, Department, User, Fees, Exam } from '../types';

// State interface
interface AppState {
  // Data states
  students: Student[];
  courses: Course[];
  departments: Department[];
  users: User[];
  fees: Fees[];
  exams: Exam[];
  
  // Loading states
  isLoading: boolean;
  error: string | null;
  
  // UI states
  selectedStudent: Student | null;
  selectedCourse: Course | null;
  selectedDepartment: Department | null;
  selectedUser: User | null;
  selectedFee: Fees | null;
  selectedExam: Exam | null;
  
  // Form states
  showStudentForm: boolean;
  showCourseForm: boolean;
  showDepartmentForm: boolean;
  showUserForm: boolean;
  showFeesForm: boolean;
  showExamForm: boolean;
}

// Action types
type AppAction =
  | { type: 'SET_LOADING'; payload: boolean }
  | { type: 'SET_ERROR'; payload: string | null }
  | { type: 'SET_STUDENTS'; payload: Student[] }
  | { type: 'ADD_STUDENT'; payload: Student }
  | { type: 'UPDATE_STUDENT'; payload: Student }
  | { type: 'REMOVE_STUDENT'; payload: number }
  | { type: 'SET_COURSES'; payload: Course[] }
  | { type: 'ADD_COURSE'; payload: Course }
  | { type: 'UPDATE_COURSE'; payload: Course }
  | { type: 'REMOVE_COURSE'; payload: number }
  | { type: 'SET_DEPARTMENTS'; payload: Department[] }
  | { type: 'ADD_DEPARTMENT'; payload: Department }
  | { type: 'UPDATE_DEPARTMENT'; payload: Department }
  | { type: 'REMOVE_DEPARTMENT'; payload: number }
  | { type: 'SET_USERS'; payload: User[] }
  | { type: 'ADD_USER'; payload: User }
  | { type: 'UPDATE_USER'; payload: User }
  | { type: 'REMOVE_USER'; payload: number }
  | { type: 'SET_FEES'; payload: Fees[] }
  | { type: 'ADD_FEE'; payload: Fees }
  | { type: 'UPDATE_FEE'; payload: Fees }
  | { type: 'REMOVE_FEE'; payload: number }
  | { type: 'SET_EXAMS'; payload: Exam[] }
  | { type: 'ADD_EXAM'; payload: Exam }
  | { type: 'UPDATE_EXAM'; payload: Exam }
  | { type: 'REMOVE_EXAM'; payload: number }
  | { type: 'SET_SELECTED_STUDENT'; payload: Student | null }
  | { type: 'SET_SELECTED_COURSE'; payload: Course | null }
  | { type: 'SET_SELECTED_DEPARTMENT'; payload: Department | null }
  | { type: 'SET_SELECTED_USER'; payload: User | null }
  | { type: 'SET_SELECTED_FEE'; payload: Fees | null }
  | { type: 'SET_SELECTED_EXAM'; payload: Exam | null }
  | { type: 'TOGGLE_STUDENT_FORM'; payload?: boolean }
  | { type: 'TOGGLE_COURSE_FORM'; payload?: boolean }
  | { type: 'TOGGLE_DEPARTMENT_FORM'; payload?: boolean }
  | { type: 'TOGGLE_USER_FORM'; payload?: boolean }
  | { type: 'TOGGLE_FEES_FORM'; payload?: boolean }
  | { type: 'TOGGLE_EXAM_FORM'; payload?: boolean };

// Initial state
const initialState: AppState = {
  students: [],
  courses: [],
  departments: [],
  users: [],
  fees: [],
  exams: [],
  isLoading: false,
  error: null,
  selectedStudent: null,
  selectedCourse: null,
  selectedDepartment: null,
  selectedUser: null,
  selectedFee: null,
  selectedExam: null,
  showStudentForm: false,
  showCourseForm: false,
  showDepartmentForm: false,
  showUserForm: false,
  showFeesForm: false,
  showExamForm: false,
};

// Reducer
function appReducer(state: AppState, action: AppAction): AppState {
  switch (action.type) {
    case 'SET_LOADING':
      return { ...state, isLoading: action.payload };
    
    case 'SET_ERROR':
      return { ...state, error: action.payload };
    
    // Students
    case 'SET_STUDENTS':
      return { ...state, students: action.payload };
    case 'ADD_STUDENT':
      return { ...state, students: [...state.students, action.payload] };
    case 'UPDATE_STUDENT':
      return {
        ...state,
        students: state.students.map(s => s.student_id === action.payload.student_id ? action.payload : s)
      };
    case 'REMOVE_STUDENT':
      return {
        ...state,
        students: state.students.filter(s => s.student_id !== action.payload)
      };
    
    // Courses
    case 'SET_COURSES':
      return { ...state, courses: action.payload };
    case 'ADD_COURSE':
      return { ...state, courses: [...state.courses, action.payload] };
    case 'UPDATE_COURSE':
      return {
        ...state,
        courses: state.courses.map(c => c.course_id === action.payload.course_id ? action.payload : c)
      };
    case 'REMOVE_COURSE':
      return {
        ...state,
        courses: state.courses.filter(c => c.course_id !== action.payload)
      };
    
    // Departments
    case 'SET_DEPARTMENTS':
      return { ...state, departments: action.payload };
    case 'ADD_DEPARTMENT':
      return { ...state, departments: [...state.departments, action.payload] };
    case 'UPDATE_DEPARTMENT':
      return {
        ...state,
        departments: state.departments.map(d => d.dept_id === action.payload.dept_id ? action.payload : d)
      };
    case 'REMOVE_DEPARTMENT':
      return {
        ...state,
        departments: state.departments.filter(d => d.dept_id !== action.payload)
      };
    
    // Users
    case 'SET_USERS':
      return { ...state, users: action.payload };
    case 'ADD_USER':
      return { ...state, users: [...state.users, action.payload] };
    case 'UPDATE_USER':
      return {
        ...state,
        users: state.users.map(u => u.user_id === action.payload.user_id ? action.payload : u)
      };
    case 'REMOVE_USER':
      return {
        ...state,
        users: state.users.filter(u => u.user_id !== action.payload)
      };
    
    // Fees
    case 'SET_FEES':
      return { ...state, fees: action.payload };
    case 'ADD_FEE':
      return { ...state, fees: [...state.fees, action.payload] };
    case 'UPDATE_FEE':
      return {
        ...state,
        fees: state.fees.map(f => f.fee_id === action.payload.fee_id ? action.payload : f)
      };
    case 'REMOVE_FEE':
      return {
        ...state,
        fees: state.fees.filter(f => f.fee_id !== action.payload)
      };
    
    // Exams
    case 'SET_EXAMS':
      return { ...state, exams: action.payload };
    case 'ADD_EXAM':
      return { ...state, exams: [...state.exams, action.payload] };
    case 'UPDATE_EXAM':
      return {
        ...state,
        exams: state.exams.map(e => e.exam_id === action.payload.exam_id ? action.payload : e)
      };
    case 'REMOVE_EXAM':
      return {
        ...state,
        exams: state.exams.filter(e => e.exam_id !== action.payload)
      };
    
    // Selected items
    case 'SET_SELECTED_STUDENT':
      return { ...state, selectedStudent: action.payload };
    case 'SET_SELECTED_COURSE':
      return { ...state, selectedCourse: action.payload };
    case 'SET_SELECTED_DEPARTMENT':
      return { ...state, selectedDepartment: action.payload };
    case 'SET_SELECTED_USER':
      return { ...state, selectedUser: action.payload };
    case 'SET_SELECTED_FEE':
      return { ...state, selectedFee: action.payload };
    case 'SET_SELECTED_EXAM':
      return { ...state, selectedExam: action.payload };
    
    // Form visibility
    case 'TOGGLE_STUDENT_FORM':
      return { ...state, showStudentForm: action.payload !== undefined ? action.payload : !state.showStudentForm };
    case 'TOGGLE_COURSE_FORM':
      return { ...state, showCourseForm: action.payload !== undefined ? action.payload : !state.showCourseForm };
    case 'TOGGLE_DEPARTMENT_FORM':
      return { ...state, showDepartmentForm: action.payload !== undefined ? action.payload : !state.showDepartmentForm };
    case 'TOGGLE_USER_FORM':
      return { ...state, showUserForm: action.payload !== undefined ? action.payload : !state.showUserForm };
    case 'TOGGLE_FEES_FORM':
      return { ...state, showFeesForm: action.payload !== undefined ? action.payload : !state.showFeesForm };
    case 'TOGGLE_EXAM_FORM':
      return { ...state, showExamForm: action.payload !== undefined ? action.payload : !state.showExamForm };
    
    default:
      return state;
  }
}

// Context
const AppContext = createContext<{
  state: AppState;
  dispatch: React.Dispatch<AppAction>;
} | null>(null);

// Provider component
interface AppProviderProps {
  children: ReactNode;
}

export function AppProvider({ children }: AppProviderProps) {
  const [state, dispatch] = useReducer(appReducer, initialState);

  return (
    <AppContext.Provider value={{ state, dispatch }}>
      {children}
    </AppContext.Provider>
  );
}

// Custom hook to use the context
export function useApp() {
  const context = useContext(AppContext);
  if (!context) {
    throw new Error('useApp must be used within an AppProvider');
  }
  return context;
}

// Action creators
export const appActions = {
  setLoading: (loading: boolean) => ({ type: 'SET_LOADING' as const, payload: loading }),
  setError: (error: string | null) => ({ type: 'SET_ERROR' as const, payload: error }),
  
  // Students
  setStudents: (students: Student[]) => ({ type: 'SET_STUDENTS' as const, payload: students }),
  addStudent: (student: Student) => ({ type: 'ADD_STUDENT' as const, payload: student }),
  updateStudent: (student: Student) => ({ type: 'UPDATE_STUDENT' as const, payload: student }),
  removeStudent: (id: number) => ({ type: 'REMOVE_STUDENT' as const, payload: id }),
  
  // Courses
  setCourses: (courses: Course[]) => ({ type: 'SET_COURSES' as const, payload: courses }),
  addCourse: (course: Course) => ({ type: 'ADD_COURSE' as const, payload: course }),
  updateCourse: (course: Course) => ({ type: 'UPDATE_COURSE' as const, payload: course }),
  removeCourse: (id: number) => ({ type: 'REMOVE_COURSE' as const, payload: id }),
  
  // Departments
  setDepartments: (departments: Department[]) => ({ type: 'SET_DEPARTMENTS' as const, payload: departments }),
  addDepartment: (department: Department) => ({ type: 'ADD_DEPARTMENT' as const, payload: department }),
  updateDepartment: (department: Department) => ({ type: 'UPDATE_DEPARTMENT' as const, payload: department }),
  removeDepartment: (id: number) => ({ type: 'REMOVE_DEPARTMENT' as const, payload: id }),
  
  // Users
  setUsers: (users: User[]) => ({ type: 'SET_USERS' as const, payload: users }),
  addUser: (user: User) => ({ type: 'ADD_USER' as const, payload: user }),
  updateUser: (user: User) => ({ type: 'UPDATE_USER' as const, payload: user }),
  removeUser: (id: number) => ({ type: 'REMOVE_USER' as const, payload: id }),
  
  // Fees
  setFees: (fees: Fees[]) => ({ type: 'SET_FEES' as const, payload: fees }),
  addFee: (fee: Fees) => ({ type: 'ADD_FEE' as const, payload: fee }),
  updateFee: (fee: Fees) => ({ type: 'UPDATE_FEE' as const, payload: fee }),
  removeFee: (id: number) => ({ type: 'REMOVE_FEE' as const, payload: id }),
  
  // Exams
  setExams: (exams: Exam[]) => ({ type: 'SET_EXAMS' as const, payload: exams }),
  addExam: (exam: Exam) => ({ type: 'ADD_EXAM' as const, payload: exam }),
  updateExam: (exam: Exam) => ({ type: 'UPDATE_EXAM' as const, payload: exam }),
  removeExam: (id: number) => ({ type: 'REMOVE_EXAM' as const, payload: id }),
  
  // Selected items
  setSelectedStudent: (student: Student | null) => ({ type: 'SET_SELECTED_STUDENT' as const, payload: student }),
  setSelectedCourse: (course: Course | null) => ({ type: 'SET_SELECTED_COURSE' as const, payload: course }),
  setSelectedDepartment: (department: Department | null) => ({ type: 'SET_SELECTED_DEPARTMENT' as const, payload: department }),
  setSelectedUser: (user: User | null) => ({ type: 'SET_SELECTED_USER' as const, payload: user }),
  setSelectedFee: (fee: Fees | null) => ({ type: 'SET_SELECTED_FEE' as const, payload: fee }),
  setSelectedExam: (exam: Exam | null) => ({ type: 'SET_SELECTED_EXAM' as const, payload: exam }),
  
  // Form visibility
  toggleStudentForm: (show?: boolean) => ({ type: 'TOGGLE_STUDENT_FORM' as const, payload: show }),
  toggleCourseForm: (show?: boolean) => ({ type: 'TOGGLE_COURSE_FORM' as const, payload: show }),
  toggleDepartmentForm: (show?: boolean) => ({ type: 'TOGGLE_DEPARTMENT_FORM' as const, payload: show }),
  toggleUserForm: (show?: boolean) => ({ type: 'TOGGLE_USER_FORM' as const, payload: show }),
  toggleFeesForm: (show?: boolean) => ({ type: 'TOGGLE_FEES_FORM' as const, payload: show }),
  toggleExamForm: (show?: boolean) => ({ type: 'TOGGLE_EXAM_FORM' as const, payload: show }),
};
import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { AppProvider } from './context/AppContext';
import { Layout } from './components/layout/Layout';

// Pages
import { Dashboard } from './pages/Dashboard';
import { StudentList } from './pages/StudentList';
import { StudentForm } from './pages/StudentForm';
import { CourseList } from './pages/CourseList';
import { CourseForm } from './pages/CourseForm';
import { DepartmentList } from './pages/DepartmentList';
import { DepartmentForm } from './pages/DepartmentForm';
import { UserList } from './pages/UserList';
import { FeesList } from './pages/FeesList';
import { ExamList } from './pages/ExamList';

export function App() {
  return (
    <AppProvider>
      <Router>
        <Layout>
          <Routes>
            {/* Dashboard */}
            <Route path="/" element={<Dashboard />} />
            
            {/* Students */}
            <Route path="/students" element={<StudentList />} />
            <Route path="/students/new" element={<StudentForm />} />
            <Route path="/students/:id" element={<StudentForm />} />
            <Route path="/students/:id/edit" element={<StudentForm />} />
            
            {/* Courses */}
            <Route path="/courses" element={<CourseList />} />
            <Route path="/courses/new" element={<CourseForm />} />
            <Route path="/courses/:id" element={<CourseForm />} />
            <Route path="/courses/:id/edit" element={<CourseForm />} />
            
            {/* Departments */}
            <Route path="/departments" element={<DepartmentList />} />
            <Route path="/departments/new" element={<DepartmentForm />} />
            <Route path="/departments/:id" element={<DepartmentForm />} />
            <Route path="/departments/:id/edit" element={<DepartmentForm />} />
            
            {/* Users */}
            <Route path="/users" element={<UserList />} />
            
            {/* Fees */}
            <Route path="/fees" element={<FeesList />} />
            
            {/* Exams */}
            <Route path="/exams" element={<ExamList />} />
            
            {/* Catch all - redirect to dashboard */}
            <Route path="*" element={<Navigate to="/" replace />} />
          </Routes>
        </Layout>
      </Router>
    </AppProvider>
  );
}
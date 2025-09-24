import React, { useEffect } from 'react';
import { Users, BookOpen, Building2, User, CreditCard, FileText, TrendingUp } from 'lucide-react';
import { Card } from '../components/ui/Card';
import { useApp, appActions } from '../context/AppContext';
import { studentService, courseService, departmentService, userService, feesService, examService } from '../services/soapClient';

export function Dashboard() {
  const { state, dispatch } = useApp();

  useEffect(() => {
    const loadDashboardData = async () => {
      dispatch(appActions.setLoading(true));
      try {
        // Load all data in parallel
        const [students, courses, departments, users, fees, exams] = await Promise.all([
          studentService.list(10, 0),
          courseService.list(10, 0),
          departmentService.list(10, 0),
          userService.list(10, 0),
          feesService.list(10, 0),
          examService.list(10, 0),
        ]);

        dispatch(appActions.setStudents(students));
        dispatch(appActions.setCourses(courses));
        dispatch(appActions.setDepartments(departments));
        dispatch(appActions.setUsers(users));
        dispatch(appActions.setFees(fees));
        dispatch(appActions.setExams(exams));
      } catch (error) {
        dispatch(appActions.setError('Failed to load dashboard data'));
        console.error('Dashboard data loading error:', error);
      } finally {
        dispatch(appActions.setLoading(false));
      }
    };

    loadDashboardData();
  }, [dispatch]);

  const stats = [
    {
      name: 'Total Students',
      value: state.students.length,
      icon: Users,
      change: '+12%',
      changeType: 'positive' as const,
    },
    {
      name: 'Total Courses',
      value: state.courses.length,
      icon: BookOpen,
      change: '+8%',
      changeType: 'positive' as const,
    },
    {
      name: 'Departments',
      value: state.departments.length,
      icon: Building2,
      change: '+2%',
      changeType: 'positive' as const,
    },
    {
      name: 'Users',
      value: state.users.length,
      icon: User,
      change: '+5%',
      changeType: 'positive' as const,
    },
    {
      name: 'Fee Records',
      value: state.fees.length,
      icon: CreditCard,
      change: '+15%',
      changeType: 'positive' as const,
    },
    {
      name: 'Exams',
      value: state.exams.length,
      icon: FileText,
      change: '+3%',
      changeType: 'positive' as const,
    },
  ];

  if (state.isLoading) {
    return (
      <div className="flex items-center justify-center h-64">
        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-primary-600"></div>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      <div>
        <h1 className="text-2xl font-bold text-gray-900">Dashboard</h1>
        <p className="mt-1 text-sm text-gray-500">
          Welcome to the SIH ERP System. Here's an overview of your data.
        </p>
      </div>

      {state.error && (
        <div className="bg-red-50 border border-red-200 rounded-md p-4">
          <div className="flex">
            <div className="ml-3">
              <h3 className="text-sm font-medium text-red-800">Error</h3>
              <div className="mt-2 text-sm text-red-700">{state.error}</div>
            </div>
          </div>
        </div>
      )}

      {/* Stats Grid */}
      <div className="grid grid-cols-1 gap-5 sm:grid-cols-2 lg:grid-cols-3">
        {stats.map((stat) => (
          <Card key={stat.name} className="relative overflow-hidden">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <stat.icon className="h-8 w-8 text-gray-400" />
              </div>
              <div className="ml-5 w-0 flex-1">
                <dl>
                  <dt className="text-sm font-medium text-gray-500 truncate">
                    {stat.name}
                  </dt>
                  <dd className="flex items-baseline">
                    <div className="text-2xl font-semibold text-gray-900">
                      {stat.value}
                    </div>
                    <div className={`ml-2 flex items-baseline text-sm font-semibold ${
                      stat.changeType === 'positive' ? 'text-green-600' : 'text-red-600'
                    }`}>
                      <TrendingUp className="self-center flex-shrink-0 h-4 w-4" />
                      <span className="sr-only">
                        {stat.changeType === 'positive' ? 'Increased' : 'Decreased'} by
                      </span>
                      {stat.change}
                    </div>
                  </dd>
                </dl>
              </div>
            </div>
          </Card>
        ))}
      </div>

      {/* Recent Activity */}
      <div className="grid grid-cols-1 gap-6 lg:grid-cols-2">
        {/* Recent Students */}
        <Card>
          <div className="px-4 py-5 sm:p-6">
            <h3 className="text-lg leading-6 font-medium text-gray-900 mb-4">
              Recent Students
            </h3>
            {state.students.length > 0 ? (
              <div className="space-y-3">
                {state.students.slice(0, 5).map((student) => (
                  <div key={student.student_id} className="flex items-center justify-between">
                    <div className="flex items-center">
                      <div className="h-8 w-8 bg-gray-300 rounded-full flex items-center justify-center">
                        <Users className="h-4 w-4 text-gray-600" />
                      </div>
                      <div className="ml-3">
                        <p className="text-sm font-medium text-gray-900">
                          Student ID: {student.student_id}
                        </p>
                        <p className="text-sm text-gray-500">
                          Department: {student.dept_id || 'N/A'}
                        </p>
                      </div>
                    </div>
                    <div className="text-sm text-gray-500">
                      {student.admission_date ? new Date(student.admission_date).toLocaleDateString() : 'N/A'}
                    </div>
                  </div>
                ))}
              </div>
            ) : (
              <p className="text-gray-500 text-sm">No students found</p>
            )}
          </div>
        </Card>

        {/* Recent Courses */}
        <Card>
          <div className="px-4 py-5 sm:p-6">
            <h3 className="text-lg leading-6 font-medium text-gray-900 mb-4">
              Recent Courses
            </h3>
            {state.courses.length > 0 ? (
              <div className="space-y-3">
                {state.courses.slice(0, 5).map((course) => (
                  <div key={course.course_id} className="flex items-center justify-between">
                    <div className="flex items-center">
                      <div className="h-8 w-8 bg-blue-100 rounded-full flex items-center justify-center">
                        <BookOpen className="h-4 w-4 text-blue-600" />
                      </div>
                      <div className="ml-3">
                        <p className="text-sm font-medium text-gray-900">
                          {course.course_name}
                        </p>
                        <p className="text-sm text-gray-500">
                          Code: {course.course_code || 'N/A'}
                        </p>
                      </div>
                    </div>
                    <div className="text-sm text-gray-500">
                      Dept: {course.dept_id || 'N/A'}
                    </div>
                  </div>
                ))}
              </div>
            ) : (
              <p className="text-gray-500 text-sm">No courses found</p>
            )}
          </div>
        </Card>
      </div>
    </div>
  );
}
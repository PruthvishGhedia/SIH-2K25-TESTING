import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Button } from '../components/ui/Button';
import { Input } from '../components/ui/Input';
import { Select } from '../components/ui/Select';
import { useApp, appActions } from '../context/AppContext';
import { studentService, departmentService, courseService, userService } from '../services/soapClient';
import { CreateStudentRequest, Department, Course, User } from '../types';

export function StudentForm() {
  const navigate = useNavigate();
  const { id } = useParams<{ id: string }>();
  const isEdit = Boolean(id);
  const { state, dispatch } = useApp();

  const [formData, setFormData] = useState<CreateStudentRequest>({
    user_id: undefined,
    dept_id: undefined,
    course_id: undefined,
    admission_date: '',
    verified: false,
  });

  const [errors, setErrors] = useState<Record<string, string>>({});
  const [isSubmitting, setIsSubmitting] = useState(false);

  useEffect(() => {
    if (isEdit && id) {
      loadStudent(parseInt(id));
    }
    loadDropdownData();
  }, [id, isEdit]);

  const loadStudent = async (studentId: number) => {
    dispatch(appActions.setLoading(true));
    try {
      const student = await studentService.get(studentId);
      if (student) {
        setFormData({
          user_id: student.user_id,
          dept_id: student.dept_id,
          course_id: student.course_id,
          admission_date: student.admission_date || '',
          verified: student.verified || false,
        });
      }
    } catch (error) {
      dispatch(appActions.setError('Failed to load student data'));
      console.error('Error loading student:', error);
    } finally {
      dispatch(appActions.setLoading(false));
    }
  };

  const loadDropdownData = async () => {
    try {
      const [departments, courses, users] = await Promise.all([
        departmentService.list(100, 0),
        courseService.list(100, 0),
        userService.list(100, 0),
      ]);

      dispatch(appActions.setDepartments(departments));
      dispatch(appActions.setCourses(courses));
      dispatch(appActions.setUsers(users));
    } catch (error) {
      console.error('Error loading dropdown data:', error);
    }
  };

  const validateForm = (): boolean => {
    const newErrors: Record<string, string> = {};

    if (!formData.admission_date) {
      newErrors.admission_date = 'Admission date is required';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!validateForm()) {
      return;
    }

    setIsSubmitting(true);
    try {
      if (isEdit && id) {
        const updatedStudent = await studentService.update(parseInt(id), formData);
        if (updatedStudent) {
          dispatch(appActions.updateStudent(updatedStudent));
        }
      } else {
        const newStudent = await studentService.create(formData);
        dispatch(appActions.addStudent(newStudent));
      }
      navigate('/students');
    } catch (error) {
      dispatch(appActions.setError(`Failed to ${isEdit ? 'update' : 'create'} student`));
      console.error('Error saving student:', error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleInputChange = (field: keyof CreateStudentRequest, value: any) => {
    setFormData(prev => ({
      ...prev,
      [field]: value,
    }));
    
    // Clear error when user starts typing
    if (errors[field]) {
      setErrors(prev => ({
        ...prev,
        [field]: '',
      }));
    }
  };

  const departmentOptions = state.departments.map(dept => ({
    value: dept.dept_id,
    label: dept.dept_name,
  }));

  const courseOptions = state.courses.map(course => ({
    value: course.course_id,
    label: course.course_name,
  }));

  const userOptions = state.users.map(user => ({
    value: user.user_id,
    label: `${user.full_name || 'User'} (${user.email || user.user_id})`,
  }));

  return (
    <div className="max-w-2xl mx-auto">
      <div className="mb-6">
        <h1 className="text-2xl font-bold text-gray-900">
          {isEdit ? 'Edit Student' : 'Add New Student'}
        </h1>
        <p className="mt-1 text-sm text-gray-500">
          {isEdit ? 'Update student information' : 'Fill in the details to add a new student'}
        </p>
      </div>

      {state.error && (
        <div className="mb-6 bg-red-50 border border-red-200 rounded-md p-4">
          <div className="flex">
            <div className="ml-3">
              <h3 className="text-sm font-medium text-red-800">Error</h3>
              <div className="mt-2 text-sm text-red-700">{state.error}</div>
            </div>
          </div>
        </div>
      )}

      <form onSubmit={handleSubmit} className="space-y-6">
        <div className="bg-white shadow rounded-lg p-6">
          <div className="grid grid-cols-1 gap-6 sm:grid-cols-2">
            <Select
              label="User"
              value={formData.user_id || ''}
              onChange={(e) => handleInputChange('user_id', e.target.value ? parseInt(e.target.value) : undefined)}
              options={userOptions}
              placeholder="Select a user"
              error={errors.user_id}
            />

            <Select
              label="Department"
              value={formData.dept_id || ''}
              onChange={(e) => handleInputChange('dept_id', e.target.value ? parseInt(e.target.value) : undefined)}
              options={departmentOptions}
              placeholder="Select a department"
              error={errors.dept_id}
            />

            <Select
              label="Course"
              value={formData.course_id || ''}
              onChange={(e) => handleInputChange('course_id', e.target.value ? parseInt(e.target.value) : undefined)}
              options={courseOptions}
              placeholder="Select a course"
              error={errors.course_id}
            />

            <Input
              label="Admission Date"
              type="date"
              value={formData.admission_date}
              onChange={(e) => handleInputChange('admission_date', e.target.value)}
              error={errors.admission_date}
            />

            <div className="sm:col-span-2">
              <div className="flex items-center">
                <input
                  id="verified"
                  type="checkbox"
                  checked={formData.verified || false}
                  onChange={(e) => handleInputChange('verified', e.target.checked)}
                  className="h-4 w-4 text-primary-600 focus:ring-primary-500 border-gray-300 rounded"
                />
                <label htmlFor="verified" className="ml-2 block text-sm text-gray-900">
                  Verified
                </label>
              </div>
            </div>
          </div>
        </div>

        <div className="flex justify-end space-x-3">
          <Button
            type="button"
            variant="secondary"
            onClick={() => navigate('/students')}
          >
            Cancel
          </Button>
          <Button
            type="submit"
            isLoading={isSubmitting}
            disabled={isSubmitting}
          >
            {isEdit ? 'Update Student' : 'Create Student'}
          </Button>
        </div>
      </form>
    </div>
  );
}
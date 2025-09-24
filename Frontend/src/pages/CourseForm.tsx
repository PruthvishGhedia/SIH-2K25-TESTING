import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Button } from '../components/ui/Button';
import { Input } from '../components/ui/Input';
import { Select } from '../components/ui/Select';
import { useApp, appActions } from '../context/AppContext';
import { courseService, departmentService } from '../services/soapClient';
import { CreateCourseRequest, Department } from '../types';

export function CourseForm() {
  const navigate = useNavigate();
  const { id } = useParams<{ id: string }>();
  const isEdit = Boolean(id);
  const { state, dispatch } = useApp();

  const [formData, setFormData] = useState<CreateCourseRequest>({
    dept_id: undefined,
    course_name: '',
    course_code: '',
  });

  const [errors, setErrors] = useState<Record<string, string>>({});
  const [isSubmitting, setIsSubmitting] = useState(false);

  useEffect(() => {
    if (isEdit && id) {
      loadCourse(parseInt(id));
    }
    loadDepartments();
  }, [id, isEdit]);

  const loadCourse = async (courseId: number) => {
    dispatch(appActions.setLoading(true));
    try {
      const course = await courseService.get(courseId);
      if (course) {
        setFormData({
          dept_id: course.dept_id,
          course_name: course.course_name,
          course_code: course.course_code || '',
        });
      }
    } catch (error) {
      dispatch(appActions.setError('Failed to load course data'));
      console.error('Error loading course:', error);
    } finally {
      dispatch(appActions.setLoading(false));
    }
  };

  const loadDepartments = async () => {
    try {
      const departments = await departmentService.list(100, 0);
      dispatch(appActions.setDepartments(departments));
    } catch (error) {
      console.error('Error loading departments:', error);
    }
  };

  const validateForm = (): boolean => {
    const newErrors: Record<string, string> = {};

    if (!formData.course_name.trim()) {
      newErrors.course_name = 'Course name is required';
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
        const updatedCourse = await courseService.update(parseInt(id), formData);
        if (updatedCourse) {
          dispatch(appActions.updateCourse(updatedCourse));
        }
      } else {
        const newCourse = await courseService.create(formData);
        dispatch(appActions.addCourse(newCourse));
      }
      navigate('/courses');
    } catch (error) {
      dispatch(appActions.setError(`Failed to ${isEdit ? 'update' : 'create'} course`));
      console.error('Error saving course:', error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleInputChange = (field: keyof CreateCourseRequest, value: any) => {
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

  return (
    <div className="max-w-2xl mx-auto">
      <div className="mb-6">
        <h1 className="text-2xl font-bold text-gray-900">
          {isEdit ? 'Edit Course' : 'Add New Course'}
        </h1>
        <p className="mt-1 text-sm text-gray-500">
          {isEdit ? 'Update course information' : 'Fill in the details to add a new course'}
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
          <div className="grid grid-cols-1 gap-6">
            <Input
              label="Course Name"
              value={formData.course_name}
              onChange={(e) => handleInputChange('course_name', e.target.value)}
              placeholder="Enter course name"
              error={errors.course_name}
              required
            />

            <Input
              label="Course Code"
              value={formData.course_code}
              onChange={(e) => handleInputChange('course_code', e.target.value)}
              placeholder="Enter course code (optional)"
              error={errors.course_code}
            />

            <Select
              label="Department"
              value={formData.dept_id || ''}
              onChange={(e) => handleInputChange('dept_id', e.target.value ? parseInt(e.target.value) : undefined)}
              options={departmentOptions}
              placeholder="Select a department (optional)"
              error={errors.dept_id}
            />
          </div>
        </div>

        <div className="flex justify-end space-x-3">
          <Button
            type="button"
            variant="secondary"
            onClick={() => navigate('/courses')}
          >
            Cancel
          </Button>
          <Button
            type="submit"
            isLoading={isSubmitting}
            disabled={isSubmitting}
          >
            {isEdit ? 'Update Course' : 'Create Course'}
          </Button>
        </div>
      </form>
    </div>
  );
}
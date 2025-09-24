import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Button } from '../components/ui/Button';
import { Input } from '../components/ui/Input';
import { useApp, appActions } from '../context/AppContext';
import { departmentService } from '../services/soapClient';
import { CreateDepartmentRequest } from '../types';

export function DepartmentForm() {
  const navigate = useNavigate();
  const { id } = useParams<{ id: string }>();
  const isEdit = Boolean(id);
  const { state, dispatch } = useApp();

  const [formData, setFormData] = useState<CreateDepartmentRequest>({
    dept_name: '',
  });

  const [errors, setErrors] = useState<Record<string, string>>({});
  const [isSubmitting, setIsSubmitting] = useState(false);

  useEffect(() => {
    if (isEdit && id) {
      loadDepartment(parseInt(id));
    }
  }, [id, isEdit]);

  const loadDepartment = async (departmentId: number) => {
    dispatch(appActions.setLoading(true));
    try {
      const department = await departmentService.get(departmentId);
      if (department) {
        setFormData({
          dept_name: department.dept_name,
        });
      }
    } catch (error) {
      dispatch(appActions.setError('Failed to load department data'));
      console.error('Error loading department:', error);
    } finally {
      dispatch(appActions.setLoading(false));
    }
  };

  const validateForm = (): boolean => {
    const newErrors: Record<string, string> = {};

    if (!formData.dept_name.trim()) {
      newErrors.dept_name = 'Department name is required';
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
        const updatedDepartment = await departmentService.update(parseInt(id), formData);
        if (updatedDepartment) {
          dispatch(appActions.updateDepartment(updatedDepartment));
        }
      } else {
        const newDepartment = await departmentService.create(formData);
        dispatch(appActions.addDepartment(newDepartment));
      }
      navigate('/departments');
    } catch (error) {
      dispatch(appActions.setError(`Failed to ${isEdit ? 'update' : 'create'} department`));
      console.error('Error saving department:', error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleInputChange = (field: keyof CreateDepartmentRequest, value: any) => {
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

  return (
    <div className="max-w-2xl mx-auto">
      <div className="mb-6">
        <h1 className="text-2xl font-bold text-gray-900">
          {isEdit ? 'Edit Department' : 'Add New Department'}
        </h1>
        <p className="mt-1 text-sm text-gray-500">
          {isEdit ? 'Update department information' : 'Fill in the details to add a new department'}
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
              label="Department Name"
              value={formData.dept_name}
              onChange={(e) => handleInputChange('dept_name', e.target.value)}
              placeholder="Enter department name"
              error={errors.dept_name}
              required
            />
          </div>
        </div>

        <div className="flex justify-end space-x-3">
          <Button
            type="button"
            variant="secondary"
            onClick={() => navigate('/departments')}
          >
            Cancel
          </Button>
          <Button
            type="submit"
            isLoading={isSubmitting}
            disabled={isSubmitting}
          >
            {isEdit ? 'Update Department' : 'Create Department'}
          </Button>
        </div>
      </form>
    </div>
  );
}
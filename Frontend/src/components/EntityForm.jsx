import React, { useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Save, ArrowLeft } from 'lucide-react';
import Button from './ui/Button';
import Input from './ui/Input';
import Select from './ui/Select';
import LoadingSpinner from './ui/LoadingSpinner';
import ErrorAlert from './ui/ErrorAlert';
import { useForm } from '../hooks/useForm';
import { useFetch, useCrud } from '../hooks/useFetch';
import { useAppContext } from '../context/AppContext';
import { validators } from '../utils/validators';
import { formatters } from '../utils/formatters';
import entitiesConfig from '../utils/entitiesConfig';

const EntityForm = ({ entityName }) => {
  const navigate = useNavigate();
  const { id } = useParams();
  const isEdit = Boolean(id);
  const { actions } = useAppContext();
  
  const entityConfig = entitiesConfig[entityName];

  // Fetch entity data if editing
  const { data: entity, loading: entityLoading, error: entityError } = useFetch(entityName, id);
  
  // CRUD operations
  const { create, update, loading: saveLoading, error: saveError } = useCrud(entityName);

  // Generate validation rules based on entity configuration
  const validationRules = {};
  entityConfig.fields.forEach(field => {
    const rules = [];
    if (field.required) {
      rules.push(validators.required);
    }
    if (field.type === 'email') {
      rules.push(validators.email);
    }
    if (field.type === 'number') {
      rules.push(validators.number);
    }
    if (field.type === 'date') {
      rules.push(validators.date);
    }
    if (field.maxLength) {
      rules.push(validators.maxLength(field.maxLength));
    }
    validationRules[field.key] = rules;
  });

  // Initialize form with default values
  const initialFormValues = {};
  entityConfig.fields.forEach(field => {
    initialFormValues[field.key] = '';
  });

  // Initialize form
  const {
    values,
    errors,
    isSubmitting,
    isValid,
    isDirty,
    handleSubmit,
    setFormValues,
    getFieldProps
  } = useForm(initialFormValues, validationRules);

  // Load entity data into form when editing
  useEffect(() => {
    if (isEdit && entity) {
      const formData = {};
      entityConfig.fields.forEach(field => {
        if (field.key.includes('date')) {
          formData[field.key] = formatters.formatDateForInput(entity[field.key]) || '';
        } else {
          formData[field.key] = entity[field.key] || '';
        }
      });
      setFormValues(formData);
    }
  }, [isEdit, entity, setFormValues]);

  const handleFormSubmit = async (formData) => {
    try {
      let result;
      if (isEdit) {
        result = await update(id, formData);
      } else {
        result = await create(formData);
      }

      if (result.success) {
        actions.showSuccess(
          isEdit ? `${entityConfig.name} updated successfully` : `${entityConfig.name} created successfully`
        );
        navigate(`/${entityName}s`);
      } else {
        actions.showError(result.error || `Failed to save ${entityConfig.name.toLowerCase()}`);
      }
    } catch (error) {
      actions.showError('An unexpected error occurred');
    }
  };

  // Render form field based on type
  const renderFormField = (field) => {
    switch (field.type) {
      case 'select':
        return (
          <Select
            key={field.key}
            label={field.label}
            required={field.required}
            options={field.options ? field.options.map(option => ({
              value: option,
              label: option
            })) : []}
            placeholder={`Select ${field.label.toLowerCase()}`}
            {...getFieldProps(field.key)}
          />
        );
      case 'textarea':
        return (
          <textarea
            key={field.key}
            className="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-primary-500 focus:border-primary-500"
            rows="4"
            placeholder={field.label}
            {...getFieldProps(field.key)}
          />
        );
      default:
        return (
          <Input
            key={field.key}
            label={field.label}
            type={field.type}
            required={field.required}
            {...getFieldProps(field.key)}
          />
        );
    }
  };

  if (entityLoading) {
    return (
      <div className="flex items-center justify-center h-64">
        <LoadingSpinner size="lg" text={`Loading ${entityConfig.name.toLowerCase()}...`} />
      </div>
    );
  }

  return (
    <div className="space-y-6">
      {/* Page Header */}
      <div className="md:flex md:items-center md:justify-between">
        <div className="flex-1 min-w-0">
          <div className="flex items-center space-x-3">
            <Button
              variant="ghost"
              size="sm"
              onClick={() => navigate(`/${entityName}s`)}
            >
              <ArrowLeft className="h-4 w-4" />
            </Button>
            <div>
              <h2 className="text-2xl font-bold leading-7 text-gray-900 sm:text-3xl sm:truncate">
                {isEdit ? `Edit ${entityConfig.name}` : `Add New ${entityConfig.name}`}
              </h2>
              <p className="mt-1 text-sm text-gray-500">
                {isEdit ? `Update ${entityConfig.name.toLowerCase()} information` : `Create a new ${entityConfig.name.toLowerCase()} record`}
              </p>
            </div>
          </div>
        </div>
      </div>

      {/* Error Alerts */}
      {entityError && (
        <ErrorAlert message={entityError} />
      )}
      {saveError && (
        <ErrorAlert message={saveError} />
      )}

      {/* Form */}
      <form onSubmit={(e) => {
        e.preventDefault();
        handleSubmit(handleFormSubmit);
      }}>
        <div className="card p-6 space-y-6">
          {/* Form Fields */}
          <div>
            <h3 className="text-lg font-medium text-gray-900 mb-4">
              {entityConfig.name} Information
            </h3>
            <div className="grid grid-cols-1 gap-6 sm:grid-cols-2">
              {entityConfig.fields.map(field => renderFormField(field))}
            </div>
          </div>

          {/* Form Actions */}
          <div className="flex justify-end space-x-3 pt-6 border-t border-gray-200">
            <Button
              type="button"
              variant="outline"
              onClick={() => navigate(`/${entityName}s`)}
            >
              Cancel
            </Button>
            <Button
              type="submit"
              variant="primary"
              loading={isSubmitting || saveLoading}
              disabled={!isValid || (!isDirty && isEdit)}
            >
              <Save className="h-4 w-4 mr-2" />
              {isEdit ? `Update ${entityConfig.name}` : `Create ${entityConfig.name}`}
            </Button>
          </div>
        </div>
      </form>
    </div>
  );
};

export default EntityForm;
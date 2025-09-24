import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Plus, Edit, Trash2, Eye } from 'lucide-react';
import { Button } from '../components/ui/Button';
import { Table } from '../components/ui/Table';
import { SearchBar } from '../components/ui/SearchBar';
import { Pagination } from '../components/ui/Pagination';
import { Modal } from '../components/ui/Modal';
import { useApp, appActions } from '../context/AppContext';
import { departmentService } from '../services/soapClient';
import { Department } from '../types';

export function DepartmentList() {
  const navigate = useNavigate();
  const { state, dispatch } = useApp();
  const [searchTerm, setSearchTerm] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize] = useState(10);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [departmentToDelete, setDepartmentToDelete] = useState<Department | null>(null);

  useEffect(() => {
    loadDepartments();
  }, [currentPage, pageSize]);

  const loadDepartments = async () => {
    dispatch(appActions.setLoading(true));
    try {
      const departments = await departmentService.list(pageSize, (currentPage - 1) * pageSize);
      dispatch(appActions.setDepartments(departments));
    } catch (error) {
      dispatch(appActions.setError('Failed to load departments'));
      console.error('Error loading departments:', error);
    } finally {
      dispatch(appActions.setLoading(false));
    }
  };

  const handleSearch = (value: string) => {
    setSearchTerm(value);
    setCurrentPage(1);
  };

  const handleDelete = (department: Department) => {
    setDepartmentToDelete(department);
    setShowDeleteModal(true);
  };

  const confirmDelete = async () => {
    if (!departmentToDelete) return;

    try {
      await departmentService.remove(departmentToDelete.dept_id);
      dispatch(appActions.removeDepartment(departmentToDelete.dept_id));
      setShowDeleteModal(false);
      setDepartmentToDelete(null);
    } catch (error) {
      dispatch(appActions.setError('Failed to delete department'));
      console.error('Error deleting department:', error);
    }
  };

  const filteredDepartments = state.departments.filter(department =>
    department.dept_name.toLowerCase().includes(searchTerm.toLowerCase()) ||
    department.dept_id.toString().includes(searchTerm)
  );

  const columns = [
    {
      key: 'dept_id',
      label: 'Department ID',
      sortable: true,
    },
    {
      key: 'dept_name',
      label: 'Department Name',
      sortable: true,
    },
    {
      key: 'actions',
      label: 'Actions',
      render: (_: any, department: Department) => (
        <div className="flex space-x-2">
          <Button
            size="sm"
            variant="outline"
            onClick={() => navigate(`/departments/${department.dept_id}`)}
          >
            <Eye className="h-4 w-4" />
          </Button>
          <Button
            size="sm"
            variant="outline"
            onClick={() => navigate(`/departments/${department.dept_id}/edit`)}
          >
            <Edit className="h-4 w-4" />
          </Button>
          <Button
            size="sm"
            variant="danger"
            onClick={() => handleDelete(department)}
          >
            <Trash2 className="h-4 w-4" />
          </Button>
        </div>
      ),
    },
  ];

  const totalPages = Math.ceil(filteredDepartments.length / pageSize);
  const paginatedDepartments = filteredDepartments.slice(
    (currentPage - 1) * pageSize,
    currentPage * pageSize
  );

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-gray-900">Departments</h1>
          <p className="mt-1 text-sm text-gray-500">
            Manage department records and information
          </p>
        </div>
        <Button onClick={() => navigate('/departments/new')}>
          <Plus className="h-4 w-4 mr-2" />
          Add Department
        </Button>
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

      <div className="bg-white shadow rounded-lg">
        <div className="p-6">
          <div className="mb-4">
            <SearchBar
              value={searchTerm}
              onChange={handleSearch}
              placeholder="Search departments by name or ID..."
            />
          </div>

          <Table
            data={paginatedDepartments}
            columns={columns}
            loading={state.isLoading}
            emptyMessage="No departments found"
          />

          {totalPages > 1 && (
            <div className="mt-6">
              <Pagination
                currentPage={currentPage}
                totalPages={totalPages}
                onPageChange={setCurrentPage}
              />
            </div>
          )}
        </div>
      </div>

      {/* Delete Confirmation Modal */}
      <Modal
        isOpen={showDeleteModal}
        onClose={() => setShowDeleteModal(false)}
        title="Delete Department"
      >
        <div className="space-y-4">
          <p className="text-sm text-gray-500">
            Are you sure you want to delete the department "{departmentToDelete?.dept_name}"? 
            This action cannot be undone.
          </p>
          <div className="flex justify-end space-x-3">
            <Button
              variant="secondary"
              onClick={() => setShowDeleteModal(false)}
            >
              Cancel
            </Button>
            <Button
              variant="danger"
              onClick={confirmDelete}
            >
              Delete
            </Button>
          </div>
        </div>
      </Modal>
    </div>
  );
}
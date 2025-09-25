import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Plus, Edit, Trash2, Eye, Download } from 'lucide-react';
import Table, { TableActions } from './ui/Table';
import Button from './ui/Button';
import SearchBar from './ui/SearchBar';
import Pagination from './ui/Pagination';
import Modal from './ui/Modal';
import LoadingSpinner from './ui/LoadingSpinner';
import ErrorAlert from './ui/ErrorAlert';
import { useFetch, useCrud } from '../hooks/useFetch';
import { usePagination } from '../hooks/usePagination';
import { useAppContext } from '../context/AppContext';
import { formatters } from '../utils/formatters';
import entitiesConfig from '../utils/entitiesConfig';

const EntityList = ({ entityName }) => {
  const { actions } = useAppContext();
  const entityConfig = entitiesConfig[entityName];
  
  const { data: entities, loading, error, refetch } = useFetch(entityName);
  const { remove, loading: deleteLoading } = useCrud(entityName);
  
  const [selectedEntities, setSelectedEntities] = useState([]);
  const [deleteModal, setDeleteModal] = useState({ open: false, entity: null });
  const [bulkDeleteModal, setBulkDeleteModal] = useState(false);

  const {
    data: paginatedEntities,
    currentPage,
    totalPages,
    totalItems,
    pageSize,
    searchTerm,
    handlePageChange,
    handlePageSizeChange,
    handleSearch,
    handleSort,
    sortField,
    sortDirection
  } = usePagination(entities || [], 10);

  // Search fields for the entity
  const searchFields = entityConfig.listFields;

  useEffect(() => {
    handleSearch(searchTerm, searchFields);
  }, [searchTerm]);

  const handleDelete = async (entity) => {
    const result = await remove(entity[entityConfig.idField]);
    if (result.success) {
      actions.showSuccess(`${entityConfig.name} deleted successfully`);
      refetch();
      setDeleteModal({ open: false, entity: null });
    } else {
      actions.showError(result.error || `Failed to delete ${entityConfig.name.toLowerCase()}`);
    }
  };

  const handleBulkDelete = async () => {
    // Implementation for bulk delete
    setBulkDeleteModal(false);
    setSelectedEntities([]);
    actions.showSuccess(`${selectedEntities.length} ${entityConfig.name.toLowerCase()} records deleted successfully`);
  };

  // Generate columns based on entity configuration
  const columns = entityConfig.listFields.map(fieldKey => {
    const fieldConfig = entityConfig.fields.find(f => f.key === fieldKey);
    return {
      key: fieldKey,
      label: fieldConfig?.label || fieldKey,
      sortable: true,
      render: fieldConfig?.render || ((value) => {
        if (fieldKey.includes('date')) {
          return formatters.formatDate(value);
        }
        return value;
      })
    };
  });

  // Add actions column
  columns.push({
    key: 'actions',
    label: 'Actions',
    render: (_, entity) => (
      <TableActions>
        <Button
          variant="ghost"
          size="sm"
          onClick={() => {/* View entity details */}}
        >
          <Eye className="h-4 w-4" />
        </Button>
        <Link to={`/${entityName}s/${entity[entityConfig.idField]}/edit`}>
          <Button variant="ghost" size="sm">
            <Edit className="h-4 w-4" />
          </Button>
        </Link>
        <Button
          variant="ghost"
          size="sm"
          onClick={() => setDeleteModal({ open: true, entity })}
          className="text-red-600 hover:text-red-700"
        >
          <Trash2 className="h-4 w-4" />
        </Button>
      </TableActions>
    )
  });

  if (loading) {
    return (
      <div className="flex items-center justify-center h-64">
        <LoadingSpinner size="lg" text={`Loading ${entityConfig.name.toLowerCase()} records...`} />
      </div>
    );
  }

  return (
    <div className="space-y-6">
      {/* Page Header */}
      <div className="md:flex md:items-center md:justify-between">
        <div className="flex-1 min-w-0">
          <h2 className="text-2xl font-bold leading-7 text-gray-900 sm:text-3xl sm:truncate">
            {entityConfig.name}s
          </h2>
          <p className="mt-1 text-sm text-gray-500">
            Manage {entityConfig.name.toLowerCase()} records and information
          </p>
        </div>
        <div className="mt-4 flex space-x-3 md:mt-0 md:ml-4">
          <Button variant="outline">
            <Download className="h-4 w-4 mr-2" />
            Export
          </Button>
          <Link to={`/${entityName}s/new`}>
            <Button variant="primary">
              <Plus className="h-4 w-4 mr-2" />
              Add {entityConfig.name}
            </Button>
          </Link>
        </div>
      </div>

      {/* Error Alert */}
      {error && (
        <ErrorAlert
          message={error}
          onClose={() => refetch()}
        />
      )}

      {/* Filters and Search */}
      <div className="card p-6">
        <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between space-y-4 sm:space-y-0">
          <SearchBar
            placeholder={`Search ${entityConfig.name.toLowerCase()} records...`}
            value={searchTerm}
            onChange={handleSearch}
            className="sm:max-w-xs"
          />
          
          {selectedEntities.length > 0 && (
            <div className="flex items-center space-x-3">
              <span className="text-sm text-gray-700">
                {selectedEntities.length} selected
              </span>
              <Button
                variant="danger"
                size="sm"
                onClick={() => setBulkDeleteModal(true)}
              >
                <Trash2 className="h-4 w-4 mr-2" />
                Delete Selected
              </Button>
            </div>
          )}
        </div>
      </div>

      {/* Entities Table */}
      <div className="card">
        <Table
          columns={columns}
          data={paginatedEntities}
          sortField={sortField}
          sortDirection={sortDirection}
          onSort={handleSort}
          emptyMessage={`No ${entityConfig.name.toLowerCase()} records found`}
        />
        
        <Pagination
          currentPage={currentPage}
          totalPages={totalPages}
          totalItems={totalItems}
          pageSize={pageSize}
          onPageChange={handlePageChange}
          onPageSizeChange={handlePageSizeChange}
        />
      </div>

      {/* Delete Confirmation Modal */}
      <Modal
        isOpen={deleteModal.open}
        onClose={() => setDeleteModal({ open: false, entity: null })}
        title={`Delete ${entityConfig.name}`}
        footer={
          <div className="flex space-x-3">
            <Button
              variant="outline"
              onClick={() => setDeleteModal({ open: false, entity: null })}
            >
              Cancel
            </Button>
            <Button
              variant="danger"
              loading={deleteLoading}
              onClick={() => handleDelete(deleteModal.entity)}
            >
              Delete
            </Button>
          </div>
        }
      >
        <p className="text-sm text-gray-500">
          Are you sure you want to delete this {entityConfig.name.toLowerCase()} record? 
          This action cannot be undone.
        </p>
      </Modal>

      {/* Bulk Delete Confirmation Modal */}
      <Modal
        isOpen={bulkDeleteModal}
        onClose={() => setBulkDeleteModal(false)}
        title={`Delete Multiple ${entityConfig.name}s`}
        footer={
          <div className="flex space-x-3">
            <Button
              variant="outline"
              onClick={() => setBulkDeleteModal(false)}
            >
              Cancel
            </Button>
            <Button
              variant="danger"
              loading={deleteLoading}
              onClick={handleBulkDelete}
            >
              Delete {selectedEntities.length} {entityConfig.name}s
            </Button>
          </div>
        }
      >
        <p className="text-sm text-gray-500">
          Are you sure you want to delete {selectedEntities.length} selected {entityConfig.name.toLowerCase()} records? 
          This action cannot be undone.
        </p>
      </Modal>
    </div>
  );
};

export default EntityList;
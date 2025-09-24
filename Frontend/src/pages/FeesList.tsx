import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Plus, Edit, Trash2, Eye } from 'lucide-react';
import { Button } from '../components/ui/Button';
import { Table } from '../components/ui/Table';
import { SearchBar } from '../components/ui/SearchBar';
import { Pagination } from '../components/ui/Pagination';
import { Modal } from '../components/ui/Modal';
import { useApp, appActions } from '../context/AppContext';
import { feesService } from '../services/soapClient';
import { Fees } from '../types';

export function FeesList() {
  const navigate = useNavigate();
  const { state, dispatch } = useApp();
  const [searchTerm, setSearchTerm] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize] = useState(10);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [feeToDelete, setFeeToDelete] = useState<Fees | null>(null);

  useEffect(() => {
    loadFees();
  }, [currentPage, pageSize]);

  const loadFees = async () => {
    dispatch(appActions.setLoading(true));
    try {
      const fees = await feesService.list(pageSize, (currentPage - 1) * pageSize);
      dispatch(appActions.setFees(fees));
    } catch (error) {
      dispatch(appActions.setError('Failed to load fees'));
      console.error('Error loading fees:', error);
    } finally {
      dispatch(appActions.setLoading(false));
    }
  };

  const handleSearch = (value: string) => {
    setSearchTerm(value);
    setCurrentPage(1);
  };

  const handleDelete = (fee: Fees) => {
    setFeeToDelete(fee);
    setShowDeleteModal(true);
  };

  const confirmDelete = async () => {
    if (!feeToDelete) return;

    try {
      await feesService.remove(feeToDelete.fee_id);
      dispatch(appActions.removeFee(feeToDelete.fee_id));
      setShowDeleteModal(false);
      setFeeToDelete(null);
    } catch (error) {
      dispatch(appActions.setError('Failed to delete fee'));
      console.error('Error deleting fee:', error);
    }
  };

  const filteredFees = state.fees.filter(fee =>
    (fee.fee_type && fee.fee_type.toLowerCase().includes(searchTerm.toLowerCase())) ||
    (fee.payment_status && fee.payment_status.toLowerCase().includes(searchTerm.toLowerCase())) ||
    fee.fee_id.toString().includes(searchTerm) ||
    (fee.student_id && fee.student_id.toString().includes(searchTerm))
  );

  const columns = [
    {
      key: 'fee_id',
      label: 'Fee ID',
      sortable: true,
    },
    {
      key: 'student_id',
      label: 'Student ID',
      sortable: true,
      render: (value: number) => value || '-',
    },
    {
      key: 'fee_type',
      label: 'Fee Type',
      sortable: true,
      render: (value: string) => value || '-',
    },
    {
      key: 'amount',
      label: 'Amount',
      sortable: true,
      render: (value: number) => value ? `₹${value.toLocaleString()}` : '-',
    },
    {
      key: 'payment_status',
      label: 'Status',
      sortable: true,
      render: (value: string) => (
        <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${
          value === 'paid' ? 'bg-green-100 text-green-800' : 
          value === 'pending' ? 'bg-yellow-100 text-yellow-800' : 'bg-red-100 text-red-800'
        }`}>
          {value || 'Unknown'}
        </span>
      ),
    },
    {
      key: 'due_date',
      label: 'Due Date',
      sortable: true,
      render: (value: string) => value ? new Date(value).toLocaleDateString() : '-',
    },
    {
      key: 'actions',
      label: 'Actions',
      render: (_: any, fee: Fees) => (
        <div className="flex space-x-2">
          <Button
            size="sm"
            variant="outline"
            onClick={() => navigate(`/fees/${fee.fee_id}`)}
          >
            <Eye className="h-4 w-4" />
          </Button>
          <Button
            size="sm"
            variant="outline"
            onClick={() => navigate(`/fees/${fee.fee_id}/edit`)}
          >
            <Edit className="h-4 w-4" />
          </Button>
          <Button
            size="sm"
            variant="danger"
            onClick={() => handleDelete(fee)}
          >
            <Trash2 className="h-4 w-4" />
          </Button>
        </div>
      ),
    },
  ];

  const totalPages = Math.ceil(filteredFees.length / pageSize);
  const paginatedFees = filteredFees.slice(
    (currentPage - 1) * pageSize,
    currentPage * pageSize
  );

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-gray-900">Fees</h1>
          <p className="mt-1 text-sm text-gray-500">
            Manage fee records and payments
          </p>
        </div>
        <Button onClick={() => navigate('/fees/new')}>
          <Plus className="h-4 w-4 mr-2" />
          Add Fee
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
              placeholder="Search fees by type, status, student ID, or fee ID..."
            />
          </div>

          <Table
            data={paginatedFees}
            columns={columns}
            loading={state.isLoading}
            emptyMessage="No fees found"
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
        title="Delete Fee"
      >
        <div className="space-y-4">
          <p className="text-sm text-gray-500">
            Are you sure you want to delete fee ID {feeToDelete?.fee_id}? 
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
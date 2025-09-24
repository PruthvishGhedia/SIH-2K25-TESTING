import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Plus, Edit, Trash2, Eye } from 'lucide-react';
import { Button } from '../components/ui/Button';
import { Table } from '../components/ui/Table';
import { SearchBar } from '../components/ui/SearchBar';
import { Pagination } from '../components/ui/Pagination';
import { Modal } from '../components/ui/Modal';
import { useApp, appActions } from '../context/AppContext';
import { examService } from '../services/soapClient';
import { Exam } from '../types';

export function ExamList() {
  const navigate = useNavigate();
  const { state, dispatch } = useApp();
  const [searchTerm, setSearchTerm] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize] = useState(10);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [examToDelete, setExamToDelete] = useState<Exam | null>(null);

  useEffect(() => {
    loadExams();
  }, [currentPage, pageSize]);

  const loadExams = async () => {
    dispatch(appActions.setLoading(true));
    try {
      const exams = await examService.list(pageSize, (currentPage - 1) * pageSize);
      dispatch(appActions.setExams(exams));
    } catch (error) {
      dispatch(appActions.setError('Failed to load exams'));
      console.error('Error loading exams:', error);
    } finally {
      dispatch(appActions.setLoading(false));
    }
  };

  const handleSearch = (value: string) => {
    setSearchTerm(value);
    setCurrentPage(1);
  };

  const handleDelete = (exam: Exam) => {
    setExamToDelete(exam);
    setShowDeleteModal(true);
  };

  const confirmDelete = async () => {
    if (!examToDelete) return;

    try {
      await examService.remove(examToDelete.exam_id);
      dispatch(appActions.removeExam(examToDelete.exam_id));
      setShowDeleteModal(false);
      setExamToDelete(null);
    } catch (error) {
      dispatch(appActions.setError('Failed to delete exam'));
      console.error('Error deleting exam:', error);
    }
  };

  const filteredExams = state.exams.filter(exam =>
    (exam.assessment_type && exam.assessment_type.toLowerCase().includes(searchTerm.toLowerCase())) ||
    exam.exam_id.toString().includes(searchTerm) ||
    (exam.subject_code && exam.subject_code.toString().includes(searchTerm))
  );

  const columns = [
    {
      key: 'exam_id',
      label: 'Exam ID',
      sortable: true,
    },
    {
      key: 'assessment_type',
      label: 'Assessment Type',
      sortable: true,
      render: (value: string) => value || '-',
    },
    {
      key: 'subject_code',
      label: 'Subject Code',
      sortable: true,
      render: (value: number) => value || '-',
    },
    {
      key: 'max_marks',
      label: 'Max Marks',
      sortable: true,
      render: (value: number) => value || '-',
    },
    {
      key: 'exam_date',
      label: 'Exam Date',
      sortable: true,
      render: (value: string) => value ? new Date(value).toLocaleDateString() : '-',
    },
    {
      key: 'dept_id',
      label: 'Department',
      sortable: true,
      render: (value: number) => value || '-',
    },
    {
      key: 'actions',
      label: 'Actions',
      render: (_: any, exam: Exam) => (
        <div className="flex space-x-2">
          <Button
            size="sm"
            variant="outline"
            onClick={() => navigate(`/exams/${exam.exam_id}`)}
          >
            <Eye className="h-4 w-4" />
          </Button>
          <Button
            size="sm"
            variant="outline"
            onClick={() => navigate(`/exams/${exam.exam_id}/edit`)}
          >
            <Edit className="h-4 w-4" />
          </Button>
          <Button
            size="sm"
            variant="danger"
            onClick={() => handleDelete(exam)}
          >
            <Trash2 className="h-4 w-4" />
          </Button>
        </div>
      ),
    },
  ];

  const totalPages = Math.ceil(filteredExams.length / pageSize);
  const paginatedExams = filteredExams.slice(
    (currentPage - 1) * pageSize,
    currentPage * pageSize
  );

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-gray-900">Exams</h1>
          <p className="mt-1 text-sm text-gray-500">
            Manage exam records and schedules
          </p>
        </div>
        <Button onClick={() => navigate('/exams/new')}>
          <Plus className="h-4 w-4 mr-2" />
          Add Exam
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
              placeholder="Search exams by type, subject code, or exam ID..."
            />
          </div>

          <Table
            data={paginatedExams}
            columns={columns}
            loading={state.isLoading}
            emptyMessage="No exams found"
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
        title="Delete Exam"
      >
        <div className="space-y-4">
          <p className="text-sm text-gray-500">
            Are you sure you want to delete exam ID {examToDelete?.exam_id}? 
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
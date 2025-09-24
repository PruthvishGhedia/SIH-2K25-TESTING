import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Plus, Edit, Trash2, Eye } from 'lucide-react';
import { Button } from '../components/ui/Button';
import { Table } from '../components/ui/Table';
import { SearchBar } from '../components/ui/SearchBar';
import { Pagination } from '../components/ui/Pagination';
import { Modal } from '../components/ui/Modal';
import { useApp, appActions } from '../context/AppContext';
import { studentService } from '../services/soapClient';
import { Student } from '../types';

export function StudentList() {
  const navigate = useNavigate();
  const { state, dispatch } = useApp();
  const [searchTerm, setSearchTerm] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize] = useState(10);
  const [sortBy, setSortBy] = useState<string>('');
  const [sortOrder, setSortOrder] = useState<'asc' | 'desc'>('asc');
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [studentToDelete, setStudentToDelete] = useState<Student | null>(null);

  useEffect(() => {
    loadStudents();
  }, [currentPage, pageSize]);

  const loadStudents = async () => {
    dispatch(appActions.setLoading(true));
    try {
      const students = await studentService.list(pageSize, (currentPage - 1) * pageSize);
      dispatch(appActions.setStudents(students));
    } catch (error) {
      dispatch(appActions.setError('Failed to load students'));
      console.error('Error loading students:', error);
    } finally {
      dispatch(appActions.setLoading(false));
    }
  };

  const handleSearch = (value: string) => {
    setSearchTerm(value);
    setCurrentPage(1);
  };

  const handleSort = (key: string) => {
    if (sortBy === key) {
      setSortOrder(sortOrder === 'asc' ? 'desc' : 'asc');
    } else {
      setSortBy(key);
      setSortOrder('asc');
    }
  };

  const handleDelete = (student: Student) => {
    setStudentToDelete(student);
    setShowDeleteModal(true);
  };

  const confirmDelete = async () => {
    if (!studentToDelete) return;

    try {
      await studentService.remove(studentToDelete.student_id);
      dispatch(appActions.removeStudent(studentToDelete.student_id));
      setShowDeleteModal(false);
      setStudentToDelete(null);
    } catch (error) {
      dispatch(appActions.setError('Failed to delete student'));
      console.error('Error deleting student:', error);
    }
  };

  const filteredStudents = state.students.filter(student =>
    student.student_id.toString().includes(searchTerm) ||
    (student.user_id && student.user_id.toString().includes(searchTerm)) ||
    (student.dept_id && student.dept_id.toString().includes(searchTerm))
  );

  const columns = [
    {
      key: 'student_id',
      label: 'Student ID',
      sortable: true,
    },
    {
      key: 'user_id',
      label: 'User ID',
      sortable: true,
      render: (value: number) => value || '-',
    },
    {
      key: 'dept_id',
      label: 'Department',
      sortable: true,
      render: (value: number) => value || '-',
    },
    {
      key: 'course_id',
      label: 'Course',
      sortable: true,
      render: (value: number) => value || '-',
    },
    {
      key: 'admission_date',
      label: 'Admission Date',
      sortable: true,
      render: (value: string) => value ? new Date(value).toLocaleDateString() : '-',
    },
    {
      key: 'verified',
      label: 'Verified',
      sortable: true,
      render: (value: boolean) => (
        <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${
          value ? 'bg-green-100 text-green-800' : 'bg-red-100 text-red-800'
        }`}>
          {value ? 'Yes' : 'No'}
        </span>
      ),
    },
    {
      key: 'actions',
      label: 'Actions',
      render: (_: any, student: Student) => (
        <div className="flex space-x-2">
          <Button
            size="sm"
            variant="outline"
            onClick={() => navigate(`/students/${student.student_id}`)}
          >
            <Eye className="h-4 w-4" />
          </Button>
          <Button
            size="sm"
            variant="outline"
            onClick={() => navigate(`/students/${student.student_id}/edit`)}
          >
            <Edit className="h-4 w-4" />
          </Button>
          <Button
            size="sm"
            variant="danger"
            onClick={() => handleDelete(student)}
          >
            <Trash2 className="h-4 w-4" />
          </Button>
        </div>
      ),
    },
  ];

  const totalPages = Math.ceil(filteredStudents.length / pageSize);
  const paginatedStudents = filteredStudents.slice(
    (currentPage - 1) * pageSize,
    currentPage * pageSize
  );

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-gray-900">Students</h1>
          <p className="mt-1 text-sm text-gray-500">
            Manage student records and information
          </p>
        </div>
        <Button onClick={() => navigate('/students/new')}>
          <Plus className="h-4 w-4 mr-2" />
          Add Student
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
              placeholder="Search students by ID, user ID, or department..."
            />
          </div>

          <Table
            data={paginatedStudents}
            columns={columns}
            loading={state.isLoading}
            onSort={handleSort}
            sortBy={sortBy}
            sortOrder={sortOrder}
            emptyMessage="No students found"
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
        title="Delete Student"
      >
        <div className="space-y-4">
          <p className="text-sm text-gray-500">
            Are you sure you want to delete student ID {studentToDelete?.student_id}? 
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
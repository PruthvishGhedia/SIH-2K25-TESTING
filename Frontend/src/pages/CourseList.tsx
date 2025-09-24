import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Plus, Edit, Trash2, Eye } from 'lucide-react';
import { Button } from '../components/ui/Button';
import { Table } from '../components/ui/Table';
import { SearchBar } from '../components/ui/SearchBar';
import { Pagination } from '../components/ui/Pagination';
import { Modal } from '../components/ui/Modal';
import { useApp, appActions } from '../context/AppContext';
import { courseService } from '../services/soapClient';
import { Course } from '../types';

export function CourseList() {
  const navigate = useNavigate();
  const { state, dispatch } = useApp();
  const [searchTerm, setSearchTerm] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize] = useState(10);
  const [sortBy, setSortBy] = useState<string>('');
  const [sortOrder, setSortOrder] = useState<'asc' | 'desc'>('asc');
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [courseToDelete, setCourseToDelete] = useState<Course | null>(null);

  useEffect(() => {
    loadCourses();
  }, [currentPage, pageSize]);

  const loadCourses = async () => {
    dispatch(appActions.setLoading(true));
    try {
      const courses = await courseService.list(pageSize, (currentPage - 1) * pageSize);
      dispatch(appActions.setCourses(courses));
    } catch (error) {
      dispatch(appActions.setError('Failed to load courses'));
      console.error('Error loading courses:', error);
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

  const handleDelete = (course: Course) => {
    setCourseToDelete(course);
    setShowDeleteModal(true);
  };

  const confirmDelete = async () => {
    if (!courseToDelete) return;

    try {
      await courseService.remove(courseToDelete.course_id);
      dispatch(appActions.removeCourse(courseToDelete.course_id));
      setShowDeleteModal(false);
      setCourseToDelete(null);
    } catch (error) {
      dispatch(appActions.setError('Failed to delete course'));
      console.error('Error deleting course:', error);
    }
  };

  const filteredCourses = state.courses.filter(course =>
    course.course_name.toLowerCase().includes(searchTerm.toLowerCase()) ||
    (course.course_code && course.course_code.toLowerCase().includes(searchTerm.toLowerCase())) ||
    course.course_id.toString().includes(searchTerm)
  );

  const columns = [
    {
      key: 'course_id',
      label: 'Course ID',
      sortable: true,
    },
    {
      key: 'course_name',
      label: 'Course Name',
      sortable: true,
    },
    {
      key: 'course_code',
      label: 'Course Code',
      sortable: true,
      render: (value: string) => value || '-',
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
      render: (_: any, course: Course) => (
        <div className="flex space-x-2">
          <Button
            size="sm"
            variant="outline"
            onClick={() => navigate(`/courses/${course.course_id}`)}
          >
            <Eye className="h-4 w-4" />
          </Button>
          <Button
            size="sm"
            variant="outline"
            onClick={() => navigate(`/courses/${course.course_id}/edit`)}
          >
            <Edit className="h-4 w-4" />
          </Button>
          <Button
            size="sm"
            variant="danger"
            onClick={() => handleDelete(course)}
          >
            <Trash2 className="h-4 w-4" />
          </Button>
        </div>
      ),
    },
  ];

  const totalPages = Math.ceil(filteredCourses.length / pageSize);
  const paginatedCourses = filteredCourses.slice(
    (currentPage - 1) * pageSize,
    currentPage * pageSize
  );

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-gray-900">Courses</h1>
          <p className="mt-1 text-sm text-gray-500">
            Manage course records and information
          </p>
        </div>
        <Button onClick={() => navigate('/courses/new')}>
          <Plus className="h-4 w-4 mr-2" />
          Add Course
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
              placeholder="Search courses by name, code, or ID..."
            />
          </div>

          <Table
            data={paginatedCourses}
            columns={columns}
            loading={state.isLoading}
            onSort={handleSort}
            sortBy={sortBy}
            sortOrder={sortOrder}
            emptyMessage="No courses found"
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
        title="Delete Course"
      >
        <div className="space-y-4">
          <p className="text-sm text-gray-500">
            Are you sure you want to delete the course "{courseToDelete?.course_name}"? 
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
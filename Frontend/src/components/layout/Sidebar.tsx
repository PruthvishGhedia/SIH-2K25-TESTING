import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import { clsx } from 'clsx';
import {
  LayoutDashboard,
  Users,
  BookOpen,
  Building2,
  User,
  CreditCard,
  FileText,
  ChevronDown,
  ChevronRight
} from 'lucide-react';
import { useState } from 'react';

interface MenuItem {
  label: string;
  path: string;
  icon: React.ComponentType<{ className?: string }>;
  children?: MenuItem[];
}

const menuItems: MenuItem[] = [
  {
    label: 'Dashboard',
    path: '/',
    icon: LayoutDashboard,
  },
  {
    label: 'Students',
    path: '/students',
    icon: Users,
    children: [
      { label: 'All Students', path: '/students', icon: Users },
      { label: 'Add Student', path: '/students/new', icon: User },
    ],
  },
  {
    label: 'Courses',
    path: '/courses',
    icon: BookOpen,
    children: [
      { label: 'All Courses', path: '/courses', icon: BookOpen },
      { label: 'Add Course', path: '/courses/new', icon: BookOpen },
    ],
  },
  {
    label: 'Departments',
    path: '/departments',
    icon: Building2,
    children: [
      { label: 'All Departments', path: '/departments', icon: Building2 },
      { label: 'Add Department', path: '/departments/new', icon: Building2 },
    ],
  },
  {
    label: 'Users',
    path: '/users',
    icon: User,
    children: [
      { label: 'All Users', path: '/users', icon: User },
      { label: 'Add User', path: '/users/new', icon: User },
    ],
  },
  {
    label: 'Fees',
    path: '/fees',
    icon: CreditCard,
    children: [
      { label: 'All Fees', path: '/fees', icon: CreditCard },
      { label: 'Add Fee', path: '/fees/new', icon: CreditCard },
    ],
  },
  {
    label: 'Exams',
    path: '/exams',
    icon: FileText,
    children: [
      { label: 'All Exams', path: '/exams', icon: FileText },
      { label: 'Add Exam', path: '/exams/new', icon: FileText },
    ],
  },
];

interface SidebarProps {
  isOpen: boolean;
}

export function Sidebar({ isOpen }: SidebarProps) {
  const location = useLocation();
  const [expandedItems, setExpandedItems] = useState<string[]>([]);

  const toggleExpanded = (path: string) => {
    setExpandedItems(prev =>
      prev.includes(path)
        ? prev.filter(item => item !== path)
        : [...prev, path]
    );
  };

  const isActive = (path: string) => {
    return location.pathname === path;
  };

  const hasActiveChild = (item: MenuItem) => {
    if (!item.children) return false;
    return item.children.some(child => location.pathname === child.path);
  };

  const renderMenuItem = (item: MenuItem, level = 0) => {
    const hasChildren = item.children && item.children.length > 0;
    const isExpanded = expandedItems.includes(item.path);
    const isItemActive = isActive(item.path) || hasActiveChild(item);

    return (
      <div key={item.path}>
        <div className="flex items-center">
          <Link
            to={item.path}
            onClick={() => hasChildren && toggleExpanded(item.path)}
            className={clsx(
              'flex items-center w-full px-3 py-2 text-sm font-medium rounded-md transition-colors',
              level > 0 && 'ml-6',
              isItemActive
                ? 'bg-primary-100 text-primary-700'
                : 'text-gray-600 hover:bg-gray-50 hover:text-gray-900'
            )}
          >
            <item.icon className={clsx('mr-3 h-5 w-5', level > 0 && 'h-4 w-4')} />
            <span className="flex-1">{item.label}</span>
            {hasChildren && (
              isExpanded ? (
                <ChevronDown className="h-4 w-4" />
              ) : (
                <ChevronRight className="h-4 w-4" />
              )
            )}
          </Link>
        </div>
        
        {hasChildren && isExpanded && (
          <div className="mt-1 space-y-1">
            {item.children!.map(child => renderMenuItem(child, level + 1))}
          </div>
        )}
      </div>
    );
  };

  return (
    <div
      className={clsx(
        'fixed inset-y-0 left-0 z-50 w-64 bg-white shadow-lg transform transition-transform duration-300 ease-in-out',
        isOpen ? 'translate-x-0' : '-translate-x-full',
        'lg:translate-x-0 lg:static lg:inset-0'
      )}
    >
      <div className="flex flex-col h-full">
        <div className="flex-1 px-4 py-6 space-y-2 overflow-y-auto">
          {menuItems.map(item => renderMenuItem(item))}
        </div>
        
        <div className="p-4 border-t border-gray-200">
          <div className="text-xs text-gray-500">
            SIH ERP System v1.0
          </div>
        </div>
      </div>
    </div>
  );
}
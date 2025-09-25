import React from 'react';
import Header from './Header';
import Sidebar from './Sidebar';
import NotificationToast from '../ui/NotificationToast';
import Breadcrumbs from '../ui/Breadcrumbs';
import { useAppContext } from '../../context/AppContext';

const Layout = ({ children }) => {
  const { isSidebarOpen } = useAppContext();
  const isCollapsed = !isSidebarOpen;

  return (
    <div className="min-h-screen bg-gray-50 flex">
      <Sidebar />
      
      <div className={`flex-1 flex flex-col transition-all duration-300 ${
        isCollapsed ? 'ml-16' : 'ml-64'
      }`}>
        <Header />
        
        <main className="flex-1 py-6">
          <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            {/* Breadcrumbs */}
            <div className="mb-6">
              <Breadcrumbs />
            </div>
            
            {/* Page Content */}
            {children}
          </div>
        </main>
      </div>
      
      {/* Global Notifications */}
      <NotificationToast />
    </div>
  );
};

export default Layout;
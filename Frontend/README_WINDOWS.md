# SIH ERP Frontend - Windows Setup Guide

## Prerequisites
- Node.js 18+ (LTS version recommended)
- npm or yarn package manager
- Windows 10/11 with PowerShell

## Setup Instructions

### 1. Install Dependencies

Open PowerShell and navigate to the frontend directory:

```powershell
cd Frontend
```

Install all required dependencies:

```powershell
npm install
```

### 2. Environment Configuration

The application uses environment variables for configuration. The following variables are defined in the `.env` file:

- `VITE_BACKEND_URL` - The base URL for the SOAP backend (default: http://localhost:5000/soap)

To customize these values, edit the `.env` file:

```env
VITE_BACKEND_URL=http://localhost:5000/soap
```

### 3. Run Development Server

Start the development server with hot reloading:

```powershell
npm run dev
```

The application will be available at `http://localhost:5173` by default.

### 4. Build for Production

To create a production build:

```powershell
npm run build
```

The built files will be available in the `dist` directory.

### 5. Preview Production Build

To preview the production build locally:

```powershell
npm run preview
```

## Project Structure

```
src/
├── components/          # Reusable UI components
│   ├── layout/          # Layout components (Header, Sidebar, etc.)
│   └── ui/              # Generic UI components (Button, Table, Modal, etc.)
├── context/             # React Context providers
├── hooks/               # Custom React hooks
├── pages/               # Page components for each entity
├── services/            # API services and SOAP client
├── styles/              # Global styles and Tailwind configuration
├── utils/               # Utility functions and constants
└── App.jsx             # Main application component
```

## Entity Management

The frontend supports CRUD operations for the following entities:
- Students
- Courses
- Departments
- Users
- Fees
- Exams
- Guardians
- Admissions
- Hostels
- Library
- Faculty
- Enrollments
- Attendance
- Payments

Each entity has dedicated list and form pages with full CRUD functionality.

## SOAP Client Usage

The SOAP client is located at `src/services/soapClient.js` and provides the following functions:

- `list(entityEndpoint, limit, offset)` - Fetch a list of entities
- `get(entityEndpoint, id)` - Fetch a single entity by ID
- `create(entityEndpoint, data)` - Create a new entity
- `update(entityEndpoint, id, data)` - Update an existing entity
- `delete(entityEndpoint, id)` - Delete an entity

Example usage:
```javascript
import { soapClient } from '../services/soapClient';

// List students
const students = await soapClient.list('/student', 10, 0);

// Get a specific student
const student = await soapClient.get('/student', 123);

// Create a new student
const newStudent = await soapClient.create('/student', {
  first_name: 'John',
  last_name: 'Doe',
  email: 'john.doe@example.com'
});
```

## Custom Hooks

The application provides several custom hooks for common functionality:

- `useForm` - Form state management with validation
- `useFetch` - Data fetching with loading and error states
- `usePagination` - Pagination, search, and sorting functionality

## UI Components

All UI components are built with Tailwind CSS and support both light and dark modes. Key components include:

- Table with sorting and pagination
- Form inputs with validation
- Modals and dialogs
- Notifications and alerts
- Dashboard cards and statistics

## Testing CRUD Operations

To test CRUD operations:

1. Ensure the backend is running at `http://localhost:5000`
2. Start the frontend development server
3. Navigate to any entity page (e.g., http://localhost:5173/students)
4. Use the "Add" button to create new records
5. Use the edit/delete actions in the table to modify existing records

## Troubleshooting

### CORS Issues
If you encounter CORS errors, ensure the backend is configured to allow requests from `http://localhost:5173`.

### Network Errors
Verify that the backend is running and accessible at the configured URL.

### Build Issues
Clear the node_modules directory and reinstall dependencies:
```powershell
rm -Recurse -Force node_modules
npm install
```
# SIH ERP Frontend

A modern React-based frontend for the SIH ERP System built with TypeScript, Vite, and Tailwind CSS.

## Features

- **Modern UI/UX**: Clean, responsive design with Tailwind CSS
- **Type Safety**: Full TypeScript implementation
- **State Management**: React Context for global state management
- **SOAP Integration**: Complete integration with ASP.NET Core SOAP services
- **CRUD Operations**: Full Create, Read, Update, Delete functionality for all entities
- **Search & Pagination**: Advanced search and pagination for all data tables
- **Form Validation**: Client-side validation with error handling
- **Responsive Design**: Mobile-first responsive design

## Tech Stack

- **React 18**: Modern React with hooks
- **TypeScript**: Type-safe development
- **Vite**: Fast build tool and dev server
- **Tailwind CSS**: Utility-first CSS framework
- **React Router**: Client-side routing
- **Lucide React**: Beautiful icons
- **SOAP Client**: Custom SOAP integration

## Project Structure

```
src/
├── components/
│   ├── ui/              # Reusable UI components
│   └── layout/          # Layout components (Header, Sidebar, etc.)
├── context/             # React Context for state management
├── pages/               # Page components
├── services/            # SOAP client and API services
├── types/               # TypeScript type definitions
└── App.tsx              # Main application component
```

## Available Pages

- **Dashboard**: Overview of system data and statistics
- **Students**: Manage student records
- **Courses**: Manage course information
- **Departments**: Manage department data
- **Users**: Manage user accounts
- **Fees**: Manage fee records and payments
- **Exams**: Manage exam schedules and data

## Setup Instructions

1. **Install Dependencies**
   ```bash
   npm install
   ```

2. **Configure Backend URL**
   Update the `BASE_URL` in `src/services/soapClient.ts` to match your backend URL:
   ```typescript
   const BASE_URL = 'http://localhost:5000'; // Change this to your backend URL
   ```

3. **Start Development Server**
   ```bash
   npm run dev
   ```

4. **Build for Production**
   ```bash
   npm run build
   ```

## Backend Integration

The frontend integrates with the following SOAP endpoints:

- `/soap/StudentService.svc`
- `/soap/CourseService.svc`
- `/soap/DepartmentService.svc`
- `/soap/UserService.svc`
- `/soap/FeesService.svc`
- `/soap/ExamService.svc`
- `/health` (Health check endpoint)

## Features by Entity

### Students
- View all students with pagination
- Search by student ID, user ID, or department
- Add new students with form validation
- Edit existing student information
- Delete students with confirmation
- Display admission date and verification status

### Courses
- Manage course information
- Link courses to departments
- Course code management
- Full CRUD operations

### Departments
- Simple department management
- Department name and ID tracking
- Integration with courses and students

### Users
- User account management
- Email and personal information
- Active/inactive status tracking

### Fees
- Fee record management
- Payment status tracking
- Student fee associations
- Amount and due date management

### Exams
- Exam schedule management
- Assessment type tracking
- Department and subject associations
- Maximum marks configuration

## Customization

### Styling
- Modify `src/index.css` for global styles
- Update `tailwind.config.js` for theme customization
- Component-specific styles use Tailwind utility classes

### Adding New Features
1. Add new types in `src/types/index.ts`
2. Create service methods in `src/services/soapClient.ts`
3. Add state management in `src/context/AppContext.tsx`
4. Create page components in `src/pages/`
5. Add routes in `src/App.tsx`

## Development Notes

- All API calls are handled through the centralized SOAP client
- State management uses React Context with useReducer
- Form validation is handled client-side with error display
- Loading states and error handling are implemented throughout
- Responsive design works on mobile, tablet, and desktop

## Browser Support

- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)

## Contributing

1. Follow TypeScript best practices
2. Use Tailwind CSS for styling
3. Implement proper error handling
4. Add loading states for async operations
5. Ensure responsive design
6. Test all CRUD operations
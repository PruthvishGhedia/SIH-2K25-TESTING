-- Create minimal data for dev testing
-- Run this script to populate the database with sample data

-- Insert departments
INSERT INTO department (dept_id, dept_name) VALUES 
(1, 'Computer Science'),
(2, 'Electronics'),
(3, 'Mechanical'),
(4, 'Civil')
ON CONFLICT (dept_id) DO NOTHING;

-- Insert courses
INSERT INTO course (course_id, course_name, dept_id) VALUES 
(1, 'BSc Computer Science', 1),
(2, 'MSc Computer Science', 1),
(3, 'BE Electronics', 2),
(4, 'BE Mechanical', 3),
(5, 'BE Civil', 4)
ON CONFLICT (course_id) DO NOTHING;

-- Insert roles
INSERT INTO role (role_id, role_name) VALUES 
(1, 'Admin'),
(2, 'Teacher'),
(3, 'Student'),
(4, 'Staff')
ON CONFLICT (role_id) DO NOTHING;

-- Insert users
INSERT INTO user (user_id, full_name, email, dob, is_active, created_at) VALUES 
(1, 'Admin User', 'admin@school.edu', '1980-01-01', true, NOW()),
(2, 'John Teacher', 'teacher@school.edu', '1985-05-15', true, NOW()),
(3, 'Jane Student', 'student@school.edu', '2000-03-20', true, NOW())
ON CONFLICT (user_id) DO NOTHING;

-- Insert students
INSERT INTO student (student_id, first_name, last_name, dob, email, department_id, course_id, admission_date, verified) VALUES 
(1, 'John', 'Doe', '2000-01-15', 'john.doe@student.edu', 1, 1, '2023-09-01', true),
(2, 'Jane', 'Smith', '2000-03-20', 'jane.smith@student.edu', 1, 1, '2023-09-01', true),
(3, 'Bob', 'Johnson', '1999-07-10', 'bob.johnson@student.edu', 2, 3, '2022-09-01', true)
ON CONFLICT (student_id) DO NOTHING;

-- Insert fees
INSERT INTO fees (fee_id, student_id, fee_type, amount, due_date, payment_status, payment_mode) VALUES 
(1, 1, 'Tuition Fee', 50000.00, '2024-01-15', 'paid', 'online'),
(2, 2, 'Tuition Fee', 50000.00, '2024-01-15', 'pending', NULL),
(3, 3, 'Tuition Fee', 45000.00, '2024-01-15', 'paid', 'cash')
ON CONFLICT (fee_id) DO NOTHING;

-- Insert exams
INSERT INTO exam (exam_id, dept_id, subject_code, exam_date, assessment_type, max_marks, created_by) VALUES 
(1, 1, 101, '2024-02-15', 'Midterm', 100, 2),
(2, 1, 102, '2024-02-20', 'Final', 100, 2),
(3, 2, 201, '2024-02-18', 'Midterm', 100, 2)
ON CONFLICT (exam_id) DO NOTHING;

-- Insert guardians
INSERT INTO guardian (guardian_id, first_name, last_name, email, phone, student_id) VALUES 
(1, 'Robert', 'Doe', 'robert.doe@email.com', '+1234567890', 1),
(2, 'Mary', 'Smith', 'mary.smith@email.com', '+1234567891', 2),
(3, 'David', 'Johnson', 'david.johnson@email.com', '+1234567892', 3)
ON CONFLICT (guardian_id) DO NOTHING;

-- Insert library books
INSERT INTO library (book_id, title, author, publisher, isbn, available_copies) VALUES 
(1, 'Introduction to Algorithms', 'Thomas H. Cormen', 'MIT Press', '978-0262033848', 5),
(2, 'Clean Code', 'Robert C. Martin', 'Prentice Hall', '978-0132350884', 3),
(3, 'Design Patterns', 'Gang of Four', 'Addison-Wesley', '978-0201633610', 2)
ON CONFLICT (book_id) DO NOTHING;

-- Insert hostel
INSERT INTO hostel (hostel_id, hostel_name, capacity) VALUES 
(1, 'Boys Hostel A', 100),
(2, 'Girls Hostel B', 80),
(3, 'Boys Hostel C', 120)
ON CONFLICT (hostel_id) DO NOTHING;

-- Insert rooms
INSERT INTO room (room_id, hostel_id, room_number, capacity) VALUES 
(1, 1, '101', 4),
(2, 1, '102', 4),
(3, 2, '201', 3),
(4, 2, '202', 3)
ON CONFLICT (room_id) DO NOTHING;

-- Insert hostel allocations
INSERT INTO hostelallocation (allocation_id, student_id, room_id, start_date, end_date) VALUES 
(1, 1, 1, '2023-09-01', '2024-05-31'),
(2, 2, 3, '2023-09-01', '2024-05-31')
ON CONFLICT (allocation_id) DO NOTHING;

-- Insert contact details
INSERT INTO contactdetails (contact_id, user_id, phone, address, city, state, zip) VALUES 
(1, 1, '+1234567890', '123 Admin St', 'Admin City', 'Admin State', '12345'),
(2, 2, '+1234567891', '456 Teacher Ave', 'Teacher City', 'Teacher State', '23456'),
(3, 3, '+1234567892', '789 Student Blvd', 'Student City', 'Student State', '34567')
ON CONFLICT (contact_id) DO NOTHING;

-- Stage 1.2 entities

-- Faculty
INSERT INTO faculty (faculty_id, first_name, last_name, email, phone, department_id, is_active) VALUES
(1, 'Alice', 'Wong', 'alice.wong@school.edu', '+1800123456', 1, true),
(2, 'Ravi', 'Kumar', 'ravi.kumar@school.edu', '+1800123457', 2, true)
ON CONFLICT (faculty_id) DO NOTHING;

-- Enrollment
INSERT INTO enrollment (enrollment_id, student_id, course_id, enrollment_date, status) VALUES
(1, 1, 1, '2023-09-05', 'active'),
(2, 2, 1, '2023-09-06', 'active')
ON CONFLICT (enrollment_id) DO NOTHING;

-- Attendance
INSERT INTO attendance (attendance_id, student_id, course_id, date, present) VALUES
(1, 1, 1, '2023-09-10', true),
(2, 2, 1, '2023-09-10', false)
ON CONFLICT (attendance_id) DO NOTHING;

-- Payments
INSERT INTO payment (payment_id, student_id, amount, payment_date, status, mode) VALUES
(1, 1, 50000.00, '2024-01-10', 'paid', 'online'),
(2, 2, 25000.00, '2024-01-12', 'pending', 'cash')
ON CONFLICT (payment_id) DO NOTHING;
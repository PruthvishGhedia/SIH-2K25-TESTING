using Xunit;
using Dapper;
using System.Data;
using Npgsql;
using SIH.ERP.Soap.Repositories;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Exceptions;

namespace SIH.ERP.Soap.Tests
{
    public class StudentRepositoryTests : IDisposable
    {
        private readonly IDbConnection _connection;
        private readonly StudentRepository _repository;

        public StudentRepositoryTests()
        {
            // Use test connection string - adjust as needed for your test environment
            var connectionString = Environment.GetEnvironmentVariable("TEST_DATABASE_URL") 
                ?? "Host=localhost;Database=sih_test;Username=postgres;Password=password";
            
            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();
            _repository = new StudentRepository(_connection);
            
            // Clean up test data before each test
            CleanupTestData();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedStudent()
        {
            // Arrange
            var student = new Student
            {
                first_name = "Test",
                last_name = "Student",
                dob = new DateTime(2000, 1, 1),
                email = "test@student.com",
                department_id = 1,
                course_id = 1,
                verified = true
            };

            // Act
            var result = await _repository.CreateAsync(student);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.first_name);
            Assert.Equal("Student", result.last_name);
            Assert.Equal("test@student.com", result.email);
            Assert.True(result.verified);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnStudent_WhenStudentExists()
        {
            // Arrange
            var student = await CreateTestStudent();

            // Act
            var result = await _repository.GetAsync(student.student_id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(student.student_id, result.student_id);
            Assert.Equal(student.first_name, result.first_name);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenStudentDoesNotExist()
        {
            // Act
            var result = await _repository.GetAsync(99999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ListAsync_ShouldReturnStudents()
        {
            // Arrange
            await CreateTestStudent();
            await CreateTestStudent();

            // Act
            var result = await _repository.ListAsync(10, 0);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count() >= 2);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnUpdatedStudent()
        {
            // Arrange
            var student = await CreateTestStudent();
            student.first_name = "Updated";
            student.email = "updated@student.com";

            // Act
            var result = await _repository.UpdateAsync(student.student_id, student);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated", result.first_name);
            Assert.Equal("updated@student.com", result.email);
        }

        [Fact]
        public async Task RemoveAsync_ShouldReturnDeletedStudent()
        {
            // Arrange
            var student = await CreateTestStudent();

            // Act
            var result = await _repository.RemoveAsync(student.student_id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(student.student_id, result.student_id);

            // Verify student is deleted
            var deletedStudent = await _repository.GetAsync(student.student_id);
            Assert.Null(deletedStudent);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowRepositoryException_WhenInvalidData()
        {
            // Arrange
            var invalidStudent = new Student
            {
                // Missing required fields
            };

            // Act & Assert
            await Assert.ThrowsAsync<RepositoryException>(() => _repository.CreateAsync(invalidStudent));
        }

        private async Task<Student> CreateTestStudent()
        {
            var student = new Student
            {
                first_name = "Test",
                last_name = "Student",
                dob = new DateTime(2000, 1, 1),
                email = $"test{DateTime.Now.Ticks}@student.com",
                department_id = 1,
                course_id = 1,
                verified = true
            };

            return await _repository.CreateAsync(student);
        }

        private void CleanupTestData()
        {
            try
            {
                _connection.Execute("DELETE FROM student WHERE email LIKE 'test%@student.com'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Could not clean up test data: {ex.Message}");
            }
        }

        public void Dispose()
        {
            CleanupTestData();
            _connection?.Dispose();
        }
    }
}
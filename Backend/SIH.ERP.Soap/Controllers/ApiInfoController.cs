using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace SIH.ERP.Soap.Controllers
{
    /// <summary>
    /// Provides API information and documentation endpoints
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ApiInfoController : ControllerBase
    {
        /// <summary>
        /// Gets API version and build information
        /// </summary>
        /// <returns>API version details</returns>
        [HttpGet("version")]
        public IActionResult GetVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown";
            var buildDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
            
            return Ok(new
            {
                Version = version,
                BuildDate = buildDate,
                Framework = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription,
                OperatingSystem = System.Runtime.InteropServices.RuntimeInformation.OSDescription,
                Architecture = System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture.ToString()
            });
        }

        /// <summary>
        /// Gets API documentation information
        /// </summary>
        /// <returns>API documentation details</returns>
        [HttpGet("documentation")]
        public IActionResult GetDocumentation()
        {
            return Ok(new
            {
                Title = "SIH ERP SOAP API",
                Description = "SOAP-based Web Services for SIH ERP System",
                DocumentationUrl = "/swagger",
                Version = "v1",
                Contact = new
            {
                    Name = "SIH ERP Team",
                    Email = "support@sih-erp.com",
                    Url = "https://github.com/SIH-ERP"
                }
            });
        }

        /// <summary>
        /// Gets health status of the API
        /// </summary>
        /// <returns>Health status</returns>
        [HttpGet("health")]
        public IActionResult GetHealth()
        {
            return Ok(new
            {
                Status = "Healthy",
                Timestamp = DateTime.UtcNow,
                Service = "SIH ERP SOAP API"
            });
        }
        
        /// <summary>
        /// Gets comprehensive list of all available SOAP APIs with exact endpoints
        /// </summary>
        /// <returns>List of all SOAP APIs organized by service with exact endpoints</returns>
        [HttpGet("services")]
        public IActionResult GetAllServices()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            
            return Ok(new
            {
                Message = "All SOAP services are available at the following endpoints. Use SOAP clients to interact with these endpoints.",
                BaseUrl = baseUrl,
                Services = new[]
                {
                    new { Name = "AdmissionService", Endpoint = $"{baseUrl}/soap/admission", Description = "Manages student admission processes" },
                    new { Name = "StudentService", Endpoint = $"{baseUrl}/soap/student", Description = "Manages student records and information" },
                    new { Name = "DepartmentService", Endpoint = $"{baseUrl}/soap/department", Description = "Manages academic departments" },
                    new { Name = "CourseService", Endpoint = $"{baseUrl}/soap/course", Description = "Manages academic courses" },
                    new { Name = "SubjectService", Endpoint = $"{baseUrl}/soap/subject", Description = "Manages academic subjects" },
                    new { Name = "RoleService", Endpoint = $"{baseUrl}/soap/role", Description = "Manages user roles and permissions" },
                    new { Name = "UserService", Endpoint = $"{baseUrl}/soap/user", Description = "Manages user accounts" },
                    new { Name = "GuardianService", Endpoint = $"{baseUrl}/soap/guardian", Description = "Manages student guardian information" },
                    new { Name = "HostelService", Endpoint = $"{baseUrl}/soap/hostel", Description = "Manages hostel facilities" },
                    new { Name = "RoomService", Endpoint = $"{baseUrl}/soap/room", Description = "Manages hostel rooms" },
                    new { Name = "HostelAllocationService", Endpoint = $"{baseUrl}/soap/hostelallocation", Description = "Manages hostel allocations for students" },
                    new { Name = "FeesService", Endpoint = $"{baseUrl}/soap/fees", Description = "Manages fee structures and calculations" },
                    new { Name = "LibraryService", Endpoint = $"{baseUrl}/soap/library", Description = "Manages library books and resources" },
                    new { Name = "BookIssueService", Endpoint = $"{baseUrl}/soap/bookissue", Description = "Manages library book issues and returns" },
                    new { Name = "ExamService", Endpoint = $"{baseUrl}/soap/exam", Description = "Manages examination schedules and information" },
                    new { Name = "ResultService", Endpoint = $"{baseUrl}/soap/result", Description = "Manages examination results" },
                    new { Name = "UserRoleService", Endpoint = $"{baseUrl}/soap/userrole", Description = "Manages user-role associations" },
                    new { Name = "ContactDetailsService", Endpoint = $"{baseUrl}/soap/contactdetails", Description = "Manages contact information" },
                    new { Name = "FacultyService", Endpoint = $"{baseUrl}/soap/faculty", Description = "Manages faculty members" },
                    new { Name = "EnrollmentService", Endpoint = $"{baseUrl}/soap/enrollment", Description = "Manages student course enrollments" },
                    new { Name = "AttendanceService", Endpoint = $"{baseUrl}/soap/attendance", Description = "Manages student attendance records" },
                    new { Name = "PaymentService", Endpoint = $"{baseUrl}/soap/payment", Description = "Manages payment transactions" },
                    new { Name = "GenericCrudService", Endpoint = $"{baseUrl}/soap", Description = "Provides generic CRUD operations for all tables" }
                }
            });
        }
        
        /// <summary>
        /// Gets detailed information about all SOAP service operations
        /// </summary>
        /// <returns>Detailed information about all SOAP service operations</returns>
        [HttpGet("services/details")]
        public IActionResult GetAllServiceDetails()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            
            return Ok(new
            {
                Message = "Detailed information about all SOAP service operations. Each service supports ListAsync, GetAsync, CreateAsync, UpdateAsync, and RemoveAsync operations.",
                BaseUrl = baseUrl,
                ServiceDetails = new[]
                {
                    new { 
                        ServiceName = "StudentService", 
                        Endpoint = $"{baseUrl}/soap/student", 
                        Operations = new[] {
                            new { Name = "ListAsync", Method = "GET", Description = "Retrieve a list of students with pagination support" },
                            new { Name = "GetAsync", Method = "GET", Description = "Retrieve a specific student by ID" },
                            new { Name = "CreateAsync", Method = "POST", Description = "Create a new student record" },
                            new { Name = "UpdateAsync", Method = "PUT", Description = "Update an existing student record" },
                            new { Name = "RemoveAsync", Method = "DELETE", Description = "Remove a student record by ID" }
                        }
                    },
                    new { 
                        ServiceName = "DepartmentService", 
                        Endpoint = $"{baseUrl}/soap/department", 
                        Operations = new[] {
                            new { Name = "ListAsync", Method = "GET", Description = "Retrieve a list of departments with pagination support" },
                            new { Name = "GetAsync", Method = "GET", Description = "Retrieve a specific department by ID" },
                            new { Name = "CreateAsync", Method = "POST", Description = "Create a new department record" },
                            new { Name = "UpdateAsync", Method = "PUT", Description = "Update an existing department record" },
                            new { Name = "RemoveAsync", Method = "DELETE", Description = "Remove a department record by ID" }
                        }
                    },
                    // Additional services would follow the same pattern
                    new { 
                        ServiceName = "GenericCrudService", 
                        Endpoint = $"{baseUrl}/soap", 
                        Operations = new[] {
                            new { Name = "ListAsync", Method = "GET", Description = "Retrieve a list of records from a specified table" },
                            new { Name = "GetAsync", Method = "GET", Description = "Retrieve a specific record by ID from a specified table" },
                            new { Name = "CreateAsync", Method = "POST", Description = "Create a new record in a specified table" },
                            new { Name = "UpdateAsync", Method = "PUT", Description = "Update an existing record in a specified table" },
                            new { Name = "RemoveAsync", Method = "DELETE", Description = "Remove a record by ID from a specified table" }
                        }
                    }
                }
            });
        }
    }
}
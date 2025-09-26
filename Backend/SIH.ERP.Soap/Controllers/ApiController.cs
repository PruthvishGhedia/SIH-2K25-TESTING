using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace SIH.ERP.Soap.Controllers
{
    /// <summary>
    /// Provides direct API information and documentation endpoints
    /// </summary>
    [ApiController]
    public class ApiController : ControllerBase
    {
        /// <summary>
        /// Gets API version and build information
        /// </summary>
        /// <returns>API version details</returns>
        [HttpGet("api/version")]
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
        [HttpGet("api/documentation")]
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
        [HttpGet("api/health")]
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
        [HttpGet("api/services")]
        public IActionResult GetAllServices()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            
            return Ok(new
            {
                Message = "All SOAP services are available at the following endpoints. Use SOAP clients to interact with these endpoints.",
                BaseUrl = baseUrl,
                Services = new[]
                {
                    new { Name = "AdmissionService", Endpoint = $"{baseUrl}/soap/admission" },
                    new { Name = "StudentService", Endpoint = $"{baseUrl}/soap/student" },
                    new { Name = "DepartmentService", Endpoint = $"{baseUrl}/soap/department" },
                    new { Name = "CourseService", Endpoint = $"{baseUrl}/soap/course" },
                    new { Name = "SubjectService", Endpoint = $"{baseUrl}/soap/subject" },
                    new { Name = "RoleService", Endpoint = $"{baseUrl}/soap/role" },
                    new { Name = "UserService", Endpoint = $"{baseUrl}/soap/user" },
                    new { Name = "GuardianService", Endpoint = $"{baseUrl}/soap/guardian" },
                    new { Name = "HostelService", Endpoint = $"{baseUrl}/soap/hostel" },
                    new { Name = "RoomService", Endpoint = $"{baseUrl}/soap/room" },
                    new { Name = "HostelAllocationService", Endpoint = $"{baseUrl}/soap/hostelallocation" },
                    new { Name = "FeesService", Endpoint = $"{baseUrl}/soap/fees" },
                    new { Name = "LibraryService", Endpoint = $"{baseUrl}/soap/library" },
                    new { Name = "BookIssueService", Endpoint = $"{baseUrl}/soap/bookissue" },
                    new { Name = "ExamService", Endpoint = $"{baseUrl}/soap/exam" },
                    new { Name = "ResultService", Endpoint = $"{baseUrl}/soap/result" },
                    new { Name = "UserRoleService", Endpoint = $"{baseUrl}/soap/userrole" },
                    new { Name = "ContactDetailsService", Endpoint = $"{baseUrl}/soap/contactdetails" },
                    new { Name = "FacultyService", Endpoint = $"{baseUrl}/soap/faculty" },
                    new { Name = "EnrollmentService", Endpoint = $"{baseUrl}/soap/enrollment" },
                    new { Name = "AttendanceService", Endpoint = $"{baseUrl}/soap/attendance" },
                    new { Name = "PaymentService", Endpoint = $"{baseUrl}/soap/payment" },
                    new { Name = "GenericCrudService", Endpoint = $"{baseUrl}/soap" }
                }
            });
        }
    }
}
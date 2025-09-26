using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SIH.ERP.Soap.Hubs;
using SIH.ERP.Soap.Models;
using SIH.ERP.Soap.Repositories;

namespace SIH.ERP.Soap.Controllers;

/// <summary>
/// REST API controller for the Dashboard functionality.
/// Provides aggregated data from all other services and real-time updates.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IFeesRepository _feesRepository;
    private readonly IExamRepository _examRepository;
    private readonly IUserRepository _userRepository;
    private readonly IHubContext<DashboardHub> _hubContext;

    /// <summary>
    /// Initializes a new instance of the DashboardController class.
    /// </summary>
    /// <param name="studentRepository">The student repository for data access.</param>
    /// <param name="courseRepository">The course repository for data access.</param>
    /// <param name="departmentRepository">The department repository for data access.</param>
    /// <param name="feesRepository">The fees repository for data access.</param>
    /// <param name="examRepository">The exam repository for data access.</param>
    /// <param name="userRepository">The user repository for data access.</param>
    /// <param name="hubContext">The SignalR hub context for real-time updates.</param>
    public DashboardController(
        IStudentRepository studentRepository,
        ICourseRepository courseRepository,
        IDepartmentRepository departmentRepository,
        IFeesRepository feesRepository,
        IExamRepository examRepository,
        IUserRepository userRepository,
        IHubContext<DashboardHub> hubContext)
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _departmentRepository = departmentRepository;
        _feesRepository = feesRepository;
        _examRepository = examRepository;
        _userRepository = userRepository;
        _hubContext = hubContext;
    }

    /// <summary>
    /// Retrieves a summary of key metrics for the dashboard.
    /// </summary>
    /// <returns>A dashboard summary object with key metrics</returns>
    /// <response code="200">Returns the dashboard summary</response>
    [HttpGet("summary")]
    public async Task<ActionResult<DashboardSummary>> GetDashboardSummary()
    {
        try
        {
            var totalStudents = (await _studentRepository.ListAsync(1000, 0)).Count();
            var totalCourses = (await _courseRepository.ListAsync(1000, 0)).Count();
            var totalDepartments = (await _departmentRepository.ListAsync(1000, 0)).Count();
            var totalUsers = (await _userRepository.ListAsync(1000, 0)).Count();

            var summary = new DashboardSummary
            {
                TotalStudents = totalStudents,
                TotalCourses = totalCourses,
                TotalDepartments = totalDepartments,
                TotalUsers = totalUsers,
                LastUpdated = DateTime.UtcNow
            };

            return Ok(summary);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves statistics for the dashboard.
    /// </summary>
    /// <returns>A dashboard statistics object with detailed metrics</returns>
    /// <response code="200">Returns the dashboard statistics</response>
    [HttpGet("stats")]
    public async Task<ActionResult<DashboardStats>> GetDashboardStats()
    {
        try
        {
            // Get recent students
            var recentStudents = await _studentRepository.ListAsync(5, 0);
            
            // Get recent courses
            var recentCourses = await _courseRepository.ListAsync(5, 0);
            
            // Get department distribution
            var departments = await _departmentRepository.ListAsync(100, 0);
            var departmentStats = departments.Select(d => new DepartmentStat
            {
                DepartmentName = d.dept_name,
                StudentCount = 0 // This would need a more complex query in a real implementation
            }).ToList();

            var stats = new DashboardStats
            {
                RecentStudents = recentStudents,
                RecentCourses = recentCourses,
                DepartmentStats = departmentStats,
                LastUpdated = DateTime.UtcNow
            };

            return Ok(stats);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves recent activity for the dashboard.
    /// </summary>
    /// <returns>A collection of recent activity items</returns>
    /// <response code="200">Returns the recent activity</response>
    [HttpGet("recent-activity")]
    public async Task<ActionResult<IEnumerable<ActivityItem>>> GetRecentActivity()
    {
        try
        {
            var activities = new List<ActivityItem>();

            // Get recent students
            var recentStudents = await _studentRepository.ListAsync(3, 0);
            foreach (var student in recentStudents)
            {
                activities.Add(new ActivityItem
                {
                    Id = student.student_id.ToString(),
                    Type = "Student",
                    Action = "Created",
                    Description = $"New student {student.first_name} {student.last_name} added",
                    Timestamp = DateTime.UtcNow // This should come from the database in a real implementation
                });
            }

            // Get recent courses
            var recentCourses = await _courseRepository.ListAsync(3, 0);
            foreach (var course in recentCourses)
            {
                activities.Add(new ActivityItem
                {
                    Id = course.course_id.ToString(),
                    Type = "Course",
                    Action = "Created",
                    Description = $"New course {course.course_name} added",
                    Timestamp = DateTime.UtcNow // This should come from the database in a real implementation
                });
            }

            // Sort by timestamp descending
            var sortedActivities = activities.OrderByDescending(a => a.Timestamp).Take(10);

            return Ok(sortedActivities);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}

/// <summary>
/// Represents a dashboard summary with key metrics.
/// </summary>
public class DashboardSummary
{
    /// <summary>
    /// Gets or sets the total number of students.
    /// </summary>
    public int TotalStudents { get; set; }

    /// <summary>
    /// Gets or sets the total number of courses.
    /// </summary>
    public int TotalCourses { get; set; }

    /// <summary>
    /// Gets or sets the total number of departments.
    /// </summary>
    public int TotalDepartments { get; set; }

    /// <summary>
    /// Gets or sets the total number of users.
    /// </summary>
    public int TotalUsers { get; set; }

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime LastUpdated { get; set; }
}

/// <summary>
/// Represents detailed dashboard statistics.
/// </summary>
public class DashboardStats
{
    /// <summary>
    /// Gets or sets the recently added students.
    /// </summary>
    public IEnumerable<Student> RecentStudents { get; set; } = new List<Student>();

    /// <summary>
    /// Gets or sets the recently added courses.
    /// </summary>
    public IEnumerable<Course> RecentCourses { get; set; } = new List<Course>();

    /// <summary>
    /// Gets or sets the department statistics.
    /// </summary>
    public IEnumerable<DepartmentStat> DepartmentStats { get; set; } = new List<DepartmentStat>();

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime LastUpdated { get; set; }
}

/// <summary>
/// Represents statistics for a department.
/// </summary>
public class DepartmentStat
{
    /// <summary>
    /// Gets or sets the department name.
    /// </summary>
    public string DepartmentName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the student count in the department.
    /// </summary>
    public int StudentCount { get; set; }
}

/// <summary>
/// Represents an activity item for the dashboard.
/// </summary>
public class ActivityItem
{
    /// <summary>
    /// Gets or sets the ID of the activity item.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of the activity item.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the action performed.
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the activity.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp of the activity.
    /// </summary>
    public DateTime Timestamp { get; set; }
}
using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Web;
using DotNetEnv;
using Npgsql;
using System.Data;
using SIH.ERP.Soap.Services;
using SIH.ERP.Soap.Repositories;
using SIH.ERP.Soap.Middleware;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using Scrutor;
using SIH.ERP.Soap.Controllers;
using SIH.ERP.Soap; // Add this line for SwaggerDefaultValues
using SIH.ERP.Soap.Hubs; // Add this line for DashboardHub

var builder = WebApplication.CreateBuilder(args);

// Load environment variables early
Env.Load();

// Configure CORS
var CORS_POLICY = "SihFrontendPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(CORS_POLICY, policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Add SignalR services
builder.Services.AddSignalR();

// Add MVC controllers
builder.Services.AddControllers();

// Register scoped IDbConnection (replaces singleton)
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connStr = Environment.GetEnvironmentVariable("DATABASE_URL") ?? 
                  builder.Configuration.GetConnectionString("SIH") ?? 
                  throw new Exception("DATABASE_URL or SIH connection string not set");
    var conn = new NpgsqlConnection(connStr);
    conn.Open();
    return conn;
});

builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelWebServices(); // Add this for WebHttp support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "SIH ERP SOAP API", 
        Version = "v1",
        Description = "SOAP-based Web Services for SIH ERP System - Complete API Documentation\n\n" +
                     "## Overview\n" +
                     "This API provides comprehensive SOAP services for managing all aspects of an educational institution, " +
                     "including students, courses, departments, faculty, admissions, hostel management, library services, and more.\n\n" +
                     "## SOAP Services\n" +
                     "All SOAP services are available at their respective endpoints. Each service supports standard CRUD operations:\n" +
                     "- **ListAsync**: Retrieve a list of items with pagination\n" +
                     "- **GetAsync**: Retrieve a specific item by ID\n" +
                     "- **CreateAsync**: Create a new item\n" +
                     "- **UpdateAsync**: Update an existing item\n" +
                     "- **RemoveAsync**: Delete an item by ID\n\n" +
                     "## Available SOAP Endpoints\n" +
                     "- Admission Service: `/soap/admission`\n" +
                     "- Student Service: `/soap/student`\n" +
                     "- Department Service: `/soap/department`\n" +
                     "- Course Service: `/soap/course`\n" +
                     "- Subject Service: `/soap/subject`\n" +
                     "- Role Service: `/soap/role`\n" +
                     "- User Service: `/soap/user`\n" +
                     "- Guardian Service: `/soap/guardian`\n" +
                     "- Hostel Service: `/soap/hostel`\n" +
                     "- Room Service: `/soap/room`\n" +
                     "- Hostel Allocation Service: `/soap/hostelallocation`\n" +
                     "- Fees Service: `/soap/fees`\n" +
                     "- Library Service: `/soap/library`\n" +
                     "- Book Issue Service: `/soap/bookissue`\n" +
                     "- Exam Service: `/soap/exam`\n" +
                     "- Result Service: `/soap/result`\n" +
                     "- User Role Service: `/soap/userrole`\n" +
                     "- Contact Details Service: `/soap/contactdetails`\n" +
                     "- Faculty Service: `/soap/faculty`\n" +
                     "- Enrollment Service: `/soap/enrollment`\n" +
                     "- Attendance Service: `/soap/attendance`\n" +
                     "- Payment Service: `/soap/payment`\n" +
                     "- Generic CRUD Service: `/soap` (restricted access)\n\n" +
                     "## REST API Endpoints\n" +
                     "This API also provides REST endpoints for API information, health checks, and dashboard functionality:\n" +
                     "- API Version: `/api/version`\n" +
                     "- API Documentation: `/api/documentation`\n" +
                     "- Health Check: `/api/health`\n" +
                     "- Services List: `/api/services`\n" +
                     "- Services Details: `/api/services/details`\n" +
                     "- Dashboard Summary: `/api/dashboard/summary`\n" +
                     "- Dashboard Statistics: `/api/dashboard/stats`\n" +
                     "- Dashboard Recent Activity: `/api/dashboard/recent-activity`\n" +
                     "- Dashboard Real-time Updates: `/api/dashboard/hub` (SignalR hub)\n\n" +
                     "## Authentication\n" +
                     "The API uses Basic Authentication for secured endpoints. Credentials must be provided in the Authorization header.\n\n" +
                     "## Data Flow\n" +
                     "All data in this system flows from various sources:\n" +
                     "- **Student Data**: Admission forms, enrollment records, and student self-service portals\n" +
                     "- **Academic Data**: Course catalogs, faculty assignments, and examination results\n" +
                     "- **Financial Data**: Fee structures, payment processing, and financial reports\n" +
                     "- **Facility Data**: Hostel management, room allocations, and library resources\n" +
                     "- **Administrative Data**: User accounts, roles, and system configurations",
        Contact = new OpenApiContact
        {
            Name = "SIH ERP Team",
            Email = "support@sih-erp.com",
            Url = new Uri("https://github.com/SIH-ERP")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://github.com/SIH-ERP/license")
        }
    });
    
    // Add XML comments support
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
    
    // Add security definition for SOAP services
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Bearer scheme."
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            new string[] {}
        }
    });
    
    // Group operations by service type for better organization
    c.TagActionsBy(api =>
    {
        if (api.RelativePath != null)
        {
            var route = api.RelativePath.ToLower();
            
            if (route.Contains("/soap/admission")) return new[] { "Admission APIs" };
            if (route.Contains("/soap/student")) return new[] { "Student APIs" };
            if (route.Contains("/soap/department")) return new[] { "Department APIs" };
            if (route.Contains("/soap/course")) return new[] { "Course APIs" };
            if (route.Contains("/soap/subject")) return new[] { "Subject APIs" };
            if (route.Contains("/soap/role")) return new[] { "Role APIs" };
            if (route.Contains("/soap/user")) return new[] { "User APIs" };
            if (route.Contains("/soap/guardian")) return new[] { "Guardian APIs" };
            if (route.Contains("/soap/hostel")) return new[] { "Hostel APIs" };
            if (route.Contains("/soap/room")) return new[] { "Room APIs" };
            if (route.Contains("/soap/hostelallocation")) return new[] { "Hostel Allocation APIs" };
            if (route.Contains("/soap/fees")) return new[] { "Fees APIs" };
            if (route.Contains("/soap/library")) return new[] { "Library APIs" };
            if (route.Contains("/soap/bookissue")) return new[] { "Book Issue APIs" };
            if (route.Contains("/soap/exam")) return new[] { "Exam APIs" };
            if (route.Contains("/soap/result")) return new[] { "Result APIs" };
            if (route.Contains("/soap/userrole")) return new[] { "User Role APIs" };
            if (route.Contains("/soap/contactdetails")) return new[] { "Contact Details APIs" };
            if (route.Contains("/soap/faculty")) return new[] { "Faculty APIs" };
            if (route.Contains("/soap/enrollment")) return new[] { "Enrollment APIs" };
            if (route.Contains("/soap/attendance")) return new[] { "Attendance APIs" };
            if (route.Contains("/soap/payment")) return new[] { "Payment APIs" };
            if (route.Contains("/soap")) return new[] { "Generic CRUD APIs" };
            if (route.Contains("/api/dashboard")) return new[] { "Dashboard APIs" };
        }
        
        return new[] { "General APIs" };
    });
    
    c.DocInclusionPredicate((name, api) => true);
    
    // Add custom schema mapping for common types
    c.CustomSchemaIds(type => type.ToString());
    
    // Add SwaggerDefaultValues operation filter
    c.OperationFilter<SwaggerDefaultValues>();
});

// Auto-register repositories with Scrutor (scoped lifetime)
builder.Services.Scan(scan => scan
    .FromAssemblyOf<StudentRepository>()
    .AddClasses(classes => classes
        .Where(type => type.Name.EndsWith("Repository"))
        .Where(type => !type.Name.Contains("GenericCrud")))
    .AsImplementedInterfaces()
    .WithScopedLifetime());

// Register services with their dependencies
builder.Services.AddScoped<SIH.ERP.Soap.Services.StudentService>();
builder.Services.AddScoped<SIH.ERP.Soap.Services.CourseService>();
builder.Services.AddScoped<SIH.ERP.Soap.Services.DepartmentService>();
builder.Services.AddScoped<SIH.ERP.Soap.Services.UserService>();
builder.Services.AddScoped<SIH.ERP.Soap.Services.FeesService>();
builder.Services.AddScoped<SIH.ERP.Soap.Services.ExamService>();

// Register GenericCrudService separately (needs special handling)
builder.Services.AddScoped<GenericCrudService>();

var app = builder.Build();

// Add CORS
app.UseCors(CORS_POLICY);

// Add error handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

// Add SignalR hub
app.MapHub<DashboardHub>("/api/dashboard/hub");

// Add MVC controllers
app.MapControllers();

app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<GenericCrudService>();
    serviceBuilder.AddServiceEndpoint<GenericCrudService, IGenericCrud>(new BasicHttpBinding(), "/soap");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.DepartmentService>();
    serviceBuilder.AddServiceEndpoint<SIH.ERP.Soap.Services.DepartmentService, SIH.ERP.Soap.Contracts.IDepartmentService>(new WebHttpBinding(), "/soap/department");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.RoleService>();
    serviceBuilder.AddServiceEndpoint<RoleService, SIH.ERP.Soap.Contracts.IRoleService>(new WebHttpBinding(), "/soap/role");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.CourseService>();
    serviceBuilder.AddServiceEndpoint<SIH.ERP.Soap.Services.CourseService, SIH.ERP.Soap.Contracts.ICourseService>(new WebHttpBinding(), "/soap/course");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.SubjectService>();
    serviceBuilder.AddServiceEndpoint<SubjectService, SIH.ERP.Soap.Contracts.ISubjectService>(new WebHttpBinding(), "/soap/subject");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.StudentService>();
    serviceBuilder.AddServiceEndpoint<SIH.ERP.Soap.Services.StudentService, SIH.ERP.Soap.Contracts.IStudentService>(new WebHttpBinding(), "/soap/student");
    
    // New service endpoints
    serviceBuilder.AddService<SIH.ERP.Soap.Services.GuardianService>();
    serviceBuilder.AddServiceEndpoint<GuardianService, SIH.ERP.Soap.Contracts.IGuardianService>(new WebHttpBinding(), "/soap/guardian");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.AdmissionService>();
    serviceBuilder.AddServiceEndpoint<AdmissionService, SIH.ERP.Soap.Contracts.IAdmissionService>(new WebHttpBinding(), "/soap/admission");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.HostelService>();
    serviceBuilder.AddServiceEndpoint<HostelService, SIH.ERP.Soap.Contracts.IHostelService>(new WebHttpBinding(), "/soap/hostel");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.RoomService>();
    serviceBuilder.AddServiceEndpoint<RoomService, SIH.ERP.Soap.Contracts.IRoomService>(new WebHttpBinding(), "/soap/room");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.HostelAllocationService>();
    serviceBuilder.AddServiceEndpoint<HostelAllocationService, SIH.ERP.Soap.Contracts.IHostelAllocationService>(new WebHttpBinding(), "/soap/hostelallocation");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.FeesService>();
    serviceBuilder.AddServiceEndpoint<SIH.ERP.Soap.Services.FeesService, SIH.ERP.Soap.Contracts.IFeesService>(new WebHttpBinding(), "/soap/fees");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.LibraryService>();
    serviceBuilder.AddServiceEndpoint<LibraryService, SIH.ERP.Soap.Contracts.ILibraryService>(new WebHttpBinding(), "/soap/library");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.BookIssueService>();
    serviceBuilder.AddServiceEndpoint<BookIssueService, SIH.ERP.Soap.Contracts.IBookIssueService>(new WebHttpBinding(), "/soap/bookissue");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.ExamService>();
    serviceBuilder.AddServiceEndpoint<SIH.ERP.Soap.Services.ExamService, SIH.ERP.Soap.Contracts.IExamService>(new WebHttpBinding(), "/soap/exam");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.ResultService>();
    serviceBuilder.AddServiceEndpoint<ResultService, SIH.ERP.Soap.Contracts.IResultService>(new WebHttpBinding(), "/soap/result");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.UserService>();
    serviceBuilder.AddServiceEndpoint<SIH.ERP.Soap.Services.UserService, SIH.ERP.Soap.Contracts.IUserService>(new WebHttpBinding(), "/soap/user");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.UserRoleService>();
    serviceBuilder.AddServiceEndpoint<UserRoleService, SIH.ERP.Soap.Contracts.IUserRoleService>(new WebHttpBinding(), "/soap/userrole");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.ContactDetailsService>();
    serviceBuilder.AddServiceEndpoint<ContactDetailsService, SIH.ERP.Soap.Contracts.IContactDetailsService>(new WebHttpBinding(), "/soap/contactdetails");

    // Stage 1.2 services
    serviceBuilder.AddService<SIH.ERP.Soap.Services.FacultyService>();
    serviceBuilder.AddServiceEndpoint<FacultyService, SIH.ERP.Soap.Contracts.IFacultyService>(new WebHttpBinding(), "/soap/faculty");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.EnrollmentService>();
    serviceBuilder.AddServiceEndpoint<EnrollmentService, SIH.ERP.Soap.Contracts.IEnrollmentService>(new WebHttpBinding(), "/soap/enrollment");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.AttendanceService>();
    serviceBuilder.AddServiceEndpoint<AttendanceService, SIH.ERP.Soap.Contracts.IAttendanceService>(new WebHttpBinding(), "/soap/attendance");
    serviceBuilder.AddService<SIH.ERP.Soap.Services.PaymentService>();
    serviceBuilder.AddServiceEndpoint<PaymentService, SIH.ERP.Soap.Contracts.IPaymentService>(new WebHttpBinding(), "/soap/payment");
    
    // WebHttpBehavior configuration removed as it's not compatible with current CoreWCF version
});

// Enable middleware to serve generated Swagger as a JSON endpoint
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SIH ERP SOAP API v1");
    c.RoutePrefix = "swagger";
    c.DocumentTitle = "SIH ERP SOAP API Documentation";
    c.DefaultModelsExpandDepth(-1); // Hide schemas by default
    c.DisplayOperationId();
    c.DisplayRequestDuration();
    c.EnableDeepLinking();
    
    // Additional UI configuration for better usability
    c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Example);
    c.ShowExtensions();
    c.ShowCommonExtensions();
    c.EnableValidator();
    
    // Ensure all tags are expanded by default to show all APIs
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
});

app.Run();

// Make Program class public for testing
public partial class Program { }
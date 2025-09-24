using CoreWCF;
using CoreWCF.Configuration;
using DotNetEnv;
using Npgsql;
using System.Data;
using SIH.ERP.Soap.Services;
using SIH.ERP.Soap.Repositories;
using SIH.ERP.Soap.Middleware;
using Microsoft.OpenApi.Models;
using Scrutor;
using Microsoft.Extensions.Diagnostics.HealthChecks;

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

// Add health checks
builder.Services.AddHealthChecks()
    .AddNpgSql(Environment.GetEnvironmentVariable("DATABASE_URL") ?? 
               builder.Configuration.GetConnectionString("SIH") ?? 
               throw new Exception("DATABASE_URL or SIH connection string not set"));

builder.Services.AddServiceModelServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SIH ERP SOAP/Health", Version = "v1" });
});

// Auto-register repositories and services with Scrutor (scoped lifetime)
builder.Services.Scan(scan => scan
    .FromAssemblyOf<StudentRepository>()
    .AddClasses(classes => classes
        .Where(type => type.Name.EndsWith("Repository") || type.Name.EndsWith("Service"))
        .Where(type => !type.Name.Contains("GenericCrud")))
    .AsImplementedInterfaces()
    .WithScopedLifetime());

// Register GenericCrudService separately (needs special handling)
builder.Services.AddScoped<GenericCrudService>();

var app = builder.Build();

// Add CORS
app.UseCors(CORS_POLICY);

// Add error handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<GenericCrudService>();
    serviceBuilder.AddServiceEndpoint<GenericCrudService, IGenericCrud>(new BasicHttpBinding(), "/soap");
    serviceBuilder.AddService<DepartmentService>();
    serviceBuilder.AddServiceEndpoint<DepartmentService, SIH.ERP.Soap.Contracts.IDepartmentService>(new BasicHttpBinding(), "/soap/department");
    serviceBuilder.AddService<RoleService>();
    serviceBuilder.AddServiceEndpoint<RoleService, SIH.ERP.Soap.Contracts.IRoleService>(new BasicHttpBinding(), "/soap/role");
    serviceBuilder.AddService<CourseService>();
    serviceBuilder.AddServiceEndpoint<CourseService, SIH.ERP.Soap.Contracts.ICourseService>(new BasicHttpBinding(), "/soap/course");
    serviceBuilder.AddService<SubjectService>();
    serviceBuilder.AddServiceEndpoint<SubjectService, SIH.ERP.Soap.Contracts.ISubjectService>(new BasicHttpBinding(), "/soap/subject");
    serviceBuilder.AddService<StudentService>();
    serviceBuilder.AddServiceEndpoint<StudentService, SIH.ERP.Soap.Contracts.IStudentService>(new BasicHttpBinding(), "/soap/student");
    
    // New service endpoints
    serviceBuilder.AddService<GuardianService>();
    serviceBuilder.AddServiceEndpoint<GuardianService, SIH.ERP.Soap.Contracts.IGuardianService>(new BasicHttpBinding(), "/soap/guardian");
    serviceBuilder.AddService<AdmissionService>();
    serviceBuilder.AddServiceEndpoint<AdmissionService, SIH.ERP.Soap.Contracts.IAdmissionService>(new BasicHttpBinding(), "/soap/admission");
    serviceBuilder.AddService<HostelService>();
    serviceBuilder.AddServiceEndpoint<HostelService, SIH.ERP.Soap.Contracts.IHostelService>(new BasicHttpBinding(), "/soap/hostel");
    serviceBuilder.AddService<RoomService>();
    serviceBuilder.AddServiceEndpoint<RoomService, SIH.ERP.Soap.Contracts.IRoomService>(new BasicHttpBinding(), "/soap/room");
    serviceBuilder.AddService<HostelAllocationService>();
    serviceBuilder.AddServiceEndpoint<HostelAllocationService, SIH.ERP.Soap.Contracts.IHostelAllocationService>(new BasicHttpBinding(), "/soap/hostelallocation");
    serviceBuilder.AddService<FeesService>();
    serviceBuilder.AddServiceEndpoint<FeesService, SIH.ERP.Soap.Contracts.IFeesService>(new BasicHttpBinding(), "/soap/fees");
    serviceBuilder.AddService<LibraryService>();
    serviceBuilder.AddServiceEndpoint<LibraryService, SIH.ERP.Soap.Contracts.ILibraryService>(new BasicHttpBinding(), "/soap/library");
    serviceBuilder.AddService<BookIssueService>();
    serviceBuilder.AddServiceEndpoint<BookIssueService, SIH.ERP.Soap.Contracts.IBookIssueService>(new BasicHttpBinding(), "/soap/bookissue");
    serviceBuilder.AddService<ExamService>();
    serviceBuilder.AddServiceEndpoint<ExamService, SIH.ERP.Soap.Contracts.IExamService>(new BasicHttpBinding(), "/soap/exam");
    serviceBuilder.AddService<ResultService>();
    serviceBuilder.AddServiceEndpoint<ResultService, SIH.ERP.Soap.Contracts.IResultService>(new BasicHttpBinding(), "/soap/result");
    serviceBuilder.AddService<UserService>();
    serviceBuilder.AddServiceEndpoint<UserService, SIH.ERP.Soap.Contracts.IUserService>(new BasicHttpBinding(), "/soap/user");
    serviceBuilder.AddService<UserRoleService>();
    serviceBuilder.AddServiceEndpoint<UserRoleService, SIH.ERP.Soap.Contracts.IUserRoleService>(new BasicHttpBinding(), "/soap/userrole");
    serviceBuilder.AddService<ContactDetailsService>();
    serviceBuilder.AddServiceEndpoint<ContactDetailsService, SIH.ERP.Soap.Contracts.IContactDetailsService>(new BasicHttpBinding(), "/soap/contactdetails");

    // Stage 1.2 services
    serviceBuilder.AddService<FacultyService>();
    serviceBuilder.AddServiceEndpoint<FacultyService, SIH.ERP.Soap.Contracts.IFacultyService>(new BasicHttpBinding(), "/soap/faculty");
    serviceBuilder.AddService<EnrollmentService>();
    serviceBuilder.AddServiceEndpoint<EnrollmentService, SIH.ERP.Soap.Contracts.IEnrollmentService>(new BasicHttpBinding(), "/soap/enrollment");
    serviceBuilder.AddService<AttendanceService>();
    serviceBuilder.AddServiceEndpoint<AttendanceService, SIH.ERP.Soap.Contracts.IAttendanceService>(new BasicHttpBinding(), "/soap/attendance");
    serviceBuilder.AddService<PaymentService>();
    serviceBuilder.AddServiceEndpoint<PaymentService, SIH.ERP.Soap.Contracts.IPaymentService>(new BasicHttpBinding(), "/soap/payment");
});

// Map health checks endpoint
app.MapHealthChecks("/health");

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
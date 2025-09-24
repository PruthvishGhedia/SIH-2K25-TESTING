using CoreWCF;
using CoreWCF.Configuration;
using DotNetEnv;
using Npgsql;
using System.Data;
using SIH.ERP.Soap.Services;
using SIH.ERP.Soap.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
Env.Load();

builder.Services.AddServiceModelServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SIH ERP SOAP/Health", Version = "v1" });
});
builder.Services.AddSingleton<IDbConnection>(_ => {
    var cs = Environment.GetEnvironmentVariable("DATABASE_URL") ?? throw new Exception("DATABASE_URL not set");
    var conn = new NpgsqlConnection(cs);
    return conn;
});

// Existing services
builder.Services.AddSingleton<GenericCrudService>();
builder.Services.AddSingleton<DepartmentRepository>();
builder.Services.AddSingleton<DepartmentService>();
builder.Services.AddSingleton<RoleRepository>();
builder.Services.AddSingleton<RoleService>();
builder.Services.AddSingleton<CourseRepository>();
builder.Services.AddSingleton<CourseService>();
builder.Services.AddSingleton<SubjectRepository>();
builder.Services.AddSingleton<SubjectService>();
builder.Services.AddSingleton<StudentRepository>();
builder.Services.AddSingleton<StudentService>();

// New services
builder.Services.AddSingleton<GuardianRepository>();
builder.Services.AddSingleton<GuardianService>();
builder.Services.AddSingleton<AdmissionRepository>();
builder.Services.AddSingleton<AdmissionService>();
builder.Services.AddSingleton<HostelRepository>();
builder.Services.AddSingleton<HostelService>();
builder.Services.AddSingleton<RoomRepository>();
builder.Services.AddSingleton<RoomService>();
builder.Services.AddSingleton<HostelAllocationRepository>();
builder.Services.AddSingleton<HostelAllocationService>();
builder.Services.AddSingleton<FeesRepository>();
builder.Services.AddSingleton<FeesService>();
builder.Services.AddSingleton<LibraryRepository>();
builder.Services.AddSingleton<LibraryService>();
builder.Services.AddSingleton<BookIssueRepository>();
builder.Services.AddSingleton<BookIssueService>();
builder.Services.AddSingleton<ExamRepository>();
builder.Services.AddSingleton<ExamService>();
builder.Services.AddSingleton<ResultRepository>();
builder.Services.AddSingleton<ResultService>();
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<UserRoleRepository>();
builder.Services.AddSingleton<UserRoleService>();
builder.Services.AddSingleton<ContactDetailsRepository>();
builder.Services.AddSingleton<ContactDetailsService>();

var app = builder.Build();

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
});

app.MapGet("/health", () => Results.Ok(new { status = "ok", time = DateTime.UtcNow }));

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Scrutor;
using SIH.ERP.Soap.Repositories;
using SIH.ERP.Soap.Services;
using System.Data;
using System.IO;
using System.Reflection;
using Npgsql;
using DotNetEnv;
using SIH.ERP.Soap.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables
DotNetEnv.Env.Load();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "SIH ERP API",
        Description = "A comprehensive REST API for managing educational institutions",
        Contact = new OpenApiContact
        {
            Name = "SIH ERP Team",
            Email = "support@siherp.com",
            Url = new Uri("https://siherp.com")
        }
    });
    
    options.OperationFilter<SIH.ERP.Soap.SwaggerDefaultValues>();
    
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// Register database connection
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? 
                          builder.Configuration.GetConnectionString("DefaultConnection");
    return new NpgsqlConnection(connectionString);
});

// Register repositories and services using Scrutor
builder.Services.Scan(scan => scan
    .FromAssemblyOf<Program>()
    .AddClasses(classes => classes.AssignableTo<IRepository>())
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);

builder.Services.Scan(scan => scan
    .FromAssemblyOf<Program>()
    .AddClasses(classes => classes.AssignableTo<IService>())
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);

// Add SignalR for real-time updates
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SIH ERP API V1");
        c.RoutePrefix = "swagger"; // Swagger UI at '/swagger'
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List); // Expand all by default
    });
}
else
{
    // Also enable Swagger in production for testing purposes
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SIH ERP API V1");
        c.RoutePrefix = "swagger"; // Swagger UI at '/swagger'
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List); // Expand all by default
    });
}

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SIH ERP API V1");
    c.RoutePrefix = "swagger"; // Swagger UI at '/swagger'
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List); // Expand all by default
});

// Only use HTTPS redirection in production environment
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();
app.MapHub<DashboardHub>("/dashboard");

app.Run();
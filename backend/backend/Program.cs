using backend.Data;
using backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1 Define a CORS policy name
const string AllowDevClient = "AllowDevClient";

// 2 Register CORS BEFORE Build()
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowDevClient, policy =>
    {
        policy
            .WithOrigins("http://localhost:5173") // Vite React app
            .AllowAnyHeader()
            .AllowAnyMethod()
        // Expose this custom header so the browser lets JS read it
         .WithExposedHeaders("X-Total-Count");
    });
});

// Add services to the container.

// Registering DataContext
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Registering Services
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Simple health-check endpoint to verify API availability
app.MapGet("/health", () => Results.Ok("Healthy"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//3 Enable CORS Before Authorization & MapControllers
app.UseCors(AllowDevClient);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

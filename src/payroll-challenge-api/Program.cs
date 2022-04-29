using payroll_challenge_api.Config;
using payroll_challenge_api.Db;
using payroll_challenge_api.Dependents;
using payroll_challenge_api.Employees;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>
{
    options.Filters.Add<NotFoundExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<EmployeeManagerService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<DependentService>();
builder.Services.AddScoped<IDependentRepository, DependentRepository>();

builder.UseEmployeeContext();

var app = builder.Build();

app.Services.CreateDatabase();

app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
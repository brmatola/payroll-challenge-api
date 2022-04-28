using payroll_challenge_api.Config;
using payroll_challenge_api.Dependents;
using payroll_challenge_api.Employees;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>
{
    options.Filters.Add<NotFoundExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<DependentService>();

builder.UseEmployeeContext();

var app = builder.Build();

app.Services.CreateDatabase();

app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
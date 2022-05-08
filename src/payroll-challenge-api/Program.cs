using System.Text.Json.Serialization;
using payroll_challenge_api.Benefits;
using payroll_challenge_api.Benefits.Dependents;
using payroll_challenge_api.Benefits.Employees;
using payroll_challenge_api.Config;
using payroll_challenge_api.Db;
using payroll_challenge_api.Dependents;
using payroll_challenge_api.Employees;
using payroll_challenge_api.Pay;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>
{
    options.Filters.Add<NotFoundExceptionFilter>();
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<EmployeeManagerService>();
builder.Services.AddScoped<EmployeeBenefitService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeBenefitProviderFactory, EmployeeBenefitProviderFactory>();
builder.Services.AddScoped<IDependentBenefitProviderFactory, DependentBenefitProviderFactory>();
builder.Services.AddScoped<IEmployeePayProvider, EqualEmployeePayProvider>();
builder.Services.AddScoped<IPayConverter, BiMonthlyPayConverter>();

builder.Services.AddScoped<DependentService>();
builder.Services.AddScoped<IDependentRepository, DependentRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policyBuilder =>
    {
        var origin = builder.Configuration["FrontendOrigin"];
        if (!string.IsNullOrEmpty(origin))
        {
            policyBuilder
                .WithOrigins(origin)
                .AllowAnyMethod()
                .AllowAnyHeader();
        }
        
    });
});

builder.UseEmployeeContext();

var app = builder.Build();

app.Services.CreateDatabase();

app.UseDeveloperExceptionPage();
app.UseCors("Frontend");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
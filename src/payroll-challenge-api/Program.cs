using Microsoft.EntityFrameworkCore;
using payroll_challenge_api.Config;
using payroll_challenge_api.Db;
using payroll_challenge_api.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<EmployeeService>();
builder.UseEmployeeContext();


var app = builder.Build();

app.CreateDatabase();

app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
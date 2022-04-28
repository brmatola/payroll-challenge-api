using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using payroll_challenge_api.Employees.Model;

namespace payroll_challenge_api.Employees;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeesController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public IEnumerable<EmployeeViewModel> GetAll()
    {
        return _employeeService.GetEmployees();
    }
}
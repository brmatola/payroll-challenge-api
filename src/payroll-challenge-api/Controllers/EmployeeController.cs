using Microsoft.AspNetCore.Mvc;
using payroll_challenge_api.Services;

namespace payroll_challenge_api.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public IEnumerable<EmployeeViewModel> GetAll()
    {
        return _employeeService.GetEmployees();
    }
}
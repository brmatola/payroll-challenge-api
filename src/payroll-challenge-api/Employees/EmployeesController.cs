using Microsoft.AspNetCore.Mvc;
using payroll_challenge_api.Employees.Model;

namespace payroll_challenge_api.Employees;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeManagerService _employeeManagerService;

    public EmployeesController(EmployeeManagerService employeeManagerService)
    {
        _employeeManagerService = employeeManagerService;
    }

    [HttpGet]
    public Task<IEnumerable<EmployeeViewModel>> GetAll()
    {
        return _employeeManagerService.GetEmployees();
    }

    [HttpGet("{id:guid}")]
    public Task<EmployeeViewModel> Get(Guid id)
    {
        return _employeeManagerService.GetById(id);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeViewModel>> Add([FromBody] NewEmployeeRequest employee)
    {
        var newEmployee = await _employeeManagerService.Add(employee.Name);
        return Created($"/employees/{newEmployee.Id}", newEmployee);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _employeeManagerService.DeleteById(id);
        return Ok();
    }
}
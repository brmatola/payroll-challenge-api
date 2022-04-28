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

    [HttpGet("{id:guid}")]
    public Task<EmployeeViewModel> Get(Guid id)
    {
        return _employeeService.GetById(id);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeViewModel>> Add([FromBody] NewEmployeeRequest employee)
    {
        var newEmployee = await _employeeService.Add(employee.Name);
        return Created($"/employees/{newEmployee.Id}", newEmployee);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _employeeService.DeleteById(id);
        return Ok();
    }
}
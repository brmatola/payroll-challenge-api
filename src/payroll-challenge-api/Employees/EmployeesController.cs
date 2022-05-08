using Microsoft.AspNetCore.Mvc;
using payroll_challenge_api.Employees.Model;

namespace payroll_challenge_api.Employees;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeManagerService _employeeManagerService;
    private readonly EmployeeBenefitService _employeeBenefitService;

    public EmployeesController(EmployeeManagerService employeeManagerService, EmployeeBenefitService employeeBenefitService)
    {
        _employeeManagerService = employeeManagerService;
        _employeeBenefitService = employeeBenefitService;
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

    [HttpGet("{id:guid}/benefit_cost")]
    public Task<BenefitCostResponse> GetBenefitCost(Guid id, [FromQuery] TimePeriod timePeriod)
    {
        return _employeeBenefitService.GetBenefitCost(id);
    }

    [HttpGet("{id:guid}/paycheck")]
    public Task<BenefitCostResponse> GetPaycheck(Guid id, [FromQuery] TimePeriod timePeriod)
    {
        return _employeeBenefitService.GetPaycheck(id);
    }
}
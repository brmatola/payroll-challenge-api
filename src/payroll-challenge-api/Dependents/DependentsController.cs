using Microsoft.AspNetCore.Mvc;
using payroll_challenge_api.Dependents.Model;

namespace payroll_challenge_api.Dependents;

[ApiController]
[Route("/employees/{employeeId:guid}/dependents")]
public class DependentsController : ControllerBase
{
    private readonly DependentService _dependentService;

    public DependentsController(DependentService dependentService)
    {
        _dependentService = dependentService;
    }

    [HttpGet]
    public Task<IEnumerable<DependentViewModel>> GetAll(Guid employeeId)
    {
        return _dependentService.GetDependents(employeeId);
    }
}
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
    public async Task<IEnumerable<DependentViewModel>> GetAll(Guid employeeId)
    {
        return await _dependentService.GetDependents(employeeId);
    }

    [HttpGet("/dependents/{dependentId:guid}")]
    public async Task<DependentViewModel> Get(Guid dependentId)
    {
        return await _dependentService.GetById(dependentId);
    }

    [HttpDelete("/dependents/{dependentId:guid}")]
    public async Task<ActionResult> Delete(Guid dependentId)
    {
        await _dependentService.DeleteById(dependentId);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<DependentViewModel>> Create(Guid employeeId, [FromBody] NewDependentRequest newDependentRequest)
    {
        var dependent = await _dependentService.AddDependent(employeeId, newDependentRequest.Name);
        return Created($"/employees/{employeeId}/dependents/{dependent.Id}", dependent);
    }
}
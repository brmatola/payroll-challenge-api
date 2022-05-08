using System.ComponentModel.DataAnnotations;

namespace payroll_challenge_api.Dependents.Model;

public class NewDependentRequest
{
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }
}
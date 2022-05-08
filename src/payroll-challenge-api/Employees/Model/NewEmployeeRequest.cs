using System.ComponentModel.DataAnnotations;

namespace payroll_challenge_api.Employees.Model;

public class NewEmployeeRequest
{
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }
}
namespace payroll_challenge_api.Db;

public class Dependent
{
    public Guid DependentId { get; set; }
    public string Name { get; set; }
    
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
}
namespace payroll_challenge_api.Units;

public class DollarsPerBiWeek
{
    public DollarsPerBiWeek(double value)
    {
        Value = value;
    }
    
    public double Value { get; }

    public DollarsPerYear PerYear()
    {
        return new DollarsPerYear(Value * 26);
    }
}
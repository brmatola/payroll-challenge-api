namespace payroll_challenge_api.Units;

public class DollarsPerYear
{
    public DollarsPerYear(double value)
    {
        Value = value;
    }
    
    public double Value { get; }

    public static DollarsPerYear operator +(DollarsPerYear a, DollarsPerYear b)
    {
        return new DollarsPerYear(a.Value + b.Value);
    }
}
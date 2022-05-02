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

    public static DollarsPerYear operator -(DollarsPerYear a, DollarsPerYear b)
    {
        var amount = a.Value - b.Value;
        if (amount < 0) throw new ArgumentException("Cannot have negative dollars per year");
        return new DollarsPerYear(amount);
    }
}
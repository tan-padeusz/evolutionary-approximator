namespace Genetics;

using Structs;

public class RealGene
{
    private static Random Random { get; } = new Random();
    public double Value { get; }

    public RealGene(ApproximatorJob job)
    {
        var maxValue = Math.Pow(10, job.PrecisionDigits);
        this.Value = Math.Round(RealGene.Random.NextDouble() * 2 * maxValue - maxValue, job.PrecisionDigits);
    }

    private RealGene(double value)
    {
        this.Value = value;
    }
    
    public RealGene Identical()
    {
        return new RealGene(this.Value);
    }

    public RealGene Mutated(ApproximatorJob job)
    {
        var constant = Math.Pow(10, -job.PrecisionDigits) * job.MutationDelta;
        var variable = job.MutationDelta / 100.0 * this.Value;
        var delta = RealGene.Random.Next() % 2 == 0 ? (constant + variable) : -(constant + variable);
        return new RealGene(this.Value + delta);
    }
}
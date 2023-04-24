namespace Genetics;

using Structs;

public class RealGene
{
    private static Random Random { get; } = new Random();
    public double Value { get; }

    public RealGene(ApproximatorJob job)
    {
        var value = 0.0;
        for (var tenPower = 0; tenPower >= -job.PrecisionDigits; tenPower--)
            value += (RealGene.Random.Next(19) - 9) * Math.Pow(10, tenPower);
        this.Value = value;
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
        var delta = RealGene.Random.NextDouble() * 2 - 1;
        return new RealGene(this.Value + delta);
    }
}
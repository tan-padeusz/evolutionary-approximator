namespace Genetics;

using Data;

public readonly struct RealGene
{
    private static Random Random { get; } = new Random();
    public double Value { get; }

    public RealGene(ApproximatorJob job)
    {
        this.Value = RealGene.Random.NextDouble() * 2 - 1;
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
        var delta = 1.0 / Math.Pow(10, job.PrecisionDigits);
        delta *= RealGene.Random.Next() % 2 == 1 ? -1 : 1;
        return new RealGene(this.Value + delta);
    }
}
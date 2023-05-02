namespace Genetics;

using Data;

public class RealGene: Gene
{
    public double Value { get; }

    public RealGene()
    {
        this.Value = Gene.Random.NextDouble() * 2 - 1;
    }

    private RealGene(double value)
    {
        this.Value = value;
    }
    
    public override Gene Identical()
    {
        return new RealGene(this.Value);
    }

    public override Gene Mutated(ApproximatorJob job)
    {
        var delta = 1.0 / Math.Pow(10, job.PrecisionDigits);
        delta *= Gene.Random.Next() % 2 == 1 ? -1 : 1;
        return new RealGene(this.Value + delta);
    }
}
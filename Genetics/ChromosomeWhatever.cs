using Data;

namespace Genetics;

public abstract class ChromosomeWhatever
{
    public int FactorCount { get; }
    public int GenesPerFactor { get; protected init; }
    protected int MaxPolynomialDegree { get; }
    protected int PrecisionDigits { get; }

    protected ChromosomeWhatever(ApproximatorJob job)
    {
        this.FactorCount = (job.MaxPolynomialDegree + 1) * (job.MaxPolynomialDegree + 2) / 2;
        this.MaxPolynomialDegree = job.MaxPolynomialDegree;
        this.PrecisionDigits = job.PrecisionDigits;
    }
    
    public abstract Gene NewRandomGene();
    public abstract double[][] Decode(Gene[] genes);

    protected static Gene[] GetPart(Gene[] source, int from, int count)
    {
        var target = new Gene[count];
        for (var index = 0; index < count; index++)
            target[index] = source[from + index];
        return target;
    }
}
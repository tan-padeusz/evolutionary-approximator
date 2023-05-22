using Enums;

namespace Data;

public struct ApproximatorJob
{
    
    public int DominantParentGeneStrength { get; }
    public ErrorMetric ErrorMetric { get; }
    public GeneType GeneType { get; }
    public int MaxPolynomialDegree { get; }
    public int MutationProbability { get; }
    public int ParentPoolSize { get; }
    public int PopulationSize { get; }
    public int PrecisionDigits { get; }

    public ApproximatorJob
    (
        int dominantParentGeneStrength,
        ErrorMetric errorMetric,
        GeneType geneType,
        int maxPolynomialDegree,
        int mutationProbability,
        int parentPoolSize,
        int populationSize,
        int precisionDigits
    )
    {
        this.DominantParentGeneStrength = dominantParentGeneStrength;
        this.ErrorMetric = errorMetric;
        this.GeneType = geneType;
        this.MaxPolynomialDegree = maxPolynomialDegree;
        this.MutationProbability = mutationProbability;
        this.ParentPoolSize = parentPoolSize;
        this.PopulationSize = populationSize;
        this.PrecisionDigits = precisionDigits;
    }
}
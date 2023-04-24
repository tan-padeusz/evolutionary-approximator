using Enums;

namespace Structs;

public struct ApproximatorJob
{
    public int Contestants { get; }
    public int DominantParentGeneStrength { get; }
    public ErrorMetric ErrorMetric { get; }
    public GeneType GeneType { get; }
    public int MaxPolynomialDegree { get; }
    public int MutationDelta { get; }
    public int MutationProbability { get; }
    public PointFunction PointFunction { get; }
    public int PopulationSize { get; }
    public int PrecisionDigits { get; }

    public ApproximatorJob
    (
        int contestants,
        int dominantParentGeneStrength,
        ErrorMetric errorMetric,
        GeneType geneType,
        int maxPolynomialDegree,
        int mutationDelta,
        int mutationProbability,
        PointFunction pointFunction,
        int populationSize,
        int precisionDigits
    )
    {
        this.Contestants = contestants;
        this.DominantParentGeneStrength = dominantParentGeneStrength;
        this.ErrorMetric = errorMetric;
        this.GeneType = geneType;
        this.MaxPolynomialDegree = maxPolynomialDegree;
        this.MutationDelta = mutationDelta;
        this.MutationProbability = mutationProbability;
        this.PointFunction = pointFunction;
        this.PopulationSize = populationSize;
        this.PrecisionDigits = precisionDigits;
    }
}
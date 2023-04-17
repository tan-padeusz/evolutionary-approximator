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
}
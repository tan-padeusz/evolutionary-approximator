﻿using Data;

namespace Genetics;

// decogen = decoder + generator
public abstract class ChromosomeDecogen
{
    public int FactorCount { get; }
    public int GenesPerFactor { get; protected init; }
    protected int PrecisionDigits { get; }
    protected int MaxPolynomialDegree { get; }

    protected ChromosomeDecogen(ApproximatorJob job)
    {
        this.FactorCount = (job.MaxPolynomialDegree + 1) * (job.MaxPolynomialDegree + 2) / 2;
        this.MaxPolynomialDegree = job.MaxPolynomialDegree;
        this.PrecisionDigits = job.PrecisionDigits;
    }
    
    public abstract Gene NewRandomGene();
    public abstract float[,] Decode(Gene[] genes);

    protected static Gene[] GetPart(Gene[] source, int from, int count)
    {
        var target = new Gene[count];
        for (var index = 0; index < count; index++)
            target[index] = source[from + index];
        return target;
    }
}
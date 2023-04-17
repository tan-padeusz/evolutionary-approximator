﻿using Structs;

namespace Genetics;

public class RealChromosome
{
    private RealGene[] Genes { get; }
    private static Random Random { get; } = new Random();
    
    public RealChromosome(ApproximatorJob job)
    {
        var size = (job.MaxPolynomialDegree + 1) * (job.MaxPolynomialDegree + 2) / 2;
        var genes = new RealGene[size];
        for (int index = 0; index < size; index++) genes[index] = new RealGene(job);
        this.Genes = genes;
    }

    public RealChromosome(ApproximatorJob job, RealChromosome dominant, RealChromosome recessive)
    {
        var size = dominant.Genes.Length;
        var genes = new RealGene[size];
        for (var index = 0; index < size; index++)
        {
            RealChromosome parent = Random.Next(1000) < job.DominantParentGeneStrength ? dominant : recessive;
            genes[index] = parent.Genes[index].Identical();
        }

        if (Random.Next(1000) < job.MutationProbability)
        {
            int mutationPointIndex = Random.Next(size);
            genes[mutationPointIndex] = genes[mutationPointIndex].Mutated(job);
        }

        this.Genes = genes;
    }

    public double[][] Decode(ApproximatorJob job)
    {
        double[][] factors = new double[job.MaxPolynomialDegree + 1][];
        int startingIndex = 0;
        int genesPerFactor = 1;
        for (int degree = 0; degree <= job.MaxPolynomialDegree; degree++)
        {
            double[] degreeFactors = new double[degree + 1];
            for (int yPower = 0; yPower <= degree; yPower++)
            {
                degreeFactors[yPower] = this.GetGenes(startingIndex, genesPerFactor)[0].Value;
                startingIndex += genesPerFactor;
            }
            factors[degree] = degreeFactors;
        }

        return factors;
    }
    
    private RealGene[] GetGenes(int from, int count)
    {
        var genes = new RealGene[count];
        for (var index = 0; index < count; index++) genes[index] = this.Genes[index + from];
        return genes;
    }
}
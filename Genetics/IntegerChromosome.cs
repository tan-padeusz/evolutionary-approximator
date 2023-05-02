using System.Diagnostics.CodeAnalysis;
using Data;

namespace Genetics;

public class IntegerChromosome: Chromosome
{
    private IntegerGene[] Genes { get; }

    public IntegerChromosome(ApproximatorJob job)
    {
        var genesPerFactor = 1 + job.PrecisionDigits;
        var size = (job.MaxPolynomialDegree + 1) * (job.MaxPolynomialDegree + 2) / 2 * genesPerFactor;
        var genes = new IntegerGene[size];
        for (var index = 0; index < size; index++) genes[index] = new IntegerGene();
        this.Genes = genes;
    }

    public IntegerChromosome(ApproximatorJob job, IntegerChromosome dominant, IntegerChromosome recessive)
    {
        var size = dominant.Genes.Length;
        var genes = new IntegerGene[size];
        for (var index = 0; index < size; index++)
        {
            IntegerChromosome parent = Random.Next(1000) < job.DominantParentGeneStrength ? dominant : recessive;
            genes[index] = parent.Genes[index].Identical();
        }

        if (Random.Next(1000) < job.MutationProbability)
        {
            var mutationIndex = Chromosome.Random.Next(size);
            genes[mutationIndex] = genes[mutationIndex].Mutated();
        }

        this.Genes = genes;
    }

    [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
    public override double[][] Decode(ApproximatorJob job)
    {
        var factors = new double[job.MaxPolynomialDegree + 1][];
        var precisionValue = Math.Pow(10, -job.PrecisionDigits);
        var startingIndex = 0;
        var genesPerFactor = job.PrecisionDigits + 1;
        for (var degree = 0; degree <= job.MaxPolynomialDegree; degree++)
        {
            var degreeFactors = new double[degree + 1];
            for (var yPower = 0; yPower <= degree; yPower++)
            {
                var value = 0.0;
                for (var gene = 0; gene < genesPerFactor; gene++)
                {
                    value += this.Genes[startingIndex + gene].Value * precisionValue;
                }
                degreeFactors[yPower] = Math.Round(value, job.PrecisionDigits);
                startingIndex += genesPerFactor;
            }
            factors[degree] = degreeFactors;
        }
        return factors;
    }
    
    private IntegerGene[] GetGenes(int from, int count)
    {
        var genes = new IntegerGene[count];
        for (var index = 0; index < count; index++) genes[index] = this.Genes[index + from];
        return genes;
    }
}
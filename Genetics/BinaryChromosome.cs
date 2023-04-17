using Structs;

namespace Genetics;

public class BinaryChromosome
{
    private BinaryGene[] Genes { get; }
    private static Random Random { get; } = new Random();

    public BinaryChromosome(ApproximatorJob job)
    {
        var factorCount = (job.MaxPolynomialDegree + 1) * (job.MaxPolynomialDegree + 2) / 2;
        var size = factorCount * (1 + 2 * job.PrecisionDigits) * 9;
        var genes = new BinaryGene[size];
        for (var index = 0; index < size; index++) genes[index] = new BinaryGene();
        this.Genes = genes;
    }

    public BinaryChromosome(ApproximatorJob job, BinaryChromosome dominant, BinaryChromosome recessive)
    {
        var size = dominant.Genes.Length;
        var genes = new BinaryGene[size];
        var genesPerFactor = (1 + 2 * job.PrecisionDigits) * 9;
        for (var index = 0; index < size; index += genesPerFactor)
        {
            var mutatedIndexes = this.GenerateMutatedIndexes(job.MutationProbability, index, genesPerFactor);
            var parent = Random.Next(1000) < job.DominantParentGeneStrength ? dominant : recessive;
            genes[index] = mutatedIndexes.Contains(index) ? parent.Genes[index].Mutated(job) : parent.Genes[index].Identical();
        }
        this.Genes = genes;
    }

    private int[] GenerateMutatedIndexes(int mutationProbability, int from, int count)
    {
        var mutatedCount = mutationProbability / 1000 * count;
        var mutatedIndexes = new int[mutatedCount];
        for (int index = 0; index < mutatedCount; index++)
        {
            int mutatedIndex;
            do
            {
                mutatedIndex = Random.Next(from, from + count);
            } while (mutatedIndexes.Contains(mutatedIndex));
            mutatedIndexes[index] = mutatedIndex;
        }
        return mutatedIndexes;
    }

    public double[][] Decode(ApproximatorJob job)
    {
        var factors = new double[job.MaxPolynomialDegree + 1][];
        var startingIndex = 0;
        var genesPerFactor = (1 + 2 * job.PrecisionDigits) * 9;
        for (var degree = 0; degree <= job.MaxPolynomialDegree; degree++)
        {
            var degreeFactors = new double[degree + 1];
            for (var yPower = 0; yPower <= degree; yPower++)
            {
                var value = 0.0;
                var factorGenes = this.GetGenes(startingIndex, genesPerFactor);
                var tenPower = job.PrecisionDigits;
                for (int index = 0; index < genesPerFactor; index += 9)
                {
                    double delta = Math.Pow(10, tenPower);
                    for (int geneIndex = index; geneIndex < index + 9; index++)
                        if (factorGenes[geneIndex].Value) value += delta;
                    tenPower--;
                }
                degreeFactors[yPower] = Math.Round(value, job.PrecisionDigits);
                startingIndex += genesPerFactor;
            }
            factors[degree] = degreeFactors;
        }
        return factors;
    }

    private BinaryGene[] GetGenes(int from, int count)
    {
        var genes = new BinaryGene[count];
        for (var index = 0; index < count; index++) genes[index] = this.Genes[index + from];
        return genes;
    }
}
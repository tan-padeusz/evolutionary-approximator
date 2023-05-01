using Data;

namespace Genetics;

public class BinaryChromosome: Chromosome
{
    private BinaryGene[] Genes { get; }

    public BinaryChromosome(ApproximatorJob job)
    {
        var factorCount = (job.MaxPolynomialDegree + 1) * (job.MaxPolynomialDegree + 2) / 2;
        var genesPerFactor = (1 + job.PrecisionDigits) * 9 + 1;
        var size = factorCount * genesPerFactor;
        var genes = new BinaryGene[size];
        for (var index = 0; index < size; index++) genes[index] = new BinaryGene();
        this.Genes = genes;
    }

    public BinaryChromosome(ApproximatorJob job, BinaryChromosome dominant, BinaryChromosome recessive)
    {
        var size = dominant.Genes.Length;
        var genes = new BinaryGene[size];
        for (var index = 0; index < size; index++)
        {
            var parent = BinaryChromosome.Random.Next(1000) < job.DominantParentGeneStrength ? dominant : recessive;
            genes[index] = parent.Genes[index].Identical();
        }

        if (BinaryChromosome.Random.Next(1000) < job.MutationProbability)
        {
            var mutationIndex = Chromosome.Random.Next(size);
            genes[mutationIndex] = genes[mutationIndex].Mutated();
        }
        
        this.Genes = genes;
    }

    public override double[][] Decode(ApproximatorJob job)
    {
        var factors = new double[job.MaxPolynomialDegree + 1][];
        var startingIndex = 0;
        var genesPerFactor = (1 + job.PrecisionDigits) * 9 + 1;
        for (var degree = 0; degree <= job.MaxPolynomialDegree; degree++)
        {
            var degreeFactors = new double[degree + 1];
            for (var yPower = 0; yPower <= degree; yPower++)
            {
                var value = 0.0;
                var factorGenes = this.GetGenes(startingIndex, genesPerFactor);
                var tenPower = 0;
                for (var factorGeneIndex = 1; factorGeneIndex < genesPerFactor; factorGeneIndex += 9)
                {
                    var sum = 0;
                    for (var digitGeneIndex = factorGeneIndex; digitGeneIndex < factorGeneIndex + 9; digitGeneIndex++)
                    {
                        var gene = factorGenes[digitGeneIndex];
                        if (gene.Value) sum++;
                    }
                    value += sum * Math.Pow(10, tenPower);
                    tenPower--;
                }
                if (factorGenes[0].Value) value *= -1;
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
        for (var index = 0; index < count; index++) genes[index] = this.Genes[index + from].Identical();
        return genes;
    }
}
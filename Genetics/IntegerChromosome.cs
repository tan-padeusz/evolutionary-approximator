using Structs;

namespace Genetics;

public class IntegerChromosome
{
    private IntegerGene[] Genes { get; }
    private static Random Random { get; } = new Random();

    public IntegerChromosome(ApproximatorJob job)
    {
        var genesPerFactor = 1 + 2 * job.PrecisionDigits;
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
            var genesPerFactor = 1 + 2 * job.PrecisionDigits;
            for (var startIndex = 0; startIndex < size; startIndex += genesPerFactor)
            {
                int mutationIndex = Random.Next(startIndex, startIndex + genesPerFactor);
                genes[mutationIndex] = genes[mutationIndex].Mutated(job);
            }
        }

        this.Genes = genes;
    }

    public double[][] Decode(ApproximatorJob job)
    {
        double[][] factors = new double[job.MaxPolynomialDegree + 1][];
        int startingIndex = 0;
        int genesPerFactor = job.PrecisionDigits + 1;
        for (int degree = 0; degree <= job.MaxPolynomialDegree; degree++)
        {
            double[] degreeFactors = new double[degree + 1];
            for (int yPower = 0; yPower <= degree; yPower++)
            {
                double value = 0;
                var factorChromosomePart = this.GetGenes(startingIndex, genesPerFactor);
                int tenPower = job.PrecisionDigits;
                for (int gene = 0; gene < genesPerFactor; gene++)
                {
                    value += factorChromosomePart[gene].Value * Math.Pow(10, tenPower);
                    tenPower--;
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
using Structs;

namespace Genetics;

public class BinaryIndividual: Individual
{
    private BinaryChromosome Chromosome { get; }

    public BinaryIndividual(ApproximatorJob job, InputPoint[] points)
    {
        this.Chromosome = new BinaryChromosome(job);
        this.Factors = this.Chromosome.Decode(job);
        this.Error = this.EvaluateError(points);
    }

    public BinaryIndividual(ApproximatorJob job, InputPoint[] points, (BinaryIndividual, BinaryIndividual) parents)
    {
        this.Chromosome = new BinaryChromosome(job, parents.Item1.Chromosome, parents.Item2.Chromosome);
        this.Factors = this.Chromosome.Decode(job);
        this.Error = this.EvaluateError(points);
    }
}
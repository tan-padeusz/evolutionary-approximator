using Structs;

namespace Genetics;

public class IntegerIndividual: Individual
{
    private IntegerChromosome Chromosome { get; }

    public IntegerIndividual(ApproximatorJob job, InputPoint[] points)
    {
        this.Chromosome = new IntegerChromosome(job);
        this.Factors = this.Chromosome.Decode(job);
        this.Error = this.EvaluateError(points);
    }

    public IntegerIndividual(ApproximatorJob job, InputPoint[] points, (IntegerIndividual, IntegerIndividual) parents)
    {
        this.Chromosome = new IntegerChromosome(job, parents.Item1.Chromosome, parents.Item2.Chromosome);
        this.Factors = this.Chromosome.Decode(job);
        this.Error = this.EvaluateError(points);
    }
}
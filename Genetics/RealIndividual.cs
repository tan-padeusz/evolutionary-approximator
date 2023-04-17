using Structs;

namespace Genetics;

public class RealIndividual: Individual
{
    private RealChromosome Chromosome { get; }

    public RealIndividual(ApproximatorJob job, InputPoint[] points)
    {
        this.Chromosome = new RealChromosome(job);
        this.Factors = this.Chromosome.Decode(job);
        this.Error = this.EvaluateError(points);
    }
    
    public RealIndividual(ApproximatorJob job, InputPoint[] points, (RealIndividual, RealIndividual) parents)
    {
        this.Chromosome = new RealChromosome(job, parents.Item1.Chromosome, parents.Item2.Chromosome);
        this.Factors = this.Chromosome.Decode(job);
        this.Error = this.EvaluateError(points);
    }
}
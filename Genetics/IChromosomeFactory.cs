using Data;

namespace Genetics;

public interface IChromosomeFactory
{
    public Chromosome NewChromosome(ApproximatorJob job);
    public Chromosome NewChromosome(ApproximatorJob job, Chromosome dominant, Chromosome recessive);
}
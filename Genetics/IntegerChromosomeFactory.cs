using Data;

namespace Genetics;

public class IntegerChromosomeFactory: IChromosomeFactory
{
    public Chromosome NewChromosome(ApproximatorJob job)
    {
        return new IntegerChromosome(job);
    }

    public Chromosome NewChromosome(ApproximatorJob job, Chromosome dominant, Chromosome recessive)
    {
        return new IntegerChromosome(job, (IntegerChromosome) dominant, (IntegerChromosome) recessive);
    }
}
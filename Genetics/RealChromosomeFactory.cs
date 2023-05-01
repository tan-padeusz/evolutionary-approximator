using Data;

namespace Genetics;

public class RealChromosomeFactory: IChromosomeFactory
{
    public Chromosome NewChromosome(ApproximatorJob job)
    {
        return new RealChromosome(job);
    }

    public Chromosome NewChromosome(ApproximatorJob job, Chromosome dominant, Chromosome recessive)
    {
        return new RealChromosome(job, (RealChromosome) dominant, (RealChromosome) recessive);
    }
}
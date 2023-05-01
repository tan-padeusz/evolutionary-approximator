using System.ComponentModel;
using Data;
using Enums;

namespace Genetics;

public class BinaryChromosomeFactory: IChromosomeFactory
{
    public Chromosome NewChromosome(ApproximatorJob job)
    {
        return new BinaryChromosome(job);
    }

    public Chromosome NewChromosome(ApproximatorJob job, Chromosome dominant, Chromosome recessive)
    {
        return new BinaryChromosome(job, (BinaryChromosome)dominant, (BinaryChromosome)recessive);
    }
}
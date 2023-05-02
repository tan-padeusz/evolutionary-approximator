using System.ComponentModel;
using Data;
using Enums;

namespace Genetics;

public class Chromosome
{
    private static ChromosomeWhatever ChromosomeWhatever { get; set; }
    private Gene[] Genes { get; }
    protected static Random Random { get; } = new Random();
    
    public Chromosome()
    {
        var whatever = Chromosome.ChromosomeWhatever;
        var size = whatever.FactorCount * whatever.GenesPerFactor;
        var genes = new Gene[size];
        
        for (var index = 0; index < size; index++)
            genes[index] = whatever.NewRandomGene();
        
        this.Genes = genes;
    }

    public Chromosome(ApproximatorJob job, Chromosome dominant, Chromosome recessive)
    {
        var whatever = Chromosome.ChromosomeWhatever;
        var size = whatever.FactorCount * whatever.GenesPerFactor;
        var genes = new Gene[size];
        
        for (var index = 0; index < size; index++)
        {
            var parent = Chromosome.Random.Next(1000) < job.DominantParentGeneStrength ? dominant : recessive;
            genes[index] = parent.Genes[index].Identical();
        }

        if (Chromosome.Random.Next(1000) < job.MutationProbability)
        {
            var mutationIndex = Chromosome.Random.Next(size);
            genes[mutationIndex] = genes[mutationIndex].Mutated(job);
        }

        this.Genes = genes;
    }

    public double[][] Decode()
    {
        return Chromosome.ChromosomeWhatever.Decode(this);
    }

    public Gene[] GetGenes(int from, int count)
    {
        var genes = new Gene[count];

        for (var index = 0; index < count; index++)
            genes[index] = this.Genes[index + from];

        return genes;
    }

    public static void InitialiseChromosomeWhatever(ApproximatorJob job)
    {
        Chromosome.ChromosomeWhatever= job.GeneType switch
        {
            GeneType.Binary => new BinaryChromosomeWhatever(job),
            GeneType.Integer => new IntegerChromosomeWhatever(job),
            GeneType.Real => new RealChromosomeWhatever(job),
            _ => throw new InvalidEnumArgumentException($"Invalid gene type : {job.GeneType}")
        };
    }
}
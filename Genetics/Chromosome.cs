using System.ComponentModel;
using Data;
using Enums;

namespace Genetics;

public class Chromosome
{
    private static ChromosomeDecogen? ChromosomeDecogen { get; set; }
    private Gene[] Genes { get; }
    private static Random Random { get; } = new Random();
    
    public Chromosome()
    {
        var decogen = Chromosome.ChromosomeDecogen;
        var size = decogen!.FactorCount * decogen.GenesPerFactor;
        var genes = new Gene[size];
        
        for (var index = 0; index < size; index++)
            genes[index] = decogen.NewRandomGene();
        
        this.Genes = genes;
    }

    public Chromosome(ApproximatorJob job, Chromosome dominant, Chromosome recessive)
    {
        var decogen = Chromosome.ChromosomeDecogen;
        var size = decogen!.FactorCount * decogen.GenesPerFactor;
        var genes = new Gene[size];
        
        for (var index = 0; index < size; index++)
        {
            var parent =
                Chromosome.Random.Next(1000) < job.DominantParentGeneStrength
                    ? dominant : recessive;
            genes[index] = parent.Genes[index].Identical();
        }

        if (Chromosome.Random.Next(1000) < job.MutationProbability)
        {
            var mutationIndex = Chromosome.Random.Next(size);
            genes[mutationIndex] = genes[mutationIndex].Mutated(job);
        }

        this.Genes = genes;
    }

    public float[,] Decode()
    {
        return Chromosome.ChromosomeDecogen!.Decode(this.Genes);
    }

    public static void InitialiseChromosomeDecogen(ApproximatorJob job)
    {
        Chromosome.ChromosomeDecogen= job.GeneType switch
        {
            GeneType.BINARY => new BinaryChromosomeDecogen(job),
            GeneType.INTEGER => new IntegerChromosomeDecogen(job),
            GeneType.REAL => new RealChromosomeDecogen(job),
            _ => throw new InvalidEnumArgumentException($"Invalid gene type : {job.GeneType}")
        };
    }
}
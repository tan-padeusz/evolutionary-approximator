using Data;

namespace Genetics;

public abstract class Chromosome
{
    protected static Random Random { get; } = new Random();
    
    public abstract double[][] Decode(ApproximatorJob job);
}
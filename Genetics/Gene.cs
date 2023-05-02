using Data;

namespace Genetics;

public abstract class Gene
{
    protected static Random Random { get; } = new Random();
    
    public abstract Gene Identical();
    public abstract Gene Mutated(ApproximatorJob job);
}
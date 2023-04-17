using Structs;

namespace Genetics;

public class BinaryGene
{
    private static Random Random { get; } = new Random();
    public bool Value { get; }

    public BinaryGene()
    {
        this.Value = BinaryGene.Random.Next() % 2 == 1;
    }
    
    private BinaryGene(bool value)
    {
        this.Value = value;
    }

    public BinaryGene Identical()
    {
        return new BinaryGene(this.Value);
    }

    public BinaryGene Mutated(ApproximatorJob job)
    {
        return new BinaryGene(!this.Value);
    }
}
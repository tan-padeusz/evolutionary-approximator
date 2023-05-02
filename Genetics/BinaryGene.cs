using Data;

namespace Genetics;

public class BinaryGene: Gene
{
    public bool Value { get; }

    public BinaryGene()
    {
        this.Value = Gene.Random.Next() % 2 == 1;
    }
    
    private BinaryGene(bool value)
    {
        this.Value = value;
    }

    public override Gene Identical()
    {
        return new BinaryGene(this.Value);
    }

    public override Gene Mutated(ApproximatorJob job)
    {
        return new BinaryGene(!this.Value);
    }
}
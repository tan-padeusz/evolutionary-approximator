using Data;

namespace Genetics;

public class IntegerGene: Gene
{
    public int Value { get; }

    public IntegerGene()
    {
        this.Value = Gene.Random.Next(3) - 1;
    }

    private IntegerGene(int value)
    {
        this.Value = value;
    }

    public override Gene Identical()
    {
        return new IntegerGene(this.Value);
    }

    public override Gene Mutated(ApproximatorJob job)
    {
        var delta = Gene.Random.Next() % 2 == 0 ? 1 : -1;
        return new IntegerGene(this.Value + delta);
    }
}
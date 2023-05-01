namespace Genetics;

public readonly struct IntegerGene
{
    private static Random Random { get; } = new Random();
    public int Value { get; }

    public IntegerGene()
    {
        this.Value = IntegerGene.Random.Next(3) - 1;
    }

    private IntegerGene(int value)
    {
        this.Value = value;
    }

    public IntegerGene Identical()
    {
        return new IntegerGene(this.Value);
    }

    public IntegerGene Mutated()
    {
        var delta = IntegerGene.Random.Next() % 2 == 0 ? 1 : -1;
        return new IntegerGene(this.Value + delta);
    }
}
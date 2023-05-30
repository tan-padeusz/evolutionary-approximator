using Data;

namespace Genetics;

public class BinaryChromosomeDecogen: ChromosomeDecogen
{
    private double[] RootArray { get; }
    
    public BinaryChromosomeDecogen(ApproximatorJob job) : base(job)
    {
        this.GenesPerFactor = (job.PrecisionDigits + 1) * 10 + 1;
        this.RootArray = this.CreateRootArray();
    }

    public override Gene NewRandomGene()
    {
        return new BinaryGene();
    }

    private double[] CreateRootArray()
    {
        var size = this.GenesPerFactor * (this.GenesPerFactor - 1) / 2;
        var rootArray = new double[size];
        for (var index = 0; index < size; index++)
            rootArray[index] = Math.Cbrt(index);
        return rootArray;
    }

    public override double[][] Decode(Gene[] genes)
    {
        const double range = 10.0;
        var delta = range / (this.GenesPerFactor - 2.0);
        var factors = new double[this.MaxPolynomialDegree + 1][];
        var startingIndex = 0;
        for (var degree = 0; degree <= this.MaxPolynomialDegree; degree++)
        {
            var degreeFactors = new double[degree + 1];
            for (var yPower = 0; yPower <= degree; yPower++)
            {
                var sum = 0.0;
                var ones = 0;
                var factorGenes = ChromosomeDecogen.GetPart(genes, startingIndex, this.GenesPerFactor);
                for (var geneIndex = 1; geneIndex < this.GenesPerFactor; geneIndex++)
                {
                    var gene = (BinaryGene) factorGenes[geneIndex];
                    if (!gene.Value) continue;
                    sum += (geneIndex - 1) * delta - range;
                    ones++;
                }
                if (ones == 0) ones = 1;
                var factor = sum / ones;
                if (((BinaryGene) factorGenes[0]).Value) factor *= -1;
                degreeFactors[yPower] = Math.Round(factor, this.PrecisionDigits);
                startingIndex += this.GenesPerFactor;
            }
            factors[degree] = degreeFactors;
        }
        return factors;
    }
}
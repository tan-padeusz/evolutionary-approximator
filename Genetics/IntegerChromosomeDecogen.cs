using Data;

namespace Genetics;

public class IntegerChromosomeDecogen: ChromosomeDecogen
{
    public IntegerChromosomeDecogen(ApproximatorJob job) : base(job)
    {
        this.GenesPerFactor = job.PrecisionDigits + 1;
    }

    public override Gene NewRandomGene()
    {
        return new IntegerGene();
    }

    public override double[][] Decode(Gene[] genes)
    {
        var factors = new double[this.MaxPolynomialDegree + 1][];
        var startingIndex = 0;
        var precisionValue = Math.Pow(10, -this.PrecisionDigits);
        for (var degree = 0; degree <= this.MaxPolynomialDegree; degree++)
        {
            var degreeFactors = new double[degree + 1];
            for (var yPower = 0; yPower <= degree; yPower++)
            {
                var factorGenes = ChromosomeDecogen.GetPart(genes, startingIndex, this.GenesPerFactor);
                var factor = factorGenes.Sum(gene => ((IntegerGene) gene).Value);
                degreeFactors[yPower] = Math.Round(factor * precisionValue, this.PrecisionDigits);
                startingIndex += this.GenesPerFactor;
            }
            factors[degree] = degreeFactors;
        }
        return factors;
    }
}
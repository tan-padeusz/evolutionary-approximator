using Data;

namespace Genetics;

public class RealChromosomeWhatever: ChromosomeWhatever
{
    public RealChromosomeWhatever(ApproximatorJob job) : base(job)
    {
        this.GenesPerFactor = 1;
    }

    public override Gene NewRandomGene()
    {
        return new RealGene();
    }

    public override double[][] Decode(Chromosome chromosome)
    {
        var factors = new double[this.MaxPolynomialDegree + 1][];
        var startingIndex = 0;
        for (var degree = 0; degree <= this.MaxPolynomialDegree; degree++)
        {
            var degreeFactors = new double[degree + 1];
            for (var yPower = 0; yPower <= degree; yPower++)
            {
                var factorGenes = chromosome.GetGenes(startingIndex, this.GenesPerFactor);
                var factor = ((RealGene) factorGenes[0]).Value;
                degreeFactors[yPower] = Math.Round(factor, this.PrecisionDigits);
                startingIndex += this.GenesPerFactor;
            }
            factors[degree] = degreeFactors;
        }
        return factors;
    }
}
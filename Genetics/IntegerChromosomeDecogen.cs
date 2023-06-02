using Data;

namespace Genetics;

public class IntegerChromosomeDecogen: ChromosomeDecogen
{
    public IntegerChromosomeDecogen(ApproximatorJob job) : base(job)
    {
        this.GenesPerFactor = 1;
    }

    public override Gene NewRandomGene()
    {
        return new IntegerGene();
    }

    public override float[,] Decode(Gene[] genes)
    {
        const float step = 0.0001F;

        var factors = new float[this.MaxPolynomialDegree + 1, this.MaxPolynomialDegree + 1];
        var geneIndex = 0;
        
        for (var xPower = 0; xPower <= this.MaxPolynomialDegree; xPower++)
        for (var yPower = 0; yPower <= this.MaxPolynomialDegree; yPower++)
        {
            if (xPower + yPower > this.MaxPolynomialDegree) continue;

            var factor = ((IntegerGene) genes[geneIndex]).Value * step;
            factors[xPower, yPower] = (float) Math.Round(factor, 4);
            geneIndex++;
        }

        return factors;
    }
}
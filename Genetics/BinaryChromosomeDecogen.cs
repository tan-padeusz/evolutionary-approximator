using Data;

namespace Genetics;

public class BinaryChromosomeDecogen: ChromosomeDecogen
{
    public BinaryChromosomeDecogen(ApproximatorJob job) : base(job)
    {
        this.GenesPerFactor = 32;
    }

    public override Gene NewRandomGene()
    {
        return new BinaryGene();
    }

    public override float[,] Decode(Gene[] genes)
    {
        const int genesPerFactor = 32;
        const float range = 10.0F;
        const float delta = range / (genesPerFactor - 2.0F);
        
        var factors = new float[this.MaxPolynomialDegree + 1, this.MaxPolynomialDegree + 1];
        var startingIndex = 0;
        
        for (var xPower = 0; xPower <= this.MaxPolynomialDegree; xPower++)
        for (var yPower = 0; yPower <= this.MaxPolynomialDegree; yPower++)
        {
            if (xPower + yPower > this.MaxPolynomialDegree) continue;

            var factor = 0.0F;
            var ones = 0;
            var factorGenes = ChromosomeDecogen.GetPart(genes, startingIndex, genesPerFactor);
            
            for (var geneIndex = 1; geneIndex < genesPerFactor; geneIndex++)
            {
                var gene = (BinaryGene) factorGenes[geneIndex];
                if (!gene.Value) continue;
                factor += (geneIndex - 1) * delta - range;
                ones++;
            }

            if (ones == 0) ones = 1;
            factor /= ones;
            if (((BinaryGene) factorGenes[0]).Value) factor *= -1;

            factors[xPower, yPower] = (float) Math.Round(factor, 4);
            startingIndex += genesPerFactor;
        }

        return factors;
    }
}
using Data;

namespace Genetics;

public class BinaryChromosomeWhatever: ChromosomeWhatever
{
    public BinaryChromosomeWhatever(ApproximatorJob job) : base(job)
    {
        this.GenesPerFactor = (job.PrecisionDigits + 1) * 9 + 1;
    }

    public override Gene NewRandomGene()
    {
        return new BinaryGene();
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
                var factor = 0.0;
                var tenPower = 0;
                var factorGenes = chromosome.GetGenes(startingIndex, this.GenesPerFactor);
                for (var factorGeneIndex = 1; factorGeneIndex < GenesPerFactor; factorGeneIndex += 9)
                {
                    var digit = 0;
                    for (var digitGeneIndex = factorGeneIndex; digitGeneIndex < factorGeneIndex + 9; digitGeneIndex++)
                    {
                        var digitGene = (BinaryGene) factorGenes[digitGeneIndex];
                        if (digitGene.Value) digit++;
                    }
                    factor += digit * Math.Pow(10, tenPower);
                    tenPower--;
                }
                if (((BinaryGene) factorGenes[0]).Value) factor *= -1;
                degreeFactors[yPower] = Math.Round(factor, this.PrecisionDigits);
                startingIndex += this.GenesPerFactor;
            }
            factors[degree] = degreeFactors;
        }
        return factors;
        // var factors = new double[this.MaxPolynomialDegree + 1][];
        // for (var degree = 0; degree <= this.MaxPolynomialDegree; degree++)
        // {
        //     var degreeFactors = new double[degree + 1];
        //     for (var yPower = 0; yPower <= degree; yPower++)
        //     {
        //         var factor = 0.0;
        //         var tenPower = 0;
        //         var startingIndex = (yPower * (yPower + 1) / 2) + (degree * (degree + 1) / 2);
        //         var factorGenes = chromosome.GetGenes(startingIndex, this.GenesPerFactor);
        //         for (var factorGeneIndex = 0; factorGeneIndex < GenesPerFactor; factorGeneIndex++)
        //         {
        //             var digitGene = (BinaryGene) factorGenes[factorGeneIndex];
        //             if (digitGene.Value) factor += Math.Pow(10, tenPower);
        //             tenPower--;
        //         }
        //         if (((BinaryGene) factorGenes[0]).Value) factor *= -1;
        //         degreeFactors[yPower] = Math.Round(factor, this.PrecisionDigits);
        //     }
        //     factors[degree] = degreeFactors;
        // }
        // return factors;
    }
}
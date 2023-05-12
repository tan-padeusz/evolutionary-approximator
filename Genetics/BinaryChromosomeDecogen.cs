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
        var size = 1;
        for (var index = 1; index < this.GenesPerFactor; index++)
            size += index;
        var rootArray = new double[size];
        for (var index = 0; index < size; index++)
            rootArray[index] = Math.Cbrt(index);
        return rootArray;
    }

    public override double[][] Decode(Gene[] genes)
    {
        var factors = new double[this.MaxPolynomialDegree + 1][];
        var startingIndex = 0;
        for (var degree = 0; degree <= this.MaxPolynomialDegree; degree++)
        {
            var degreeFactors = new double[degree + 1];
            for (var yPower = 0; yPower <= degree; yPower++)
            {
                var sum = 0;
                var factorGenes = ChromosomeDecogen.GetPart(genes, startingIndex, this.GenesPerFactor);
                for (var geneIndex = 1; geneIndex < this.GenesPerFactor; geneIndex++)
                {
                    var gene = (BinaryGene) factorGenes[geneIndex];
                    if (gene.Value) sum += geneIndex;
                }
                var factor = 10 * (1 / Math.Cbrt(this.GenesPerFactor - 1)) * this.RootArray[sum];
                if (((BinaryGene)factorGenes[0]).Value) factor *= -1;
                degreeFactors[yPower] = Math.Round(factor, this.PrecisionDigits);
                startingIndex += this.GenesPerFactor;
            }
            factors[degree] = degreeFactors;
        }
        return factors;
    }
    
    // public override double[][] Decode(Gene[] genes)
    // {
    //     var factors = new double[this.MaxPolynomialDegree + 1][];
    //     var startingIndex = 0;
    //     for (var degree = 0; degree <= this.MaxPolynomialDegree; degree++)
    //     {
    //         var degreeFactors = new double[degree + 1];
    //         for (var yPower = 0; yPower <= degree; yPower++)
    //         {
    //             var factor = 0.0;
    //             var tenPower = 0;
    //             var factorGenes = ChromosomeWhatever.GetPart(genes, startingIndex, this.GenesPerFactor);
    //             for (var factorGeneIndex = 1; factorGeneIndex < GenesPerFactor; factorGeneIndex += 9)
    //             {
    //                 var digit = 0;
    //                 for (var digitGeneIndex = factorGeneIndex; digitGeneIndex < factorGeneIndex + 9; digitGeneIndex++)
    //                 {
    //                     var digitGene = (BinaryGene) factorGenes[digitGeneIndex];
    //                     if (digitGene.Value) digit++;
    //                 }
    //                 factor += digit * Math.Pow(10, tenPower);
    //                 tenPower--;
    //             }
    //             if (((BinaryGene) factorGenes[0]).Value) factor *= -1;
    //             degreeFactors[yPower] = Math.Round(factor, this.PrecisionDigits);
    //             startingIndex += this.GenesPerFactor;
    //         }
    //         factors[degree] = degreeFactors;
    //     }
    //     return factors;
    // }
    // public override double[][] Decode(Gene[] genes)
    // {
    //     var factors = new double[this.MaxPolynomialDegree + 1][];
    //     var startingIndex = 0;
    //     for (var degree = 0; degree <= this.MaxPolynomialDegree; degree++)
    //     {
    //         var degreeFactors = new double[degree + 1];
    //         for (var yPower = 0; yPower <= degree; yPower++)
    //         {
    //             var factor = 0.0;
    //             var maxFactor = 0.0;
    //             var factorGenes = ChromosomeWhatever.GetPart(genes, startingIndex, this.GenesPerFactor);
    //             var weight = Math.Pow(2, factorGenes.Length - 2);
    //             for (var geneIndex = 1; geneIndex < this.GenesPerFactor; geneIndex++)
    //             {
    //                 var twoPowerMultiplier = Math.Pow(2, geneIndex - 1);
    //                 var gene = (BinaryGene) factorGenes[geneIndex];
    //                 if (gene.Value) factor += weight * twoPowerMultiplier;
    //                 maxFactor += weight * twoPowerMultiplier;
    //                 weight /= 2;
    //             }
    //             factor = (factor / maxFactor) * 10;
    //             if (((BinaryGene) factorGenes[0]).Value) factor *= -1;
    //             degreeFactors[yPower] = Math.Round(factor, this.PrecisionDigits);
    //             startingIndex += this.GenesPerFactor;
    //         }
    //         factors[degree] = degreeFactors;
    //     }
    //     return factors;
    // }
}
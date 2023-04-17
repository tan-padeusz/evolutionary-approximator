using Structs;

namespace Genetics;

public class BinaryPopulation: Population
{
    public BinaryPopulation(ApproximatorJob job, InputPoint[] points)
    {
        this.Id = 0;
        this.Individuals = BinaryPopulation.GeneratePopulation(job, points);
        this.BestIndividual = this.FindBestIndividual();
    }

    public BinaryPopulation(ApproximatorJob job, InputPoint[] points, Population previousPopulation)
    {
        this.Id = previousPopulation.Id + 1;
        this.Individuals = BinaryPopulation.GeneratePopulation(job, points, previousPopulation);
        this.BestIndividual = this.FindBestIndividual();
    }

    private static Individual ConductCrossover(ApproximatorJob job, InputPoint[] points, Population previousPopulation)
    {
        var (first, second) = previousPopulation.SelectIndividualsForCrossover(job);
        if (first.IsWorseThan(second)) (first, second) = (second, first);
        return new BinaryIndividual(job, points, (first as BinaryIndividual, second as BinaryIndividual)!);
    }

    private static Individual[] GeneratePopulation(ApproximatorJob job, InputPoint[] points)
    {
        var individuals = new Individual[job.PopulationSize];
        Parallel.For(0, job.PopulationSize, index =>
        {
            Individual individual;
            do
            {
                individual = new BinaryIndividual(job, points);
            } while (double.IsNaN(individual.Error) || double.IsPositiveInfinity(individual.Error));
            individuals[index] = individual;
        });
        return individuals;
    }

    private static Individual[] GeneratePopulation(ApproximatorJob job, InputPoint[] points, Population previousPopulation)
    {
        var individuals = new Individual[job.PopulationSize];
        Parallel.For(0, job.PopulationSize, index =>
        {
            Individual individual;
            do
            {
                individual = BinaryPopulation.ConductCrossover(job, points, previousPopulation);
            } while (double.IsNaN(individual.Error) || double.IsPositiveInfinity(individual.Error));
            individuals[index] = individual;
        });
        return individuals;
    }
}
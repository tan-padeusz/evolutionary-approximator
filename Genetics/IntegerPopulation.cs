using Structs;

namespace Genetics;

public class IntegerPopulation: Population
{
    public IntegerPopulation(ApproximatorJob job, InputPoint[] points)
    {
        this.Id = 0;
        this.Individuals = IntegerPopulation.GeneratePopulation(job, points);
        this.BestIndividual = this.FindBestIndividual();
    }

    public IntegerPopulation(ApproximatorJob job, InputPoint[] points, Population previousPopulation)
    {
        this.Id = previousPopulation.Id + 1;
        this.Individuals = IntegerPopulation.GeneratePopulation(job, points, previousPopulation);
        this.BestIndividual = this.FindBestIndividual();
    }

    private static Individual ConductCrossover(ApproximatorJob job, InputPoint[] points, Population previousPopulation)
    {
        var (first, second) = previousPopulation.SelectIndividualsForCrossover(job);
        if (first.IsWorseThan(second)) (first, second) = (second, first);
        return new IntegerIndividual(job, points, (first as IntegerIndividual, second as IntegerIndividual)!);
    }

    private static Individual[] GeneratePopulation(ApproximatorJob job, InputPoint[] points)
    {
        var individuals = new Individual[job.PopulationSize];
        Parallel.For(0, job.PopulationSize, index =>
        {
            Individual individual;
            do
            {
                individual = new IntegerIndividual(job, points);
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
                individual = IntegerPopulation.ConductCrossover(job, points, previousPopulation);
            } while (double.IsNaN(individual.Error) || double.IsPositiveInfinity(individual.Error));
            individuals[index] = individual;
        });
        return individuals;
    }
}
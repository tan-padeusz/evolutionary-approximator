using Structs;

namespace Genetics;

public class RealPopulation: Population
{
    public RealPopulation(ApproximatorJob job, InputPoint[] points)
    {
        this.Id = 0;
        this.Individuals = RealPopulation.GeneratePopulation(job, points);
        this.BestIndividual = this.FindBestIndividual();
    }

    public RealPopulation(ApproximatorJob job, InputPoint[] points, Population previousPopulation)
    {
        this.Id = previousPopulation.Id + 1;
        this.Individuals = RealPopulation.GeneratePopulation(job, points, previousPopulation);
        this.BestIndividual = this.FindBestIndividual();
    }

    private static Individual ConductCrossover(ApproximatorJob job, InputPoint[] points, Population previousPopulation)
    {
        var (first, second) = previousPopulation.SelectIndividualsForCrossover(job);
        if (first.IsWorseThan(second)) (first, second) = (second, first);
        return new RealIndividual(job, points, (first as RealIndividual, second as RealIndividual)!);
    }

    private static Individual[] GeneratePopulation(ApproximatorJob job, InputPoint[] points)
    {
        var individuals = new Individual[job.PopulationSize];
        Parallel.For(0, job.PopulationSize, index =>
        {
            Individual individual;
            do
            {
                individual = new RealIndividual(job, points);
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
                individual = RealPopulation.ConductCrossover(job, points, previousPopulation);
            } while (double.IsNaN(individual.Error) || double.IsPositiveInfinity(individual.Error));
            individuals[index] = individual;
        });
        return individuals;
    }
}
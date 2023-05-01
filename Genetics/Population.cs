using Data;

namespace Genetics;

public class Population
{
    public Individual BestIndividual { get; protected init; }
    public long Id { get; protected init; }
    private Individual[] Individuals { get; init; }
    private static Random Random { get; } = new Random();
    
    private Individual this[int index]
    {
        get => this.Individuals[index];
        set => this.Individuals[index] = value;
    }

    public Population(ApproximatorJob job, InputPoint[] points)
    {
        this.Id = 0;
        this.Individuals = Population.GeneratePopulation(job, points);
        this.BestIndividual = this.FindBestIndividual();
    }
    
    public Population(ApproximatorJob job, InputPoint[] points, Population previousPopulation)
    {
        this.Id = previousPopulation.Id + 1;
        this.Individuals = Population.GeneratePopulation(job, points, previousPopulation);
        this.BestIndividual = this.FindBestIndividual();
    }

    protected Individual FindBestIndividual()
    {
        var bestIndividual = this.Individuals[0];
        for (var index = 1; index < this.Individuals.Length; index++)
            if (bestIndividual.IsWorseThan(this.Individuals[index])) bestIndividual = this.Individuals[index];
        return bestIndividual;
    }
    
    private static Individual[] GeneratePopulation(ApproximatorJob job, InputPoint[] points)
    {
        var individuals = new Individual[job.PopulationSize];
        Parallel.For(0, job.PopulationSize, index =>
        {
            individuals[index] = new Individual(job, points);
        });
        return individuals;
    }

    private static Individual[] GeneratePopulation(ApproximatorJob job, InputPoint[] points, Population previousPopulation)
    {
        var individuals = new Individual[job.PopulationSize];
        Parallel.For(0, job.PopulationSize, index =>
        {
            var parents = previousPopulation.SelectParents(job);
            individuals[index] = new Individual(job, points, parents);
        });
        return individuals;
    }
    
    private Individual[] SelectParents(ApproximatorJob job)
    {
        var availableIndividuals = job.PopulationSize;
        var parents = new Individual?[] { null, null };
        for (var contestantIndex = 0; contestantIndex < 2 * job.Contestants; contestantIndex++)
        {
            var randomIndividualIndex = Random.Next(availableIndividuals);
            var randomIndividual = this.Individuals[randomIndividualIndex];
            var groupIndex = contestantIndex % 2;
            if (parents[groupIndex] == null || parents[groupIndex]!.IsWorseThan(randomIndividual))
                parents[groupIndex] = randomIndividual;
            (this[randomIndividualIndex], this[availableIndividuals - 1]) = (this[availableIndividuals - 1], randomIndividual);
            availableIndividuals--;
        }
        if (parents[0]!.IsWorseThan(parents[1]!)) (parents[0], parents[1]) = (parents[1], parents[0]);
        return parents!;
    }
}
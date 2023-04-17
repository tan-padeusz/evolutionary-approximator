using Structs;

namespace Genetics;

public abstract class Population
{
    public Individual BestIndividual { get; protected init; }
    public long Id { get; protected init; }
    protected Individual[] Individuals { get; init; }
    private static Random Random { get; } = new Random();
    
    private Individual this[int index]
    {
        get => this.Individuals[index];
        set => this.Individuals[index] = value;
    }

    internal (Individual, Individual) SelectIndividualsForCrossover(ApproximatorJob job)
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
        return (parents[0]!, parents[1]!);
    }

    protected Individual FindBestIndividual()
    {
        var bestIndividual = this.Individuals[0];
        for (var index = 1; index < this.Individuals.Length; index++)
            if (bestIndividual.IsWorseThan(this.Individuals[index])) bestIndividual = this.Individuals[index];
        return bestIndividual;
    }
}
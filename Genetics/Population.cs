using Data;
using Point = Data.Point;

namespace Genetics;

public class Population
{
    public Individual BestIndividual { get; }
    public long Id { get; }
    private Individual[] Individuals { get; }
    private static Random Random { get; } = new Random();

    public Population(ApproximatorJob job, Point[] points)
    {
        this.Id = 0;
        this.Individuals = Population.GeneratePopulation(job, points);
        this.BestIndividual = this.FindBestIndividual();
    }
    
    public Population(ApproximatorJob job, Point[] points, Population previousPopulation)
    {
        this.Id = previousPopulation.Id + 1;
        this.Individuals = Population.GeneratePopulation(job, points, previousPopulation);
        this.BestIndividual = this.FindBestIndividual();
    }

    private Individual FindBestIndividual()
    {
        var bestIndividual = this.Individuals[0];
        for (var index = 1; index < this.Individuals.Length; index++)
            if (bestIndividual.IsWorseThan(this.Individuals[index])) bestIndividual = this.Individuals[index];
        return bestIndividual;
    }
    
    private static Individual[] GeneratePopulation(ApproximatorJob job, Point[] points)
    {
        var individuals = new Individual[job.PopulationSize];
        Parallel.For(0, job.PopulationSize, index =>
        {
            individuals[index] = new Individual(job, points);
        });
        return individuals;
    }

    private static Individual[] GeneratePopulation(ApproximatorJob job, Point[] points, Population previousPopulation)
    {
        var individuals = new Individual[job.PopulationSize];
        var parentPool = previousPopulation.SelectParents(job);
        Parallel.For(0, job.PopulationSize, index =>
        {
            var firstParent = parentPool[Population.Random.Next(job.ParentPoolSize)];
            Individual secondParent;
            do secondParent = parentPool[Population.Random.Next(job.ParentPoolSize)];
            while (secondParent == firstParent);
            var parents = new Individual[] { firstParent, secondParent };
            if (firstParent.IsWorseThan(secondParent)) (parents[0], parents[1]) = (parents[1], parents[0]);
            individuals[index] = new Individual(job, points, parents);
        });
        return individuals;
    }
    
    private Individual[] SelectParents(ApproximatorJob job)
    {
        var parents = new Individual[job.ParentPoolSize];
        for (var index = 0; index < job.ParentPoolSize; index++)
        {
            Individual randomIndividual;
            do randomIndividual = this.Individuals[Population.Random.Next(job.PopulationSize)];
            while (parents.Contains(randomIndividual));
            parents[index] = randomIndividual;
        }
        return parents;
    }
}
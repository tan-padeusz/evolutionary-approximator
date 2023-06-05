using System.Diagnostics;
using Genetics;
using Data;
using Point = Data.Point;

namespace Approximator;

public class ApproximatorEngine
{
    private bool Paused { get; set; }
    public Point[]? Points { get; private set; }
    private bool Running { get; set; }

    private Individual? GlobalBestIndividual { get; set; }
    private long LastImprovement { get; set; }
    private long PopulationsCreated { get; set; }
    private Stopwatch Stopwatch { get; } = new Stopwatch();

    public Point[] GeneratePoints(int pointNumber)
    {
        var points = new Point[pointNumber];
        var random = new Random();
        for (var index = 0; index < pointNumber; index++)
        {
            var x = random.NextDouble() * 20 - 10;
            var y = random.NextDouble() * 20 - 10;
            var z = random.NextDouble() * 20 - 10;
            points[index] = new Point(x, y, z);
        }
        this.Points = points;
        return points;
    }

    private void Approximate(ApproximatorJob job)
    {
        var thread = new Thread(() =>
        {
            ApproximatorEngine.InitializeStaticFields(job);
            this.Stopwatch.Start();
            var population = new Population(job, this.Points!);
            this.GlobalBestIndividual = population.BestIndividual;
            this.LastImprovement = population.Id;
            this.PopulationsCreated = 1;
            
            while (this.Running)
            {
                if (this.Paused)
                {
                    Thread.Sleep(10);
                    continue;
                }
                
                population = new Population(job, this.Points!, population);
                // pbi -> population best individual
                var pbi = population.BestIndividual;
                if (pbi.Error < this.GlobalBestIndividual.Error)
                {
                    this.GlobalBestIndividual = pbi;
                    this.LastImprovement = population.Id;
                }
                this.PopulationsCreated++;
            }
            
            this.Stopwatch.Stop();
        });
        thread.Start();
    }

    public double CalculateFunctionResult(double x, double y)
    {
        return this.GlobalBestIndividual?.CalculateFunctionResult(x, y) ?? 0;
    }

    public ApproximatorState? GetCurrentState()
    {
        if (!this.Running) return null;
        return new ApproximatorState
        (
            this.GlobalBestIndividual?.Error ?? double.PositiveInfinity,
            this.Stopwatch.ElapsedMilliseconds,
            this.LastImprovement,
            this.PopulationsCreated
        );
    }

    private static void InitializeStaticFields(ApproximatorJob job)
    {
        Chromosome.InitialiseChromosomeDecogen(job);
        Individual.InitialiseMetric(job);
    }

    private void Reset()
    {
        this.GlobalBestIndividual = null;
        this.LastImprovement = 0;
        this.Paused = false;
        this.PopulationsCreated = 0;
        this.Stopwatch.Reset();
    }

    public bool Pause()
    {
        if (!this.Running || this.Paused) return false;
        this.Stopwatch.Stop();
        this.Paused = true;
        return true;
    }

    public bool Resume()
    {
        if (!this.Running || !this.Paused) return false;
        this.Stopwatch.Start();
        this.Paused = false;
        return true;
    }
    
    public bool Start(ApproximatorJob job)
    {
        if (this.Running) return false;
        this.Reset();
        this.Running = true;
        this.Approximate(job);
        return true;
    }

    public void Stop()
    {
        this.Running = false;
    }
}
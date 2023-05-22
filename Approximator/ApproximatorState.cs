using Genetics;

namespace Approximator;

public struct ApproximatorState
{
    public long ElapsedTime { get; }
    public Individual GlobalBestIndividual { get; }
    public long LastImprovement { get; }

    public ApproximatorState(long elapsedTime, Individual globalBestIndividual, long lastImprovement)
    {
        this.ElapsedTime = elapsedTime;
        this.GlobalBestIndividual = globalBestIndividual;
        this.LastImprovement = lastImprovement;
    }
}
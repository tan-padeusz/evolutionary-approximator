namespace Data;

public class ApproximatorState
{
    public string AverageError { get; }
    public string ElapsedTime { get; }
    public string LastImprovement { get; }
    public string PopulationsCreated { get; }
    
    public ApproximatorState(double averageError, long elapsedMilliseconds, long lastImprovement, long populationsCreated)
    {
        this.AverageError = $"{averageError}";
        this.ElapsedTime = ApproximatorState.FormatTime(elapsedMilliseconds);
        this.LastImprovement = $"{lastImprovement}";
        this.PopulationsCreated = $"{populationsCreated}";
    }
    
    private static string FormatTime(long milliseconds)
    {
        var seconds = (milliseconds / 1000) % 60;
        var minutes = milliseconds / 60000;

        var minutesString = minutes < 10 ? $"0{minutes}" : minutes.ToString();
        var secondsString = seconds < 10 ? $"0{seconds}" : seconds.ToString();

        return $"{minutesString}:{secondsString}";
    }
}
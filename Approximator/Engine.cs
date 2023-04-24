using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Enums;
using Genetics;
using Structs;

namespace Approximator;

public class Engine
{
    private Population CurrentPopulation { get; set; }
    private ApproximatorForm Form { get; }
    private Individual GlobalBestIndividual { get; set; }
    private long LastImprovement { get; set; }
    private InputPoint[] Points { get; set; }
    private bool Running { get; set; }
    private Stopwatch Stopwatch { get; }
    private int TickCount { get; set; }
    private BackgroundWorker Worker { get; }

    public Engine(ApproximatorForm form)
    {
        this.Form = form;
        this.Running = false;
        this.Stopwatch = new Stopwatch();
        this.Worker = this.CreateWorker();
    }

    private BackgroundWorker CreateWorker()
    {
        var worker = new BackgroundWorker();
        worker.WorkerReportsProgress = true;
        worker.WorkerSupportsCancellation = true;
        worker.DoWork += this.WorkerWork;
        worker.ProgressChanged += this.WorkerProgressChanged;
        worker.RunWorkerCompleted += this.WorkerWorkCompleted;
        return worker;
    }
    
    private string FormatTime(long milliseconds)
    {
        var seconds = (milliseconds / 1000) % 60;
        var minutes = milliseconds / 60000;

        var minutesString = minutes < 10 ? $"0{minutes}" : minutes.ToString();
        var secondsString = seconds < 10 ? $"0{seconds}" : seconds.ToString();

        return $"{minutesString}:{secondsString}";
    }

    private InputPoint[] GeneratePoints(ApproximatorJob job)
    {
        var n = 5;
        var points = new InputPoint[n * n];
        var index = 0;
        var function = Engine.GetPointFunction(job);
        for (var x = 0.0; x < n; x++) for (var y = 0.0; y < n; y++)
        {
            var z = Math.Round(function(x, y), 4);
            points[index++] = new InputPoint(x, y, z);
        }
        return points;
    }
    
    private static Func<double, double, double> GetPointFunction(ApproximatorJob job)
    {
        return job.PointFunction switch
        {
            PointFunction.Cubic => (x, y) => 1.3 * x * x * y - 0.6 * x * y * y - 2.1 * x * x + 1.7 * y * y,
            PointFunction.Quadratic => (x, y) => 2.6 * x * y - 1.2 * x * x + 0.7 * y * y + 3.1 * x - 2.7 * y - 3.4,
            PointFunction.Trigonometric => (x, y) => 2.0 / 3.0 * Math.Sin(x) * Math.Cos(y) + 1.0 / 3.0 * x * y,
            _ => throw new InvalidEnumArgumentException($"Invalid point function : {job.PointFunction}")
        };
    }

    public void Start(ApproximatorJob job)
    {
        if (this.Running) return;
        this.Form.ControlTableOutputControl.Clear();
        this.Running = true;
        this.Worker.RunWorkerAsync(job);
    }

    public void Stop()
    {
        if (!this.Running) return;
        this.Worker.CancelAsync();
        this.Running = false;
    }

    private void WorkerProgressChanged(object? sender, ProgressChangedEventArgs args)
    {
        if (Environment.TickCount - this.TickCount > 1)
        {
            this.TickCount = Environment.TickCount;
            this.Form.AverageErrorControl.SetValue(this.GlobalBestIndividual.Error.ToString());
            this.Form.BestFunctionOutputControl.Text = this.GlobalBestIndividual.ToString();
            this.Form.ElapsedTimeControl.SetValue(this.FormatTime(this.Stopwatch.ElapsedMilliseconds));
            this.Form.LastImprovementControl.SetValue(this.LastImprovement.ToString());
            this.Form.PopulationsCreatedControl.SetValue((this.CurrentPopulation.Id + 1).ToString());
        }
    }
    
    private void WorkerWork(object? sender, DoWorkEventArgs args)
    {
        ApproximatorJob job = (ApproximatorJob)args.Argument!;
        this.Points = this.GeneratePoints(job);
        Individual.SetErrorMetric(job);
        this.Stopwatch.Restart();
        this.TickCount = Environment.TickCount;
        this.CurrentPopulation = job.GeneType switch
        {
            GeneType.Binary => new BinaryPopulation(job, this.Points),
            GeneType.Integer => new IntegerPopulation(job, this.Points),
            GeneType.Real => new RealPopulation(job, this.Points),
            _ => throw new InvalidEnumArgumentException($"Invalid gene type : {job.GeneType}")
        };
        this.GlobalBestIndividual = this.CurrentPopulation.BestIndividual;
        this.LastImprovement = this.CurrentPopulation.Id;

        while (this.Running && !this.Worker.CancellationPending)
        {
            this.CurrentPopulation = job.GeneType switch
            {
                GeneType.Binary => new BinaryPopulation(job, this.Points, this.CurrentPopulation),
                GeneType.Integer => new IntegerPopulation(job, this.Points, this.CurrentPopulation),
                GeneType.Real => new RealPopulation(job, this.Points, this.CurrentPopulation),
                _ => throw new InvalidEnumArgumentException($"Invalid gene type : {job.GeneType}")
            };
            
            if (this.GlobalBestIndividual.IsWorseThan(this.CurrentPopulation.BestIndividual))
            {
                this.GlobalBestIndividual = this.CurrentPopulation.BestIndividual;
                this.LastImprovement = this.CurrentPopulation.Id;
            }
            
            this.Worker.ReportProgress(0, "update ui");
        }
    }

    private void WorkerWorkCompleted(object? sender, RunWorkerCompletedEventArgs args)
    {
        this.Stopwatch.Stop();
        StringBuilder builder = new StringBuilder();
        foreach (InputPoint point in this.Points)
        {
            double result = Math.Round(this.GlobalBestIndividual.CalculateFunctionResult(point), 4);
            double error = Math.Round(Math.Abs(result - point.Z), 4);
            builder.Append($"[ {point.X} | {point.Y} | {point.Z} ] : [ {result} | {error} ]\n");
        }
        builder.Remove(builder.Length - 1, 1);
        this.Form.ControlTableOutputControl.Text = builder.ToString();
    }
}
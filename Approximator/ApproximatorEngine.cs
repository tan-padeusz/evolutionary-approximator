using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Enums;
using Genetics;
using Data;
using Point = Data.Point;

namespace Approximator;

public class ApproximatorEngine
{
    private Population? CurrentPopulation { get; set; }
    // private ApproximatorForm Form { get; }
    private Individual? GlobalBestIndividual { get; set; }
    private long LastImprovement { get; set; }
    public Point[]? Points { get; private set; }
    public bool Running { get; private set; }
    private Stopwatch Stopwatch { get; }
    private int TickCount { get; set; }
    private BackgroundWorker Worker { get; }

    public ApproximatorEngine(ApproximatorForm form)
    {
        // this.Form = form;
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
    
    
    public Point[] GeneratePoints(int pointNumber, int precisionDigits)
    {
        var points = new Point[pointNumber];
        var random = new Random();
        for (var index = 0; index < pointNumber; index++)
        {
            var x = Math.Round(random.NextDouble() * 20 - 10, precisionDigits);
            var y = Math.Round(random.NextDouble() * 20 - 10, precisionDigits);
            var z = Math.Round(random.NextDouble() * 20 - 10, precisionDigits);
            points[index] = new Point(x, y, z);
        }
        this.Points = points;
        return points;
    }

    public ApproximatorState? GetCurrentState()
    {
        if (this.Points == null || this.GlobalBestIndividual == null) return null;
        return new ApproximatorState
        (
            this.Stopwatch.ElapsedMilliseconds,
            this.GlobalBestIndividual,
            this.LastImprovement
        );
    }

    private void InitialiseStaticFields(ApproximatorJob job)
    {
        Chromosome.InitialiseChromosomeWhatever(job);
        Individual.InitialiseMetric(job);
    }
    
    // private static Func<double, double, double> GetPointFunction(ApproximatorJob job)
    // {
    //     return job.PointFunction switch
    //     {
    //         PointFunction.Cubic => (x, y) => 1.3 * x * x * y - 0.6 * x * y * y - 2.1 * x * x + 1.7 * y * y,
    //         PointFunction.Quadratic => (x, y) => 2.6 * x * y - 1.2 * x * x + 0.7 * y * y + 3.1 * x - 2.7 * y - 3.4,
    //         PointFunction.Trigonometric => (x, y) => 2.0 / 3.0 * Math.Sin(x) * Math.Cos(y) + 1.0 / 3.0 * x * y,
    //         _ => throw new InvalidEnumArgumentException($"Invalid point function : {job.PointFunction}")
    //     };
    // }

    public void Start(ApproximatorJob job)
    {
        if (this.Running) return;
        // this.Form.ControlTableOutputControl.Clear();
        this.Running = true;
        this.Worker.RunWorkerAsync(job);
    }

    public void Stop()
    {
        if (!this.Running) return;
        this.Worker.CancelAsync();
        this.Running = false;
    }

    public void VisualiseSolution()
    {
        if (this.Running) return;
        if (this.GlobalBestIndividual == null || this.Points == null) return;
        // this.VisualiseSolution(this.Form.GetPixelsPerOneX(), this.Form.GetPixelsPerOneY());
    }
    
    private void VisualiseSolution(int pixelsPerOneX, int pixelsPerOneY)
    {
        // var graphics = Graphics.FromImage(this.Form.Plot.Image);
        // graphics.Clear(Color.Black);

        const int nodesCount = 31;
        
        const int leftXBias = 300;
        const int rightXBias = 200;
        const int topYBias = 200;
        const int bottomYBias = 100;

        const int pixelsFromLeft = 60;
        const int pixelsFromTop = 210;
        const int pixelsFromRight = pixelsFromLeft + leftXBias + rightXBias;
        const int pixelsFromBottom = pixelsFromTop + topYBias + bottomYBias;

        const int centerX = (pixelsFromLeft + pixelsFromRight) / 2;
        const int centerY = (pixelsFromTop + pixelsFromBottom) / 2;

        const double leftXStep = leftXBias / (nodesCount - 1.0);
        const double rightXStep = rightXBias / (nodesCount - 1.0);
        const double topYStep = topYBias / (nodesCount - 1.0);
        const double bottomYStep = bottomYBias / (nodesCount - 1.0);

        const double range = 0.5 * (nodesCount - 1.0);

        var invertedXScale = leftXStep / pixelsPerOneX;

        int i, j;
        int p, q;

        for (q = 0; q < nodesCount; q++) for (p = 0; p < nodesCount; p++)
        {
            i = pixelsFromLeft + rightXBias + (int) (leftXStep * p - rightXStep * q);
            j = pixelsFromTop + (int) (topYStep * p + bottomYStep * q);
            // graphics.FillEllipse(Brushes.Blue, i - 3, j - 1, 6, 2);
        }
        
        // graphics.DrawLine(Pens.Gray, centerX, centerY - 300, centerX, centerY);
        var (previousI, previousJ) = (centerX, centerY);

        var xs = new int[nodesCount];
        var ys = new int[nodesCount];
        
        for (q = 0; q < nodesCount; q++) for (p = 0; p < nodesCount; p++)
        {
            i = pixelsFromLeft + rightXBias + (int) (leftXStep * p - rightXStep * q);
            j = pixelsFromTop + (int) (topYStep * p + bottomYStep * q);
            var x = invertedXScale * (p - range);
            var y = invertedXScale * (q - range);
            var z = this.GlobalBestIndividual.CalculateFunctionResult(new Point(x, y, 0));
            var k = j - (int) (pixelsPerOneY * z);
            // if (p > 0) graphics.DrawLine(Pens.Red, previousI, previousJ, i, k);
            // if (q > 0) graphics.DrawLine(Pens.Red, xs[p], ys[p], i, k);
            previousI = i;
            previousJ = k;
            xs[p] = i;
            ys[p] = k;
        }
        // this.Form.Plot.Refresh();
    }

    private void WorkerProgressChanged(object? sender, ProgressChangedEventArgs args)
    {
        if (Environment.TickCount - this.TickCount > 1)
        {
            // this.TickCount = Environment.TickCount;
            // this.Form.AverageErrorControl.SetValue(this.GlobalBestIndividual.Error.ToString());
            // this.Form.BestFunctionOutputControl.Text = this.GlobalBestIndividual.ToString();
            // this.Form.ElapsedTimeControl.SetValue(this.FormatTime(this.Stopwatch.ElapsedMilliseconds));
            // this.Form.LastImprovementControl.SetValue(this.LastImprovement.ToString());
            // this.Form.PopulationsCreatedControl.SetValue((this.CurrentPopulation.Id + 1).ToString());
            // this.VisualiseSolution(this.Form.GetPixelsPerOneX(), this.Form.GetPixelsPerOneY());
        }
    }

    private void WorkerWork(object? sender, DoWorkEventArgs args)
    {
        ApproximatorJob job = (ApproximatorJob)args.Argument!;
        // this.Points = this.GeneratePoints(job);
        this.InitialiseStaticFields(job);
        this.Stopwatch.Restart();
        this.TickCount = Environment.TickCount;
        this.CurrentPopulation = new Population(job, this.Points);
        this.GlobalBestIndividual = this.CurrentPopulation.BestIndividual;
        this.LastImprovement = this.CurrentPopulation.Id;
        
        while (this.Running && !this.Worker.CancellationPending)
        {
            this.CurrentPopulation = new Population(job, this.Points, this.CurrentPopulation);
            
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
        foreach (Point point in this.Points)
        {
            double result = Math.Round(this.GlobalBestIndividual.CalculateFunctionResult(point), 4);
            double error = Math.Round(Math.Abs(result - point.Z), 4);
            builder.Append($"[ {point.X} | {point.Y} | {point.Z} ] : [ {result} | {error} ]\n");
        }
        builder.Remove(builder.Length - 1, 1);
        // this.Form.ControlTableOutputControl.Text = builder.ToString();
    }
}
using System.ComponentModel;
using System.Text;
using Enums;
using Data;
using Point = Data.Point;

namespace Genetics;

public class Individual
{
    private Chromosome Chromosome { get; }
    public double Error { get; }
    private float[,] Factors { get; }

    private int PrecisionDigits { get; }

    private static Func<double, double, double> Metric { get; set; } = (_, _) => 0;
    private static Func<double, double> ReversedMetric { get; set; } = _ => 0;

    public Individual(ApproximatorJob job, Point[] points)
    {
        this.PrecisionDigits = job.PrecisionDigits;
        this.Chromosome = new Chromosome();
        this.Factors = this.Chromosome.Decode();
        this.Error = this.EvaluateError(points);
    }

    public Individual(ApproximatorJob job, Point[] points, Individual[] parents)
    {
        this.PrecisionDigits = job.PrecisionDigits;
        this.Chromosome = new Chromosome(job, parents[0].Chromosome, parents[1].Chromosome);
        this.Factors = this.Chromosome.Decode();
        this.Error = this.EvaluateError(points);
    }

    public double CalculateFunctionResult(double x, double y)
    {
        var result = 0.0;
        for (var xPower = 0; xPower < this.Factors.GetLength(1); xPower++)
        for (var yPower = 0; yPower < this.Factors.GetLength(1); yPower++)
        {
            if (xPower + yPower >= this.Factors.GetLength(1)) continue;
            result += this.Factors[xPower, yPower] * Math.Pow(x, xPower) * Math.Pow(y, yPower);
        }
        return Math.Round(result, 4);
    }
    
    private double EvaluateError(Point[] points)
    {
        var error = points.Sum(point => Individual.Metric(point.Z, this.CalculateFunctionResult(point.X, point.Y)));
        return Math.Round(Individual.ReversedMetric(error / points.Length), this.PrecisionDigits);
    }

    public bool IsWorseThan(Individual other)
    {
        return other.Error < this.Error;
    }

    public static void InitialiseMetric(ApproximatorJob job)
    {
        Individual.Metric = job.ErrorMetric switch
        {
            ErrorMetric.ABSOLUTE => (expected, actual) => Math.Abs(expected - actual),
            ErrorMetric.SQUARED => (expected, actual) => Math.Pow(expected - actual, 2),
            ErrorMetric.DOUBLE_SQUARED => (expected, actual) => Math.Pow(expected - actual, job.PrecisionDigits),
            _ => throw new InvalidEnumArgumentException($"Invalid error metric : {job.ErrorMetric}")
        };

        const double quarter = 1.0 / 4.0;
        Individual.ReversedMetric = job.ErrorMetric switch
        {
            ErrorMetric.ABSOLUTE => (value) => value,
            ErrorMetric.SQUARED => Math.Sqrt,
            ErrorMetric.DOUBLE_SQUARED => value => Math.Pow(value, quarter),
            _ => throw new InvalidEnumArgumentException($"Invalid reversed error metric : {job.ErrorMetric}")
        };
    }
    
    public override string ToString()
    {
        var builder = new StringBuilder();
        for (var xPower = 0; xPower < this.Factors.GetLength(1); xPower++)
        for (var yPower = 0; yPower < this.Factors.GetLength(1); yPower++)
        {
            if (xPower + yPower >= this.Factors.GetLength(1)) continue;
            var factor = this.Factors[xPower, yPower];
            if (factor >= 0) builder.Append('+');
            builder.Append($"{factor}[{xPower},{yPower}]");
        }
        return builder.ToString();
    }
}
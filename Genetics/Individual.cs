﻿using System.ComponentModel;
using System.Text;
using Enums;
using Structs;

namespace Genetics;

public abstract class Individual
{
    public double Error { get; protected init; }
    protected double[][] Factors { get; init; }
    private static Func<double, double, double> Metric { get; set; }

    public double CalculateFunctionResult(InputPoint point)
    {
        var result = 0.0;
        for (var degree = 0; degree < this.Factors.Length; degree++) for (var yPower = 0; yPower <= degree; yPower++)
            result += this.Factors[degree][yPower] * Math.Pow(point.X, degree - yPower) * Math.Pow(point.Y, yPower);
        return Math.Round(result, 4);
    }
    
    protected double EvaluateError(InputPoint[] points)
    {
        var error = points.Sum(point => Individual.Metric(this.CalculateFunctionResult(point), point.Z));
        return Math.Round(error / points.Length, 4);
    }

    public bool IsWorseThan(Individual other)
    {
        return other.Error < this.Error;
    }
    
    public static void SetErrorMetric(ApproximatorJob job)
    {
        Individual.Metric = job.ErrorMetric switch
        {
            ErrorMetric.Absolute => (given, expected) => Math.Abs(given - expected),
            ErrorMetric.Squared => (given, expected) => Math.Pow(given - expected, 2),
            ErrorMetric.DoubleSquared => (given, expected) => Math.Pow(given - expected, 4),
            _ => throw new InvalidEnumArgumentException($"Invalid error metric : {job.ErrorMetric}")
        };
    }
    
    public override string ToString()
    {
        var builder = new StringBuilder();
        for (var degree = 0; degree < this.Factors.Length; degree++) for (var yPower = 0; yPower <= degree; yPower++)
        {
            var value = this.Factors[degree][yPower];
            if (value >= 0) builder.Append('+');
            builder.Append($"{value}[{degree - yPower},{yPower}]");
        }
        return builder.ToString();
    }
}
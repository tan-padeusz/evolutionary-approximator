namespace Data;

public struct InputPoint
{
    public double X { get; }
    public double Y { get; }
    public double Z { get; }

    public InputPoint(double x, double y, double z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }
}
namespace Data;

public readonly struct Point
{
    public double X { get; }
    public double Y { get; }
    public double Z { get; }

    public Point(double x, double y, double z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }
    
    public override string ToString()
    {
        return $"{this.X}|{this.Y}|{this.Z}";
    }
}
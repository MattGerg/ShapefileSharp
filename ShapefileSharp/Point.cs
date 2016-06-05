using System;

namespace ShapefileSharp
{
    public class Point : IPoint
    {
        public Point() : base()
        {
        }

        public Point(IPoint point) : this()
        {
            X = point.X;
            Y = point.Y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public void Minimize(IPoint other)
        {
            X = Math.Min(X, other.X);
            Y = Math.Min(Y, other.Y);
        }

        public void Maximize(IPoint other)
        {
            X = Math.Max(X, other.X);
            Y = Math.Max(Y, other.Y);
        }
    }
}

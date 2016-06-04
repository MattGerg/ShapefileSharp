using System;

namespace ShapefileSharp
{
    public class Point : IPoint, IPointM, IPointZ
    {
        public Point() : base()
        {
        }

        public Point(IPoint point) : this()
        {
            X = point.X;
            Y = point.Y;
        }

        public Point(IPointM point) : this((IPoint) point)
        {
            M = point.M;
        }

        public Point(IPointZ point) : this((IPointM) point)
        {
            Z = point.Z;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double M { get; set; }

        public Point Minimize(IPoint other)
        {
            return new Point()
            {
                X = Math.Min(X, other.X),
                Y = Math.Min(Y, other.Y),
            };
        }

        public Point Minimize(IPointM other)
        {
            var point = Minimize((IPoint)other);

            point.M = Math.Min(M, other.M);

            return point;
        }

        public Point Minimize(IPointZ other)
        {
            var point = Minimize((IPointM)other);

            point.Z = Math.Min(Z, other.Z);

            return point;
        }

        public Point Maximize(IPoint other)
        {
            return new Point()
            {
                X = Math.Max(X, other.X),
                Y = Math.Max(Y, other.Y)
            };
        }

        public Point Maximize(IPointM other)
        {
            var point = Maximize((IPoint)other);

            point.M = Math.Max(M, other.M);

            return point;
        }

        public Point Maximize(IPointZ other)
        {
            var point = Maximize((IPointM)other);

            point.Z = Math.Max(Z, other.Z);

            return point;
        }
    }
}

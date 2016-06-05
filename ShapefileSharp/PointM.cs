using System;

namespace ShapefileSharp
{
    class PointM : Point, IPointM
    {
        public PointM() : base()
        {
        }

        public PointM(IPoint point) : base(point)
        {
        }

        public PointM(IPointM point) : this((IPoint) point)
        {
            M = point.M;
        }

        public double M { get; set; }

        public void Minimize(IPointM other)
        {
            Minimize((IPoint)other);

            M = Math.Min(M, other.M);
        }

        public void Maximize(IPointM other)
        {
            Maximize((IPoint)other);

            M = Math.Max(M, other.M);
        }
    }
}

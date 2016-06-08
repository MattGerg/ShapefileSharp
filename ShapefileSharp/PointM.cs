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

        /// <summary>
        /// Any M value less than this is a "no data" value.
        /// </summary>
        public const double NoDataMax = -10e38;

        public double M { get; set; }

        public void Minimize(IPointM other)
        {
            Minimize((IPoint)other);

            if (this.IsNoDataM())
            {
                M = other.M;
                return;
            }

            if (other.IsNoDataM())
            {
                return;
            }

            M = Math.Min(M, other.M);
        }

        public void Maximize(IPointM other)
        {
            Maximize((IPoint)other);

            if (this.IsNoDataM())
            {
                M = other.M;
                return;
            }

            if (other.IsNoDataM())
            {
                return;
            }

            M = Math.Max(M, other.M);
        }
    }
}

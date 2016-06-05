using System;

namespace ShapefileSharp
{
    class PointZ : PointM, IPointZ
    {
        public PointZ() : base()
        {
        }

        public PointZ(IPoint point) : base(point)
        {
        }

        public PointZ(IPointM point) : base(point)
        {
        }

        public PointZ(IPointZ point) : this((IPointM)point)
        {
            Z = point.Z;
        }

        public double Z { get; set; }

        public void Minimize(IPointZ other)
        {
            Minimize((IPointM)other);

            Z = Math.Min(Z, other.Z);
        }

        public void Maximize(IPointZ other)
        {
            Maximize((IPointM)other);

            Z = Math.Max(Z, other.Z);
        }
    }
}

using System.Linq;

namespace ShapefileSharp
{
    public class BoundingBox : IReadOnlyBoundingBox
    {
        public BoundingBox() : base()
        {
        }

        public BoundingBox(IReadOnlyBoundingBox box) : this()
        {
            XMin = box.XMin;
            XMax = box.XMax;
            YMin = box.YMin;
            YMax = box.YMax;
            ZMin = box.ZMin;
            ZMax = box.ZMax;
            MMin = box.MMin;
            MMax = box.MMax;
        }

        private double[] ToArray()
        {
            return new double[] {
                XMin,
                XMax,
                YMin,
                YMax,
                ZMin,
                ZMax,
                MMin,
                MMax
            };
        }

        public override int GetHashCode()
        {
            int hash = 13;

            foreach (double iValue in ToArray()) {
                hash = (hash * 7) + iValue.GetHashCode();
            }

            return hash;
        }

        public override bool Equals(object obj)
        {
            BoundingBox other = obj as BoundingBox;

            if (other == null)
            {
                return false;
            }

            return ToArray().SequenceEqual(other.ToArray());
        }

        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }
        public double ZMin { get; set; }
        public double ZMax { get; set; }
        public double MMin { get; set; }
        public double MMax { get; set; }
    }
}

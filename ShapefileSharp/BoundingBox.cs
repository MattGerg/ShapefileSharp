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

        public override bool Equals(object obj)
        {
            BoundingBox other = obj as BoundingBox;

            if (other == null)
            {
                return false;
            }

            if (!XMin.Equals(other.XMin))
            {
                return false;
            }

            if (!XMax.Equals(other.XMax))
            {
                return false;
            }

            if (!YMin.Equals(other.YMin))
            {
                return false;
            }

            if (!ZMin.Equals(other.ZMin))
            {
                return false;
            }

            if (!ZMax.Equals(other.ZMax))
            {
                return false;
            }

            if (!MMin.Equals(other.MMin))
            {
                return false;
            }

            if (!MMax.Equals(other.MMax))
            {
                return false;
            }

            return true;
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

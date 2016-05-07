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

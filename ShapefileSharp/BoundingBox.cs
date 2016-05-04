namespace ShapefileSharp
{
    public class BoundingBox : IReadOnlyBoundingBox
    {
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

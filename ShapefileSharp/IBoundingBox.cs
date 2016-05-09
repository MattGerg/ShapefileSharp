namespace ShapefileSharp
{
    public interface IBoundingBox
    {
        double XMin { get; }
        double XMax { get; }
        double YMin { get; }
        double YMax { get; }
        double ZMin { get; }
        double ZMax { get; }
        double MMin { get; }
        double MMax { get; }
    }

    public static class IBoundingBoxExtensions
    {
        public static BoundingBox ToMutable(this IBoundingBox box)
        {
            return new BoundingBox(box);
        }
    }
}

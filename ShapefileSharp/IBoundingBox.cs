namespace ShapefileSharp
{
    public interface IBoundingBox : IBoundingBox2d
    {
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

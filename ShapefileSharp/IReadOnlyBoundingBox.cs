namespace ShapefileSharp
{
    public interface IReadOnlyBoundingBox
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

    public static class IReadonlyBoundingBoxExtensions
    {
        public static BoundingBox ToMutable(this IReadOnlyBoundingBox box)
        {
            return new BoundingBox(box);
        }
    }
}

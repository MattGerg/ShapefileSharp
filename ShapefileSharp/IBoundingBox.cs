namespace ShapefileSharp
{
    public interface IBoundingBox<out T> where T:IPoint
    {
        T Min { get; }
        T Max { get; }
    }

    public static class IBoundingBoxExtensions
    {
        internal static BoundingBox<T> ToMutable<T>(this IBoundingBox<T> box) where T:IPoint
        {
            return new BoundingBox<T>(box);
        }
    }
}

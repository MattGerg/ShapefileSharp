namespace ShapefileSharp
{
    internal abstract class Shape : IShape
    {
        public abstract ShapeType ShapeType { get; } 
    }
}

namespace ShapefileSharp
{
    public abstract class Shape : IShape
    {
        public abstract ShapeType ShapeType { get; } 
    }
}

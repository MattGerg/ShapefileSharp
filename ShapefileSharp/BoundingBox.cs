namespace ShapefileSharp
{
    internal sealed class BoundingBox<T> : IBoundingBox<T> where T:IPoint
    { 
        public BoundingBox() : base()
        {
        }

        public BoundingBox(IBoundingBox<T> box) : this(box.Min, box.Max)
        {
        }

        public BoundingBox(T min, T max) : this()
        {
            Min = min;
            Max = max;
        }

        public T Min { get; set; }
        public T Max { get; set; }
    }
}
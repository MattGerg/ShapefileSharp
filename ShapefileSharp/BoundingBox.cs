namespace ShapefileSharp
{
    //TODO: Mutable struct... bad practice?
    public struct BoundingBox<T> : IBoundingBox<T> where T:IPoint
    { 
        public BoundingBox(IBoundingBox<T> box) : this(box.Min, box.Max)
        {
        }

        public BoundingBox(T min, T max)
        {
            Min = min;
            Max = max;
        }

        public T Min { get; set; }
        public T Max { get; set; }
    }
}
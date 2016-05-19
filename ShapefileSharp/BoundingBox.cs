namespace ShapefileSharp
{
    public struct BoundingBox<T> : IBoundingBox<T> where T:IPoint
    { 
        public BoundingBox(IBoundingBox<T> box)
        {
            Min = box.Min;
            Max = box.Max;
        }

        public T Min { get; set; }
        public T Max { get; set; }
    }
}
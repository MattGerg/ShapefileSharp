namespace ShapefileSharp
{
    internal sealed class ShapefileRecord<T> : IShapefileRecord<T> where T : IShape
    {
        public ShapefileRecord() : base()
        {
        }

        public ShapefileRecord(IShpRecord shpRecord) : this()
        {
            RecordNumber = shpRecord.Header.RecordNumber;
            Shape = (T)shpRecord.Shape; //TODO: Create IShpRecord<T> interface?
        }

        public int RecordNumber { get; set; }
        public T Shape { get; set; }

        IShape IShapefileRecord.Shape
        {
            get
            {
                return Shape;
            }
        }
    }
}

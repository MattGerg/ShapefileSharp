namespace ShapefileSharp.Spec
{
    internal sealed class BoundingBox2dField : Field
    {
        public BoundingBox2dField(WordCount offset) : base(offset, WordCount.FromBytes(sizeof(double) * 4))
        {
            XMin = new DoubleField(offset);
            YMin = new DoubleField(XMin.Offset + XMin.Length);
            XMax = new DoubleField(YMin.Offset + YMin.Length);
            YMax = new DoubleField(XMax.Offset + XMax.Length);
        }

        public DoubleField XMin { get; } 
        public DoubleField YMin { get; } 
        public DoubleField XMax { get; } 
        public DoubleField YMax { get; } 
    }
}

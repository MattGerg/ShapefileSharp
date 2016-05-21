namespace ShapefileSharp.Spec
{
    internal sealed class BoundingBox2dField : Field
    {
        public BoundingBox2dField(WordCount offset) : base(offset)
        {
            XMin = new DoubleField(offset);
            YMin = new DoubleField(XMin.Offset + XMin.Length);
            XMax = new DoubleField(YMin.Offset + YMin.Length);
            YMax = new DoubleField(XMax.Offset + XMax.Length);

            Length = XMin.Length + YMin.Length + XMax.Length + YMax.Length;
        }

        public override WordCount Length { get; }

        public DoubleField XMin { get; } 
        public DoubleField YMin { get; } 
        public DoubleField XMax { get; } 
        public DoubleField YMax { get; } 
    }
}

namespace ShapefileSharp.Spec
{
    internal sealed class PointField : Field
    {
        public PointField(WordCount offset) : base(offset, WordCount.FromBytes(sizeof(double) * 2))
        {
            X = new DoubleField(offset);
            Y = new DoubleField(offset + X.Length);
        }

        public DoubleField X { get; }
        public DoubleField Y { get; }
    }
}

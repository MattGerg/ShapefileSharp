namespace ShapefileSharp.Spec
{
    internal sealed class PointField : Field
    {
        public PointField(WordCount offset) : base(offset)
        {
            X = new DoubleField(offset);
            Y = new DoubleField(offset + X.Length);

            Length = X.Length + Y.Length;
        }

        public override WordCount Length { get; }

        public DoubleField X { get; }
        public DoubleField Y { get; }
    }
}

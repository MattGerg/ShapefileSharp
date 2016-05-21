namespace ShapefileSharp.Spec
{
    internal sealed class DoubleField : Field
    {
        public DoubleField(WordCount offset) : base(offset)
        {
            Length = WordCount.FromBytes(sizeof(double));
        }

        public override WordCount Length { get; }
    }
}

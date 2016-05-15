namespace ShapefileSharp.Spec
{
    internal sealed class DoubleField : Field
    {
        public DoubleField(WordCount offset) : base(offset, WordCount.FromBytes(sizeof(double)))
        {
        }
    }
}

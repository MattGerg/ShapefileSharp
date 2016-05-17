namespace ShapefileSharp.Spec
{
    internal sealed class BoundingBox2dField : Field
    {
        public BoundingBox2dField(WordCount offset) : base(offset, WordCount.FromBytes(sizeof(double) * 4))
        {

        }
    }
}

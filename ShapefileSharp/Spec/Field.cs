namespace ShapefileSharp.Spec
{
    internal abstract class Field
    {
        public Field(WordCount offset, WordCount length) : base()
        {
            Offset = offset;
            Length = length;
        }

        public WordCount Offset { get; }
        public WordCount Length { get; }
    }
}

namespace ShapefileSharp.Spec
{
    internal sealed class Field
    {
        public Field(WordCount offset, FieldType type, Endianness endianness) : base()
        {
            Offset = offset;
            Type = type;
            Endianness = endianness;
        }

        public WordCount Offset { get; }
        public FieldType Type { get; }
        public Endianness Endianness { get; }
    }
}

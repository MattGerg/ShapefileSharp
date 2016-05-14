namespace ShapefileSharp.Spec
{
    internal static class ShpSpec
    {
        public static class Record
        {
            public static Field RecordNumber { get; } = new Field(new WordCount(0), FieldType.Int, Endianness.Big);
            public static Field ContentLength { get; } = new Field(new WordCount(1), FieldType.Int, Endianness.Big);
        }
    }
}

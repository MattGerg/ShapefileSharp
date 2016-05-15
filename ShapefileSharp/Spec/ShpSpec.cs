namespace ShapefileSharp.Spec
{
    internal static class ShpSpec
    {
        public static class Record
        {
            public static IntField RecordNumber { get; } = new IntField(WordCount.FromWords(0), Endianness.Big);
            public static IntField ContentLength { get; } = new IntField(WordCount.FromWords(1), Endianness.Big);
        }
    }
}

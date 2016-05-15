namespace ShapefileSharp.Spec
{
    internal static class ShpSpec
    {
        public static class Record
        {
            public static class Header
            {
                public static IntField RecordNumber { get; } = new IntField(WordCount.FromBytes(0), Endianness.Big);
                public static IntField ContentLength { get; } = new IntField(WordCount.FromBytes(4), Endianness.Big);

                public static WordCount Length { get; } = RecordNumber.Length + ContentLength.Length;
            }

            public static IntField ShapeType { get; } = new IntField(Header.Length, Endianness.Little);
        }
    }
}

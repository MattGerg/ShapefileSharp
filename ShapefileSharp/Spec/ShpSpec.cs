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

            public static class Contents
            {
                /// <summary>
                /// The offset, from the beginning of the Shape record, where the Contents begin.
                /// </summary>
                public static WordCount Offset { get; } = Header.Length;

                public static IntField ShapeType { get; } = new IntField(WordCount.FromBytes(0), Endianness.Little);

                public static class PointShape
                {
                    public static DoubleField X { get; } = new DoubleField(WordCount.FromBytes(4));
                    public static DoubleField Y { get; } = new DoubleField(WordCount.FromBytes(12));
                }
            }
        }
    }
}

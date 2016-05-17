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
                    public static PointField Point { get; } = new PointField(WordCount.FromBytes(4));
                }

                public static class MultiPointShape
                {
                    public static class Box
                    {
                        public static DoubleField XMin { get; } = new DoubleField(WordCount.FromBytes(4));
                        public static DoubleField YMin { get; } = new DoubleField(WordCount.FromBytes(12));
                        public static DoubleField XMax { get; } = new DoubleField(WordCount.FromBytes(20));
                        public static DoubleField YMax { get; } = new DoubleField(WordCount.FromBytes(28));
                    }

                    public static IntField NumPoints { get; } = new IntField(WordCount.FromBytes(36), Endianness.Little);

                    public static PointField Point(int numPoint)
                    {
                        return new PointField(WordCount.FromBytes(40 + (numPoint * 32)));
                    }
                }
            }
        }
    }
}

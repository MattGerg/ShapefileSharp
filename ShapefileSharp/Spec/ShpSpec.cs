namespace ShapefileSharp.Spec
{
    internal static class ShpSpec
    {
        public static class Record
        {
            public static ShpRecordHeaderField Header { get; } = new ShpRecordHeaderField(WordCount.FromBytes(0));

            public static class Contents
            {
                /// <summary>
                /// The offset, from the beginning of the Shape record, where the Contents begin.
                /// </summary>
                public static WordCount Offset { get; } = Header.Length;

                public static IntField ShapeType { get; } = new IntField(WordCount.FromBytes(0), Endianness.Little);

                public static class MultiPartShape
                {
                    public static BoundingBox2dField Box { get; } = new BoundingBox2dField(WordCount.FromBytes(4));

                    public static IntField NumParts { get; } = new IntField(WordCount.FromBytes(36), Endianness.Little);
                    public static IntField NumPoints { get; } = new IntField(WordCount.FromBytes(40), Endianness.Little);

                    /// <summary>
                    /// The index of the first point in the part.
                    /// </summary>
                    public static IntField Part(int partIndex)
                    {
                        return new IntField(WordCount.FromBytes(44 + (partIndex * 4)), Endianness.Little);
                    }

                    public static PointField Point(int numParts, int pointIndex)
                    {
                        return new PointField(WordCount.FromBytes(44 + (4 * numParts) + (pointIndex * 16)));
                    }
                }
            }
        }
    }
}

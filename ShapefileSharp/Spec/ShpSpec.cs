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
                    public static BoundingBox2dField Box { get; } = new BoundingBox2dField(WordCount.FromBytes(4));

                    public static IntField NumPoints { get; } = new IntField(WordCount.FromBytes(36), Endianness.Little);

                    public static PointField Point(int pointIndex)
                    {
                        return new PointField(WordCount.FromBytes(40 + (pointIndex * 16)));
                    }
                }

                public static class PolyLineShape
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

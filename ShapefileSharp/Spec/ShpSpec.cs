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
            }
        }
    }
}

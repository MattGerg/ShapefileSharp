namespace ShapefileSharp.Spec
{
    internal static class ShxSpec
    {
        public static class Header
        {
            public static WordCount Pos { get; } = new WordCount(0);
            public static WordCount Length { get; } = new WordCount(50);
        }

        public static long FirstRecordPos = Header.Length.Bytes;
        public const uint RecordBytes = 8;

        public static long GetRecordPos(uint recordIndex)
        {
            return FirstRecordPos + (recordIndex * RecordBytes);
        }
    }
}

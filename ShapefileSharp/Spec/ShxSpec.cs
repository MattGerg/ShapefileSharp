namespace ShapefileSharp.Spec
{
    public static class ShxSpec
    {
        public const uint HeaderPos = 0;
        public const uint HeaderBytes = 100;

        public const uint FirstRecordPos = HeaderBytes;
        public const uint RecordBytes = 8;

        public static uint GetRecordPos(uint recordIndex)
        {
            return FirstRecordPos + (recordIndex * RecordBytes);
        }
    }
}

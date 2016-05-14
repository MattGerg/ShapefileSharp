namespace ShapefileSharp.Spec
{
    internal static class ShpSpec
    {
        public static class Record
        {
            public static class RecordNumber
            {
                public static WordCount Pos { get; } = new WordCount(0);
                public static WordCount Length { get; } = new WordCount(1);
            }

            public static class ConentLength
            {
                public static WordCount Pos { get; } = new WordCount(1);
                public static WordCount Length { get; } = new WordCount(1);
            }
        }
    }
}

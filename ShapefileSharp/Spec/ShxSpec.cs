namespace ShapefileSharp.Spec
{
    internal static class ShxSpec
    {
        public static class Header
        {
            public static WordCount Pos { get; } = new WordCount(0);
            public static WordCount Length { get; } = new WordCount(50);
        }

        public static class Record
        {
            private static class First
            {
                public static WordCount Pos { get; } = Header.Length;
            }

            private static class Offset
            {
                public static WordCount Pos { get; } = new WordCount(0);
                public static WordCount Length { get; } = new WordCount(1);
            }

            private static class ContentLength
            {
                public static WordCount Pos { get; } = new WordCount(1);
                public static WordCount Length { get; } = new WordCount(1);
            }

            public static WordCount Length { get; } = Offset.Length + ContentLength.Length;

            public static WordCount GetPos(int recordIndex)
            {
                return First.Pos + (recordIndex * Length);
            }
        }
    }
}

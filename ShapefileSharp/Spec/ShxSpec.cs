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

            public static WordCount Length { get; } = new WordCount(2);

            public static WordCount GetPos(int recordIndex)
            {
                return First.Pos + (recordIndex * Length);
            }
        }
    }
}

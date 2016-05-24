namespace ShapefileSharp
{
    internal static class ShapefileSpec
    {
        public static readonly WordCount HeaderLength = WordCount.FromWords(50);

        public const uint FileCodePos = 0;
        public const uint FileCodeBytes = 4;

        public const uint FileLengthPos = 24;
        public const uint FileLengthBytes = 4;

        public const uint ShapeTypePos = 32;
        public const uint ShapeTypeBytes = 4;

        public const uint BoundingBoxXMinPos = 36;
        public const uint BoundingBoxXMinBytes = 8;

        public const uint BoundingBoxYMinPos = 44;
        public const uint BoundingBoxYMinBytes = 8;

        public const uint BoundingBoxXMaxPos = 52;
        public const uint BoundingBoxXaxBytes = 8;

        public const uint BoundingBoxYMaxPos = 60;
        public const uint BoundingBoxYMaxBytes = 8;

        public const uint BoundingBoxZMinPos = 68;
        public const uint BoundingBoxZMinBytes = 8;

        public const uint BoundingBoxZMaxPos = 76;
        public const uint BoundingBoxZMaxBytes = 8;

        public const uint BoundingBoxMMinPos = 84;
        public const uint BoundingBoxMMinBytes = 8;

        public const uint BoundingBoxMMaxPos = 92;
        public const uint BoundingBoxMMaxBytes = 8;
    }
}

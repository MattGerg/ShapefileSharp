namespace ShapefileSharp
{
    public static class ShapeMainFileFactory
    {
        public static IShapeMainFile Create(string filePath)
        {
            return new ShapeMainFile(filePath);
        }
    }
}

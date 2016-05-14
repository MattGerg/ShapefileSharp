namespace ShapefileSharp
{
    public static class ShapeMainFileFactory
    {
        public static IShpFile Create(string filePath)
        {
            return new ShapeMainFile(filePath);
        }
    }
}

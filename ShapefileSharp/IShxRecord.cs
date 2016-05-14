namespace ShapefileSharp
{
    public interface IShxRecord
    {
        /// <summary>
        /// The offset from the start of the Main (.shp) File.
        /// </summary>
        WordCount Offset { get; }
        /// <summary>
        /// The length of the record.
        /// </summary>
        WordCount ContentLength { get; }
    }
}

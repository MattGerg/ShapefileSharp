namespace ShapefileSharp
{
    /// <remarks>
    /// Yarrrrrr, matey!
    /// </remarks>
    public interface IPatch<T> : IMultiPointGeometry<T> where T:IPoint
    {
        PartType PatchType { get; }
    }
}

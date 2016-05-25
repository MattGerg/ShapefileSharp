namespace ShapefileSharp.Spec
{
    /// <summary>
    /// A field with a fixed length.
    /// </summary>
    internal abstract class FixedField<T> : Field<T>
    {
        public FixedField(WordCount offset) : base(offset)
        {
        }

        public abstract WordCount Length { get; }
    }
}

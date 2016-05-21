namespace ShapefileSharp.Spec
{
    internal abstract class Field
    {
        protected Field(WordCount offset) : base()
        {
            Offset = offset;
        }

        public WordCount Offset { get; }
        public abstract WordCount Length { get; }
    }
}

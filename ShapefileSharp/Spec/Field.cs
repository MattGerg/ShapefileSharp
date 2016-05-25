using System.IO;

namespace ShapefileSharp.Spec
{
    internal abstract class Field<T>
    {
        protected Field(WordCount offset) : base()
        {
            Offset = offset;
        }

        public WordCount Offset { get; }

        public abstract T Read(BinaryReader reader, WordCount origin);

        public T Read(BinaryReader reader)
        {
            return Read(reader, WordCount.Zero);
        }

        public abstract void Write(BinaryWriter writer, T value, WordCount origin);

        public void Write(BinaryWriter writer, T value)
        {
            Write(writer, value, WordCount.Zero);
        }
    }
}

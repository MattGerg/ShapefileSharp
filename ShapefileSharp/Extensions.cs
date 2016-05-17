using ShapefileSharp.Spec;
using System;
using System.IO;
using System.Linq;

namespace ShapefileSharp
{
    internal static class Extensions
    {
        public static double ReadField(this BinaryReader reader, DoubleField field)
        {
            return reader.ReadField(field, WordCount.FromBytes(0));
        }

        public static double ReadField(this BinaryReader reader, DoubleField field, WordCount origin)
        {
            reader.BaseStream.Position = (origin + field.Offset).Bytes;
            return reader.ReadDouble();
        }

        /// <summary>
        /// Reads an <see cref="int"/> in Big-endian format.
        /// </summary>
        public static int ReadInt32Big(this BinaryReader reader)
        {
            var bytes = reader.ReadBytes(sizeof(int));

            if (BitConverter.IsLittleEndian)
            {
                bytes = bytes.Reverse().ToArray();
            }

            return BitConverter.ToInt32(bytes, 0);
        }
    }
}

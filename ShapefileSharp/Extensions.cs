using ShapefileSharp.Spec;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ShapefileSharp
{
    internal static class Extensions
    {
        public static IBoundingBox<IPoint> ReadField(this BinaryReader reader, BoundingBox2dField field)
        {
            return reader.ReadField(field, WordCount.Zero);
        }

        public static IBoundingBox<IPoint> ReadField(this BinaryReader reader, BoundingBox2dField field, WordCount origin)
        {
            return new BoundingBox<IPoint>()
            {
                Min = new Point()
                {
                    X = reader.ReadField(field.XMin),
                    Y = reader.ReadField(field.YMin)
                },
                Max = new Point()
                {
                    X = reader.ReadField(field.XMax),
                    Y = reader.ReadField(field.YMax)
                }
            };
        }

        public static double ReadField(this BinaryReader reader, DoubleField field)
        {
            return reader.ReadField(field, WordCount.Zero);
        }

        public static double ReadField(this BinaryReader reader, DoubleField field, WordCount origin)
        {
            reader.BaseStream.Position = (origin + field.Offset).Bytes;
            return reader.ReadDouble();
        }

        public static int ReadField(this BinaryReader reader, IntField field)
        {
            return reader.ReadField(field, WordCount.Zero);
        }

        public static int ReadField(this BinaryReader reader, IntField field, WordCount origin)
        {
            reader.BaseStream.Position = (origin + field.Offset).Bytes;

            switch (field.Endianness)
            {
                case Endianness.Little:
                    return reader.ReadInt32();

                case Endianness.Big:
                    return reader.ReadInt32Big();

                default:
                    Debug.Fail("Unimplemented Endianness.");
                    throw new NotImplementedException("Unimplemented Endianness.");

            }
        }

        public static IPoint ReadField(this BinaryReader reader, PointField field)
        {
            return reader.ReadField(field, WordCount.Zero);
        }

        public static IPoint ReadField(this BinaryReader reader, PointField field, WordCount origin)
        {
            return new Point()
            {
                X = reader.ReadField(field.X),
                Y = reader.ReadField(field.Y)
            };
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

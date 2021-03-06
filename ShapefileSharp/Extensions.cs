﻿using ShapefileSharp.Spec;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ShapefileSharp
{
    internal static class Extensions
    {
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

        /// <summary>
        /// Writes an <see cref="int"/> in Big-endian format.
        /// </summary>
        public static void WriteInt32Big(this BinaryWriter writer, int value)
        {
            var bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                bytes = bytes.Reverse().ToArray();
            }

            writer.Write(bytes);
        }
    }
}

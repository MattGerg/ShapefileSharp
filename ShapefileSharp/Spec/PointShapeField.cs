﻿using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class PointShapeField : Field<IPointShape<IPoint>>
    {
        public PointShapeField(WordCount offset) : base(offset)
        {
            Point = new PointField(offset);
        }

        private PointField Point { get; }

        public override WordCount Length
        {
            get
            {
                return Point.Length;
            }
        }

        public override IPointShape<IPoint> Read(BinaryReader reader, WordCount origin)
        {
            var point = Point.Read(reader, origin);

            return new PointShape()
            {
                Point = point
            };
        }
    }
}
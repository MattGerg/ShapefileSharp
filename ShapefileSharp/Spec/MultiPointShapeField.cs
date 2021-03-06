﻿using System;
using System.Collections.Generic;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class MultiPointShapeField : Field<IMultiPointShape<IPoint>>
    {
        public MultiPointShapeField(WordCount offset) : base(offset)
        {
            Box = new BoundingBox2dField(offset);
            NumPoints = new IntField(offset + Box.Length, Endianness.Little);
        }

        private BoundingBox2dField Box { get; }
        private IntField NumPoints { get; }

        private PointField Point(int pointIndex)
        {
            var offset = NumPoints.Offset + NumPoints.Length + (pointIndex * PointField.FieldLength);

            return new PointField(offset);
        }

        public override IMultiPointShape<IPoint> Read(BinaryReader reader, WordCount origin)
        {
            var box = Box.Read(reader, origin);

            var numPoints = NumPoints.Read(reader, origin);
            var points = new List<IPoint>();

            for (var i = 0; i < numPoints; i++)
            {
                var point = Point(i).Read(reader, origin);

                points.Add(point);
            }

            return new MultiPointShape()
            {
                Box = box,
                Points = points
            };
        }

        public override void Write(BinaryWriter writer, IMultiPointShape<IPoint> value, WordCount origin)
        {
            Box.Write(writer, value.Box, origin);
            NumPoints.Write(writer, value.Points.Count, origin);

            for (int i = 0; i < value.Points.Count; i++)
            {
                Point(i).Write(writer, value.Points[i], origin);
            }
        }
    }
}

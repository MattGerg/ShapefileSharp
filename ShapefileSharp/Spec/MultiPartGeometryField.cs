using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ShapefileSharp.Spec
{
    //TODO: Can this be a generic class?  Or do we need MultiPartGeomtryMField and MultiPartGeometryZField as well?
    internal sealed class MultiPartGeometryField : Field<IMultiPartGeometry<IPoint>>
    {
        public MultiPartGeometryField(WordCount offset) : base(offset)
        {
            Box = new BoundingBox2dField(offset);
            NumParts = new IntField(Box.Offset + Box.Length, Endianness.Little);
            NumPoints = new IntField(NumParts.Offset + NumParts.Length, Endianness.Little);
        }

        private BoundingBox2dField Box { get; }
        private IntField NumParts { get; }
        private IntField NumPoints { get; }

        /// <summary>
        /// The index of the first point in the part.
        /// </summary>
        private IntField Part(int partIndex)
        {
            //TODO: Access these literals in a static field kind of way...
            //TODO: Do we have to do all of this math again and again for every part?
            return new IntField(NumPoints.Offset + NumPoints.Length + WordCount.FromBytes(partIndex * 4), Endianness.Little);
        }

        private PointField Point(int numParts, int pointIndex)
        {
            //TODO: Access these literals in a static field kind of way...
            //TODO: Do we have to do all of this math again and again for every point?
            return new PointField(NumPoints.Offset + NumPoints.Length + WordCount.FromBytes((4 * numParts) + (pointIndex * 16)));
        }

        public override IMultiPartGeometry<IPoint> Read(BinaryReader reader, WordCount origin)
        {
            var box = Box.Read(reader, origin);
            var numParts = NumParts.Read(reader, origin);
            var numPoints = NumPoints.Read(reader, origin);

            var pointStartIndices = new List<int>();

            for (var i = 0; i < numParts; i++)
            {
                var pointStartIndex = Part(i).Read(reader, origin);

                pointStartIndices.Add(pointStartIndex);
            }

            var parts = new List<IMultiPointGeometry<IPoint>>();

            for (var i = 0; i < pointStartIndices.Count; i++)
            {
                var startIndex = pointStartIndices[i];
                var endIndex = (pointStartIndices.Count > (i + 1) ? pointStartIndices[i + 1] : numPoints) - 1;
                var part = new MultiPointGeometry<IPoint>();

                for (var iPointIndex = startIndex; iPointIndex <= endIndex; iPointIndex++)
                {
                    var point = Point(numParts, iPointIndex).Read(reader, origin);
                    part.Points.Add(point);
                }

                parts.Add(part);
            }

            return new MultiPartGeometry<IPoint>()
            {
                Box = box,
                Parts = parts
            };
        }

        public override void Write(BinaryWriter writer, IMultiPartGeometry<IPoint> value, WordCount origin)
        {
            Box.Write(writer, value.Box, origin);

            var partCount = value.Parts.Count;
            NumParts.Write(writer, partCount, origin);

            var points = value.Parts.SelectMany(part => part.Points).ToArray();

            NumPoints.Write(writer, points.Length, origin);

            int pointCount = 0;

            for (int i = 0; i < partCount; i++)
            {
                Part(i).Write(writer, pointCount, origin);

                pointCount += value.Parts[i].Points.Count;
            }

            for (int i = 0; i < pointCount; i++)
            {
                Point(partCount, i).Write(writer, points[i], origin);
            }
        }
    }
}

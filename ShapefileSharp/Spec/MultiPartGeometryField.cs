using System.Collections.Generic;
using System.IO;

namespace ShapefileSharp.Spec
{
    //TODO: Can this be a generic class?  Or do we need MultiPartGeomtryMField and MultiPartGeometryZField as well?
    class MultiPartGeometryField : Field<IMultiPartGeometry<IPoint>>
    {
        public MultiPartGeometryField(WordCount offset, WordCount length) : base(offset)
        {
            Length = length;

            Box = new BoundingBox2dField(offset);
            NumParts = new IntField(Box.Offset + Box.Length, Endianness.Little);
            NumPoints = new IntField(NumParts.Offset + NumParts.Length, Endianness.Little);
        }

        public override WordCount Length { get; }

        private BoundingBox2dField Box { get; }
        private IntField NumParts { get; }
        private IntField NumPoints { get; }

        /// <summary>
        /// The index of the first point in the part.
        /// </summary>
        private IntField Part(int partIndex)
        {
            //TODO: Access these literals in a static field kind of way...
            return new IntField(WordCount.FromBytes(44 + (partIndex * 4)), Endianness.Little);
        }

        private PointField Point(int numParts, int pointIndex)
        {
            //TODO: Access these literals in a static field kind of way...
            return new PointField(WordCount.FromBytes(44 + (4 * numParts) + (pointIndex * 16)));
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

            var parts = new List<IReadOnlyList<IPoint>>();

            for (var i = 0; i < pointStartIndices.Count; i++)
            {
                var startIndex = pointStartIndices[i];
                var endIndex = (pointStartIndices.Count > (i + 1) ? pointStartIndices[i + 1] : numPoints) - 1;
                var points = new List<IPoint>();

                for (var iPointIndex = startIndex; iPointIndex <= endIndex; iPointIndex++)
                {
                    var point = Point(numParts, iPointIndex).Read(reader, origin);
                    points.Add(point);
                }

                parts.Add(points);
            }

            return new MultiPartGeometry<IPoint>()
            {
                Box = box,
                Parts = parts
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class ShapeSerializer
    {
        public ShapeSerializer() : base()
        {
        }

        public IShape Deserialize(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                using (var reader = new BinaryReader(ms))
                {
                    //TODO: This entire block could be rolled into a ShapeField?
                    //      Or maybe this whole class just becomes a ShapeField?
                    //TODO: Create ShapeType field?
                    ShapeType shapeType = (ShapeType)ShpSpec.Record.Contents.ShapeType.Read(reader);

                    switch (shapeType)
                    {
                        case ShapeType.NullShape:
                            return new NullShape();

                        case ShapeType.Point:
                            {
                                var shapeField = new PointShapeField(ShpSpec.Record.Contents.ShapeType.Length);

                                return shapeField.Read(reader);
                            }

                        case ShapeType.MultiPoint:
                            {
                                var shapeField = new MultiPointShapeField(ShpSpec.Record.Contents.ShapeType.Length, WordCount.FromBytes(bytes.Length));

                                return shapeField.Read(reader);
                            }

                        case ShapeType.PolyLine: case ShapeType.Polygon:
                            {
                                var box = ShpSpec.Record.Contents.MultiPartShape.Box.Read(reader);
                                var numParts = ShpSpec.Record.Contents.MultiPartShape.NumParts.Read(reader);
                                var numPoints = ShpSpec.Record.Contents.MultiPartShape.NumPoints.Read(reader);

                                var pointStartIndices = new List<int>();

                                for (var i = 0; i < numParts; i++)
                                {
                                    var pointStartIndex = ShpSpec.Record.Contents.MultiPartShape.Part(i).Read(reader);

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
                                        var point = ShpSpec.Record.Contents.MultiPartShape.Point(numParts, iPointIndex).Read(reader);
                                        points.Add(point);                     
                                    }

                                    parts.Add(points.AsReadOnly());
                                }

                                switch (shapeType)
                                {
                                    case ShapeType.PolyLine:
                                        return new PolyLineShape()
                                        {
                                            Box = box,
                                            Lines = parts.AsReadOnly()
                                        };

                                    case ShapeType.Polygon:
                                        return new PolygonShape()
                                        {
                                            Box = box,
                                            Rings = parts.AsReadOnly()
                                        };
                                }
                                
                                throw new Exception("Unpossible.");
                            }

                        default:
                            Debug.Fail(string.Format("Unimplemented ShapeType: {0}", shapeType));
                            throw new NotImplementedException();
                    }
                }
            }
        }
    }
}

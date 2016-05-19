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
                    //TODO: Create ShapeType field?
                    ShapeType shapeType = (ShapeType)reader.ReadField(ShpSpec.Record.Contents.ShapeType);

                    switch (shapeType)
                    {
                        case ShapeType.NullShape:
                            return new NullShape();

                        case ShapeType.Point:
                            {
                                var point = reader.ReadField(ShpSpec.Record.Contents.PointShape.Point);

                                return new PointShape()
                                {
                                    Point = point
                                };
                            }

                        case ShapeType.MultiPoint:
                            {
                                var box = reader.ReadField(ShpSpec.Record.Contents.MultiPointShape.Box);

                                var numPoints = reader.ReadField(ShpSpec.Record.Contents.MultiPointShape.NumPoints);
                                var points = new List<IPoint>();

                                for (var i = 0; i < numPoints; i++)
                                {
                                    var point = reader.ReadField(ShpSpec.Record.Contents.MultiPointShape.Point(i));

                                    points.Add(point);
                                }

                                return new MultiPointShape()
                                {
                                    Box = box,
                                    Points = points.AsReadOnly()
                                };
                            }

                        case ShapeType.PolyLine:
                            {
                                var box = reader.ReadField(ShpSpec.Record.Contents.PolyLineShape.Box);
                                var numParts = reader.ReadField(ShpSpec.Record.Contents.PolyLineShape.NumParts);
                                var numPoints = reader.ReadField(ShpSpec.Record.Contents.PolyLineShape.NumPoints);

                                var pointStartIndices = new List<int>();

                                for (var i = 0; i < numParts; i++)
                                {
                                    var pointStartField = ShpSpec.Record.Contents.PolyLineShape.Part(i);
                                    var pointStartIndex = reader.ReadField(pointStartField);

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
                                        var pointField = ShpSpec.Record.Contents.PolyLineShape.Point(numParts, iPointIndex);
                                        var point = reader.ReadField(pointField);
                                        points.Add(point);                     
                                    }

                                    parts.Add(points.AsReadOnly());
                                }

                                return new PolyLineShape()
                                {
                                    Box = box,
                                    Parts = parts.AsReadOnly()
                                };
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

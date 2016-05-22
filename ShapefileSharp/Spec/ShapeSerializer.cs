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
                    ShapeType shapeType = (ShapeType)ShpSpec.Record.Contents.ShapeType.Read(reader);

                    switch (shapeType)
                    {
                        case ShapeType.NullShape:
                            return new NullShape();

                        case ShapeType.Point:
                            {
                                var pointShapeField = new PointShapeField(ShpSpec.Record.Contents.ShapeType.Length);

                                return pointShapeField.Read(reader);
                            }

                        case ShapeType.MultiPoint:
                            {
                                var box = ShpSpec.Record.Contents.MultiPointShape.Box.Read(reader);

                                var numPoints = ShpSpec.Record.Contents.MultiPointShape.NumPoints.Read(reader);
                                var points = new List<IPoint>();

                                for (var i = 0; i < numPoints; i++)
                                {
                                    var point = ShpSpec.Record.Contents.MultiPointShape.Point(i).Read(reader);

                                    points.Add(point);
                                }

                                return new MultiPointShape()
                                {
                                    Box = box,
                                    Points = points.AsReadOnly()
                                };
                            }

                        case ShapeType.PolyLine: case ShapeType.Polygon:
                            {
                                var box = ShpSpec.Record.Contents.PolyLineShape.Box.Read(reader);
                                var numParts = ShpSpec.Record.Contents.PolyLineShape.NumParts.Read(reader);
                                var numPoints = ShpSpec.Record.Contents.PolyLineShape.NumPoints.Read(reader);

                                var pointStartIndices = new List<int>();

                                for (var i = 0; i < numParts; i++)
                                {
                                    var pointStartIndex = ShpSpec.Record.Contents.PolyLineShape.Part(i).Read(reader);

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
                                        var point = ShpSpec.Record.Contents.PolyLineShape.Point(numParts, iPointIndex).Read(reader);
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
                                            Parts = parts.AsReadOnly()
                                        };

                                    case ShapeType.Polygon:
                                        return new PolygonShape()
                                        {
                                            Box = box,
                                            Parts = parts.AsReadOnly()
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

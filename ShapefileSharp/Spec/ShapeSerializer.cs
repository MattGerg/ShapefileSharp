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

                        case ShapeType.PolyLine:
                            {
                                var shapeField = new PolyLineShapeField(ShpSpec.Record.Contents.ShapeType.Length, WordCount.FromBytes(bytes.Length));

                                return shapeField.Read(reader);
                            }

                        case ShapeType.Polygon:
                            {
                                var shapeField = new PolygonShapeField(ShpSpec.Record.Contents.ShapeType.Length, WordCount.FromBytes(bytes.Length));

                                return shapeField.Read(reader);
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

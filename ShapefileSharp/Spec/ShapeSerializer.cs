﻿using System;
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
                    reader.BaseStream.Position = ShpSpec.Record.Contents.ShapeType.Offset.Bytes; //TODO: Capture in spec class...
                    ShapeType shapeType = (ShapeType)reader.ReadInt32();

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


                        default:
                            Debug.Fail(string.Format("Unimplemented ShapeType: {0}", shapeType));
                            throw new NotImplementedException();
                    }
                }
            }
        }
    }
}

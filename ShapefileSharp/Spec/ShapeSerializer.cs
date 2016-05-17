using System;
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
                            var point = new Point()
                            {
                                X = reader.ReadField(ShpSpec.Record.Contents.PointShape.X),
                                Y = reader.ReadField(ShpSpec.Record.Contents.PointShape.Y)
                            };
                                                        
                            return new PointShape()
                            {
                                Point = point
                            };

                        default:
                            Debug.Fail(string.Format("Unimplemented ShapeType: {0}", shapeType));
                            throw new NotImplementedException();
                    }
                }
            }
        }
    }
}

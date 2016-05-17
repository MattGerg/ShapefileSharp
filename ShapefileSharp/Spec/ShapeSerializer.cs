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
                    reader.BaseStream.Position = 0; //TODO: Capture in spec class...
                    ShapeType shapeType = (ShapeType)reader.ReadInt32();

                    switch (shapeType)
                    {
                        case ShapeType.NullShape:
                            return new NullShape();

                        case ShapeType.Point:
                            var point = new Point();
                            var pointShape = new PointShape()
                            {
                                Point = point
                            };

                            reader.BaseStream.Position = 4; //TODO: 4 should be a const in a Spec class...
                            point.X = reader.ReadDouble();

                            reader.BaseStream.Position = 12; //TODO: 12 should be a const in a Spec class...
                            point.Y = reader.ReadDouble();

                            return pointShape;

                        default:
                            Debug.Fail(string.Format("Unimplemented ShapeType: {0}", shapeType));
                            throw new NotImplementedException();
                    }
                }
            }
        }
    }
}

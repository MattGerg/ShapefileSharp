using System;
using System.Diagnostics;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class ShapeField : Field<IShape>
    {
        public ShapeField(WordCount offset) : base(offset)
        {
            ShapeTypeField = new IntField(offset, Endianness.Little);
        }

        //TODO: Create ShapeType field?
        private IntField ShapeTypeField { get; }

        public override IShape Read(BinaryReader reader, WordCount origin)
        {
            //TODO: Create ShapeType field?
            ShapeType shapeType = (ShapeType)ShapeTypeField.Read(reader, origin);

            switch (shapeType)
            {
                case ShapeType.NullShape:
                    return new NullShape();

                case ShapeType.Point:
                    {
                        var shapeField = new PointShapeField(Offset + ShapeTypeField.Length);

                        return shapeField.Read(reader, origin);
                    }

                case ShapeType.MultiPoint:
                    {
                        var shapeField = new MultiPointShapeField(Offset + ShapeTypeField.Length);

                        return shapeField.Read(reader);
                    }

                case ShapeType.PolyLine:
                    {
                        var shapeField = new PolyLineShapeField(Offset + ShapeTypeField.Length);

                        return shapeField.Read(reader);
                    }

                case ShapeType.Polygon:
                    {
                        var shapeField = new PolygonShapeField(Offset + ShapeTypeField.Length);

                        return shapeField.Read(reader);
                    }

                case ShapeType.PointM:
                    {
                        var shapeField = new PointMShapeField(Offset + ShapeTypeField.Length);

                        return shapeField.Read(reader);
                    }

                case ShapeType.MultiPointM:
                    {
                        var shapeField = new MultiPointMShapeField(Offset + ShapeTypeField.Length);

                        return shapeField.Read(reader);
                    }

                case ShapeType.PointZ:
                    {
                        var shapeField = new PointZShapeField(Offset + ShapeTypeField.Length);

                        return shapeField.Read(reader);
                    }

                default:
                    Debug.Fail(string.Format("Unimplemented ShapeType: {0}", shapeType));
                    throw new NotImplementedException();
            }
        }

        public override void Write(BinaryWriter writer, IShape value, WordCount origin)
        {
            writer.BaseStream.Position = origin.Bytes;
            writer.Write((int)value.ShapeType);

            //TODO: This switch should drive off of the Type somehow...
            //      So then we can serialize a PointZ as a PointM, for example.
            switch (value.ShapeType)
            {
                case ShapeType.NullShape:
                    throw new NotImplementedException();

                case ShapeType.Point:
                    {
                        var shapeField = new PointShapeField(Offset + ShapeTypeField.Length);

                        shapeField.Write(writer, (IPointShape<IPoint>)value); //TODO: Presumptuous cast...
                    }
                    break;

                default:
                    Debug.Fail(string.Format("Unimplemented IShape: {0}", typeof(IShape)));
                    throw new NotImplementedException();
            }
        }
    }
}

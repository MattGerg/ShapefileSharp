using System;
using System.Diagnostics;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class ShapeField : Field<IShape>
    {
        public ShapeField(WordCount offset) : base(offset)
        {
            ShapeTypeField = new ShapeTypeField(offset);
        }

        private ShapeTypeField ShapeTypeField { get; }

        public override IShape Read(BinaryReader reader, WordCount origin)
        {
            ShapeType shapeType = ShapeTypeField.Read(reader, origin);

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

                case ShapeType.PolyLineM:
                    {
                        var shapeField = new PolyLineMShapeField(Offset + ShapeTypeField.Length);

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
            ShapeTypeField.Write(writer, value.ShapeType, origin);

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

                case ShapeType.PolyLine:
                    {
                        var shapeField = new PolyLineShapeField(Offset + ShapeTypeField.Length);

                        shapeField.Write(writer, (IPolyLineShape<IPoint>)value); //TODO: Presumptuous cast...
                    }
                    break;

                case ShapeType.PolyLineM:
                    {
                        var shapeField = new PolyLineMShapeField(Offset + ShapeTypeField.Length);

                        shapeField.Write(writer, (IPolyLineShape<IPointM>)value); //TODO: Presumptuous cast...
                    }
                    break;

                case ShapeType.Polygon:
                    {
                        var shapeField = new PolygonShapeField(Offset + ShapeTypeField.Length);

                        shapeField.Write(writer, (IPolygonShape<IPoint>)value); //TODO: Presumptuous cast...
                    }
                    break;

                case ShapeType.MultiPoint:
                    {
                        var shapeField = new MultiPointShapeField(Offset + ShapeTypeField.Length);

                        shapeField.Write(writer, (IMultiPointShape<IPoint>)value); //TODO: Presumptuous cast...
                    }
                    break;

                case ShapeType.PointM:
                    {
                        var shapeField = new PointMShapeField(Offset + ShapeTypeField.Length);

                        shapeField.Write(writer, (IPointShape<IPointM>)value); //TODO: Presumptuous cast...
                    }
                    break;

                case ShapeType.MultiPointM:
                    {
                        var shapeField = new MultiPointMShapeField(Offset + ShapeTypeField.Length);

                        shapeField.Write(writer, (IMultiPointShape<IPointM>)value); //TODO: Presumptuous cast...
                    }
                    break;

                case ShapeType.PointZ:
                    {
                        var shapeField = new PointZShapeField(Offset + ShapeTypeField.Length);

                        shapeField.Write(writer, (IPointShape<IPointZ>)value); //TODO: Presumptuous cast...
                    }
                    break;

                default:
                    Debug.Fail(string.Format("Unimplemented IShape: {0}", typeof(IShape)));
                    throw new NotImplementedException();
            }
        }
    }
}

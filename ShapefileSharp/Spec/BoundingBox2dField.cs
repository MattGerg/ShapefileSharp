using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class BoundingBox2dField : FixedField<IBoundingBox<IPoint>>
    {
        public BoundingBox2dField(WordCount offset) : base(offset)
        {
            Min = new PointField(offset);
            Max = new PointField(Min.Offset + Min.Length);
        }

        public static readonly WordCount FieldLength = PointField.FieldLength * 2;

        public override WordCount Length
        {
            get
            {
                return FieldLength;
            }
        }

        private PointField Min { get; }
        private PointField Max { get; }

        public override IBoundingBox<IPoint> Read(BinaryReader reader, WordCount origin)
        {
            return new BoundingBox<IPoint>()
            {
                Min = Min.Read(reader, origin),
                Max = Max.Read(reader, origin)
            };
        }

        public override void Write(BinaryWriter writer, IBoundingBox<IPoint> value, WordCount origin)
        {
            throw new NotImplementedException();
        }
    }
}

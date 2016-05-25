using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class PolygonShapeField : Field<IPolygonShape<IPoint>>
    {
        public PolygonShapeField(WordCount offset) : base(offset)
        {
            Geometry = new MultiPartGeometryField(offset);
        }

        private MultiPartGeometryField Geometry { get; }

        public override IPolygonShape<IPoint> Read(BinaryReader reader, WordCount origin)
        {
            var geometry = Geometry.Read(reader, origin);

            return new PolygonShape()
            {
                Box = geometry.Box,
                Rings = geometry.Parts
            };
        }

        public override void Write(BinaryWriter writer, IPolygonShape<IPoint> value, WordCount origin)
        {
            throw new NotImplementedException();
        }
    }
}

using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class PolygonZShapeField : Field<IPolygonShape<IPointZ>>
    {
        public PolygonZShapeField(WordCount offset) : base(offset)
        {
            Geometry = new MultiPartGeometryZField(offset);
        }

        private MultiPartGeometryZField Geometry { get; }

        public override IPolygonShape<IPointZ> Read(BinaryReader reader, WordCount origin)
        {
            var geometry = Geometry.Read(reader, origin);

            return new PolygonZShape()
            {
                Box = geometry.Box,
                Rings = geometry.Parts
            };
        }

        public override void Write(BinaryWriter writer, IPolygonShape<IPointZ> value, WordCount origin)
        {
            var geometry = new MultiPartGeometry<IPointZ>()
            {
                Box = value.Box,
                Parts = value.Rings
            };

            Geometry.Write(writer, geometry, origin);
        }
    }
}
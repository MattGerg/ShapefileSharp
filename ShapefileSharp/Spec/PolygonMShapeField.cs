using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class PolygonMShapeField : Field<IPolygonShape<IPointM>>
    {
        public PolygonMShapeField(WordCount offset) : base(offset)
        {
            Geometry = new MultiPartGeometryMField(offset);
        }

        private MultiPartGeometryMField Geometry { get; }

        public override IPolygonShape<IPointM> Read(BinaryReader reader, WordCount origin)
        {
            var geometry = Geometry.Read(reader, origin);

            return new PolygonMShape()
            {
                Box = geometry.Box,
                Rings = geometry.Parts
            };
        }

        public override void Write(BinaryWriter writer, IPolygonShape<IPointM> value, WordCount origin)
        {
            var geometry = new MultiPartGeometry<IPointM>()
            {
                Box = value.Box,
                Parts = value.Rings
            };

            Geometry.Write(writer, geometry, origin);
        }
    }
}

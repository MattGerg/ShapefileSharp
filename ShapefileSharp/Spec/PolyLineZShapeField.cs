using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class PolyLineZShapeField : Field<IPolyLineShape<IPointZ>>
    {
        public PolyLineZShapeField(WordCount offset) : base(offset)
        {
            Geometry = new MultiPartGeometryZField(offset);
        }

        private MultiPartGeometryZField Geometry { get; }

        public override IPolyLineShape<IPointZ> Read(BinaryReader reader, WordCount origin)
        {
            var geometry = Geometry.Read(reader, origin);

            return new PolyLineZShape()
            {
                Box = geometry.Box,
                Lines = geometry.Parts
            };
        }

        public override void Write(BinaryWriter writer, IPolyLineShape<IPointZ> value, WordCount origin)
        {
            var geometry = new MultiPartGeometry<IPointZ>()
            {
                Box = value.Box,
                Parts = value.Lines
            };

            Geometry.Write(writer, geometry, origin);
        }
    }
}

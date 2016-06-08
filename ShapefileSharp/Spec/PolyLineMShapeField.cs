using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class PolyLineMShapeField : Field<IPolyLineShape<IPointM>>
    {
        public PolyLineMShapeField(WordCount offset) : base(offset)
        {
            Geometry = new MultiPartGeometryMField(offset);
        }

        private MultiPartGeometryMField Geometry { get; }

        public override IPolyLineShape<IPointM> Read(BinaryReader reader, WordCount origin)
        {
            var geometry = Geometry.Read(reader, origin);

            return new PolyLineMShape()
            {
                Box = geometry.Box,
                Lines = geometry.Parts
            };
        }

        public override void Write(BinaryWriter writer, IPolyLineShape<IPointM> value, WordCount origin)
        {
            var geometry = new MultiPartGeometry<IPointM>()
            {
                Box = value.Box,
                Parts = value.Lines
            };

            Geometry.Write(writer, geometry, origin);
        }
    }
}

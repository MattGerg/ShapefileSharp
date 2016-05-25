using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class PolyLineShapeField : Field<IPolyLineShape<IPoint>>
    {
        public PolyLineShapeField(WordCount offset) : base(offset)
        {
            Geometry = new MultiPartGeometryField(offset);
        }

        private MultiPartGeometryField Geometry { get; }

        public override IPolyLineShape<IPoint> Read(BinaryReader reader, WordCount origin)
        {
            var geometry = Geometry.Read(reader, origin);

            return new PolyLineShape()
            {
                Box = geometry.Box,
                Lines = geometry.Parts
            };
        }

        public override void Write(BinaryWriter writer, IPolyLineShape<IPoint> value, WordCount origin)
        {
            throw new NotImplementedException();
        }
    }
}

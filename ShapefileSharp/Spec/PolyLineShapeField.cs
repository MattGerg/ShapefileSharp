using System.IO;

namespace ShapefileSharp.Spec
{
    class PolyLineShapeField : Field<IPolyLineShape<IPoint>>
    {
        public PolyLineShapeField(WordCount offset, WordCount length) : base(offset)
        {
            Geometry = new MultiPartGeometryField(offset, length);
        }

        private MultiPartGeometryField Geometry { get; }

        public override WordCount Length
        {
            get
            {
                return Geometry.Length;
            }
        }

        public override IPolyLineShape<IPoint> Read(BinaryReader reader, WordCount origin)
        {
            var geometry = Geometry.Read(reader, origin);

            return new PolyLineShape()
            {
                Box = geometry.Box,
                Lines = geometry.Parts
            };
        }
    }
}

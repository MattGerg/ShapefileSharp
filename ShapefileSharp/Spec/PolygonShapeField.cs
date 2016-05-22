using System.IO;

namespace ShapefileSharp.Spec
{
    class PolygonShapeField : Field<IPolygonShape<IPoint>>
    {
        public PolygonShapeField(WordCount offset, WordCount length) : base(offset)
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

        public override IPolygonShape<IPoint> Read(BinaryReader reader, WordCount origin)
        {
            var geometry = Geometry.Read(reader, origin);

            return new PolygonShape()
            {
                Box = geometry.Box,
                Rings = geometry.Parts
            };
        }
    }
}

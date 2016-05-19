namespace ShapefileSharp
{
    class PolyLineShape : MultiPartShape<IBoundingBox2d, IPoint>, IPolyLineShape
    {
        public override ShapeType ShapeType
        {
            get
            {
                return ShapeType.PolyLine;
            }
        }
    }
}

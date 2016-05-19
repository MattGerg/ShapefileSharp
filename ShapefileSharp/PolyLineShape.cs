namespace ShapefileSharp
{
    class PolyLineShape : MultiPartShape<IPoint>, IPolyLineShape
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

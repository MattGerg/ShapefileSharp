namespace ShapefileSharp
{
    internal sealed class PolygonShape : MultiPartShape<IPoint>, IPolygonShape
    {
        public override ShapeType ShapeType
        {
            get
            {
                return ShapeType.Polygon;
            }
        }
    }
}

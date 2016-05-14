namespace ShapefileSharp
{
    public sealed class PolygonShape : Shape, IPolygonShape
    {
        public override ShapeType ShapeType
        {
            get
            {
                return ShapeType.Point;
            }
        }

        public IPoint Point { get; set; }
    }
}

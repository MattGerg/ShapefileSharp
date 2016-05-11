namespace ShapefileSharp
{
    public sealed class PointShape : Shape, IPointShape
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

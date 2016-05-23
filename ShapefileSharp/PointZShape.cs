namespace ShapefileSharp
{
    public sealed class PointZShape : Shape, IPointZShape
    {
        public override ShapeType ShapeType
        {
            get
            {
                return ShapeType.PointZ;
            }
        }

        public IPointZ Point { get; set; }
    }
}

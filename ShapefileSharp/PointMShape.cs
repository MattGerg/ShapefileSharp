namespace ShapefileSharp
{
    public sealed class PointMShape : Shape, IPointShape<IPointM>
    {
        public override ShapeType ShapeType
        {
            get
            {
                return ShapeType.PointM;
            }
        }

        public IPointM Point { get; set; }
    }
}

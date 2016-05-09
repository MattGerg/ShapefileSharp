namespace ShapefileSharp
{
    class NullShape : Shape
    {
        public override ShapeType ShapeType
        {
            get
            {
                return ShapeType.NullShape;
            }
        }
    }
}

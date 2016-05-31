namespace ShapefileSharp
{
    //TODO: Shouldn't an IPointZ should equal an IPoint based only on X+Y?  
    //      With this struct, it won't...
    //TODO: Mutable struct... bad practice?
    public struct Point : IPoint, IPointM, IPointZ
    {
        public static readonly Point Empty = new Point();

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double M { get; set; }
    }
}

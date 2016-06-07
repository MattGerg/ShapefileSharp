namespace ShapefileSharp
{
    public interface IPointM : IPoint
    {
        double M { get; }
    }

    public static class IPointMExtensions
    {
        public static bool IsNoDataM(this IPointM point)
        {
            return  point.M < PointM.NoDataMax;
        }
    }
}

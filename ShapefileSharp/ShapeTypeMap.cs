using System;

namespace ShapefileSharp
{
    internal abstract class ShapeTypeMap
    {
        public static ShapeType GetShapeType<T>() where T:IShape
        {
            if (typeof(T).IsEquivalentTo(typeof(IPointShape<IPointZ>)))
            {
                return ShapeType.PointZ;
            }

            if (typeof(T).IsEquivalentTo(typeof(IPointShape<IPointM>)))
            {
                return ShapeType.PointM;
            }

            if (typeof(T).IsEquivalentTo(typeof(IPointShape<IPoint>)))
            {
                return ShapeType.Point;
            }

            if (typeof(T).IsEquivalentTo(typeof(IPolyLineShape<IPoint>)))
            {
                return ShapeType.PolyLine;
            }

            if (typeof(T).IsEquivalentTo(typeof(IPolygonShape<IPoint>)))
            {
                return ShapeType.Polygon;
            }

            if (typeof(T).IsEquivalentTo(typeof(IMultiPointShape<IPoint>)))
            {
                return ShapeType.MultiPoint;
            }

            throw new NotImplementedException(typeof(T).ToString());
        }
    }
}

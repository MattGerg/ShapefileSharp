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

            if (typeof(T).IsEquivalentTo(typeof(IPolyLineShape<IPointZ>)))
            {
                return ShapeType.PolyLineZ;
            }

            if (typeof(T).IsEquivalentTo(typeof(IPolyLineShape<IPointM>)))
            {
                return ShapeType.PolyLineM;
            }

            if (typeof(T).IsEquivalentTo(typeof(IPolyLineShape<IPoint>)))
            {
                return ShapeType.PolyLine;
            }

            if (typeof(T).IsEquivalentTo(typeof(IPolygonShape<IPointM>)))
            {
                return ShapeType.PolygonM;
            }

            if (typeof(T).IsEquivalentTo(typeof(IPolygonShape<IPoint>)))
            {
                return ShapeType.Polygon;
            }

            if (typeof(T).IsEquivalentTo(typeof(IMultiPointShape<IPoint>)))
            {
                return ShapeType.MultiPoint;
            }

            if (typeof(T).IsEquivalentTo(typeof(IMultiPointShape<IPointM>)))
            {
                return ShapeType.MultiPointM;
            }

            if (typeof(T).IsEquivalentTo(typeof(IMultiPointShape<IPointZ>)))
            {
                return ShapeType.MultiPointZ;
            }

            throw new NotImplementedException(typeof(T).ToString());
        }
    }
}

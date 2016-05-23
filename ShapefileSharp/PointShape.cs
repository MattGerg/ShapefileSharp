﻿namespace ShapefileSharp
{
    public sealed class PointShape : Shape, IPointShape<IPoint>
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

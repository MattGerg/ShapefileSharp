using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class BoundingBox2dField : Field<IBoundingBox<IPoint>>
    {
        public BoundingBox2dField(WordCount offset) : base(offset)
        {
            XMin = new DoubleField(offset);
            YMin = new DoubleField(XMin.Offset + XMin.Length);
            XMax = new DoubleField(YMin.Offset + YMin.Length);
            YMax = new DoubleField(XMax.Offset + XMax.Length);

            Length = XMin.Length + YMin.Length + XMax.Length + YMax.Length;
        }

        public override WordCount Length { get; }

        //TODO: These should just be a pair of IPoint fields...
        public DoubleField XMin { get; } 
        public DoubleField YMin { get; } 
        public DoubleField XMax { get; } 
        public DoubleField YMax { get; }

        public override IBoundingBox<IPoint> Read(BinaryReader reader, WordCount origin)
        {
            return new BoundingBox<IPoint>()
            {
                Min = new Point()
                {
                    X = XMin.Read(reader, origin),
                    Y = YMin.Read(reader, origin)
                },
                Max = new Point()
                {
                    X = XMax.Read(reader, origin),
                    Y = YMax.Read(reader, origin)
                }
            };
        }
    }
}

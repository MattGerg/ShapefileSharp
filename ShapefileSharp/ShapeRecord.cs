﻿namespace ShapefileSharp
{
    internal sealed class ShapeRecord : IShapeRecord
    {
        public IRecordHeader Header { get; set; }
        public IShape Shape { get; set; }
    }
}
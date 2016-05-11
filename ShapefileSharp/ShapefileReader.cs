﻿using System;
using System.Diagnostics;
using System.IO;

namespace ShapefileSharp
{
    internal sealed class ShapefileReader : IDisposable
    {
        public ShapefileReader(Stream stream) : base()
        {
            BinaryReader = new BinaryReader(stream);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    BinaryReader.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // GC.SuppressFinalize(this);
        }
        #endregion

        private BinaryReader BinaryReader { get; }

        public IShapefileHeader ReadHeader()
        {
            BinaryReader.BaseStream.Seek(ShapefileSpec.ShapeTypePos, SeekOrigin.Begin);
            var shapeType = (ShapeType) BinaryReader.ReadInt32();

            var boundingBox = new BoundingBox();

            BinaryReader.BaseStream.Seek(ShapefileSpec.BoundingBoxXMinPos, SeekOrigin.Begin);
            boundingBox.XMin = BinaryReader.ReadDouble();

            BinaryReader.BaseStream.Seek(ShapefileSpec.BoundingBoxXMaxPos, SeekOrigin.Begin);
            boundingBox.XMax = BinaryReader.ReadDouble();

            BinaryReader.BaseStream.Seek(ShapefileSpec.BoundingBoxYMinPos, SeekOrigin.Begin);
            boundingBox.YMin = BinaryReader.ReadDouble();

            BinaryReader.BaseStream.Seek(ShapefileSpec.BoundingBoxYMaxPos, SeekOrigin.Begin);
            boundingBox.YMax = BinaryReader.ReadDouble();

            BinaryReader.BaseStream.Seek(ShapefileSpec.BoundingBoxZMinPos, SeekOrigin.Begin);
            boundingBox.ZMin = BinaryReader.ReadDouble();

            BinaryReader.BaseStream.Seek(ShapefileSpec.BoundingBoxZMaxPos, SeekOrigin.Begin);
            boundingBox.ZMax = BinaryReader.ReadDouble();

            BinaryReader.BaseStream.Seek(ShapefileSpec.BoundingBoxMMinPos, SeekOrigin.Begin);
            boundingBox.MMin = BinaryReader.ReadDouble();

            BinaryReader.BaseStream.Seek(ShapefileSpec.BoundingBoxMMaxPos, SeekOrigin.Begin);
            boundingBox.MMax = BinaryReader.ReadDouble();

            return new ShapefileHeader()
            {
                ShapeType = shapeType,
                BoundingBox = boundingBox
            };
        }

        public IShapeRecord ReadShapeRecord(IShapeIndexRecord indexRecord)
        {
            var shapeRecord = new ShapeRecord()
            {
                Header = ReadShapeHeader(indexRecord),
                ShapeType = ReadShapeType(indexRecord)
            };

            switch (shapeRecord.ShapeType)
            {
                case ShapeType.NullShape:
                    shapeRecord.Shape = null;
                    break;

                case ShapeType.Point:
                    shapeRecord.Shape = ReadPointShape(indexRecord);
                    break;

                default:
                    Debug.Fail(string.Format("Unimplemented ShapeType: {0}", shapeRecord.ShapeType));
                    break;
            }

            return shapeRecord;
        }

        private IRecordHeader ReadShapeHeader(IShapeIndexRecord indexRecord)
        {
            var recordHeader = new RecordHeader();

            BinaryReader.BaseStream.Position = indexRecord.Offset.Bytes;
            recordHeader.RecordNumber = BinaryReader.ReadInt32();

            BinaryReader.BaseStream.Position = indexRecord.Offset.Bytes + 4; //TODO: 4 should be a const in a Spec class...
            recordHeader.ContentLength = new WordCount(BinaryReader.ReadInt32());

            return recordHeader;
        }

        private ShapeType ReadShapeType(IShapeIndexRecord indexRecord)
        {
            BinaryReader.BaseStream.Position = indexRecord.Offset.Bytes + 8; //TODO: 8 should be a const in a Spec class...
            return (ShapeType)BinaryReader.ReadInt32();
        }

        private IPointShape ReadPointShape(IShapeIndexRecord indexRecord)
        {
            var point = new Point();
            var pointShape = new PointShape()
            {
                Point = point
            };

            BinaryReader.BaseStream.Position = indexRecord.Offset.Bytes + 12; //TODO: 12 should be a const in a Spec class...
            point.X = BinaryReader.ReadDouble();

            BinaryReader.BaseStream.Position = indexRecord.Offset.Bytes + 20; //TODO: 20 should be a const in a Spec class...
            point.Y = BinaryReader.ReadDouble();

            return pointShape;
        }
    }
}

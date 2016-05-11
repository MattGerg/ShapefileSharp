using System;
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

        public IShapeRecord ReadShapeRecord(ShapeType shapeType, IShapeIndexRecord indexRecord)
        {
            var shapeRecord = new ShapeRecord()
            {
                Header = ReadShapeHeader(indexRecord.Offset)
            };

            switch (shapeType)
            {
                case ShapeType.NullShape:
                    shapeRecord.Shape = null;
                    break;

                default:
                    Debug.Fail(string.Format("Unimplemented ShapeType: {0}", shapeType));
                    break;
            }

            return shapeRecord;
        }

        private IRecordHeader ReadShapeHeader(WordCount offset)
        {
            var recordHeader = new RecordHeader();

            BinaryReader.BaseStream.Position = offset.Bytes;
            recordHeader.RecordNumber = BinaryReader.ReadInt32();

            BinaryReader.BaseStream.Position = offset.Bytes + 4; //TODO: 4 should be a const in a Spec class...
            recordHeader.ContentLength = new WordCount(BinaryReader.ReadInt32());

            return recordHeader;
        }

    }
}

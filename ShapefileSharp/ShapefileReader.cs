using System;
using System.IO;

namespace ShapefileSharp
{
    internal sealed class ShapefileReader : IDisposable
    {
        public ShapefileReader(Stream stream) : base()
        {
            Stream = stream;
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

        private Stream Stream { get; }
        private BinaryReader BinaryReader { get; }

        public IShapefileHeader ReadHeader()
        {
            Stream.Seek(ShapefileSpec.ShapeTypePos, SeekOrigin.Begin);
            var shapeType = (ShapeType) BinaryReader.ReadInt32();

            var boundingBox = new BoundingBox();

            Stream.Seek(ShapefileSpec.BoundingBoxXMinPos, SeekOrigin.Begin);
            boundingBox.XMin = BinaryReader.ReadDouble();

            Stream.Seek(ShapefileSpec.BoundingBoxXMaxPos, SeekOrigin.Begin);
            boundingBox.XMax = BinaryReader.ReadDouble();

            Stream.Seek(ShapefileSpec.BoundingBoxYMinPos, SeekOrigin.Begin);
            boundingBox.YMin = BinaryReader.ReadDouble();

            Stream.Seek(ShapefileSpec.BoundingBoxYMaxPos, SeekOrigin.Begin);
            boundingBox.YMax = BinaryReader.ReadDouble();

            Stream.Seek(ShapefileSpec.BoundingBoxZMinPos, SeekOrigin.Begin);
            boundingBox.ZMin = BinaryReader.ReadDouble();

            Stream.Seek(ShapefileSpec.BoundingBoxZMaxPos, SeekOrigin.Begin);
            boundingBox.ZMax = BinaryReader.ReadDouble();

            Stream.Seek(ShapefileSpec.BoundingBoxMMinPos, SeekOrigin.Begin);
            boundingBox.MMin = BinaryReader.ReadDouble();

            Stream.Seek(ShapefileSpec.BoundingBoxMMaxPos, SeekOrigin.Begin);
            boundingBox.MMax = BinaryReader.ReadDouble();

            return new ShapefileHeader()
            {
                ShapeType = shapeType,
                BoundingBox = boundingBox
            };
        }
    }
}

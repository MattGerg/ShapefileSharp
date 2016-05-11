using System;
using System.IO;

namespace ShapefileSharp
{
    /// <summary>
    /// A Shape Main (.shp) file.
    /// </summary>
    internal sealed class ShapeMainFile : IShapeMainFile, IDisposable
    {
        public ShapeMainFile(string filePath)
        {
            Stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            Reader = new ShapefileReader(Stream);
            Header = Reader.ReadHeader();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Reader.Dispose();
                    Stream.Dispose();
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

        public IShapefileHeader Header { get; }
        private FileStream Stream { get; }
        private ShapefileReader Reader { get; }

        public IShapeRecord GetRecord(IShapeIndexRecord indexRecord)
        {
            return Reader.ReadShapeRecord(Header.ShapeType, indexRecord);
        }
    }
}

using System;
using System.IO;

namespace ShapefileSharp
{
    /// <summary>
    /// A Shapefile main (.shp) file.
    /// </summary>
    internal sealed class ShpFile : IShpFile, IDisposable
    {
        public ShpFile(string filePath)
        {
            Reader = new ShapefileReader(filePath);
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
        private readonly ShapefileReader Reader;

        public IShapeRecord GetRecord(IShapeIndexRecord indexRecord)
        {
            return Reader.ReadShapeRecord(indexRecord);
        }
    }
}

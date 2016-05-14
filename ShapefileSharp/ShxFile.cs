using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ShapefileSharp
{
    /// <summary>
    /// A Shape Index (.shx) file.
    /// </summary>
    internal sealed class ShxFile : IShxFile, IDisposable
    {
        public ShxFile(string filePath) : base()
        {
            FileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            Reader = new ShapefileReader(FileStream);
            Count = Convert.ToInt32((FileStream.Length - ShapeIndexSpec.HeaderBytes) / ShapeIndexSpec.RecordBytes);
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
                    FileStream.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion

        private FileStream FileStream { get; }
        private ShapefileReader Reader { get; }

        public IShapeIndexRecord this[int index]
        {
            get
            {
                return Reader.ReadShapeIndexRecord(index);
            }
        }

        public int Count { get; }

        public IEnumerator<IShapeIndexRecord> GetEnumerator()
        {
            return new ShapeIndexEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

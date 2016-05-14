using System;
using System.Collections;
using System.Collections.Generic;

namespace ShapefileSharp
{
    /// <summary>
    /// A Shape Index (.shx) file.
    /// </summary>
    internal sealed class ShxFile : IShxFile, IDisposable
    {
        public ShxFile(string filePath) : base()
        {
            Reader = new ShxReader(filePath);
            RecordCount = Reader.GetRecordCount();            
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
        }
        #endregion

        private ShxReader Reader { get; }

        public IShxRecord this[int index]
        {
            get
            {
                return Reader.ReadRecord(index);
            }
        }

        public int RecordCount { get; }

        public int Count
        {
            get
            {
                return RecordCount;
            }
        }

        public IEnumerator<IShxRecord> GetEnumerator()
        {
            return new ShapeIndexEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

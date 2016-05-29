using System;
using System.Collections;
using System.Collections.Generic;

namespace ShapefileSharp
{
    /// <summary>
    /// A Shape Index (.shx) file.
    /// </summary>
    internal sealed class ShxFile : IShxFile, IReadOnlyList<IShxRecord>, IDisposable
    {
        public ShxFile(string filePath) : base()
        {
            Reader = new ShxReader(filePath);
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

        public IShapefileHeader Header {
            get
            {
                return Reader.ReadHeader();
            }
        }

        public IReadOnlyList<IShxRecord> Records
        {
            get
            {
                return this;
            }
        }

        int IReadOnlyCollection<IShxRecord>.Count
        {
            get
            {
                return Reader.GetRecordCount();
            }
        }

        IShxRecord IReadOnlyList<IShxRecord>.this[int index]
        {
            get
            {
                return Reader.ReadRecord(index);
            }
        }

        IEnumerator<IShxRecord> IEnumerable<IShxRecord>.GetEnumerator()
        {
            var self = (IReadOnlyList<IShxRecord>)this;

            for (int i = 0; i < self.Count; i++)
            {
                yield return self[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IReadOnlyList<IShxRecord>)this).GetEnumerator();
        }
    }
}

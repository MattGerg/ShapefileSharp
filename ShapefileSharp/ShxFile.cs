using ShapefileSharp.Spec;
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
            Reader = new ShxReader(filePath);

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                //TODO: The reader should figure this out...
                Count = Convert.ToInt32((fs.Length - ShxSpec.Header.Length.Bytes) / ShxSpec.Record.Length.Bytes);
            }                
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

        public IShapeIndexRecord this[int index]
        {
            get
            {
                return Reader.ReadRecord(index);
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

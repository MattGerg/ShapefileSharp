using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ShapefileSharp
{
    internal sealed class ShapeIndex : IShapeIndex, IDisposable
    {
        public ShapeIndex(string filePath) : base()
        {
            FileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
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
                    BinaryReader.Dispose();
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
        private BinaryReader BinaryReader { get; }

        public IShapeIndexRecord this[int index]
        {
            get
            {
                FileStream.Seek(ShapeIndexSpec.GetRecordPos((uint) index), SeekOrigin.Begin);

                return new ShapeIndexRecord()
                {
                    Offset = new WordCount(BinaryReader.ReadInt32()),
                    ContentLength = new WordCount(BinaryReader.ReadInt32())
                };
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

using System;
using System.Collections;
using System.Collections.Generic;

namespace ShapefileSharp
{
    internal sealed class ShapeIndexEnumerator : IEnumerator<IShapeIndexRecord>
    {
        public ShapeIndexEnumerator(IShapeIndex shapeIndex) : base()
        {
            ShapeIndex = shapeIndex;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
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

        private IShapeIndex ShapeIndex { get; }
        private int Position { get; set; } = -1;

        public IShapeIndexRecord Current
        {
            get
            {
                try
                {
                    return ShapeIndex[Position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public bool MoveNext()
        {
            Position++;
            return (Position < ShapeIndex.Count);
        }

        public void Reset()
        {
            Position = -1;
        }
    }
}
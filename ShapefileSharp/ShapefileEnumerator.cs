using System;
using System.Collections;
using System.Collections.Generic;

namespace ShapefileSharp
{
    internal sealed class ShapefileEnumerator : IEnumerator<IShpRecord>
    {
        public ShapefileEnumerator(IShapefile shapefile) : base()
        {
            Shapefile = shapefile;
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

        private IShapefile Shapefile { get; }
        private int Position { get; set; } = -1;

        public IShpRecord Current
        {
            get
            {
                try
                {
                    return Shapefile[Position];
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
            return (Position < Shapefile.Count);
        }

        public void Reset()
        {
            Position = -1;
        }
    }
}
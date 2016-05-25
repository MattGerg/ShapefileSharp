using System;
using System.IO;

namespace ShapefileSharp
{
    public sealed class ShapefileWriter<T> : IDisposable where T : IShape
    {
        public ShapefileWriter(string shpFilePath) : base()
        {
            ShpStream = new FileStream(shpFilePath, FileMode.Create);
            ShxStream = new FileStream(Path.ChangeExtension(shpFilePath, ".shx"), FileMode.Create);

            ShpHeader = new ShapefileHeader()
            {
                FileLength = ShapefileSpec.HeaderLength
            };

            ShxHeader = new ShapefileHeader()
            {
                FileLength = ShapefileSpec.HeaderLength
            };
        }

        private readonly FileStream ShpStream;
        private readonly FileStream ShxStream;

        private readonly ShapefileHeader ShpHeader;
        private readonly ShapefileHeader ShxHeader;

        public void Close()
        {
            Dispose();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //TODO: Write headers to files...

                    ShpStream.Dispose();
                    ShxStream.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

        /// <summary>
        /// The RecordNumber for the next shape.
        /// </summary>
        private int RecordNumber = 1;

        public IShapefileRecord<T> Write(T shape)
        {
            var record = new ShapefileRecord<T>()
            {
                RecordNumber = RecordNumber,
                Shape = shape
            };

            //TODO: Write to the SHP file...

            //TODO: Write to the SHX file...

            RecordNumber++;

            return record;
        }
    }
}

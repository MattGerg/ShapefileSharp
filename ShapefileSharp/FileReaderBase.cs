using System.IO;

namespace ShapefileSharp
{
    internal abstract class FileReaderBase
    {
        public FileReaderBase(string filePath) : base()
        {
            FileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader = new BinaryReader(FileStream);
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
            // GC.SuppressFinalize(this);
        }
        #endregion

        private readonly FileStream FileStream;
        protected BinaryReader BinaryReader { get; }

        public IShapefileHeader ReadHeader()
        {
            var headerField = new Spec.ShapefileHeaderField(WordCount.Zero);

            return headerField.Read(BinaryReader, WordCount.Zero);
        }
    }
}

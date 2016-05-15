using ShapefileSharp.Spec;
using System;
using System.Diagnostics;
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

        protected double ReadField(DoubleField field)
        {
            return ReadField(field, WordCount.FromWords(0));
        }

        protected double ReadField(DoubleField field, WordCount origin)
        {
            BinaryReader.BaseStream.Position = (origin + field.Offset).Bytes;
            return BinaryReader.ReadDouble();
        }

        protected int ReadField(IntField field)
        {
            return ReadField(field, WordCount.FromWords(0));
        }

        protected int ReadField(IntField field, WordCount origin)
        {
            BinaryReader.BaseStream.Position = (origin + field.Offset).Bytes;

            switch (field.Endianness)
            {
                case Endianness.Little:
                    return BinaryReader.ReadInt32();

                case Endianness.Big:
                    return BinaryReader.ReadInt32Big();

                default:
                    Debug.Fail("Unimplemented Endianness.");
                    throw new NotImplementedException("Unimplemented Endianness.");

            }
        }

        public IShapefileHeader ReadHeader()
        {
            BinaryReader.BaseStream.Seek(ShapefileSpec.ShapeTypePos, SeekOrigin.Begin);
            var shapeType = (ShapeType)BinaryReader.ReadInt32();

            var boundingBox = new BoundingBox();

            BinaryReader.BaseStream.Seek(ShapefileSpec.BoundingBoxXMinPos, SeekOrigin.Begin);
            boundingBox.XMin = BinaryReader.ReadDouble();

            BinaryReader.BaseStream.Seek(ShapefileSpec.BoundingBoxXMaxPos, SeekOrigin.Begin);
            boundingBox.XMax = BinaryReader.ReadDouble();

            BinaryReader.BaseStream.Seek(ShapefileSpec.BoundingBoxYMinPos, SeekOrigin.Begin);
            boundingBox.YMin = BinaryReader.ReadDouble();

            BinaryReader.BaseStream.Seek(ShapefileSpec.BoundingBoxYMaxPos, SeekOrigin.Begin);
            boundingBox.YMax = BinaryReader.ReadDouble();

            BinaryReader.BaseStream.Seek(ShapefileSpec.BoundingBoxZMinPos, SeekOrigin.Begin);
            boundingBox.ZMin = BinaryReader.ReadDouble();

            BinaryReader.BaseStream.Seek(ShapefileSpec.BoundingBoxZMaxPos, SeekOrigin.Begin);
            boundingBox.ZMax = BinaryReader.ReadDouble();

            BinaryReader.BaseStream.Seek(ShapefileSpec.BoundingBoxMMinPos, SeekOrigin.Begin);
            boundingBox.MMin = BinaryReader.ReadDouble();

            BinaryReader.BaseStream.Seek(ShapefileSpec.BoundingBoxMMaxPos, SeekOrigin.Begin);
            boundingBox.MMax = BinaryReader.ReadDouble();

            return new ShapefileHeader()
            {
                ShapeType = shapeType,
                BoundingBox = boundingBox
            };
        }
    }
}

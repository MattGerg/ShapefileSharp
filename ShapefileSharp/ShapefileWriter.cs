using ShapefileSharp.Spec;
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

            ShpWriter = new BinaryWriter(ShpStream);
            ShxWriter = new BinaryWriter(ShxStream);

            ShpHeader = new ShapefileHeader()
            {
                FileLength = ShapefileHeaderField.FieldLength
            };

            ShxHeader = new ShapefileHeader()
            {
                FileLength = ShapefileHeaderField.FieldLength
            };
        }

        private readonly FileStream ShpStream;
        private readonly FileStream ShxStream;

        private readonly BinaryWriter ShpWriter;
        private readonly BinaryWriter ShxWriter;

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
            var shpRecordOffset = WordCount.FromBytes(ShpWriter.BaseStream.Position);
            var shpContentOffset = shpRecordOffset + ShpRecordHeaderField.FieldLength;

            var record = new ShapefileRecord<T>()
            {
                RecordNumber = RecordNumber,
                Shape = shape
            };

            //TODO: Write the SHP header first...
            //      But we have to write the shape, to figure out its length, to add the length to the header...
            var shapeField = new ShapeField(shpContentOffset);
            shapeField.Write(ShpWriter, shape);

            var shpStreamPositionAfterRecord = ShpWriter.BaseStream.Position;
                
            var shpHeader = new ShpRecordHeader()
            {
                //TODO: The shapeField should just have a Length... vs assuming the writer will be in the correct position...
                ContentLength = WordCount.FromBytes(shpStreamPositionAfterRecord) - shpContentOffset,
                RecordNumber = RecordNumber
            };

            var shpHeaderField = new ShpRecordHeaderField(shpRecordOffset);
            shpHeaderField.Write(ShpWriter, shpHeader);

            //Reset so we are at the correct position for the next shape...
            ShpWriter.BaseStream.Position = shpStreamPositionAfterRecord;



            var shxRecord = new ShxRecord()
            {
                Offset = shpRecordOffset,
                ContentLength = WordCount.FromBytes((int)ShpWriter.BaseStream.Position) - shpRecordOffset
            };

            var shxRecordField = new ShxRecordField(WordCount.FromBytes(ShxWriter.BaseStream.Position));
            shxRecordField.Write(ShxWriter, shxRecord);


            RecordNumber++;

            return record;
        }
    }
}

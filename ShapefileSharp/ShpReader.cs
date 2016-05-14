using ShapefileSharp.Spec;
using System.Diagnostics;
using System.IO;

namespace ShapefileSharp
{
    internal sealed class ShpReader : FileReaderBase
    {
        public ShpReader(string shpFilePath) : base(shpFilePath)
        {
        }

        public IShpRecord ReadShapeRecord(IShxRecord indexRecord)
        {
            var shapeRecord = new ShpRecord()
            {
                Header = ReadShapeHeader(indexRecord),
            };

            ShapeType shapeType = ReadShapeType(indexRecord);

            switch (shapeType)
            {
                case ShapeType.NullShape:
                    shapeRecord.Shape = null;
                    break;

                case ShapeType.Point:
                    shapeRecord.Shape = ReadPointShape(indexRecord);
                    break;

                default:
                    Debug.Fail(string.Format("Unimplemented ShapeType: {0}", shapeType));
                    break;
            }

            return shapeRecord;
        }

        private IShpRecordHeader ReadShapeHeader(IShxRecord indexRecord)
        {
            var recordHeader = new ShpRecordHeader();

            BinaryReader.BaseStream.Position = (indexRecord.Offset + ShpSpec.Record.RecordNumber.Offset).Bytes;
            recordHeader.RecordNumber = BinaryReader.ReadInt32Big();

            BinaryReader.BaseStream.Position = (indexRecord.Offset + ShpSpec.Record.ContentLength.Offset).Bytes;
            recordHeader.ContentLength = new WordCount(BinaryReader.ReadInt32Big());

            return recordHeader;
        }

        private ShapeType ReadShapeType(IShxRecord indexRecord)
        {
            BinaryReader.BaseStream.Position = indexRecord.Offset.Bytes + 8; //TODO: 8 should be a const in a Spec class...
            return (ShapeType)BinaryReader.ReadInt32();
        }

        private IPointShape ReadPointShape(IShxRecord indexRecord)
        {
            var point = new Point();
            var pointShape = new PointShape()
            {
                Point = point
            };

            BinaryReader.BaseStream.Position = indexRecord.Offset.Bytes + 12; //TODO: 12 should be a const in a Spec class...
            point.X = BinaryReader.ReadDouble();

            BinaryReader.BaseStream.Position = indexRecord.Offset.Bytes + 20; //TODO: 20 should be a const in a Spec class...
            point.Y = BinaryReader.ReadDouble();

            return pointShape;
        }
    }
}

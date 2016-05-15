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
            return new ShpRecordHeader()
            {
                RecordNumber = ReadField(ShpSpec.Record.Header.RecordNumber, indexRecord.Offset),
                //TODO: Just make a WordCount field?
                ContentLength = WordCount.FromWords(ReadField(ShpSpec.Record.Header.ContentLength, indexRecord.Offset))
            };
        }

        private ShapeType ReadShapeType(IShxRecord indexRecord)
        {
            //TODO: Just make a ShapeType field?
            return (ShapeType)ReadField(ShpSpec.Record.ShapeType, indexRecord.Offset);
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

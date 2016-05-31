using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class ShapefileHeaderField : FixedField<IShapefileHeader>
    {
        public ShapefileHeaderField(WordCount offset) : base(offset)
        {
            FileCode = new IntField(offset, Endianness.Big);
            Unused1 = new IntField(offset + WordCount.FromBytes(4), Endianness.Big);
            Unused2 = new IntField(offset + WordCount.FromBytes(8), Endianness.Big);
            Unused3 = new IntField(offset + WordCount.FromBytes(12), Endianness.Big);
            Unused4 = new IntField(offset + WordCount.FromBytes(16), Endianness.Big);
            Unused5 = new IntField(offset + WordCount.FromBytes(20), Endianness.Big);
            FileLength = new WordCountField(offset + WordCount.FromBytes(24));
            Version = new IntField(offset + WordCount.FromBytes(28), Endianness.Little);
            ShapeType = new ShapeTypeField(offset + WordCount.FromBytes(32));
            BoxMin = new PointZField(
                offset + WordCount.FromBytes(36),
                offset + WordCount.FromBytes(44),
                offset + WordCount.FromBytes(68),
                offset + WordCount.FromBytes(84)
                );
            BoxMax = new PointZField(
                offset + WordCount.FromBytes(52),
                offset + WordCount.FromBytes(60),
                offset + WordCount.FromBytes(76),
                offset + WordCount.FromBytes(92)
                );
        }

        public static readonly WordCount FieldLength = WordCount.FromBytes(100);

        public override WordCount Length
        {
            get
            {
                return FieldLength;
            }
        }

        private const int FileCodeValue = 9994;
        private IntField FileCode { get; }
        private const int UnusedValue = 0; 
        private IntField Unused1 { get; }
        private IntField Unused2 { get; }
        private IntField Unused3 { get; }
        private IntField Unused4 { get; }
        private IntField Unused5 { get; }
        private WordCountField FileLength { get; }
        private const int VersionValue = 1000;
        private IntField Version { get; }
        private ShapeTypeField ShapeType { get; }

        //TODO: Can't we have a BoundingBoxZField that accepts 2 PointZFields in its constructor?
        private PointZField BoxMin { get; }
        private PointZField BoxMax { get; }

        public override IShapefileHeader Read(BinaryReader reader, WordCount origin)
        {
            return new ShapefileHeader()
            {
                BoundingBox = new BoundingBox<IPointZ>()
                {
                    Min = BoxMin.Read(reader, origin),
                    Max = BoxMax.Read(reader, origin)
                },
                FileCode = FileCode.Read(reader, origin),
                FileLength = FileLength.Read(reader, origin),
                ShapeType = (ShapeType)ShapeType.Read(reader, origin),
                Version = Version.Read(reader, origin)
            };
        }

        public override void Write(BinaryWriter writer, IShapefileHeader value, WordCount origin)
        {
            FileCode.Write(writer, value.FileCode, origin);
            Unused1.Write(writer, UnusedValue, origin);
            Unused2.Write(writer, UnusedValue, origin);
            Unused3.Write(writer, UnusedValue, origin);
            Unused4.Write(writer, UnusedValue, origin);
            Unused5.Write(writer, UnusedValue, origin);
            FileLength.Write(writer, value.FileLength, origin);
            Version.Write(writer, value.Version, origin);
            ShapeType.Write(writer, value.ShapeType, origin);
            BoxMin.Write(writer, value.BoundingBox.Min, origin);
            BoxMax.Write(writer, value.BoundingBox.Max, origin);
        }
    }
}

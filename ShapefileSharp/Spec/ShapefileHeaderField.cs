using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class ShapefileHeaderField : FixedField<IShapefileHeader>
    {
        public ShapefileHeaderField(WordCount offset) : base(offset)
        {
            FileCode = new IntField(offset, Endianness.Big);
            FileLength = new WordCountField(offset + WordCount.FromBytes(4));
            Version = new IntField(offset, Endianness.Little);
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
            throw new NotImplementedException();
        }
    }
}

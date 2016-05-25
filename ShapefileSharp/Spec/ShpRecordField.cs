using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class ShpRecordField : Field<IShpRecord>
    {
        public ShpRecordField(WordCount offset) : base(offset)
        {
            Header = new ShpRecordHeaderField(offset);
            Shape = new ShapeField(offset + Header.Length);
        }

        private ShpRecordHeaderField Header { get; }
        private ShapeField Shape { get; }

        public override IShpRecord Read(BinaryReader reader, WordCount origin)
        {
            var header = Header.Read(reader, origin);
            var shape = Shape.Read(reader, origin);

            return new ShpRecord()
            {
                Header = header,
                Shape = shape
            };
        }

        public override void Write(BinaryWriter writer, IShpRecord value, WordCount origin)
        {
            throw new NotImplementedException();
        }
    }
}

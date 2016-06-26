using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    internal sealed class PartTypeField : FixedField<PartType>
    {
        public PartTypeField(WordCount offset) : base(offset)
        {
            //In the ESRI spec, ShapeType values are always Little-Endian
            PartTypeIntField = new IntField(offset, Endianness.Little);
        }

        public static readonly WordCount FieldLength = IntField.FieldLength;

        public override WordCount Length
        {
            get
            {
                return FieldLength;
            }
        }

        private IntField PartTypeIntField { get; }

        public override PartType Read(BinaryReader reader, WordCount origin)
        {
            return (PartType)PartTypeIntField.Read(reader, origin);
        }

        public override void Write(BinaryWriter writer, PartType value, WordCount origin)
        {
            PartTypeIntField.Write(writer, (int)value, origin);
        }
    }
}

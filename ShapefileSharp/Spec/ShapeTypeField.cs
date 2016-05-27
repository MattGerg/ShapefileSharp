using System;
using System.IO;

namespace ShapefileSharp.Spec
{
    class ShapeTypeField : FixedField<ShapeType>
    {
        public ShapeTypeField(WordCount offset) : base(offset)
        {
            //In the ESRI spec, ShapeType values are always Little-Endian
            ShapeTypeIntField = new IntField(offset, Endianness.Little); 
        }

        public static readonly WordCount FieldLength = IntField.FieldLength;

        public override WordCount Length
        {
            get
            {
                return FieldLength;
            }
        }

        private IntField ShapeTypeIntField { get; }

        public override ShapeType Read(BinaryReader reader, WordCount origin)
        {
            return (ShapeType)ShapeTypeIntField.Read(reader, origin);
        }

        public override void Write(BinaryWriter writer, ShapeType value, WordCount origin)
        {
            throw new NotImplementedException();
        }
    }
}

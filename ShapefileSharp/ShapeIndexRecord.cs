namespace ShapefileSharp
{
    public sealed class ShapeIndexRecord : IShapeIndexRecord
    {
        public WordCount Offset { get; set; }
        public WordCount ContentLength { get; set; }

        public override int GetHashCode()
        {
            int hash = 13;

            hash = (hash * 7) + Offset.GetHashCode();
            hash = (hash * 7) + ContentLength.GetHashCode();

            return hash;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ShapeIndexRecord;

            if (other == null)
            {
                return false;
            }

            if (!Offset.Equals(other.Offset))
            {
                return false;
            }

            if (!ContentLength.Equals(other.ContentLength))
            {
                return false;
            }

            return true;
        }
    }
}

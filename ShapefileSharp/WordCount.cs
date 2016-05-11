namespace ShapefileSharp
{
    public struct WordCount
    {
        public WordCount(int words)
        {
            Words = words;
        }

        /// <summary>
        /// The number of 16-bit words.
        /// </summary>
        public int Words { get; set; }

        /// <summary>
        /// The number of bytes.
        /// </summary>
        public int Bytes {
            get
            {
                return Words * 2;
            }
        }

        public override int GetHashCode()
        {
            return Words.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is WordCount))
            {
                return false;
            }
            
            WordCount other = (WordCount) obj;

            return Words.Equals(other.Words);
        }
    }
}

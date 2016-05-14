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

        public static WordCount operator +(WordCount wc1, WordCount wc2)
        {
            return new WordCount(wc1.Words + wc2.Words);
        }

        public static WordCount operator *(WordCount wc, int factor)
        {
            return new WordCount(wc.Words * factor);
        }

        public static WordCount operator *(int factor, WordCount wc)
        {
            return wc * factor;
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

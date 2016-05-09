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
    }
}

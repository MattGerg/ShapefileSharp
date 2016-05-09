namespace ShapefileSharp
{
    public struct ContentLength
    {
        /// <summary>
        /// The number of 16-words.
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

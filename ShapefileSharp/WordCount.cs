using System;

namespace ShapefileSharp
{
    public struct WordCount
    {
        private const int BytesPerWord = 2;

        public WordCount(int words)
        {
            Words = words;
        }

        /// <summary>
        /// Create a <see cref="WordCount"/> based on the specified number of bytes.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the specified number of bytes is odd (i.e. un-even).</exception>
        public static WordCount FromBytes(int bytes)
        {
            if (bytes % BytesPerWord != 0)
            {
                throw new ArgumentException("Cannot determine number of words from odd number of bytes.", nameof(bytes));
            }

            return new WordCount(bytes / BytesPerWord);
        }

        public static WordCount FromWords(int words)
        {
            return new WordCount(words);
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
                return Words * BytesPerWord;
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

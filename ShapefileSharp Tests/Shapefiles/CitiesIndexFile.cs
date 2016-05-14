using System;
using System.Collections;
using System.Collections.Generic;

namespace ShapefileSharp.Tests.Shapefiles
{
    /// <summary>
    /// A mock Point Shapefile with hardcoded data.
    /// </summary>
    internal sealed class CitiesIndexFile : IShxFile
    {
        public const string FilePath = "Data/ne_10m_populated_places_simple.shx";

        public CitiesIndexFile() : base()
        {
            var indices = new List<ShxRecord>();

            indices.Add(new ShxRecord()
            {
                Offset = new WordCount(50),
                ContentLength = new WordCount(10)
            });

            Indices = indices.AsReadOnly();
        }

        public IShapefileHeader Header
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private IReadOnlyList<IShxRecord> Indices { get; }

        public IShxRecord this[int index]
        {
            get
            {
                return Indices[index];
            }
        }

        public int RecordCount
        {
            get
            {
                return Indices.Count;
            }
        }

        public int Count
        {
            get
            {
                return RecordCount;
            }
        }

        public IEnumerator<IShxRecord> GetEnumerator()
        {
            return Indices.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Indices.GetEnumerator();
        }
    }
}
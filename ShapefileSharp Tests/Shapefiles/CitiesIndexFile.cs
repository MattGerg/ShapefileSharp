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
            var records = new List<ShxRecord>();

            records.Add(new ShxRecord()
            {
                Offset = new WordCount(50),
                ContentLength = new WordCount(10)
            });

            Records = records;
        }

        public IShapefileHeader Header
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IReadOnlyList<IShxRecord> Records { get; }
    }
}
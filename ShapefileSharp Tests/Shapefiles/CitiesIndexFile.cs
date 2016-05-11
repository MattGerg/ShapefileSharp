﻿using System.Collections;
using System.Collections.Generic;

namespace ShapefileSharp.Tests.Shapefiles
{
    /// <summary>
    /// A mock Point Shapefile with hardcoded data.
    /// </summary>
    internal sealed class CitiesIndexFile : IShapeIndex
    {
        public const string FilePath = "Data/ne_10m_populated_places_simple.shx";

        public CitiesIndexFile() : base()
        {
            var indices = new List<ShapeIndexRecord>();

            indices.Add(new ShapeIndexRecord()
            {
                Offset = new WordCount(50),
                ContentLength = new WordCount(10)
            });

            Indices = indices.AsReadOnly();
        }

        private IReadOnlyList<IShapeIndexRecord> Indices { get; }

        public IShapeIndexRecord this[int index]
        {
            get
            {
                return Indices[index];
            }
        }

        public int Count
        {
            get
            {
                return Indices.Count;
            }
        }

        public IEnumerator<IShapeIndexRecord> GetEnumerator()
        {
            return Indices.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Indices.GetEnumerator();
        }
    }
}
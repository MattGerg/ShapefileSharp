using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ShapefileSharp
{
    public class Shapefile : IShapefile
    {
        public Shapefile(string filePath) : this(new ShapeMainFile(filePath), new ShapeIndexFile(Path.ChangeExtension(filePath, ".shx")))
        {
        }

        public Shapefile(IShapeMainFile shapeMainFile, IShapeIndex shapeIndex) : base()
        {
            ShapeMainFile = shapeMainFile;
            ShapeIndex = shapeIndex;
        }

        private IShapeMainFile ShapeMainFile { get; }
        private IShapeIndex ShapeIndex { get; }

        public IShapefileHeader Header
        {
            get
            {
                return ShapeMainFile.Header;
            }
        }

        public int Count
        {
            get
            {
                return ShapeIndex.Count;
            }
        }

        public IShapeRecord this[int index]
        {
            get
            {
                var indexRecord = ShapeIndex[index];

                return ShapeMainFile.GetRecord(indexRecord);
            }
        }

        public IEnumerator<IShapeRecord> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

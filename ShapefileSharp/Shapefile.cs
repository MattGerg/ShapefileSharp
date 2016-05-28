using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ShapefileSharp
{
    public class Shapefile : IShapefile
    {
        public Shapefile(string filePath) : this(new ShpFile(filePath), new ShxFile(Path.ChangeExtension(filePath, ".shx")))
        {
        }

        public Shapefile(IShpFile shapeMainFile, IShxFile shapeIndex) : base()
        {
            ShapeMainFile = shapeMainFile;
            ShapeIndex = shapeIndex;
        }

        private IShpFile ShapeMainFile { get; }
        private IShxFile ShapeIndex { get; }

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

        public IShapefileRecord this[int index]
        {
            get
            {
                var indexRecord = ShapeIndex[index];

                var shpRecord = ShapeMainFile.GetRecord(indexRecord);

                //TODO: This <IShape> smells bad...
                return new ShapefileRecord<IShape>(shpRecord);
            }
        }

        public IEnumerator<IShapefileRecord> GetEnumerator()
        {
            return new ShapefileEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

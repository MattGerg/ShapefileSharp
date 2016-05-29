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
            Features = new FeatureList(this);
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

        public IReadOnlyList<IShapefileRecord> Features { get; }

        private sealed class FeatureList : IReadOnlyList<IShapefileRecord>
        {
            public FeatureList(Shapefile shapefile)
            {
                Shapefile = shapefile;
            }

            private readonly Shapefile Shapefile;

            public int Count
            {
                get
                {
                    return Shapefile.ShapeIndex.Count;
                }
            }

            public IShapefileRecord this[int index]
            {
                get
                {
                    var indexRecord = Shapefile.ShapeIndex[index];

                    var shpRecord = Shapefile.ShapeMainFile.GetRecord(indexRecord);

                    //TODO: This <IShape> smells bad...
                    return new ShapefileRecord<IShape>(shpRecord);
                }
            }

            public IEnumerator<IShapefileRecord> GetEnumerator()
            {
                for (int i = 0; i < Count; i++)
                {
                    yield return this[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}

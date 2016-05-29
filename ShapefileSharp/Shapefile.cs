using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ShapefileSharp
{
    public class Shapefile : IShapefile
    {
        public Shapefile(string shpFilePath) : this(new ShpFile(shpFilePath), new ShxFile(Path.ChangeExtension(shpFilePath, ".shx")))
        {
        }

        public Shapefile(IShpFile shpFile, IShxFile shxFile) : base()
        {
            ShpFile = shpFile;
            ShxFile = shxFile;
            Features = new FeatureList(this);
        }

        private IShpFile ShpFile { get; }
        private IShxFile ShxFile { get; }

        public IShapefileHeader Header
        {
            get
            {
                return ShpFile.Header;
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
                    return Shapefile.ShxFile.Records.Count;
                }
            }

            public IShapefileRecord this[int index]
            {
                get
                {
                    var indexRecord = Shapefile.ShxFile.Records[index];

                    var shpRecord = Shapefile.ShpFile.GetRecord(indexRecord);

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

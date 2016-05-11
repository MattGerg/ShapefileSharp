namespace ShapefileSharp.Tests.Shapefiles
{
    class CitiesShapefile : Shapefile
    {
        public CitiesShapefile() : base(new CitiesMainFile(), new CitiesIndexFile())
        {
        }
    }
}

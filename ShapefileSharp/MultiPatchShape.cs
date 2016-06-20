using System.Collections.Generic;

namespace ShapefileSharp
{
    internal sealed class MultiPatchShape : IMultiPatchShape<IPointZ>
    {
        public ShapeType ShapeType
        {
            get
            {
                return ShapeType.MultiPatch;
            }
        }

        public IBoundingBox<IPointZ> Box { get; set; }
        public IReadOnlyList<IPatch<IPointZ>> Patches { get; set; }
    }
}

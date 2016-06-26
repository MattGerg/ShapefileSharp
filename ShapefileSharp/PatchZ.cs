using System.Collections.Generic;

namespace ShapefileSharp
{
    internal sealed class PatchZ : IPatch<IPointZ>
    {
        public PartType PatchType { get; set; }
        public IReadOnlyList<IPointZ> Points { get; set; }
    }
}
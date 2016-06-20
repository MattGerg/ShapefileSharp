using System.Collections.Generic;

namespace ShapefileSharp
{
    public interface IMultiPatchShape<T> : IShape<T> where T:IPoint
    {
        IReadOnlyList<IPatch<IPointZ>> Patches { get; }
    }
}

using System;

namespace OpenCAD.OpenCADFormat.ComponentLibraries
{
    [Serializable]
    public enum ComponentPinPolarity
    {
        NotSpecified,
        Positive,
        Negative,
        OpenCollector,
        OpenEmitter
    }
}
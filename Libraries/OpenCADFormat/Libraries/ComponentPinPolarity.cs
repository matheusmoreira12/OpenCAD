using System;

namespace OpenCAD.OpenCADFormat.Libraries
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
using System;

namespace OpenCAD.OpenCADFormat.ComponentLibraries
{
    [Serializable]
    public enum ComponentPinType
    {
        NotSpecified,
        Input,
        Output,
        Passive,
        Power,
        NotConnected
    }
}

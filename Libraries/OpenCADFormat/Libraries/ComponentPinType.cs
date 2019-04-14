using System;

namespace OpenCAD.OpenCADFormat.Libraries
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

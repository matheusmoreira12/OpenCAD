using System;

namespace OpenCAD.OpenCADFormat.Measures
{
    interface IComplementRelationship
    {
    }

    class ComplementRelationship<Q0, Q1>: IComplementRelationship
    {
    }

    static class Complements
    {
        public static IComplementRelationship TimeFrequency = new ComplementRelationship<Quantities.Time
            , Quantities.Frequency>();
        public static IComplementRelationship ResistanceConductance = new ComplementRelationship<Quantities.Resistance
            , Quantities.Conductance>();
    }
}
using System;

namespace OpenCAD.Modules.Math.Solving
{
    public delegate T VariableGetter<T>();

    public delegate void VariableSetter<T>(T value);

    public sealed class SolverVariable<T>
    {
        public readonly VariableGetter<T> Getter;

        public readonly VariableSetter<T> Setter;

        public readonly string Name;

        public SolverVariable(VariableGetter<T> getter, VariableSetter<T> setter, string name)
        {
            Getter = getter ?? throw new ArgumentNullException(nameof(getter));
            Setter = setter ?? throw new ArgumentNullException(nameof(setter));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}

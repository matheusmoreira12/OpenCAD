using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenCAD.APIs.Math
{
    public static class Helpers
    {
        public static bool IsCastableTo(this Type from, Type to, bool implicitly = false)
        {
            return to.IsAssignableFrom(from) || from.HasCastDefined(to, implicitly);
        }

        static bool HasCastDefined(this Type from, Type to, bool implicitly)
        {
            if ((from.IsPrimitive || from.IsEnum) && (to.IsPrimitive || to.IsEnum))
            {
                if (!implicitly)
                    return from == to || (from != typeof(bool) && to != typeof(bool));

                Type[][] typeHierarchy = {
                    new Type[] { typeof(byte),  typeof(sbyte), typeof(char) },
                    new Type[] { typeof(short), typeof(ushort) },
                    new Type[] { typeof(int), typeof(uint) },
                    new Type[] { typeof(long), typeof(ulong) },
                    new Type[] { typeof(float) },
                    new Type[] { typeof(double) }
                };

                IEnumerable<Type> lowerTypes = Enumerable.Empty<Type>();
                foreach (Type[] types in typeHierarchy)
                {
                    if (types.Any(t => t == to))
                        return lowerTypes.Any(t => t == from);
                    lowerTypes = lowerTypes.Concat(types);
                }

                return false;   // IntPtr, UIntPtr, Enum, Boolean
            }
            return IsCastDefined(to, m => m.GetParameters()[0].ParameterType, _ => from, implicitly, false)
                || IsCastDefined(from, _ => to, m => m.ReturnType, implicitly, true);
        }

        static bool IsCastDefined(Type type, Func<MethodInfo, Type> baseType,
                                Func<MethodInfo, Type> derivedType, bool implicitly, bool lookInBase)
        {
            var bindinFlags = BindingFlags.Public | BindingFlags.Static
                            | (lookInBase ? BindingFlags.FlattenHierarchy : BindingFlags.DeclaredOnly);
            return type.GetMethods(bindinFlags).Any(
                m => (m.Name == "op_Implicit" || (!implicitly && m.Name == "op_Explicit"))
                    && baseType(m).IsAssignableFrom(derivedType(m)));
        }
    }
}

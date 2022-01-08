using System;
using System.Linq;
using System.Reflection;

namespace OpenCAD.DataTypes
{
    static class ReflectionUtils
    {
        public static FieldInfo[] GetPublicStaticFields(Type type)
            => type.GetFields(BindingFlags.Public | BindingFlags.Static);
    }
}

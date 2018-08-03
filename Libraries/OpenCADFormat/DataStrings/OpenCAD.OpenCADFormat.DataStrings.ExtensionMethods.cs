using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenCAD.OpenCADFormat.DataStrings
{

    internal static class ExtensionMethods
    {
        private static IEnumerable<MemberInfo> getMatchingMember(Type type,
            MemberTypes memberType = MemberTypes.All, Type reflectedType = null, Type declaringType = null,
            params string[] memberNames)
        {
            MemberInfo[] members = type.GetMembers();

            foreach (var member in members)
            {
                bool memberTypeMatches = (memberType & member.MemberType) == member.MemberType,
                    memberNamesMatch = memberNames.Length == 0 || memberNames.Contains(member.Name);

                if (memberTypeMatches && memberNamesMatch)
                    yield return member;
            }
        }

        public static MemberInfo[] GetMatchingMembers(this Type type,
            MemberTypes memberType = MemberTypes.All, Type reflectedType = null, Type declaringType = null,
            params string[] memberNames)
        {
            return getMatchingMember(type, memberType, reflectedType, declaringType, memberNames).ToArray();
        }

        public static object GetValue(this MemberInfo member, object obj)
        {
            if (member.MemberType == MemberTypes.Property)
            {
                PropertyInfo property = (PropertyInfo)member;

                if (property.CanRead)
                    return property.GetMethod.Invoke(obj, new object[0]);
            }
            else if (member.MemberType == MemberTypes.Field)
                return ((FieldInfo)member).GetValue(obj);

            return null;
        }
    }

}
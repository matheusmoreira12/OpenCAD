using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenCAD.APIs.Math.Enumerations
{
    public abstract class Enumeration<T> where T : Enumeration<T>
    {
        private static IEnumerable<T> getAllFlags()
        {
            Type enumType = typeof(T);
            IEnumerable<PropertyInfo> staticProps = enumType.GetProperties(BindingFlags.Public | BindingFlags.Static);
            IEnumerable<PropertyInfo> flagProps = staticProps.Where(p => p.PropertyType == typeof(T));
            IEnumerable<T> flags = flagProps.Select(p => (T)p.GetValue(null));
            return flags;
        }

        private int inferValue()
        {
            IEnumerable<T> flags = getAllFlags();
            int value = 0;
            foreach (T flag in flags)
            {
                if (flag.value != null)
                    value = (int)flag.value;

                if (flag == this)
                    return value;

                value++;
            }
            return 0;
        }

        public static explicit operator int(Enumeration<T> flag) => flag.Value;

        public static explicit operator Enumeration<T>(int value)
        {
            IEnumerable<T> flags = getAllFlags();
            T matchingFlag = flags.FirstOrDefault(f => f.value == value);
            if (matchingFlag is null)
                throw new KeyNotFoundException("No flag matches the specified value.");
            else
                return matchingFlag;
        }

        protected Enumeration(int value)
        {
            this.value = value;
        }

        protected Enumeration() { }

        protected int Value => value ?? inferValue();

        private int? value = null;
    }
}

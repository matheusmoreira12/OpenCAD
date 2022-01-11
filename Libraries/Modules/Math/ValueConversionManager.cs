using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OpenCAD.Modules.Math
{
    public static class ValueConversionManager
    {
        static ValueConversionManager()
        {
            RuntimeHelpers.RunClassConstructor(typeof(Math).TypeHandle);
        }

        private static List<ValueConversion> RegisteredConversions = new List<ValueConversion> { };

        public static void RegisterMany(params ValueConversion[] conversions) => conversions.AsParallel().ForAll(conversion => Register(conversion));

        public static void Register(ValueConversion conversion)
        {
            if (IsRegistered(conversion))
                return;
            RegisteredConversions.Add(conversion);
        }

        public static void UnregisterMany(params ValueConversion[] conversions) => conversions.AsParallel().ForAll(conversion => Unregister(conversion));

        public static void Unregister(ValueConversion conversion) => RegisteredConversions.Remove(conversion);

        public static ValueConversion[] GetAll() => RegisteredConversions.ToArray();

        public static bool IsRegistered(ValueConversion conversion) => GetExact(conversion.InputType, conversion.OutputType) != null;

        public static ValueConversion GetExact(Type inputType, Type outputType)
            => RegisteredConversions.AsParallel().FirstOrDefault(conversion => conversion.InputType == inputType && conversion.OutputType == outputType);

        public static IEnumerable<ValueConversion> GetAll(Type inputType)
            => RegisteredConversions.AsParallel().Where(conversion => conversion.InputType == inputType);
    }
}

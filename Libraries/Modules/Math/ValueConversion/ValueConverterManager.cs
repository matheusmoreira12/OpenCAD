using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OpenCAD.Modules.Math.ValueConversion
{
    public static class ValueConverterManager
    {
        static ValueConverterManager()
        {
            RuntimeHelpers.RunClassConstructor(typeof(Math).TypeHandle);
        }

        private static List<ValueConverter> RegisteredConversions = new List<ValueConverter> { };

        public static void RegisterMany(params ValueConverter[] conversions) => conversions.AsParallel().ForAll(conversion => Register(conversion));

        public static void Register(ValueConverter conversion)
        {
            if (IsRegistered(conversion))
                return;
            RegisteredConversions.Add(conversion);
        }

        public static void UnregisterMany(params ValueConverter[] conversions) => conversions.AsParallel().ForAll(conversion => Unregister(conversion));

        public static void Unregister(ValueConverter conversion) => RegisteredConversions.Remove(conversion);

        public static ValueConverter[] GetAll() => RegisteredConversions.ToArray();

        public static bool IsRegistered(ValueConverter conversion) => TryGetExact(conversion.InputType, conversion.OutputType, out _);

        public static ValueConverter GetExact(Type inputType, Type outputType)
        {
            ValueConverter converter;
            if (TryGetExact(inputType, outputType, out converter))
                return converter;
            throw new KeyNotFoundException();
        }

        public static bool TryGetExact(Type inputType, Type outputType, out ValueConverter converter)
        {
            converter = RegisteredConversions.AsParallel().FirstOrDefault(conversion
                => conversion.InputType == inputType && conversion.OutputType == outputType);
            if (converter == null)
                return false;
            return true;
        }

        public static IEnumerable<ValueConverter> GetAll(Type inputType)
            => RegisteredConversions.AsParallel().Where(conversion => conversion.InputType == inputType);
    }
}

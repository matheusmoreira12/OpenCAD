using OpenCAD.Modules.Math.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.Modules.Math.ValueConversion
{
	public static class ValueConverterManager
	{
		private static List<ValueConverter> RegisteredConversions = new List<ValueConverter> { };

		public static void RegisterMany(params ValueConverter[] conversions)
		{
			foreach (var conversion in conversions) Register(conversion);
		}


		public static void Register(ValueConverter conversion)
		{
			if (IsRegistered(conversion))
				return;
			RegisteredConversions.Add(conversion);
		}

		public static void UnregisterMany(params ValueConverter[] conversions)
		{
			foreach (var conversion in conversions) Unregister(conversion);
		}


		public static void Unregister(ValueConverter conversion) => RegisteredConversions.Remove(conversion);

		public static ValueConverter[] GetAll() => RegisteredConversions.ToArray();

		public static bool IsRegistered(ValueConverter conversion) =>
			TryGet(conversion.InputType, conversion.OutputType, out _);

		public static ValueConverter Get(Type inputType, Type outputType) =>
			TryGet(inputType, outputType, out var converter) ? converter :
				throw new ValueConverterNotFoundException();

		public static ValueConverter Get(Type inputType, Type outputType, ValueConverterDirection directions) =>
			TryGet(inputType, outputType, directions, out var converter) ? converter :
				throw new ValueConverterNotFoundException();

		public static bool TryGet(Type inputType, Type outputType, out ValueConverter converter)
		{
			converter = RegisteredConversions.FirstOrDefault(conversion
				=> conversion.InputType == inputType && conversion.OutputType == outputType);
			if (converter == default)
				return false;
			return true;
		}

		public static bool TryGet(Type inputType,
								 Type outputType,
								 ValueConverterDirection directions,
								 out ValueConverter converter)
		{
			converter = RegisteredConversions.FirstOrDefault(conversion
				=> conversion.AllowedDirections.HasFlag(directions) &&
					conversion.InputType == inputType && conversion.OutputType == outputType);
			if (converter == default)
				return false;
			return true;
		}

		public static IEnumerable<ValueConverter> GetAll(Type inputType)
			=> RegisteredConversions.Where(conversion => conversion.InputType == inputType);
	}
}

using OpenCAD.Modules.Math.Exceptions;
using OpenCAD.Modules.Math.ValueConversion.DefaultConverters;
using System;

namespace OpenCAD.Modules.Math.ValueConversion
{
	public abstract class ValueConverter
	{
		static ValueConverter()
		{
			registerDefaultValueConversions();
		}

		#region Default Conversions
		static void registerDefaultValueConversions()
		{
			ValueConverterManager.RegisterMany(
				new DecimalToDoubleConverter(),
				new ULongToDoubleConverter(),
				new LongToDoubleConverter(),
				new IntToDoubleConverter(),
				new UShortToDoubleConverter(),
				new ShortToDoubleConverter(),
				new ByteToDoubleConverter(),
				new FloatToDoubleConverter()
			);
		}
		#endregion

		/// <summary>
		/// Converts a value from an input type to an output type.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="value">The value being converted.</param>
		/// <returns>The conversion result.</returns>
		/// <exception cref="ValueConversionNotFoundException">If a conversion is not possible.</exception>
		public static TOut ConvertValue<TIn, TOut>(TIn value) => GetConversionPredicate<TIn, TOut>()(value);

		/// <summary>
		/// Converts a value from an input type to an output type.
		/// </summary>
		/// <param name="outputType">The output type.</param>
		/// <param name="value">The value being converted.</param>
		/// <returns>The conversion result.</returns>
		/// <exception cref="ValueConversionNotFoundException">If a conversion is not possible.</exception>
		public static object ConvertValue(Type outputType, object value) =>
			GetConversionPredicate(value?.GetType(), outputType)(value);

		/// <summary>
		/// Gets the predicate that converts a value of the specified input type into the specified output type.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <returns>The conversion predicate.</returns>
		/// <exception cref="ValueConverterNotFoundException">If a conversion is not possible.</exception>
		public static Func<TIn, TOut> GetConversionPredicate<TIn, TOut>() =>
			TryGetConversionPredicate<TIn, TOut>(out var predicate) ? predicate :
				throw new ValueConverterNotFoundException();

		/// <summary>
		/// Gets the predicate that converts a value of the specified input type into the specified output type.
		/// </summary>
		/// <param name="inputType">The input type.</param>
		/// <param name="outputType">The output type.</param>
		/// <returns>The conversion predicate.</returns>
		/// <exception cref="ValueConverterNotFoundException">If a conversion is not possible.</exception>
		public static Func<object, object> GetConversionPredicate(Type inputType, Type outputType) =>
			TryGetConversionPredicate(inputType, outputType, out var predicate) ? predicate :
				throw new ValueConverterNotFoundException();

		/// <summary>
		/// Gets the predicate that converts a value of the specified input type into the specified output type.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="predicate">The conversion predicate.</param>
		/// <returns>True if a conversion is possible or false, otherwise.</returns>
		public static bool TryGetConversionPredicate<TIn, TOut>(out Func<TIn, TOut> predicate)
		{
			if (typeof(TIn) == typeof(TOut))
			{
				predicate = o => (dynamic)o;
				return true;
			}
			if (TryGet<TIn, TOut>(ValueConverterDirection.Forward, out var converter))
			{
				predicate = converter.Convert;
				return true;
			}
			if (TryGet<TOut, TIn>(ValueConverterDirection.Backward, out var backConverter))
			{
				predicate = backConverter.ConvertBack;
				return true;
			}
			predicate = default;
			return false;
		}

		/// <summary>
		/// Gets the predicate that converts a value of the specified input type into the specified output type.
		/// </summary>
		/// <param name="inputType">The input type.</param>
		/// <param name="outputType">The output type.</param>
		/// <param name="predicate">The conversion predicate.</param>
		/// <returns>True if a conversion is possible or false, otherwise.</returns>
		public static bool TryGetConversionPredicate(Type inputType,
											   Type outputType,
											   out Func<object, object> predicate)
		{
			if (inputType == outputType)
			{
				predicate = o => o;
				return true;
			}
			if (ValueConverterManager.TryGet(inputType, outputType, ValueConverterDirection.Forward,
				out var converter))
			{
				predicate = converter.Convert;
				return true;
			}
			if (ValueConverterManager.TryGet(outputType, inputType, ValueConverterDirection.Backward,
				out var backConverter))
			{
				predicate = backConverter.ConvertBack;
				return true;
			}
			predicate = default;
			return false;
		}

		/// <summary>
		/// Gets a direct value converter between the specified input type and output type.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <returns>The value converter.</returns>
		/// <exception cref="ValueConverterNotFoundException"></exception>
		public static ValueConverter<TIn, TOut> Get<TIn, TOut>() =>
			(ValueConverter<TIn, TOut>)ValueConverterManager.Get(typeof(TIn), typeof(TOut));

		/// <summary>
		/// Gets a direct value converter between the specified input type and output type which supports
		/// the required conversion directions.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="directions">The required conversion directions.</param>
		/// <returns>The value converter.</returns>
		/// <exception cref="ValueConverterNotFoundException"></exception>
		public static ValueConverter<TIn, TOut> Get<TIn, TOut>(ValueConverterDirection directions) =>
			(ValueConverter<TIn, TOut>)ValueConverterManager.Get(typeof(TIn), typeof(TOut), directions);

		/// <summary>
		/// Gets a direct value converter between the specified input type and output type.
		/// </summary>
		/// <param name="inputType">The input type.</param>
		/// <param name="outputType">The output type.</param>
		/// <returns>The value converter.</returns>
		/// <exception cref="ValueConverterNotFoundException"></exception>
		public static ValueConverter Get(Type inputType, Type outputType) =>
			ValueConverterManager.TryGet(inputType, outputType, out var converter) ? converter :
				throw new ValueConverterNotFoundException();

		/// <summary>
		/// Gets a direct value converter between the specified input type and output type which supports
		/// the required conversion directions.
		/// </summary>
		/// <param name="inputType">The input type.</param>
		/// <param name="outputType">The output type.</param>
		/// <param name="directions">The required conversion directions.</param>
		/// <returns>The value converter.</returns>
		/// <exception cref="ValueConverterNotFoundException"></exception>
		public static ValueConverter Get(Type inputType, Type outputType, ValueConverterDirection directions) =>
			ValueConverterManager.TryGet(inputType, outputType, directions, out var converter) ? converter :
				throw new ValueConverterNotFoundException();

		/// <summary>
		/// Gets a direct value converter between the specified input type and output type.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="converter">The value converter.</param>
		/// <returns>True if a direct conversion is possible or false, otherwise.</returns>
		public static bool TryGet<TIn, TOut>(out ValueConverter<TIn, TOut> converter)
		{
			if (ValueConverterManager.TryGet(typeof(TIn), typeof(TOut), out var untypedConverter))
			{
				converter = untypedConverter as ValueConverter<TIn, TOut>;
				return true;
			}
			converter = default;
			return false;
		}

		/// <summary>
		/// Gets a direct value converter between the specified input type and output type which supports
		/// the required conversion directions.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="directions">The required conversion directions.</param>
		/// <param name="converter">The value converter.</param>
		/// <returns>True if a direct conversion is possible or false, otherwise.</returns>
		public static bool TryGet<TIn, TOut>(ValueConverterDirection directions,
									   out ValueConverter<TIn, TOut> converter)
		{
			if (ValueConverterManager.TryGet(typeof(TIn), typeof(TOut), directions, out var converterBeforeCast))
			{
				converter = (ValueConverter<TIn, TOut>)converterBeforeCast;
				return true;
			}
			converter = default;
			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="inputType">The input type.</param>
		/// <param name="outputType">The output type.</param>
		/// <param name="converter">The value converter.</param>
		/// <returns>True if a direct conversion is possible or false, otherwise.</returns>
		public static bool TryGet(Type inputType, Type outputType, out ValueConverter converter) =>
			ValueConverterManager.TryGet(inputType, outputType, out converter);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="inputType">The input type.</param>
		/// <param name="outputType">The output type.</param>
		/// <param name="directions">The required conversion directions.</param>
		/// <param name="converter">The value converter.</param>
		/// <returns>True if a direct conversion is possible or false, otherwise.</returns>
		public static bool TryGet(Type inputType,
							Type outputType,
							ValueConverterDirection directions,
							out ValueConverter converter) =>
			ValueConverterManager.TryGet(inputType, outputType, directions, out converter);

		/// <summary>
		/// The input type of this value converter.
		/// </summary>
		public abstract Type InputType { get; }

		/// <summary>
		/// The output type of this value converter.
		/// </summary>
		public abstract Type OutputType { get; }

		/// <summary>
		/// Converts the specified value to the ValueConverter's output type.
		/// </summary>
		/// <param name="value">The value being converted.</param>
		/// <returns>The result of the conversion.</returns>
		/// <exception cref="NotSupportedException">If this conversion operation is not supported.</exception>
		public abstract object Convert(object value);

		/// <summary>
		/// Converts the specified value to the ValueConverter's input type.
		/// </summary>
		/// <param name="value">The value being converted.</param>
		/// <returns>The result of the conversion.</returns>
		/// <exception cref="NotSupportedException">If this conversion operation is not supported.</exception>
		public virtual object ConvertBack(object value) => throw new NotSupportedException();

		/// <summary>
		/// The allowed conversion directions.
		/// </summary>
		public virtual ValueConverterDirection AllowedDirections => ValueConverterDirection.Forward;

		public override string ToString() => $"{InputType}->{OutputType}";
	}

	public abstract class ValueConverter<TIn, TOut> : ValueConverter
	{
		public override Type InputType => typeof(TIn);

		public override Type OutputType => typeof(TOut);

		public override object Convert(object value) => Convert((TIn)value);

		public override object ConvertBack(object value) => ConvertBack((TOut)value);

		/// <summary>
		/// Converts the specified value to the ValueConverter's output type.
		/// </summary>
		/// <param name="value">The value being converted.</param>
		/// <returns>The result of the conversion.</returns>
		/// <exception cref="NotSupportedException">If this conversion operation is not supported.</exception>
		public abstract TOut Convert(TIn value);

		/// <summary>
		/// Converts the specified value to the ValueConverter's input type.
		/// </summary>
		/// <param name="value">The value being converted.</param>
		/// <returns>The result of the conversion.</returns>
		/// <exception cref="NotSupportedException">If this conversion operation is not supported.</exception>
		public virtual TIn ConvertBack(TOut value) => throw new NotSupportedException();
	}
}

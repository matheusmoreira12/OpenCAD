using System;
using System.Linq;
using System.Reflection;

namespace OpenCAD.OpenCADFormat.Measures
{
    public interface ISummable<TValue, TResult>
    {
        TResult Sum(TValue value);
    }

    public interface INegatable<TResult>
    {
        TResult Negate();
    }

    public interface IComposedMember<TBase> {
        TBase Base { get; }
    }

    public interface IMultipliable<TValue, TResult>
    {
        TResult Multiply(TValue value);
    }

    public interface IExponentiable<TResult>
    {
        TResult Exponentiate(double exponent);
    }

    public static class Math
    {
        public static TResult Power<TResult>(IExponentiable<TResult> value, double exponent) => value.Exponentiate(exponent);
        
        public static TResult Square<TResult>(IExponentiable<TResult> value) => Power(value, 2);

        public static TResult Cube<TResult>(IExponentiable<TResult> value) => Power(value, 3);

        public static TResult SquareRoot<TResult>(IExponentiable<TResult> value) => Power(value, 1.0 / 2.0);

        public static TResult CubicRoot<TResult>(IExponentiable<TResult> value) => Power(value, 1.0 / 3.0);

        public static TResult Invert<TResult>(IExponentiable<TResult> value) => Power(value, -1);

        public static TResult Multiply<TValue, TResult>(IMultipliable<TValue, TResult> a, TValue b) => a.Multiply(b);

        public static TResult Divide<TValue, TResult>(IMultipliable<TValue, TResult> a, IExponentiable<TValue> b) => 
            a.Multiply(b.Exponentiate(-1));

        public static TResult Add<TValue, TResult>(ISummable<TValue, TResult> a, TValue b) => a.Sum(b);

        public static TResult Negate<TResult>(INegatable<TResult> a) => a.Negate();

        public static TResult Subtract<TValue, TResult>(ISummable<TValue, TResult> a, INegatable<TValue> b) => a.Sum(b.Negate());
    }
}

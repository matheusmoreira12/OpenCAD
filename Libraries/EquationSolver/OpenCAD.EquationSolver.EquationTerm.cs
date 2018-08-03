using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCAD.Utils;

namespace EquationSolver
{
    public abstract class EquationTerm
    {
        public static bool ReadFromString(StringReader reader, out EquationTerm result)
        {

        }

        public List<EquationTerm> SubTerms { get; private set; }

        public EquationTerm(IEnumerable<EquationTerm> subterms = null)
        {
            SubTerms = new List<EquationTerm>(subterms);
        }
    }

    public abstract class EquationOperation : EquationTerm
    {
        public sealed class SumOperation : EquationOperation
        {
        }

        public sealed class SubtractionOperation : EquationOperation
        {
        }

        public sealed class MultiplicationOperation : EquationOperation
        {
        }

        public sealed class DivisionOperation : EquationOperation
        {
        }
    }

    public sealed class EquationGroup : EquationTerm
    {
    }

    public abstract class EquationFunction : EquationTerm
    {
        public sealed class SineFunction : EquationFunction
        {
        }

        public sealed class CosineFunction : EquationFunction
        {
        }

        public sealed class TangentFunction : EquationFunction
        {
        }

        public sealed class ArcSineFunction : EquationFunction
        {
        }

        public sealed class ArcCosineFunction : EquationFunction
        {
        }

        public sealed class ArcTangentFunction : EquationFunction
        {
        }

        public sealed class LogFunction : EquationFunction
        {
        }

        public sealed class Log10Function : EquationFunction
        {
        }

        public sealed class ExpFunction : EquationFunction
        {
        }

        public sealed class Exp10Function : EquationFunction
        {
        }
    }
}

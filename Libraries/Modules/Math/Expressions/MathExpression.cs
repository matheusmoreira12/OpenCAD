﻿using System.Linq;
using OpenCAD.APIs.Math.Expressions.Tokens;
using OpenCAD.APIs.Tokens;

namespace OpenCAD.APIs.Math.Expressions
{
    public class MathExpression : MathExpressionGroup
    {
        internal static MathExpression FromTokens(StringToken[] tokens)
        {
            var reader = new StringTokenReader<StringToken>(tokens);

            ///TODO: Read math formula here

            return null;
        }

        public static MathExpression Parse(string value)
        {
            MathExpressionTokenifier tokenifier = new MathExpressionTokenifier(value);

            var tokens = tokenifier.Tokenify().Cast<MathExpressionToken>();

            return FromTokens(tokens.Cast<StringToken>().ToArray());
        }

        public MathExpression(MathExpressionMember[] members) : base(members)
        {
        }
    }
}

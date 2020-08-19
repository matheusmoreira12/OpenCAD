﻿using OpenCAD.APIs.Math.Enumerations;
using System;

namespace OpenCAD.APIs.Math.Operations
{
    public partial class MathOperationType : Enumeration<MathOperationType>
    {
        public static MathOperationType Addition { get; } = new MathOperationType();
        public static MathOperationType Negation { get; } = new MathOperationType();
        public static MathOperationType Multiplication { get; } = new MathOperationType();
        public static MathOperationType Exponentiation { get; } = new MathOperationType();

        public MathOperationType(int value) : base(value) { }
        public MathOperationType() { }
    }
}
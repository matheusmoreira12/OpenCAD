using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCAD.Modules.Math;

namespace MathTest
{
    [TestClass]
    public class TestDefaultOperations
    {
        [TestMethod]
        public void TestDoubleAddition()
        {
            double operandA = 1;
            double operandB = 2;
            double expectedResult = 3;
            var actualResult = Math.Add(operandA, operandB);
            Assert.AreEqual(expectedResult, actualResult);
        }

		[TestMethod]
		public void TestIntAddition()
		{
			int operandA = 1;
			int operandB = 2;
			double expectedResult = 3;
			var actualResult = Math.Add(operandA, operandB);
			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestMethod]
		public void TestIntDoubleAddition()
		{
			int operandA = 1;
			double operandB = 2;
			double expectedResult = 3;
			var actualResult = Math.Add(operandA, operandB);
			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestMethod]
        public void TestDoubleSubtraction()
        {
            double operandA = 3;
            double operandB = 2;
            double expectedResult = 1;
            var actualResult = Math.Subtract(operandA, operandB);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestDoubleMultiplication()
        {
            double operandA = 2;
            double operandB = 4;
            double expectedResult = 8;
            var actualResult = Math.Multiply(operandA, operandB);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestDoubleDivision()
        {
            double operandA = 8;
            double operandB = 4;
            double expectedResult = 2;
            var actualResult = Math.Divide(operandA, operandB);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}

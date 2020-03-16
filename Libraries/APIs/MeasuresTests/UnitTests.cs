using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace OpenCAD.APIs.Measures.Tests
{
    [TestClass()]
    public class UnitTests
    {
        [TestMethod()]
        public void ParseTest()
        {
            #region Test parsing 
            {
                var metricSystem = new MetricSystem("metricSystemX");
                var quantity = new BaseQuantity(metricSystem, "quantityX");
                var unit = new BaseUnit(quantity, "unitX", "X");
                #region Test valid parsing
                {
                    try
                    {
                        var parsedUnit = Unit.Parse("X");
                        Assert.AreEqual(unit, parsedUnit, "Valid value should parse into " +
                            "the correct unit.");
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("Valid value parsing should not fail.", e);
                    }
                }
                #endregion
                #region Test invalid parsing
                //Test invalid parsing
                {
                    try
                    {
                        Unit.Parse("Z");
                    }
                    finally
                    {
                        Assert.Fail("Invalid value parsing should fail.");
                    }
                }
                #endregion
            }
            #endregion
        }

        [TestMethod()]
        public void TryParseTest()
        {
            #region Test Parsing 
            {
                var metricSystem = new MetricSystem("metricSystemX");
                var quantity = new BaseQuantity(metricSystem, "quantityX");
                var unit = new BaseUnit(quantity, "unitX", "X");
                try
                {
                    #region Test valid parsing
                    {
                        Unit parsedUnit;
                        if (!Unit.TryParse("X", out parsedUnit))
                            Assert.Fail("Valid unit parsing should not fail.");

                        Assert.AreEqual(unit, parsedUnit, "Valid value should parse" +
                            " into the correct unit.");
                    }
                    #endregion
                    #region Test invalid parsing
                    {
                        Unit parsedUnit;
                        if (Unit.TryParse("Y", out parsedUnit))
                            Assert.Fail("Invalid value parsing should fail.");
                    }
                }
                catch (Exception e)
                {
                    Assert.Fail("Parsing should not fail internally.", e);
                }
                #endregion
            }
            #endregion
        }

        [TestMethod()]
        public void CollapseTest()
        {
            #region Test unit collapsing
            {
                var metricSystem = new MetricSystem("metricSystemX");
                var quantity = new BaseQuantity(metricSystem, "quantityX");
                try
                {
                    var unit = new BaseUnit(quantity, "unitX", "X");
                    #region For base unit
                    {
                        var collapsedUnit = unit.Collapse();
                        Assert.AreEqual(unit, collapsedUnit, "A collapsed base unit " +
                            "should be equal to itself.");
                    }
                    #endregion;

                    var derivedUnit = unit * unit / unit;
                    #region For derived unit
                    {
                        var collapedDerivedUnit = derivedUnit.Collapse();
                        Assert.AreEqual(unit, collapedDerivedUnit, "A collapsed derived unit " +
                            "X*X/X should result in X.");
                    }
                    #endregion
                }
                catch (Exception e)
                {
                    Assert.Fail("Collapsing should not fail internally.", e);
                }
            }
            #endregion
        }

        [TestMethod()]
        public void EqualsTest()
        {
            #region Test equality comparison
            {
                var metricSystem = new MetricSystem("metricSystemX");
                var quantity = new BaseQuantity(metricSystem, "quantityX");
                var baseUnitA = new BaseUnit(quantity, "unitX", "X");
                var baseUnitB = new BaseUnit(quantity, "unitY", "Y");
                try
                {
                    #region For base units 
                    {
                        #region For equal units
                        {
                            Assert.IsTrue(baseUnitA.Equals(baseUnitB), "Equivalent units " +
                                "should test equal.");
                        }
                        #endregion
                        #region For different units
                        {
                            Assert.IsTrue(baseUnitA.Equals(baseUnitB), "Equivalent units " +
                                "should test equal.");
                        }
                        #endregion
                    }
                    #endregion
                    #region For derived units
                    {
                        var derivedUnitA = baseUnitA * baseUnitA;
                        var derivedUnitB = baseUnitA * baseUnitB;
                        #region For equal units
                        {
                            Assert.IsTrue(derivedUnitA.Equals(derivedUnitB), "Equivalent " +
                                "derived units should test equal.");
                        }
                        #endregion
                        #region For different units
                        {
                            Assert.IsTrue(derivedUnitA.Equals(derivedUnitB), "Equivalent " +
                                "derived units should test equal.");
                        }
                        #endregion
                    }
                    #endregion
                }
                catch (Exception e)
                {
                    Assert.Fail("Testing for equality should not fail internally.", e);
                }
            }
            #endregion
        }
    }
}
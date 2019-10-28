using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class BaseQuantity : Quantity
    {
        public static BaseQuantity Parse(string value)
        {
            return registeredQuantities.Find(q => q.Symbol == value || q.Name == value) ?? 
                throw new ArgumentOutOfRangeException("Cannot parse BaseQuantity. The specified name does not match any Base Quantities.");
        }

        public static bool TryParse(string value, out BaseQuantity result)
        {
            try
            {
                result = Parse(value);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        private static void registerQuantity(BaseQuantity quantity) => registeredQuantities.Add(quantity);

        private static void unregisterQuantity(BaseQuantity quantity) => registeredQuantities.Remove(quantity);

        public static List<BaseQuantity> registeredQuantities = new List<BaseQuantity> { };

        public BaseQuantity(string name, string symbol) : base(name, symbol) {
            registerQuantity(this);
        }
    }
}
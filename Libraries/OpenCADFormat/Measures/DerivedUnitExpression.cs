using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.OpenCADFormat.Measures
{
    public class DerivedUnitExpression
    {
        public DerivedUnitExpression(params DerivedUnitExpressionMember[] members)
        {
            Members = members ?? throw new ArgumentNullException(nameof(members));
        }

        public DerivedUnitExpressionMember[] Members { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.OpenCADFormat.Serialization
{
    public interface IResolvable<T> where T : class
    {
        T Resolve();

        void Assimilate(T value);
    }
}

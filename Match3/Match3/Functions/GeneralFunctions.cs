using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Functions
{
    public static class GeneralFunctions
    {
        public static bool AllEqual<T>(params T[] values)
        {
            if (values == null || values.Length == 0)
                return true;
            return values.All(v => v.Equals(values[0]));
        }

    }
}

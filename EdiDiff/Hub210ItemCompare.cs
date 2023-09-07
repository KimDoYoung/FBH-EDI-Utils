using System;
using System.Collections.Generic;

namespace EdiDiff
{
    internal class Hub210ItemCompare : IEqualityComparer<Hub210Item>
    {
        public bool Equals(Hub210Item x, Hub210Item y)
        {
            return x.PoNo.Trim() == y.PoNo.Trim();
        }

        public int GetHashCode(Hub210Item obj)
        {
            return obj.PoNo.GetHashCode();
        }
    }
}

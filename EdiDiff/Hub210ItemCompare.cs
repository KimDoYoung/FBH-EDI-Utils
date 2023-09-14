using FBH.EDI.Common;
using System;
using System.Collections.Generic;

namespace EdiDiff
{
    internal class Hub210ItemCompare : IEqualityComparer<Hub210Item>
    {
        public bool Equals(Hub210Item x, Hub210Item y)
        {

            return ( CommonUtil.OnlyNum(x.InvoiceDate)+x.InvoiceNo ) == ( CommonUtil.OnlyNum(x.InvoiceDate) + x.InvoiceNo );
        }

        public int GetHashCode(Hub210Item o)
        {
            return (CommonUtil.OnlyNum(o.InvoiceDate) + o.InvoiceNo).GetHashCode();
        }
    }
}

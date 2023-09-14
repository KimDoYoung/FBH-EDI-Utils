using FBH.EDI.Common;
using System;
using System.Collections.Generic;

namespace EdiDiff
{
    internal class Hub210ItemCompare : IEqualityComparer<Hub210Item>
    {
        public bool Equals(Hub210Item x, Hub210Item y)
        {

            return ( CommonUtil.OnlyNum(x.InvoiceDate)+x.InvoiceNo + string.Format("{0:0}",x.Qty) + string.Format("{0:0.00}",x.Amount) ) == ( CommonUtil.OnlyNum(y.InvoiceDate) + y.InvoiceNo + string.Format("{0:0}", y.Qty) + string.Format("{0:0.00}", y.Amount));
        }

        public int GetHashCode(Hub210Item o)
        {
            return (CommonUtil.OnlyNum(o.InvoiceDate) + o.InvoiceNo + string.Format("{0:0}", o.Qty) + string.Format("{0:0.00}", o.Amount)).GetHashCode();
        }
    }
}

using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiUtils.Common
{
    internal static class BizRule
    {
        internal static bool IsNoQty(string companyName)
        {
           if(companyName == CONST.Kroger || companyName == CONST.WMCOM) return true;
            return false;
        }
    }
}

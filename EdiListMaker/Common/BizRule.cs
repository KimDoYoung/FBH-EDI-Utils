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
        internal static string CheckCompanyNameWithPrice(decimal unitPrice)
        {
            if(unitPrice >  3)
            {
                return CONST.WMCOM;
            }
            return CONST.Walmart;
        }

        internal static bool IsNoQty(string companyName)
        {
           if(companyName == CONST.Kroger || companyName == CONST.WMCOM) return true;
            return false;
        }

        internal static bool IsNoQty(string companyName, decimal unitPrice)
        {
            if (companyName == CONST.Kroger || companyName == CONST.WMCOM)
            {
                return true;
            }
            else { 
                if(unitPrice < 3)
                {
                    return true;
                }            
            }
            return false;
        }
    }
}

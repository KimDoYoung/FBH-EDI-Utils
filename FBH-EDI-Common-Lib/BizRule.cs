using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common
{
    public static class BizRule
    {
        public static bool IsNoQty(string companyName)
        {
           if(companyName == CONST.Kroger || companyName == CONST.WMCOM) return true;
            return false;
        }
        public static string CheckCompanyNameWithPrice(decimal unitPrice)
        {
            if (unitPrice > 3)
            {
                return CONST.WMCOM;
            }
            return CONST.Walmart;
        }

        public static bool IsNoQty(string companyName, decimal unitPrice)
        {
            if (companyName == CONST.Kroger || companyName == CONST.WMCOM)
            {
                return true;
            }
            else
            {
                if (unitPrice < 3)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

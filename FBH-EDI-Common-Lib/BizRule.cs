using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        /// <summary>
        /// DCNo는 DC라는 문자열 다음에 3~4개의 숫자로 되어 있다.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        internal static string ExtractDc(string s)
        {
            var resultString = Regex.Match(s, @"\d{3,4}").Value;
            return resultString;
        }
    }
}

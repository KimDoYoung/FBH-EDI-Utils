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
        /// <summary>
        /// 230817WMW29127 PO 9534187325 이런 문자열에서 95...를 뽑아낸다.
        /// </summary>
        /// <param name="poNumber"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        internal static string ExtractPoNo(string poNumber)
        {
            if (string.IsNullOrEmpty(poNumber)) return "";
            if (CommonUtil.IsOnlyNum(poNumber.Trim()))
            {
                return poNumber.Trim();
            }
            int i = poNumber.IndexOf("PO");
            if(i >= 0)
            {
                return poNumber.Substring(i + 2).Trim();
            }
            return poNumber.Trim();
        }
        /// <summary>
        /// 2929626049WM270809 에서 27을 뽑아서 리턴, 실패시 null리턴
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        internal static int? ExtractWoy(string invoiceNo)
        {
            int p = invoiceNo.IndexOf("WM");
            if(p > -1)
            {
                string woy = invoiceNo.Substring(p+2, 2);
                return Convert.ToInt32(woy);
            }
            return null;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiUtils.Common
{
    internal class Po850Compare : IComparer<PurchaseOrder850>
    {
        Dictionary<String, String> map = new Dictionary<string, string>()
        {
            {"Walmart", "1" },
            {"WM.COM", "2" },
            {"Kroger", "3" }
        };

        public int Compare(PurchaseOrder850 x, PurchaseOrder850 y)
        {
            string a = map[x.CompanyName] + x.CompanyName + x.WeekOfYear;
            string b = map[y.CompanyName] + y.CompanyName + y.WeekOfYear;
            return a.CompareTo(b);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiUtils.Common
{
    public class PurchaseOrder850Allowance
    {
        public string Charge{ get; set; }
        public string DescCd{ get; set; }
        public int Amount{ get; set; }
        public string HandlingCd{ get; set; }
        public Decimal Percent{ get; set; }

        public override string ToString()
        {
            return $"Charge : {Charge}, " +
                    $"Description Code: {DescCd}, " +
                    $"Amount : {Amount}, " +
                    $"Handling code : {HandlingCd}, " +
                    $"Percent : {Percent}";
        }
    }
}

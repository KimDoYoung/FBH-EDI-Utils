using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    public class PurchaseOrder850Detail
    {
        public string PoNo { get; set; }
        public string Line { get; set; }
        public int Qty { get; set; }
        public string Msrmnt { get; set; }
        public decimal UnitPrice { get; set; }
        public string Gtin13 { get; set; }
        public string RetailerItemNo { get; set; }
        public string VendorItemNo { get; set; }
        public string Description { get; set; }
        public decimal ExtendedCost { get; set; }
        public int CompanyId { get; set; }

        public override string ToString()
        {
            return  $"PoNo : {PoNo}, " +
                    $"Line : {Line}, " +
                    $"Qty : {Qty}, " +
                    $"Msrmnt : {Msrmnt}, " +
                    $"UnitPrice : {UnitPrice}, " +
                    $"Gtin13 : {Gtin13}, " +
                    $"RetailerItemNo : {RetailerItemNo}, " +
                    $"VendorItemNo : {VendorItemNo}, " +
                    $"Description : {Description}, " +
                    $"ExtendedCost : {ExtendedCost}";
        }
    }
}

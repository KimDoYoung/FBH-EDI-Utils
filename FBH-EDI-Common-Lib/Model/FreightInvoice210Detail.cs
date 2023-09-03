using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    public class FreightInvoice210Detail
    {
        public string InvoiceNo { get; set; }
        public int? TransactionSetLineNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string ShippedDate { get; set; }
        public string LadingLineItem { get; set; }
        public string LadingDescription { get; set; }
        public int? BilledRatedAsQuantity { get; set; }
        public decimal? Weight { get; set; }
        public int? LadingQuantity { get; set; }
        public decimal? FreightRate { get; set; }
        public int? AmountCharged { get; set; }
        public string SpecialChargeOrAllowanceCd { get; set; }
        override public string ToString()
        {
            return $"InvoiceNo: { InvoiceNo}, "
                 + $"TransactionSetLineNumber: { TransactionSetLineNumber},"
                 + $"PurchaseOrderNumber: { PurchaseOrderNumber},"
                 + $"ShippedDate: { ShippedDate},"
                 + $"LadingLineItem: { LadingLineItem},"
                 + $"LadingDescription: { LadingDescription},"
                 + $"BilledRatedAsQuantity: { BilledRatedAsQuantity},"
                 + $"Weight: { Weight},"
                 + $"LadingQuantity: { LadingQuantity},"
                 + $"FreightRate: { FreightRate},"
                 + $"AmountCharged: { AmountCharged},"
                 + $"SpecialChargeOrAllowanceCd: { SpecialChargeOrAllowanceCd}";
        }
    }
}

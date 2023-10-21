using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    public class Payment820Detail
    {
        public string TraceId { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDt { get; set; }
        public int? EntityNo { get; set; }
        public decimal? AmountPaid { get; set; }
        public decimal? AmountInvoice { get; set; }
        public decimal? AmountOfTermsDiscount { get; set; }
        public string DivicionId { get; set; }
        public string DepartmentNo { get; set; }
        public string MerchandiseTypeCode { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string StoreNo { get; set; }
        public string MicrofileNo { get; set; }
        public decimal? AdjustmentAmount { get; set; }
        public string AdjustmentReasonCode { get; set; }
        public string AdjustmentMemo { get; set; }
        public string AdjustmentMemoType { get; set; }
        public string CrossRefCode { get; set; }
        public string MicrofilmNoOfAdjustment { get; set; }

        override public string ToString()
        {
            return $"TraceId: {TraceId}, " +
                    $"InvoiceNo: {InvoiceNo}, " +
                    $"InvoiceDt: {InvoiceDt}, " +
                    $"EntityNo: {EntityNo}, " +
                    $"AmountPaid: {AmountPaid}, " +
                    $"AmountInvoice: {AmountInvoice}, " +
                    $"AmountOfTermsDiscount: {AmountOfTermsDiscount}, " +
                    $"DivicionId: {DivicionId}, " +
                    $"DepartmentNo: {DepartmentNo}, " +
                    $"MerchandiseTypeCode: {MerchandiseTypeCode}, " +
                    $"PurchaseOrderNo: {PurchaseOrderNo}, " +
                    $"StoreNo: {StoreNo}, " +
                    $"MicrofileNo: {MicrofileNo}, " +
                    $"AdjustmentAmount: {AdjustmentAmount}, " +
                    $"AdjustmentReasonCode: {AdjustmentReasonCode}, " +
                    $"AdjustmentMemo: {AdjustmentMemo}, " +
                    $"AdjustmentMemoType: {AdjustmentMemoType}, " +
                    $"CrossRefCode: {CrossRefCode}, " +
                    $"MicrofilmNoOfAdjustment: {MicrofilmNoOfAdjustment} ";
        }
    }
}

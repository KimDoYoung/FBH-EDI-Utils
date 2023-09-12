using System;

namespace FBH.EDI.Common.Model
{
    public class Hub210Item : Base210, IEquatable<Hub210Item>
    {
        public string PaymentDate { get; set; }
        public decimal? Amount { get; set; }
        //public string InvoiceDate { get; set; }
        public string PaymentDue { get; set; }
        public string InvoiceNo { get; set; }
        public string PoNo { get;set; }
        public int? Qty { get; set; }
        public string DcNo { get; set; }
        public string Address { get;set; }

        public string PickUpDate { get; set; }  
        public string Product { get; set; }

        public string HubBolNo { get;set; }

        public int SrcRouteNo { get; set; }
        public string Status { get;set; }

        public bool Equals(Hub210Item other)
        {
            return this.InvoiceNo + this.PoNo == other.InvoiceNo + other.PoNo;
        }

        public override string ToString()
        {
            return $"PaymentDate: {PaymentDate}, "
               + $"Amount: {Amount}, "
               + $"InvoiceDate: {InvoiceDate}, "
               + $"PaymentDue: {PaymentDue}, "
               + $"InvoiceNo: {InvoiceNo}, "
               + $"PoNo: {PoNo}, "
               + $"Qty: {Qty}, "
               + $"DcNo: {DcNo}, "
               + $"Address: {Address}, "
               + $"PickUpDate: {PickUpDate}, "
               + $"Product: {Product}, "
               + $"HubBolNo: {HubBolNo}, "
               + $"SrcRouteNo: {SrcRouteNo}, "
               + $"Status: {Status}";
        }
    }
}
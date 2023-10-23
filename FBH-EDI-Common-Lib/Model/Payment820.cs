using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    public class Payment820 : EdiDocument
    {
        public string TraceId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentIssuanceDate { get; set; }
        public decimal? Amount { get; set; }
        public string CreditDebit { get; set; }
        public string Currency { get; set; }
        public string Payer { get; set; }
        public string PayerLocationNo { get; set; }
        public string PayeeName { get; set; }
        public string Received820Date { get; set; }
        public string Received820Time { get; set; }

        public List<Payment820Detail> Details;

        public Payment820()
        {
            DocumentNo = EdiDocumentNo.Payment_820;
            Details = new List<Payment820Detail>();
        }
        override public string ToString()
        {
            string s =  $"TraceId : {TraceId}\r\n" +
                        $"PaymentMethod : {PaymentMethod}\r\n" +
                        $"PaymentIssuanceDate : {PaymentIssuanceDate}\r\n" +
                        $"Amount : {Amount}\r\n" +
                        $"CreditDebit : {CreditDebit}\r\n" +
                        $"Currency : {Currency}\r\n" +
                        $"Payer : {Payer}\r\n" +
                        $"PayerLocationNo : {PayerLocationNo}\r\n" +
                        $"PayeeName : {PayeeName}\r\n" +
                        $"Received820Date : {Received820Date}\r\n" +
                        $"Received820Time : {Received820Time}\r\n";
            s += "--------------Details----------\r\n";
            foreach (var detail in Details)
            {
                s += detail.ToString() + "\r\n" ;
            }
            return s.Trim();
        }
    }
}

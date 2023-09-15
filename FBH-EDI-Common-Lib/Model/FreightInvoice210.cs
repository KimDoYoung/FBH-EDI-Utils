using System;
using System.Collections.Generic;

namespace FBH.EDI.Common.Model
{
    public class FreightInvoice210 : EdiDocument
    {
        //Beginning segment for Carrier's Invoice
        public string InvoiceNo { get; set; }
        public string InvoiceDt { get; set; } //Invoice Date
        public string ShipIdNo { get; set; } //Shipment ID number
        public string ShipMethodOfPayment { get; set; } // Shipment Method of Payment

        public decimal? AmountToBePaid { get; set; } //Amount to be Paid

        //Reference Identification	
        public string PoNumber { get; set; } // Purchase Order Number
        public string VicsBolNo { get; set; } //VICS BOL Number
        public string DcNo { get; set; }

        public string WarehouseName { get; set; }
        public string WarehouseAddress { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress { get; set; }
        public string BillToName { get; set; }
        public string BillToAddress { get; set; }


        //Total weight and Charges 
        public decimal? TotalWeight { get; set; }
        public string TotalWeightUnit { get; set; }
        public string WeightQualifier { get; set; }
        public decimal? AmountCharged { get; set; }

        public int? Qty { get; set; }


        public FreightInvoice210()
        {
            DocumentNo = EdiDocumentNo.Freight_Invoice_210;
        }

        override public string ToString()
        {
            string s = $"InvoiceNo : {InvoiceNo}\r\n"
                + $"ShipIdNo : {ShipIdNo}\r\n"
                + $"ShipMethodOfPayment : {ShipMethodOfPayment}\r\n"
                + $"InvoiceDt : {InvoiceDt}\r\n"
                + $"AmountToBePaid : {AmountToBePaid}\r\n"
                + $"PoNumber : {PoNumber}\r\n"
                + $"VicsBolNo : {VicsBolNo}\r\n"
                + $"TotalWeight : {TotalWeight}\r\n"
                + $"TotalWeightUnit : {TotalWeightUnit}\r\n"
                + $"AmountCharged : {AmountCharged}\r\n"
                + $"ExcelFileName : {FileName}\r\n";
            return s;
        }

    }
}

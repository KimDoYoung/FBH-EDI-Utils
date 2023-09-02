using System;
using System.Collections.Generic;

namespace EdiUtils.Common
{
    public class FreightInvoice210
    {
        //Beginning segment for Carrier's Invoice
        public string InvoiceNo { get; set; }
        public string ShipIdNo { get; set; } //Shipment ID number
        public string ShipMethodOfPayment { get; set; } // Shipment Method of Payment
        public string InvoiceDt { get; set; } //Invoice Date
        public int? AmountToBePaid { get; set; } //Amount to be Paid

        //Reference Identification	
        public string PoNumber { get; set; } // Purchase Order Number
        public string VicsBolNo { get; set; } //VICS BOL Number

        // Warehouse Ship-From	
        public string ShipFromCompanyName { get; set; }
        public string ShipFromAddrInfo { get; set; }
        public string ShipFromCity { get; set; }
        public string ShipFromState { get; set; }
        public string ShipFromZipcode { get; set; }
        public string ShipFromCountryCd { get; set; }

        // Consignee Ship-To
        public string ShipToCompanyName { get; set; }
        public string ShipToAddrInfo { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToState { get; set; }
        public string ShipToZipcode { get; set; }
        public string ShipToCountryCd { get; set; }

        // Bill-To
        public string BillToCompanyName { get; set; }
        public string BillToAddrInfo { get; set; }
        public string BillToCity { get; set; }
        public string BillToState { get; set; }
        public string BillToZipcode { get; set; }
        public string BillToCountryCd { get; set; }

        //Total weight and Charges 
        public decimal? TotalWeight {get;set ;}
        public string TotalWeightUnit {get;set ;}
        public string  WeightQualifier { get; set; }
        public int? AmountCharged { get; set; }

        public int? BolQtyInCases{get;set ;}
        public string ExcelFileName { get; internal set; }

        public List<FreightInvoice210Detail> Details { get; set; }

        public FreightInvoice210()
        {
            Details = new List<FreightInvoice210Detail>();
        }

        internal decimal? GetTotalFreightRate()
        {
            decimal? sum = 0.0M;
            foreach (var detail in Details)
            {
                sum += detail.FreightRate == null ? 0 : detail.FreightRate;
            }
            return sum;
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
                + $"ShipFromCompanyName : {ShipFromCompanyName}\r\n"
                + $"ShipFromAddrInfo : {ShipFromAddrInfo}\r\n"
                + $"ShipFromCity : {ShipFromCity}\r\n"
                + $"ShipFromState : {ShipFromState}\r\n"
                + $"ShipFromZipcode : {ShipFromZipcode}\r\n"
                + $"ShipFromCountryCd : {ShipFromCountryCd}\r\n"
                + $"ShipToCompanyName : {ShipToCompanyName}\r\n"
                + $"ShipToAddrInfo : {ShipToAddrInfo}\r\n"
                + $"ShipToCity : {ShipToCity}\r\n"
                + $"ShipToState : {ShipToState}\r\n"
                + $"ShipToZipcode : {ShipToZipcode}\r\n"
                + $"ShipToCountryCd : {ShipToCountryCd}\r\n"
                + $"BillToCompanyName : {BillToCompanyName}\r\n"
                + $"BillToAddrInfo : {BillToAddrInfo}\r\n"
                + $"BillToCity : {BillToCity}\r\n"
                + $"BillToState : {BillToState}\r\n"
                + $"BillToZipcode : {BillToZipcode}\r\n"
                + $"BillToCountryCd : {BillToCountryCd}\r\n"
                + $"TotalWeight : {TotalWeight}\r\n"
                + $"TotalWeightUnit : {TotalWeightUnit}\r\n"
                + $"AmountCharged : {AmountCharged}\r\n"
                + $"BolQtyInCases : {BolQtyInCases}\r\n"
                + $"ExcelFileName : {ExcelFileName}\r\n";
            s += $"------------------  Details : {Details.Count} -------------------\r\n";
            foreach (var detail in Details)
            {
                s += detail.ToString() + "\r\n";
            }
            return s ;
        }

    }
}

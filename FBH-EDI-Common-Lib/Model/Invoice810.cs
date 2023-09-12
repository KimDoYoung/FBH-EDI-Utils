using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    public class Invoice810 : EdiDocument
    {
        public string PoNo { get; set; }
        public string InvoiceNo { get; set; }
        public string SupplierNm { get; set; }
        public string SupplierCity { get; set; }
        public string SupplierState { get; set; }
        public string SupplierZip { get; set; }
        public string SupplierCountry { get; set; }
        public string DepartmentNo { get; set; }
        public string Currency { get; set; }
        public string VendorNo { get; set; }
        public string NetDay { get; set; }
        public string McdType { get; set; }
        public string Fob { get; set; }
        public string ShipToNm { get; set; }
        public string ShipToGln { get; set; }
        public string ShipToAddr { get; set; }
        public decimal TtlAmt { get; set; }

        public List<Invoice810Detail> Details;

        public Invoice810()
        {
            Details = new List<Invoice810Detail>();
        }
        override public string ToString()
        {
            string s = $"PoNo : {PoNo}\r\n" +
                $"InvoiceNo : {InvoiceNo}\r\n" +
                $"SupplierNm : {SupplierNm}\r\n" +
                $"SupplierCity : {SupplierCity}\r\n" +
                $"SupplierState : {SupplierState}\r\n" +
                $"SupplierZip : {SupplierZip}\r\n" +
                $"SupplierCountry : {SupplierCountry}\r\n" +
                $"DepartmentNo : {DepartmentNo}\r\n" +
                $"Currency : {Currency}\r\n" +
                $"VendorNo : {VendorNo}\r\n" +
                $"NetDay : {NetDay}\r\n" +
                $"McdType : {McdType}\r\n" +
                $"Fob : {Fob}\r\n" +
                $"ShipToNm : {ShipToNm}\r\n" +
                $"ShipToGln : {ShipToGln}\r\n" +
                $"ShipToAddr : {ShipToAddr}\r\n" +
                $"TtlAmt : {TtlAmt}\r\n";
            s += "--------------Details----------\r\n";
            foreach (var detail in Details)
            {
                s += detail.ToString() + "\r\n" ;
            }
            return s;
        }

        internal int SumQty()
        {
            int sum = 0;
            foreach (var detail in Details)
            {
                sum += detail.Qty;
            }
            return sum;
        }
    }
}

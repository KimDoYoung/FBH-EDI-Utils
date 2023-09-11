using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    public class PurchaseOrder850 : EdiDocument
    {
        public string PoNo { get; set; }
        public string PoDt { get; set; }
        public string PromotionDealNo { get; set; }
        public string DepartmentNo { get; set; }
        public string VendorNo { get; set; }
        public string OrderType { get; set; }
        public int NetDay { get; set; }
        public string DeliveryRefNo { get; set; }
        public string ShipNotBefore { get; set; }
        public string ShipNoLater { get; set; }
        public string MustArriveBy { get; set; }
        public string CarrierDetail { get; set; }
        public string Location { get; set; }
        public string ShipPayment { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public string BtGln { get; set; }
        public string BtNm { get; set; }
        public string BtAddr { get; set; }
        public string BtCity { get; set; }
        public string BtState { get; set; }
        public string BtZip { get; set; }
        public string BtCountry { get; set; }
        public string StGln { get; set; }
        public string StNm { get; set; }
        public string StAddr { get; set; }
        public string StCity { get; set; }
        public string StState { get; set; }
        public string StZip { get; set; }
        public string StCountry { get; set; }

        public string CompanyName { get; set; } //발주사 명
        public int WeekOfYear { get; set; } //주차수
        public string ExcelFileName { get; set; } //소스가 되는 엑셀파일명 for DC추출

        //public int SortNo { get; set; }

        public List<PurchaseOrder850Detail> Details;
        public List<PurchaseOrder850Allowance> Allowences;

        public PurchaseOrder850()
        {
            Details = new List<PurchaseOrder850Detail>();
            Allowences = new List<PurchaseOrder850Allowance>();
        }

        public object GetSCellValue()
        {
            if (Allowences.Count == 0) { return ""; }
            if (Allowences.Count >= 2)
            {
                return string.Format("{0:000.00}", Allowences[0].Amount / 100F);
            }
            return "";

        }
        public object GetTCellValue()
        {
            if (Allowences.Count == 0) { return ""; }
            if (Allowences.Count >= 2)
            {
                return string.Format("{0:000.00}", Allowences[1].Amount / 100F);
            }
            return "";
        }

        override public string ToString()
        {
            string s = $"PoNo : {PoNo}\r\n" +
                $"PoDt : {PoDt}\r\n" +
                $"PromotionDealNo : {PromotionDealNo}\r\n" +
                $"DepartmentNo : {DepartmentNo}\r\n" +
                $"VendorNo : {VendorNo}\r\n" +
                $"OrderType : {OrderType}\r\n" +
                $"NetDay : {NetDay}\r\n" +
                $"DeliveryRefNo : {DeliveryRefNo}\r\n" +
                $"ShipNotBefore : {ShipNotBefore}\r\n" +
                $"ShipNoLater : {ShipNoLater}\r\n" +
                $"MustArriveBy : {MustArriveBy}\r\n" +
                $"CarrierDetail : {CarrierDetail}\r\n" +
                $"Location : {Location}\r\n" +
                $"ShipPayment : {ShipPayment}\r\n" +
                $"Description : {Description}\r\n" +
                $"Note : {Note}\r\n" +
                $"BtGln : {BtGln}\r\n" +
                $"BtNm : {BtNm}\r\n" +
                $"BtAddr : {BtAddr}\r\n" +
                $"BtCity : {BtCity}\r\n" +
                $"BtState : {BtState}\r\n" +
                $"BtZip : {BtZip}\r\n" +
                $"BtCountry : {BtCountry}\r\n" +
                $"StGln : {StGln}\r\n" +
                $"StNm : {StNm}\r\n" +
                $"StAddr : {StAddr}\r\n" +
                $"StCity : {StCity}\r\n" +
                $"StState : {StState}\r\n" +
                $"StZip : {StZip}\r\n" +
                $"StCountry : {StCountry}\r\n" +
                $"CompanyName : {CompanyName}\r\n" +
                $"WeekOfYear : {WeekOfYear}\r\n" 
                ;
            s += "--------------Details----------\r\n";
            foreach (var detail in Details)
            {
                s += detail.ToString() + "\r\n";
            }
            s += "--------------Allowences----------\r\n";
            foreach (var allowence in Allowences)
            {
                s += allowence.ToString() + "\r\n";
            }
            return s;
        }

        public object GetSaleMoney(decimal unitPrice, int qty)
        {
            var s = GetSCellValue();
            var t = GetTCellValue();
            if(string.IsNullOrEmpty(s.ToString()))
            {
                s = 0;
            }
            if(string.IsNullOrEmpty(t.ToString()))
            {
                t = 0;
            }
            decimal price = 0;

            if (BizRule.IsNoQty(CompanyName))
            {
                price = unitPrice * qty;

            }
            else
            {
                price = unitPrice * ( qty / 6);
            }


            return price - ( Convert.ToDecimal(s) + Convert.ToDecimal(t));
        }
    }
}

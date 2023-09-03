using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    public class ShippingOrder940
    {
        public string OrderId { get; set; }
        public string OrderNo { get; set; }
        public string BuyerPoNumber { get; set; }
        public string WarehouseInfo { get; set; }
        public string ShipTo { get; set; }
        public string ReferenceIdentification { get; set; }
        public string RequestedPickupDate { get; set; }
        public string RequestedDeliveryDate { get; set; }
        public string CancelAfterDate { get; set; }
        public string PurchaseOrderDate { get; set; }
        public string WarehouseCarrierInfo { get; set; }
        public string OrderGroupId { get; set; }

        public List<ShippingOrder940Detail> Details = null;
        public ShippingOrder940()
        {
            Details = new List<ShippingOrder940Detail>();
        }

        override public string ToString()
        {
            string s = $"OrderId : {OrderId}\r\n"
                + $"OrderNo : {OrderNo}\r\n"
                + $"BuyerPoNumber : {BuyerPoNumber}\r\n"
                + $"WarehouseInfo : {WarehouseInfo}\r\n"
                + $"ShipTo : {ShipTo}\r\n"
                + $"ReferenceIdentification : {ReferenceIdentification}\r\n"
                + $"RequestedPickupDate : {RequestedPickupDate}\r\n"
                + $"RequestedDeliveryDate : {RequestedDeliveryDate}\r\n"
                + $"CancelAfterDate : {CancelAfterDate}\r\n"
                + $"PurchaseOrderDate : {PurchaseOrderDate}\r\n"
                + $"WarehouseCarrierInfo :{WarehouseCarrierInfo}\r\n"
                + $"OrderGroupId : {OrderGroupId}\r\n";
            s += "--------------Details----------\r\n";
            foreach (var detail in Details)
            {
                s += detail.ToString() + "\r\n";
            }
            return s;
        }
    }

}

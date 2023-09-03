using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    public class Transfer944
    {
        public string HubGroupsOrderNumber { get; set; }
        public string ReceiptDate { get; set; }
        public string CustomerOrderID { get; set; }
        public string CustomersBolNumber { get; set; }
        public string HubGroupsWarehouseName { get; set; }
        public string HubGroupsCustomersWarehouseId { get; set; }
        public string DestinationAddressInformation { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationState { get; set; }
        public string DestinationZipcode { get; set; }
        public string OriginCompanyName { get; set; }
        public string ShipperCompanyId { get; set; }
        public string OriginAddressInformation { get; set; }
        public string OriginCity { get; set; }
        public string OriginState { get; set; }
        public string OriginZipcode { get; set; }
        public string ScheduledDeliveryDate { get; set; }
        public string TransportationMethodTypeCode { get; set; }
        public string StandardCarrierAlphaCode { get; set; }
        public string QuantityReceived { get; set; }
        public string NumberOfUnitsShipped { get; set; }
        public string QuantityDamagedOnHold { get; set; }

        public List<Transfer944Detail> Details = null;

        public Transfer944()
        {
            Details = new List<Transfer944Detail>();
        }
        override public string ToString()
        {
            string s = $"HubGroupsOrderNumber : {HubGroupsOrderNumber}\r\n"
                        + $"ReceiptDate : {ReceiptDate}\r\n"
                        + $"CustomerOrderID : {CustomerOrderID}\r\n"
                        + $"CustomersBolNumber : {CustomersBolNumber}\r\n"
                        + $"HubGroupsWarehouseName : {HubGroupsWarehouseName}\r\n"
                        + $"HubGroupsCustomersWarehouseId : {HubGroupsCustomersWarehouseId}\r\n"
                        + $"DestinationAddressInformation : {DestinationAddressInformation}\r\n"
                        + $"DestinationCity : {DestinationCity}\r\n"
                        + $"DestinationState : {DestinationState}\r\n"
                        + $"DestinationZipcode : {DestinationZipcode}\r\n"
                        + $"OriginCompanyName : {OriginCompanyName}\r\n"
                        + $"ShipperCompanyId : {ShipperCompanyId}\r\n"
                        + $"OriginAddressInformation : {OriginAddressInformation}\r\n"
                        + $"OriginCity : {OriginCity}\r\n"
                        + $"OriginState : {OriginState}\r\n"
                        + $"OriginZipcode : {OriginZipcode}\r\n"
                        + $"ScheduledDeliveryDate : {ScheduledDeliveryDate}\r\n"
                        + $"TransportationMethodTypeCode : {TransportationMethodTypeCode}\r\n"
                        + $"StandardCarrierAlphaCode : {StandardCarrierAlphaCode}\r\n"
                        + $"QuantityReceived : {QuantityReceived}\r\n"
                        + $"NumberOfUnitsShipped : {NumberOfUnitsShipped}\r\n"
                        + $"QuantityDamagedOnHold : {QuantityDamagedOnHold}\r\n";
            s += "--------------Details----------\r\n";
            foreach (var detail in Details)
            {
                s += detail.ToString() + "\r\n";
            }
            return s;
        }
    }
}

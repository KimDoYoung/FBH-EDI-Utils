using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    public class ShippingAdvice945 : EdiDocument
    {
        public string CustomerOrderId { get; set; }
        public string ActualPickupDate { get; set; }
        public string VicsBOL { get; set; }
        public string HubGroupsOrderNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string MaterVicsBol { get; set; }
        public string LinkSequenceNumber { get; set; }

        public string SfCompanyName { get; set; }
        public string SfSellerBuyer { get; set; }
        public string SfLocationIdCode { get; set; }
        public string SfAddressInfo { get; set; }
        public string SfCity { get; set; }
        public string SfState { get; set; }
        public string SfZipcode { get; set; }
        public string SfCountryCode { get; set; }

        public string StCompanyName { get; set; }
        public string StSellerBuyer { get; set; }
        public string StLocationIdCode { get; set; }
        public string StAddressInfo { get; set; }
        public string StCity { get; set; }
        public string StState { get; set; }
        public string StZipcode { get; set; }
        public string StCountryCode { get; set; }

        public string MfCompanyName { get; set; }
        public string MfSellerBuyer { get; set; }
        public string MfLocationIdCode { get; set; }
        public string MfAddressInfo { get; set; }
        public string MfCity { get; set; }
        public string MfState { get; set; }
        public string MfZipcode { get; set; }
        public string MfCountryCode { get; set; }

        public string BtCompanyName { get; set; }
        public string BtSellerBuyer { get; set; }
        public string BtLocationIdCode { get; set; }
        public string BtAddressInfo { get; set; }
        public string BtCity { get; set; }
        public string BtState { get; set; }
        public string BtZipcode { get; set; }
        public string BtCountryCode { get; set; }

        public string ProNumber { get; set; }
        public string MasterBolNumber { get; set; }
        public string ServiceLevel { get; set; }
        public string DeliveryAppointmentNumber { get; set; }
        public string PurchaseOrderDate { get; set; }

        public string TransportationMode { get; set; }
        public string CarriersScacCode { get; set; }
        public string CarriersName { get; set; }
        public string PaymentMethod { get; set; }

        public string AllowanceOrChargeTotalAmount { get; set; }

        public string TotalUnitsShipped { get; set; }
        public string TotalWeightShipped { get; set; }
        public string LadingQuantity { get; set; }
        public string UnitOrBasisForMeasurementCode { get; set; }

        public List<ShippingAdvice945Detail> Details { get; set; }
        public ShippingAdvice945()
        {
            Details = new List<ShippingAdvice945Detail>();
        }

        override public string ToString()
        {
            string s = $"CustomerOrderId : {CustomerOrderId}\r\n" +
                $"ActualPickupDate : {ActualPickupDate}\r\n" +
                $"VicsBOL : {VicsBOL}\r\n" +
                $"HubGroupsOrderNumber : {HubGroupsOrderNumber}\r\n" +
                $"PurchaseOrderNumber : {PurchaseOrderNumber}\r\n" +
                $"MaterVicsBol : {MaterVicsBol}\r\n" +
                $"LinkSequenceNumber : {LinkSequenceNumber}\r\n" +
                $"SfCompanyName : {SfCompanyName}\r\n" +
                $"SfSellerBuyer : {SfSellerBuyer}\r\n" +
                $"SfLocationIdCode : {SfLocationIdCode}\r\n" +
                $"SfAddressInfo : {SfAddressInfo}\r\n" +
                $"SfCity : {SfCity}\r\n" +
                $"SfState : {SfState}\r\n" +
                $"SfZipcode : {SfZipcode}\r\n" +
                $"SfCountryCode : {SfCountryCode}\r\n" +
                $"StCompanyName : {StCompanyName}\r\n" +
                $"StSellerBuyer : {StSellerBuyer}\r\n" +
                $"StLocationIdCode : {StLocationIdCode}\r\n" +
                $"StfAddressInfo : {StAddressInfo}\r\n" +
                $"StCity : {StCity}\r\n" +
                $"StState : {StState}\r\n" +
                $"StZipcode : {StZipcode}\r\n" +
                $"StCountryCode : {StCountryCode}\r\n" +

                $"MfCompanyName : {MfCompanyName}\r\n" +
                $"MfSellerBuyer : {MfSellerBuyer}\r\n" +
                $"MfLocationIdCode : {MfLocationIdCode}\r\n" +
                $"MfAddressInfo : {MfAddressInfo}\r\n" +
                $"MfCity : {MfCity}\r\n" +
                $"MfState : {MfState}\r\n" +
                $"MfZipcode : {MfZipcode}\r\n" +
                $"MfCountryCode : {MfCountryCode}\r\n" +

                $"BtCompanyName : {BtCompanyName}\r\n" +
                $"BtSellerBuyer : {BtSellerBuyer}\r\n" +
                $"BtLocationIdCode : {BtLocationIdCode}\r\n" +
                $"BtAddressInfo : {BtAddressInfo}\r\n" +
                $"BtCity : {BtCity}\r\n" +
                $"BtState : {BtState}\r\n" +
                $"BtZipcode : {BtZipcode}\r\n" +
                $"BtCountryCode : {BtCountryCode}\r\n" +

                $"ProNumber : {ProNumber}\r\n" +
                $"MasterBolNumber : {MasterBolNumber}\r\n" +
                $"ServiceLevel : {ServiceLevel}\r\n" +
                $"DeliveryAppointmentNumber : {DeliveryAppointmentNumber}\r\n" +
                $"PurchaseOrderDate : {PurchaseOrderDate}\r\n" +

                $"TransportationMode : {TransportationMode}\r\n" +
                $"CarriersScacCode : {CarriersScacCode}\r\n" +
                $"CarriersName : {CarriersName}\r\n" +
                $"PaymentMethod : {PaymentMethod}\r\n" +

                $"AllowanceOrChargeTotalAmount : {AllowanceOrChargeTotalAmount}\r\n" +

                $"TotalUnitsShipped : {TotalUnitsShipped}\r\n" +
                $"TotalWeightShipped : {TotalWeightShipped}\r\n" +
                $"LadingQuantity : {LadingQuantity}\r\n" +
                $"UnitOrBasisForMeasurementCode : {UnitOrBasisForMeasurementCode}\r\n" 
                ;
                s += "--------------Details----------\r\n";
                foreach (var detail in Details)
                {
                    s += detail.ToString() + "\r\n";
                }
            return s;
        }
    }
}

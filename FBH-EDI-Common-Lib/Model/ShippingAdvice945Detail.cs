using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    public class ShippingAdvice945Detail
    {
        public int? AssignedNumber { get; set; }
        public string PalletId { get; set; }
        public string CarrierTrackingNumber { get; set; }
        public string ShipmentStatus { get; set; }
        public string RequestedQuantity { get; set; }
        public string ActualQuantityShipped { get; set; }
        public string DifferenceBetweenActualAndRequested { get; set; }
        public string UnitOrBasisMeasurementCode { get; set; }
        public string UpcCode { get; set; }
        public string SkuNo { get; set; }
        public string LotBatchCode { get; set; }
        public string TotalWeightForItemLine { get; set; }
        public string RetailersItemNumber { get; set; }
        public string LineNumber { get; set; }
        public string ExpirationDate { get; set; }

        override public string ToString()
        {
            string s = $"AssignedNumber : {AssignedNumber}\r\n" +
                $"PalletId : {PalletId}\r\n" +
                $"CarrierTrackingNumber : {CarrierTrackingNumber}\r\n" +
                $"ShipmentStatus : {ShipmentStatus}\r\n" +
                $"RequestedQuantity : {RequestedQuantity}\r\n" +
                $"ActualQuantityShipped : {ActualQuantityShipped}\r\n" +
                $"DifferenceBetweenActualAndRequested : {DifferenceBetweenActualAndRequested}\r\n" +
                $"UnitOrBasisMeasurementCode : {UnitOrBasisMeasurementCode}\r\n" +
                $"UpcCode : {UpcCode}\r\n" +
                $"SkuNo : {SkuNo}\r\n" +
                $"LotBatchCode : {LotBatchCode}\r\n" +
                $"TotalWeightForItemLine : {TotalWeightForItemLine}\r\n" +
                $"RetailersItemNumber : {RetailersItemNumber}\r\n" +
                $"LineNumber : {LineNumber}\r\n" +
                $"ExpirationDate : {ExpirationDate}";
            return s;
        }
    }
}

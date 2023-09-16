using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    public class Transfer944Detail
    {
        public string ReceiptDate { get; set; }
        public string HubGroupsOrderNumber { get; set; }
        public int? AssignedNumber { get; set; }
        public int? StockReceiptQuantityReceived { get; set; }
        public string StockReceiptUnitOfMeasureCode { get; set; }
        public string StockReceiptSku { get; set; }
        public string StockReceiptLotBatchCode { get; set; }
        public int? ExceptionQuantity { get; set; }
        public string ExceptionUnitOfMeasureCode { get; set; }
        public string ExceptionReceivingConditionCode { get; set; }
        public string ExceptionLotBatchCode { get; set; }
        public string ExceptionDamageCondition { get; set; }
        public override string ToString()
        {
            string s = $"ReceiptDate : {ReceiptDate}, "
                + $"ReceiptDate : {ReceiptDate}, "
                + $"HubGroupsOrderNumber : {HubGroupsOrderNumber}, "
                + $"AssignedNumber : {AssignedNumber}, "
                + $"StockReceiptQuantityReceived : {StockReceiptQuantityReceived}, "
                + $"StockReceiptUnitOfMeasureCode : {StockReceiptUnitOfMeasureCode}, "
                + $"StockReceiptSku : {StockReceiptSku}, "
                + $"StockReceiptLotBatchCode : {StockReceiptLotBatchCode}, "
                + $"ExceptionQuantity : {ExceptionQuantity}, "
                + $"ExceptionUnitOfMeasureCode : {ExceptionUnitOfMeasureCode}, "
                + $"ExceptionReceivingConditionCode : {ExceptionReceivingConditionCode}, "
                + $"ExceptionLotBatchCode : {ExceptionLotBatchCode}, "
                + $"ExceptionDamageCondition : {ExceptionDamageCondition}";
            return s;
        }
    }
}
